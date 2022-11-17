using System;
using System.Reflection;
using System.Security.Principal;
using HospitalLibrary.ApplicationUsers.Model;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.BloodConsumptions.Model;
using HospitalLibrary.BloodUnits.Model;
using HospitalLibrary.Doctors.Model;
using HospitalLibrary.Holidays.Model;
using HospitalLibrary.Managers;
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
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<WorkingSchedule> WorkingSchedules { get; set; }
        public DbSet<GRoom> GRooms { get; set; }
        public DbSet<RoomBed> RoomBeds { get; set; }
        public DbSet<PatientAdmission> PatientAdmissions { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<BloodUnit> BloodUnits { get; set; }
        public DbSet<BloodConsumption> BloodConsumptions { get; set; }
        public DbSet<Holiday> Holidays { get; set; }

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
            
            Building building1 = new()
            {
                Id = Guid.NewGuid(),
                Name = "Stara bolnica"
            };
            
            Building building2 = new()
            {
                Id = Guid.NewGuid(),
                Name = "Nova bolnica"
            };
            
            modelBuilder.Entity<Building>().HasData(
                building1,building2
            );
            
            Floor floor11 = new()
            {
                Id= Guid.NewGuid(),
                Name = "F0",
                FloorNumber = 0,
                BuildingId = building1.Id
            };
            Floor floor12 = new()
            {
                Id= Guid.NewGuid(),
                Name = "F1",
                FloorNumber = 1,
                BuildingId = building1.Id
            };
            Floor floor13 = new()
            {
                Id= Guid.NewGuid(),
                Name = "F2",
                FloorNumber = 2,
                BuildingId = building1.Id
            };
            
            Floor floor21 = new()
            {
                Id= Guid.NewGuid(),
                Name = "F0",
                FloorNumber = 0,
                BuildingId = building2.Id
            };
            Floor floor22 = new()
            {
                Id= Guid.NewGuid(),
                Name = "F1",
                FloorNumber = 1,
                BuildingId = building2.Id
            };
            Floor floor23 = new()
            {
                Id= Guid.NewGuid(),
                Name = "F2",
                FloorNumber = 2,
                BuildingId = building2.Id
            };
            
            modelBuilder.Entity<Floor>().HasData(
                floor11,floor12,floor13,floor21,floor22,floor23
            );
            
            Room room1 = new()
            {
                Id = Guid.NewGuid(),
                FloorId = floor11.Id,
                Name = "A11",
                BuildingId = floor11.BuildingId
            };
            
            GRoom gRoom1 = new()
            {
                Id = Guid.NewGuid(),
                RoomId = room1.Id,
                PositionX = 0,
                PositionY = 0,
                Lenght = 5,
                Width = 5
            };
            room1.GRoomId = gRoom1.Id;
            Room room2 = new()
            {
                Id = Guid.NewGuid(),
                FloorId = floor11.Id,
                Name = "B11",
                BuildingId = floor11.BuildingId
            };
            GRoom gRoom2 = new()
            {
                Id = Guid.NewGuid(),
                RoomId = room2.Id,
                PositionX = 5,
                PositionY  = 0,
                Lenght = 5,
                Width = 5
            };
            room2.GRoomId = gRoom2.Id;
            Room room3 = new()
            {
                Id = Guid.NewGuid(),
                FloorId = floor12.Id,
                Name = "A12",
                BuildingId = floor12.BuildingId
            };
            GRoom gRoom3 = new()
            {
                Id = Guid.NewGuid(),
                RoomId = room3.Id,
                PositionX = 5,
                PositionY  = 0,
                Lenght = 5,
                Width = 5
            };
            room3.GRoomId = gRoom3.Id;
            Room room4 = new()
            {
                Id = Guid.NewGuid(),
                FloorId = floor13.Id,
                Name = "A13",
                BuildingId = floor13.BuildingId
            };
            GRoom gRoom4 = new()
            {
                Id = Guid.NewGuid(),
                RoomId = room4.Id,
                PositionX = 5,
                PositionY  = 0,
                Lenght = 5,
                Width = 5
            };
            room4.GRoomId = gRoom4.Id;
            Room room5 = new()
            {
                Id = Guid.NewGuid(),
                FloorId = floor21.Id,
                Name = "A21",
                BuildingId = floor21.BuildingId
            };
            GRoom gRoom5 = new()
            {
                Id = Guid.NewGuid(),
                RoomId = room5.Id,
                PositionX = 5,
                PositionY  = 0,
                Lenght = 5,
                Width = 5
            };
            room5.GRoomId = gRoom5.Id;
            Room room6 = new()
            {
                Id = Guid.NewGuid(),
                FloorId = floor21.Id,
                Name = "B21",
                BuildingId = floor21.BuildingId
            };
            
            GRoom gRoom6 = new()
            {
                Id = Guid.NewGuid(),
                RoomId = room6.Id,
                PositionX = 5,
                PositionY  = 0,
                Lenght = 5,
                Width = 5
            };
            room6.GRoomId = gRoom6.Id;
            Room room7 = new()
            {
                Id = Guid.NewGuid(),
                Name = "A22",
                FloorId = floor22.Id,
                BuildingId = floor22.BuildingId
            };
            GRoom gRoom7 = new()
            {
                Id = Guid.NewGuid(),
                RoomId = room7.Id,
                PositionX = 5,
                PositionY  = 0,
                Lenght = 5,
                Width = 5
            };
            room7.GRoomId = gRoom7.Id;
            Room room8 = new()
            {
                Id = Guid.NewGuid(),
                Name = "C23",
                FloorId = floor23.Id,
                BuildingId = floor23.BuildingId
            };
            GRoom gRoom8 = new()
            {
                Id = Guid.NewGuid(),
                RoomId = room8.Id,
                PositionX = 5,
                PositionY  = 0,
                Lenght = 5,
                Width = 5
            };
            room8.GRoomId = gRoom8.Id;
            Room room9 = new()
            {
                Id = Guid.NewGuid(),
                Name = "B23",
                FloorId = floor23.Id,
                BuildingId = floor23.BuildingId
            };
            GRoom gRoom9 = new()
            {
                Id = Guid.NewGuid(),
                RoomId = room9.Id,
                PositionX = 5,
                PositionY  = 0,
                Lenght = 5,
                Width = 5
            };
            room9.GRoomId = gRoom9.Id;
            modelBuilder.Entity<Room>().HasData(
                room1,room2,room3,room4,room5,room6,room7,room8,room9
            );
            modelBuilder.Entity<GRoom>().HasData(
                gRoom1,gRoom2,gRoom3,gRoom4,gRoom5,gRoom6,gRoom7,gRoom8,gRoom9
            );
            RoomBed room1Bed1 = new()
            {
                Id = Guid.NewGuid(),
                IsFree = true,
                Number = "11A1",
                RoomId = room1.Id
            };
            RoomBed room1Bed2 = new()
            {
                Id = Guid.NewGuid(),
                IsFree = true,
                Number = "11A2",
                RoomId = room1.Id
            };
            RoomBed room1Bed3 = new()
            {
                Id = Guid.NewGuid(),
                IsFree = true,
                Number = "11A3",
                RoomId = room1.Id
            };
            RoomBed room1Bed4 = new()
            {
                Id = Guid.NewGuid(),
                IsFree = true,
                Number = "11A4",
                RoomId = room1.Id
            };
            RoomBed room2Bed1 = new()
            {
                Id = Guid.NewGuid(),
                IsFree = true,
                Number = "12A1",
                RoomId = room2.Id
            };
            RoomBed room2Bed2 = new()
            {
                Id = Guid.NewGuid(),
                IsFree = true,
                Number = "12A2",
                RoomId = room2.Id
            };
            RoomBed room2Bed3 = new()
            {
                Id = Guid.NewGuid(),
                IsFree = true,
                Number = "12A3",
                RoomId = room2.Id
            };
            RoomBed room2Bed4 = new()
            {
                Id = Guid.NewGuid(),
                IsFree = true,
                Number = "12A4",
                RoomId = room2.Id
            };
            RoomBed room2Bed5 = new()
            {
                Id = Guid.NewGuid(),
                IsFree = true,
                Number = "12A5",
                RoomId = room2.Id
            };
            modelBuilder.Entity<RoomBed>().HasData(
                room1Bed1, room1Bed2, room1Bed3, room1Bed4, 
                room2Bed1, room2Bed2, room2Bed3, room2Bed4, room2Bed5
            );
            //password 123
            Doctor doctor = new()
            {
                Id = Guid.NewGuid(),
                SpecializationId = specializationDermatology.Id,
                AddressId = address1.Id,
                WorkingScheduleId = workingSchedule1.Id,
                RoomId = room2.Id,
                Username = "Tadjo",
                Password = "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy",
                Name = "Djordje",
                Surname = "Vuckovic",
                Email = "DjordjeLopov@gmail.com",
                Jmbg = "99999999",
                Phone = "+612222222",
                UserRole = UserRole.Doctor,
                Enabled = true
            };
            Doctor doctor1 = new()
            {
                Id = Guid.NewGuid(),
                SpecializationId = specializationDermatology.Id,
                AddressId = address.Id,
                WorkingScheduleId = workingSchedule2.Id,
                RoomId = room1.Id,
                Username = "Ilija",
                Password = "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy",
                Name = "Ilija",
                Surname = "Maric",
                Email = "Cajons@gmail.com",
                Jmbg = "99999999",
                Phone = "+612222222",
                UserRole = UserRole.Doctor,
                Enabled = true
            };
            modelBuilder.Entity<Doctor>().HasData(
                doctor,doctor1
            );
            Patient patient1 = new()
            {
                Id = Guid.NewGuid(),
                AddressId = address.Id,
                Username = "Sale",
                Password = "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy",
                Name = "Sale",
                Surname = "Lave",
                Email = "psw.isa.mail@gmail.com",
                Jmbg = "99999999",
                Phone = "+612222222",
                UserRole = UserRole.Patient,
                Enabled = true
            };
            Patient patient2 = new()
            {
                Id = Guid.NewGuid(),
                AddressId = address2.Id,
                Username = "Miki",
                Password = "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy",
                Name = "Miki",
                Surname = "Djuricic",
                Email = "psw.isa.mail@gmail.com",
                Jmbg = "99999999",
                Phone = "+612222222",
                UserRole = UserRole.Patient,
                Enabled = true
            };
            modelBuilder.Entity<Patient>().HasData(
                patient1,patient2
            );
            //password = 123
            Manager manager = new ()
            {
                Id = Guid.NewGuid(),
                AddressId = address2.Id,
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
            modelBuilder.Entity<Manager>().HasData(
                manager
            );
            // PatientAdmission patientAdmission1 = new()
            // {
            //     Id = Guid.NewGuid(),
            //     DateOfAdmission = new DateTime(2022,13,11,15, 0, 0),
            //     PatientId = patient1.Id,
            //     SelectedBedId = room1Bed1.Id,
            //     SelectedRoomId = room1.Id,
            //     Reason = "lorem ipsum"
            // };
            // modelBuilder.Entity<PatientAdmission>().HasData(
            //     patientAdmission1
            // );
            Appointment appointment = new()
            {
                Id = Guid.NewGuid(),
                Emergent = false,
                PatientId = patient1.Id,
                DoctorId = doctor1.Id,
                AppointmentType = AppointmentType.Examination,
                AppointmentState = AppointmentState.Pending
            };

            Holiday holiday1 = new()
            {
                Id = Guid.NewGuid(),
                Description = "I want to go to Paralia",
                DoctorId = doctor1.Id,
                IsUrgent = false,
                HolidayStatus = 0
            };

            modelBuilder.Entity<Holiday>()
                .OwnsOne(holiday => holiday.DateRange)
                .HasData(
                    new
                    {
                        HolidayId = holiday1.Id,
                        From = new DateTime(2022, 10, 27, 15, 0, 0),
                        To = new DateTime(2022, 10, 27, 15, 15, 0)
                    }
                    );
            modelBuilder.Entity<Holiday>().HasData(holiday1);

            modelBuilder.Entity<Appointment>()
                .OwnsOne(app => app.Duration)
                .HasData(
                    new
                    {
                        AppointmentId = appointment.Id,
                        From = new DateTime(2023, 7, 27, 15, 0, 0),
                        To = new DateTime(2023, 8, 7, 15, 15, 0)
                    }
                );
            modelBuilder.Entity<Appointment>().HasData(
                appointment
            );

            BloodUnit unit1 = new()
            {
                Id= Guid.NewGuid(),
                BloodType = BloodType.Aneg,
                Amount = 7,
                BloodBankName = "Moja Banka Krvi"
                    
            };
            
            BloodUnit unit2 = new()
            {
                Id= Guid.NewGuid(),
                BloodType = BloodType.Oneg,
                Amount = 10,
                BloodBankName = "Moja Banka Krvi"
                    
            };
            
            BloodUnit unit3 = new()
            {
                Id= Guid.NewGuid(),
                BloodType = BloodType.Aneg,
                Amount = 1,
                BloodBankName = "Moja Banka Krvi"
                    
            };

            
            modelBuilder.Entity<BloodUnit>().HasData(
                unit1, unit2,unit3
            );

            BloodConsumption consumption1 = new BloodConsumption()
            {
                Id = Guid.NewGuid(),
                BloodUnitId = unit1.Id,
                Amount = 2,
                DoctorId = doctor.Id,
                Date = new DateTime(2022, 10, 27, 15, 0, 0),
                Purpose = "operation"
            };
            
            BloodConsumption consumption2 = new BloodConsumption()
            {
                Id = Guid.NewGuid(),
                BloodUnitId = unit1.Id,
                Amount = 4,
                DoctorId = doctor.Id,
                Date = new DateTime(2022, 11, 14, 15, 0, 0),
                Purpose = "operation"
            };
            
            modelBuilder.Entity<BloodConsumption>().HasData(
                consumption1, consumption2
            );

        }
    }
}
