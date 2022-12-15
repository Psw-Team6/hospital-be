using System;
using System.Collections.Generic;
using System.Linq;
using HospitalAPI;
using HospitalLibrary.ApplicationUsers.Model;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.Doctors.Model;
using HospitalLibrary.Examinations.Model;
using HospitalLibrary.Managers;
using HospitalLibrary.Medicines.Model;
using HospitalLibrary.Patients.Model;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Settings;
using HospitalLibrary.SharedModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HospitalTest.Setup
{
    public class TestDatabaseFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(collection =>
            {
                using var scope = BuildServiceProvider(collection).CreateScope();
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<HospitalDbContext>();
                InitializeDataBase(db);
            });
        }
        private static ServiceProvider BuildServiceProvider(IServiceCollection serviceCollection)
        {
            var desc = serviceCollection
                .SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<HospitalDbContext>));
            serviceCollection.Remove(desc);
            serviceCollection.AddDbContext<HospitalDbContext>(opt =>
            {
                opt.UseNpgsql(CreateConnectionStringForTest());
            });
            return serviceCollection.BuildServiceProvider();
        }

        private static string CreateConnectionStringForTest()
        {
            return "Host=localhost;port=5432;Database=HospitalTestDB;Username=hospital;Password=hospital123;";
        }
        private static void InitializeDataBase(HospitalDbContext context)
        {
            context.Database.EnsureCreated();
            //context.Database.ExecuteSqlRaw("TRUNCATE TABLE \"Addresses\";");
            Specialization specializationGeneral = new()
            {
                Id = Guid.NewGuid(),
                Name = "GeneralGG"
            };
            context.Specializations.Add(specializationGeneral);
            Address address = new()
            {
                Id = Guid.NewGuid(),
                City = "Novi Sad",
                StreetNumber = "23A",
                Country = "Serbia",
                Street = "Kosovska",
                Postcode = 21000
            };
            context.Addresses.Add(address);
            WorkingSchedule workingSchedule = new()
            {
                Id = Guid.NewGuid(),
                DayOfWork = new DateRange
                {
                    From = new DateTime(2022, 10, 27, 8, 0, 0),
                    To = new DateTime(2023, 12, 27, 14, 0, 0)
                },
                ExpirationDate = new NullableDateRange
                {
                    From = new DateTime(2022, 10, 27),
                    To = new DateTime(2023, 12, 27)
                }
            };
            context.WorkingSchedules.Add(workingSchedule);
            Building building1 = new()
            {
                Id = Guid.NewGuid(),
                Name = "Stara bolnica"
            };
            context.Buildings.Add(building1);
            Floor floor11 = new()
            {
                Id= Guid.NewGuid(),
                Name = "F0",
                FloorNumber = 0,
                BuildingId = building1.Id
            };
            context.Floors.Add(floor11);
            Room room = new()
            {
                Id = Guid.NewGuid(),
                FloorId = floor11.Id,
                Name = "B63",
                BuildingId = floor11.BuildingId
            };
            context.Rooms.Add(room);
            Doctor doctor = new()
            {
                Id = Guid.NewGuid(),
                SpecializationId = specializationGeneral.Id,
                AddressId = address.Id,
                WorkingScheduleId = workingSchedule.Id,
                RoomId = room.Id,
                Username = "Doctor",
                Password = "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy",
                Name = "Milan",
                Surname = "Milic",
                Email = "mm@gmail.com",
                Jmbg = "99999999",
                Phone = "+612222222",
                UserRole = UserRole.Doctor,
                Enabled = true
            };
            context.Doctors.Add(doctor);
            //context.Database.ExecuteSqlRaw("TRUNCATE TABLE \"Patients\";");
            var patient = new Patient
            {
                Id = Guid.NewGuid(),
                AddressId = address.Id,
                Username = "Patient",
                Password = "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy",
                Name = "Marko",
                Surname = "Lave",
                Email = "psw.isa.mail@gmail.com",
                Jmbg = "99999999",
                Phone = "+612222222",
                UserRole = UserRole.Patient,
                Enabled = true,
                DoctorId = doctor.Id
            };
            context.Patients.Add(patient);
            Manager manager = new ()
            {
                Id = Guid.NewGuid(),
                AddressId = address.Id,
                Username = "Manager",
                Password = "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy",
                Name = "Manager",
                Surname = "Manger",
                Email = "psw.isa.mail@gmail.com",
                Jmbg = "99999999",
                Phone = "+612222222",
                UserRole = UserRole.Manager,
                Enabled = true
            };
            context.Managers.Add(manager);
            
            
            RoomEquipment roomEquipment = new()
            {
                RoomId = room.Id,   
                RoomEquipmentId =Guid.NewGuid(),
                Amount = 111,
                EquipmentName = "BANDAGE"
            };
            context.RoomEquipment.Add(roomEquipment);
            Symptom symptom = new()
            {
                Id = new Guid(),
                Description = "Fever"
            };
            Symptom symptom1 = new()
            {
                Id = new Guid(),
                Description = "Nausea"
            };
            var medicine = new Medicine
            {
                Name = "Brufen",
                Amount = 0,
            };
            var medicine1 = new Medicine
            {
                Name = "Panadol",
                Amount = 0,
            };
            context.Symptoms.Add(symptom);
            context.Symptoms.Add(symptom1);
            context.Medicines.Add(medicine);
            context.Medicines.Add(medicine1);
            Appointment appointment1 = new()
            {
                Id = new Guid("852fa040-a1f5-46f1-963a-2addf5a86a37"),
                Emergent = false,
                PatientId = patient.Id,
                DoctorId = doctor.Id,
                AppointmentType = AppointmentType.Examination,
                AppointmentState = AppointmentState.Pending,
                Duration = new DateRange
                {
                    From = new DateTime(2023, 7, 27, 10, 0, 0),
                    To = new DateTime(2023, 7, 27, 10, 30, 0)
                }
            };
            Appointment appointment2 = new()
            {
                Id = new Guid("852fa040-a1f5-46f1-963a-2addf5a86a06"),
                Emergent = false,
                PatientId = patient.Id,
                DoctorId = doctor.Id,
                AppointmentType = AppointmentType.Examination,
                AppointmentState = AppointmentState.Pending,
                Duration = new DateRange
                {
                    From = DateTime.Now.AddMinutes(-60),
                    To = DateTime.Now.AddMinutes(-30)
                }
            };
            Appointment appointment3 = new()
            {
                Id = new Guid("852fa040-a1f5-46f1-963a-2addf5a86a90"),
                Emergent = false,
                PatientId = patient.Id,
                DoctorId = doctor.Id,
                AppointmentType = AppointmentType.Examination,
                AppointmentState = AppointmentState.Finished,
                Duration = new DateRange
                {
                    From = DateTime.Now.AddMinutes(-120),
                    To = DateTime.Now.AddMinutes(-90)
                }
            };
            context.Appointments.Add(appointment1);
            context.Appointments.Add(appointment2);
            context.Appointments.Add(appointment3);
            
            context.Database.ExecuteSqlRaw("DELETE FROM public.\"ExaminationSymptom\";");
            context.Database.ExecuteSqlRaw("DELETE FROM public.\"ExaminationPrescription\";");
            context.Database.ExecuteSqlRaw("DELETE FROM public.\"Examinations\";");
            context.Database.ExecuteSqlRaw("DELETE FROM  public.\"Patients\";");
            context.Database.ExecuteSqlRaw("DELETE FROM  public.\"Doctors\";");
            context.Database.ExecuteSqlRaw("DELETE FROM  public.\"WorkingSchedules\";");
            context.Database.ExecuteSqlRaw("DELETE FROM public.\"Rooms\";");
            context.Database.ExecuteSqlRaw("DELETE FROM public.\"Floors\";");
            context.Database.ExecuteSqlRaw("DELETE FROM public.\"Buildings\";");
            context.Database.ExecuteSqlRaw("DELETE FROM public.\"Specializations\";");
            context.Database.ExecuteSqlRaw("DELETE FROM public.\"Addresses\";");
            context.Database.ExecuteSqlRaw("DELETE FROM public.\"Managers\";");
            context.Database.ExecuteSqlRaw("DELETE FROM public.\"RoomEquipment\";");
            context.Database.ExecuteSqlRaw("DELETE FROM public.\"Symptoms\";");
            context.Database.ExecuteSqlRaw("DELETE FROM public.\"Medicines\";");
            //context.Database.ExecuteSqlRaw("DELETE FROM public.\"Appointments\";");
            //context.Database.ExecuteSqlRaw("DELETE FROM public.\"Appointments\" WHERE Id=\"852fa040-a1f5-46f1-963a-2addf5a86a90\";");
            // context.Database.ExecuteSqlRaw("drop table public.\"ExaminationSymptom\" cascade");
            // context.Database.ExecuteSqlRaw("drop table public.\"ExaminationPrescription\" cascade");
            // context.Database.ExecuteSqlRaw("drop table public.\"Examinations\" cascade");
            // context.Database.ExecuteSqlRaw("drop table public.\"Appointments\" cascade");
            context.SaveChanges();
        }
    }
}