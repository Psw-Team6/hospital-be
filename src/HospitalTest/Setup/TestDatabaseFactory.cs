using System;
using System.Linq;
using HospitalAPI;
using HospitalLibrary.ApplicationUsers.Model;
using HospitalLibrary.Doctors.Model;
using HospitalLibrary.Managers;
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
                Phone = "+612222222",
                UserRole = UserRole.Doctor,
                Enabled = true
            };
            context.Doctors.Add(doctor);
            //context.Database.ExecuteSqlRaw("TRUNCATE TABLE \"Patients\";");
            context.Patients.Add(new Patient
            {
                Id = Guid.NewGuid(),
                AddressId = address.Id,
                Username = "Patient",
                Password = "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy",
                Name = "Marko",
                Surname = "Lave",
                Email = "psw.isa.mail@gmail.com",
                Phone = "+612222222",
                UserRole = UserRole.Patient,
                Enabled = true,
                DoctorId = doctor.Id
            });
            Manager manager = new ()
            {
                Id = Guid.NewGuid(),
                AddressId = address.Id,
                Username = "Manager",
                Password = "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy",
                Name = "Manager",
                Surname = "Manger",
                Email = "psw.isa.mail@gmail.com",
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

            RoomBed bed1 = new()
            {
                RoomId = room.Id,
                IsFree = true,
                Number = "12A1"
            };
            context.RoomBeds.Add(bed1);
            
            RoomBed bed2 = new()
            {
                RoomId = room.Id,
                IsFree = false,
                Number = "12A12"
            };
            context.RoomBeds.Add(bed2);

            Patient pat1 = new()
            {
                Id = Guid.NewGuid(),
                AddressId = address.Id,
                Username = "Patientss",
                Password = "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy",
                Name = "Marko",
                Surname = "Lave",
                Email = "psw.isa.mail@gmail.com",
                Phone = "+612222222",
                UserRole = UserRole.Patient,
                Enabled = true,
                DoctorId = doctor.Id
            };

            PatientAdmission pa1 = new()
            {
                PatientId = pat1.Id,
                Reason = "bolestan",
                DateOfAdmission = DateTime.Now,
                SelectedBedId = bed2.Id,
                SelectedRoomId = bed2.RoomId,
                DateOfDischarge = null
            };
            context.PatientAdmissions.Add(pa1);
            
            
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
            context.SaveChanges();
        }
    }
}