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
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Floor> Floors { get; set; }
        public DbSet<FloorPlanView> FloorPlanViews { get; set; }
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
            
            
            //Building 1, Floor 1
            Room room1 = new()
            {
                Id = Guid.NewGuid(),
                BuildingName = "Stara zgrada",
                FloorName = "Prvi",
                Number = "Cekaonica",
                PositionX = 0,
                PositionY  = 0,
                Lenght = 15,
                Width = 10
            };
            Room room2 = new()
            {
                Id = Guid.NewGuid(),
                BuildingName = "Stara zgrada",
                FloorName = "Prvi",
                Number = "12A",
                PositionX = 10,
                PositionY  = 0,
                Lenght = 5,
                Width = 10
            };
            Room room3 = new()
            {
                Id = Guid.NewGuid(),
                BuildingName = "Stara zgrada",
                FloorName = "Prvi",
                Number = "13A",
                PositionX = 10,
                PositionY  = 5,
                Lenght = 5,
                Width = 10
            };
            Room room4 = new()
            {
                Id = Guid.NewGuid(),
                BuildingName = "Stara zgrada",
                FloorName = "Prvi",
                Number = "14A",
                PositionX = 10,
                PositionY  = 10,
                Lenght = 5,
                Width = 10
            };
            //Building 1, Floor 2
            Room room5 = new()
            {
                Id = Guid.NewGuid(),
                BuildingName = "Stara zgrada",
                FloorName = "Drugi",
                Number = "Cekaonica",
                PositionX = 0,
                PositionY  = 0,
                Lenght = 15,
                Width = 10
            };
            Room room6 = new()
            {
                Id = Guid.NewGuid(),
                BuildingName = "Stara zgrada",
                FloorName = "Drugi",
                Number = "12A",
                PositionX = 10,
                PositionY  = 0,
                Lenght = 5,
                Width = 10
            };
            Room room7 = new()
            {
                Id = Guid.NewGuid(),
                BuildingName = "Stara zgrada",
                FloorName = "Drugi",
                Number = "13A",
                PositionX = 10,
                PositionY  = 5,
                Lenght = 5,
                Width = 10
            };
            Room room8 = new()
            {
                Id = Guid.NewGuid(),
                BuildingName = "Stara zgrada",
                FloorName = "Drugi",
                Number = "14A",
                PositionX = 10,
                PositionY  = 10,
                Lenght = 5,
                Width = 10
            };
            //Building 1, Floor 3
            Room room9 = new()
            {
                Id = Guid.NewGuid(),
                BuildingName = "Stara zgrada",
                FloorName = "Treci",
                Number = "Cekaonica",
                PositionX = 0,
                PositionY  = 0,
                Lenght = 15,
                Width = 10
            };
            Room room10 = new()
            {
                Id = Guid.NewGuid(),
                BuildingName = "Stara zgrada",
                FloorName = "Treci",
                Number = "12A",
                PositionX = 10,
                PositionY  = 0,
                Lenght = 5,
                Width = 10
            };
            Room room11 = new()
            {
                Id = Guid.NewGuid(),
                BuildingName = "Stara zgrada",
                FloorName = "Treci",
                Number = "13A",
                PositionX = 10,
                PositionY  = 5,
                Lenght = 5,
                Width = 10
            };
            Room room12 = new()
            {
                Id = Guid.NewGuid(),
                BuildingName = "Stara zgrada",
                FloorName = "Treci",
                Number = "14A",
                PositionX = 10,
                PositionY  = 10,
                Lenght = 5,
                Width = 10
            };
            //Building 2, Floor 1
            Room room13 = new()
            {
                Id = Guid.NewGuid(),
                BuildingName = "Nova zgrada",
                FloorName = "Prvi",
                Number = "Cekaonica",
                PositionX = 0,
                PositionY  = 0,
                Lenght = 15,
                Width = 10
            };
            Room room14 = new()
            {
                Id = Guid.NewGuid(),
                BuildingName = "Nova zgrada",
                FloorName = "Prvi",
                Number = "12A",
                PositionX = 10,
                PositionY  = 0,
                Lenght = 5,
                Width = 10
            };
            Room room15 = new()
            {
                Id = Guid.NewGuid(),
                BuildingName = "Nova zgrada",
                FloorName = "Prvi",
                Number = "13A",
                PositionX = 10,
                PositionY  = 5,
                Lenght = 5,
                Width = 10
            };
            Room room16 = new()
            {
                Id = Guid.NewGuid(),
                BuildingName = "Nova zgrada",
                FloorName = "Prvi",
                Number = "14A",
                PositionX = 10,
                PositionY  = 10,
                Lenght = 5,
                Width = 10
            };
            //Building 2, Floor 2
            Room room17 = new()
            {
                Id = Guid.NewGuid(),
                BuildingName = "Nova zgrada",
                FloorName = "Drugi",
                Number = "Cekaonica",
                PositionX = 0,
                PositionY  = 0,
                Lenght = 15,
                Width = 10
            };
            Room room18 = new()
            {
                Id = Guid.NewGuid(),
                BuildingName = "Nova zgrada",
                FloorName = "Drugi",
                Number = "12A",
                PositionX = 10,
                PositionY  = 0,
                Lenght = 5,
                Width = 10
            };
            Room room19 = new()
            {
                Id = Guid.NewGuid(),
                BuildingName = "Nova zgrada",
                FloorName = "Drugi",
                Number = "13A",
                PositionX = 10,
                PositionY  = 5,
                Lenght = 5,
                Width = 10
            };
            Room room20 = new()
            {
                Id = Guid.NewGuid(),
                BuildingName = "Nova zgrada",
                FloorName = "Drugi",
                Number = "14A",
                PositionX = 10,
                PositionY  = 10,
                Lenght = 5,
                Width = 10
            };
            //Building 2, Floor 3
            Room room21 = new()
            {
                Id = Guid.NewGuid(),
                BuildingName = "Nova zgrada",
                FloorName = "Treci",
                Number = "Sok soba",
                PositionX = 0,
                PositionY  = 0,
                Lenght = 15,
                Width = 20
            };
            modelBuilder.Entity<Room>().HasData(
                room1,room2,room3,room4,room5,room6,room7,room8,room9,room10,room11,room12,
                room13,room14,room15,room16,room17,room18,room19,room20,room21
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
