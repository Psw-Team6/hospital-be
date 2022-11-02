using System;
using System.Reflection;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Doctors.Model;
using HospitalLibrary.Patients.Model;
using HospitalLibrary.sharedModel;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Settings
{
    public class HospitalDbContext : DbContext
    {
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<WorkingSchedule> WorkingSchedules { get; set; }

        public HospitalDbContext(DbContextOptions<HospitalDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            Specialization specializationSurgeon = new()
            {
                Id = Guid.NewGuid(),
                Name = "Surgeon"
            };
            Specialization specializationGeneral = new()
            {
                Id = Guid.NewGuid(),
                Name = "General"
            };
            Specialization specializationDermatology = new()
            {
                Id = Guid.NewGuid(),
                Name = "Dermatology"
            };
            modelBuilder.Entity<Specialization>().HasData(
                specializationSurgeon,
                specializationGeneral,
                specializationDermatology
            );
            WorkingSchedule workingSchedule1 = new()
            {
                Id = Guid.NewGuid()
            };
            WorkingSchedule workingSchedule2 = new()
            {
                Id = Guid.NewGuid()
            };
            modelBuilder.Entity<WorkingSchedule>()
                .OwnsOne(app => app.ExpirationDate)
                .HasData(
                    new
                    {
                        WorkingScheduleId = workingSchedule1.Id,
                        From = new DateTime(2022, 10, 27),
                        To = new DateTime(2023, 1, 27)
                    },
                    new
                    {
                        WorkingScheduleId = workingSchedule2.Id,
                        From = new DateTime(2022, 10, 27),
                        To = new DateTime(2023, 1, 27)
                    }
                );
            modelBuilder.Entity<WorkingSchedule>()
                .OwnsOne(app => app.DayOfWork)
                .HasData(
                    new
                    {
                        WorkingScheduleId = workingSchedule1.Id,
                        From = new DateTime(2022, 10, 27, 8, 0, 0),
                        To = new DateTime(2022, 10, 27, 14, 0, 0)
                    },
                    new
                    {
                        WorkingScheduleId = workingSchedule2.Id,
                        From = new DateTime(2022, 10, 27, 14, 0, 0),
                        To = new DateTime(2022, 10, 27, 22, 0, 0)
                    }
                );

            modelBuilder.Entity<WorkingSchedule>().HasData(
                workingSchedule1,workingSchedule2
            );
            Address address = new()
            {
                Id = Guid.NewGuid(),
                City = "Novi Sad",
                StreetNumber = "23A",
                Country = "Serbia",
                Street = "Kosovska",
                Postcode = 21000
            };
            Address address1 = new()
            {
                Id = Guid.NewGuid(),
                City = "Novi Sad",
                StreetNumber = "33",
                Country = "Serbia",
                Street = "Partizanska",
                Postcode = 21000
            };
            Address address2 = new()
            {
                Id = Guid.NewGuid(),
                City = "Novi Sad",
                StreetNumber = "33",
                Country = "Serbia",
                Street = "JNA",
                Postcode = 21000
            };
            modelBuilder.Entity<Address>().HasData(
                address,address1,address2
            );
            Room room1 = new()
            {
                Id = Guid.NewGuid(),
                Number = "11A",
                Floor = 1,
            };
            Room room2 = new()
            {
                Id = Guid.NewGuid(),
                Number = "12A",
                Floor = 2,
            };
            modelBuilder.Entity<Room>().HasData(
                room1,room2
            );
            Doctor doctor = new()
            {
                Id = Guid.NewGuid(),
                SpecializationId = specializationDermatology.Id,
                AddressId = address1.Id,
                WorkingScheduleId = workingSchedule1.Id,
                RoomId = room2.Id,
                Username = "Tadjo",
                Password = "miki123",
                Name = "Djordje",
                Surname = "Vuckovic",
                Email = "DjordjeLopov@gmail.com",
                Jmbg = "99999999",
                Phone = "+612222222"
            };
            Doctor doctor1 = new()
            {
                Id = Guid.NewGuid(),
                SpecializationId = specializationDermatology.Id,
                AddressId = address.Id,
                WorkingScheduleId = workingSchedule2.Id,
                RoomId = room1.Id,
                Username = "Ilija",
                Password = "miki123",
                Name = "Ilija",
                Surname = "Maric",
                Email = "Cajons@gmail.com",
                Jmbg = "99999999",
                Phone = "+612222222"
            };
            modelBuilder.Entity<Doctor>().HasData(
                doctor,doctor1
            );
            Patient patient1 = new()
            {
                Id = Guid.NewGuid(),
                AddressId = address.Id,
                Username = "Sale",
                Password = "sale1312",
                Name = "Sale",
                Surname = "Lave",
                Email = "psw.isa.mail@gmail.com",
                Jmbg = "99999999",
                Phone = "+612222222"
            };
            Patient patient2 = new()
            {
                Id = Guid.NewGuid(),
                AddressId = address2.Id,
                Username = "Miki",
                Password = "sale1312",
                Name = "Miki",
                Surname = "Djuricic",
                Email = "psw.isa.mail@gmail.com",
                Jmbg = "99999999",
                Phone = "+612222222"
            };
            
            modelBuilder.Entity<Patient>().HasData(
                patient1,patient2
            );

            Appointment appointment = new()
            {
                Id = Guid.NewGuid(),
                Emergent = false,
                PatientId = patient1.Id,
                DoctorId = doctor1.Id,
                AppointmentType = AppointmentType.Examination,
                AppointmentState = AppointmentState.Pending
            };
            modelBuilder.Entity<Appointment>()
                .OwnsOne(app => app.Duration)
                .HasData(
                    new
                    {
                        AppointmentId = appointment.Id,
                        From = new DateTime(2022, 10, 27, 15, 0, 0),
                        To = new DateTime(2022, 10, 27, 15, 15, 0)
                    }
                );
            modelBuilder.Entity<Appointment>().HasData(
                appointment
            );
        }
    }
}
