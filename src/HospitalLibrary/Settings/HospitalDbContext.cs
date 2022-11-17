using System;
using System.Reflection;
using HospitalLibrary.ApplicationUsers.Model;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.BloodConsumptions.Model;
using HospitalLibrary.BloodUnits.Model;
using HospitalLibrary.Doctors.Model;
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
        public DbSet<FloorPlanView> FloorPlanViews { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        //public DbSet<Address> Addresses { get; set; }
        public DbSet<WorkingSchedule> WorkingSchedules { get; set; }
        public DbSet<GRoom> GRooms { get; set; }
        public DbSet<RoomBed> RoomBeds { get; set; }
        public DbSet<PatientAdmission> PatientAdmissions { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<BloodUnit> BloodUnits { get; set; }

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
            Building building2 = new()
            {
                Id = Guid.NewGuid(),
                Name = "Nova bolnica"
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
            
            modelBuilder.Entity<Building>().HasData(
                building1,building2
            );
            
            modelBuilder.Entity<Floor>().HasData(
                floor11,floor12,floor13,floor21,floor22,floor23
            );

            FloorPlanView floorPlanView1 = new()
            {
                Id = Guid.NewGuid(),
                PosX = 0,
                PosY = 0,
                Lenght = 5,
                Width = 5
            };
            
            FloorPlanView floorPlanView2 = new()
            {
                Id = Guid.NewGuid(),
                PosX = 5,
                PosY = 0,
                Lenght = 5,
                Width = 5
            };
            
            FloorPlanView floorPlanView3 = new()
            {
                Id = Guid.NewGuid(),
                PosX = 0,
                PosY = 5,
                Lenght = 5,
                Width = 5
            };
            modelBuilder.Entity<FloorPlanView>().HasData(
                floorPlanView1, floorPlanView2, floorPlanView3
            );
            Room room1 = new()
            {
                Id = Guid.NewGuid(),
                FloorId = floor11.Id,
                BuildingName = "Stara zgrada",
                FloorName = "Prvi",
                Number = "11A",
                PositionX = 0,
                PositionY  = 0,
                Lenght = 5,
                Width = 5
            };
            //pravilno
            // Room room1 = new()
            // {
            //     Id = Guid.NewGuid(),
            //     FloorId = floor11.Id,
            //     Number = "11A",
            // };
            GRoom gRoom1 = new()
            {
                Id = Guid.NewGuid(),
                RoomId = room1.Id,
                PositionX = 0,
                PositionY = 0,
                Lenght = 5,
                Width = 5
            };
            Room room2 = new()
            {
                Id = Guid.NewGuid(),
                FloorId = floor11.Id,
                BuildingName = "Stara zgrada",
                FloorName = "Prvi",
                Number = "12A",
                PositionX = 5,
                PositionY  = 0,
                Lenght = 5,
                Width = 5
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
            Room room3 = new()
            {
                Id = Guid.NewGuid(),
                FloorId = floor12.Id,
                BuildingName = "Stara zgrada",
                FloorName = "Drugi",
                Number = "13A",
                PositionX = 10,
                PositionY  = 0,
                Lenght = 5,
                Width = 5
            };
            Room room4 = new()
            {
                Id = Guid.NewGuid(),
                FloorId = floor13.Id,
                BuildingName = "Stara zgrada",
                FloorName = "Treci",
                Number = "14A",
                PositionX = 0,
                PositionY  = 5,
                Lenght = 5,
                Width = 5
            };
            Room room5 = new()
            {
                Id = Guid.NewGuid(),
                FloorId = floor21.Id,
                BuildingName = "Nova zgrada",
                FloorName = "Prvi",
                Number = "11B",
                PositionX = 0,
                PositionY  = 10,
                Lenght = 5,
                Width = 5
            };
            Room room6 = new()
            {
                Id = Guid.NewGuid(),
                FloorId = floor21.Id,
                BuildingName = "Nova zgrada",
                FloorName = "Prvi",
                Number = "12B",
                PositionX = 5,
                PositionY  = 5,
                Lenght = 5,
                Width = 5
            };
            Room room7 = new()
            {
                Id = Guid.NewGuid(),
                FloorId = floor22.Id,
                BuildingName = "Nova zgrada",
                FloorName = "Drugi",
                Number = "13B",
                PositionX = 10,
                PositionY  = 5,
                Lenght = 5,
                Width = 5
            };
            Room room8 = new()
            {
                Id = Guid.NewGuid(),
                FloorId = floor23.Id,
                BuildingName = "Nova zgrada",
                FloorName = "Treci",
                Number = "14B",
                PositionX = 0,
                PositionY  = 0,
                Lenght = 10,
                Width = 20
            };
            Room room9 = new()
            {
                Id = Guid.NewGuid(),
                BuildingName = "Nova zgrada",
                FloorId = floor23.Id,
                FloorName = "Treci",
                Number = "15B",
                PositionX = 0,
                PositionY  = 10,
                Lenght = 5,
                Width = 20
            };
            modelBuilder.Entity<Room>().HasData(
                room1,room2,room3,room4,room5,room6,room7,room8,room9
            );
            modelBuilder.Entity<GRoom>().HasData(
                gRoom1,gRoom2
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
                UserRole = UserRole.Doctor
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
                UserRole = UserRole.Doctor
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
                UserRole = UserRole.Patient
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
                UserRole = UserRole.Patient
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
                UserRole = UserRole.Manager
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
            
            modelBuilder.Entity<BloodUnit>().HasData(
                unit1, unit2
            );

            BloodConsumption consumption1 = new BloodConsumption()
            {
                Id = Guid.NewGuid(),
                BloodType = BloodType.Aneg,
                Amount = 2,
                BloodBankName = "Moja Banka Krvi",
                DoctorId = doctor.Id,
                //Doctor = doctor,
                date = new DateTime(2022, 10, 27, 15, 0, 0),
                purpose = "operation"
            };
            
            BloodConsumption consumption2 = new BloodConsumption()
            {
                Id = Guid.NewGuid(),
                BloodType = BloodType.Aneg,
                Amount = 4,
                BloodBankName = "Moja Banka Krvi",
                DoctorId = doctor.Id,
                //Doctor = doctor,
                date = new DateTime(2022, 11, 14, 15, 0, 0),
                purpose = "operation"
            };
            
            modelBuilder.Entity<BloodConsumption>().HasData(
                consumption1, consumption2
            );

        }
    }
}
