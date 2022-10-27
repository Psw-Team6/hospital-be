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
                Id = Guid.NewGuid(),
                StartUpDate = new DateTime(2022,10,27),
                ExpiresDate = new DateTime(2023,1,27),
                StartTime = new DateTime(2022, 10, 27, 8, 0, 0),
                Duration = new TimeSpan(0,8,0,0)
            };
            WorkingSchedule workingSchedule2 = new()
            {
                Id = Guid.NewGuid(),
                StartUpDate = new DateTime(2022,10,27),
                ExpiresDate = new DateTime(2023,1,27),
                StartTime = new DateTime(2022, 10, 27, 16, 0, 0),
                Duration = new TimeSpan(0,8,0,0)
            };
            modelBuilder.Entity<WorkingSchedule>().HasData(
                workingSchedule1
            );
            modelBuilder.Entity<WorkingSchedule>().HasData(
                workingSchedule2
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
            modelBuilder.Entity<Address>().HasData(
                address,address1
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
                AddressId = address.Id,
                WorkingScheduleId = workingSchedule1.Id,
                RoomId = room1.Id,
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
                doctor
            );
            modelBuilder.Entity<Doctor>().HasData(
                doctor1
            );
            
            Patient patient1 = new()
            {
                Id = Guid.NewGuid(),
                AddressId = address.Id,
                Username = "Sale",
                Password = "sale1312",
                Name = "Sale",
                Surname = "Lave",
                Email = "SaleLave1312@gmail.com",
                Jmbg = "99999999",
                Phone = "+612222222"
            };
            
            modelBuilder.Entity<Patient>().HasData(
                patient1
            );

            Appointment appointment = new()
            {
                Id = Guid.NewGuid(),
                Emergent = false,
                StartTime=new DateTime(2022,12,11,12,0,0),
                Duration=new TimeSpan(0,1,0,0,0),
                PatientId = patient1.Id,
                DoctorId = doctor1.Id,
                AppointmentType = AppointmentType.Examination,
                AppointmentState = AppointmentState.Pending
            };

            modelBuilder.Entity<Appointment>().HasData(
                appointment
            );
        }
    }
}
