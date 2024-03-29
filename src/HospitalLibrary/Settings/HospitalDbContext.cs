﻿using System;
using System.Collections.Generic;
using System.Reflection;
using HospitalLibrary.ApplicationUsers.Model;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.BloodConsumptions.Model;
using HospitalLibrary.BloodUnits.Model;
using HospitalLibrary.Consiliums.Model;
using HospitalLibrary.Doctors.Model;
using HospitalLibrary.Enums;
using HospitalLibrary.Examinations.DbConfig;
using HospitalLibrary.Examinations.EventStores;
using HospitalLibrary.Examinations.Model;
using HospitalLibrary.Holidays.Model;
using HospitalLibrary.Managers;
using HospitalLibrary.Medicines.Model;
using HospitalLibrary.Patients.Enums;
using HospitalLibrary.Patients.Model;
using HospitalLibrary.SharedModel;
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
        public DbSet<Allergen> Allergens { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<MaliciousPatient> MaliciousPatients { get; set; }
        public DbSet<WorkingSchedule> WorkingSchedules { get; set; }
        public DbSet<GRoom> GRooms { get; set; }
        public DbSet<RoomBed> RoomBeds { get; set; }
        public DbSet<PatientAdmission> PatientAdmissions { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<BloodUnit> BloodUnits { get; set; }
        public DbSet<BloodConsumption> BloodConsumptions { get; set; }
        public DbSet<Examination> Examinations { get; set; }
        public DbSet<Symptom> Symptoms { get; set; }
        public DbSet<RoomEquipment> RoomEquipment { get; set; }
        public DbSet<Consilium> Consiliums { get; set; }
        public DbSet<Holiday> Holidays { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<EventStoreExamination> EventStoreExaminations { get; set; }
        public DbSet<PatientHealthStateNotification> PatientHealthStateNotifications { get; set; }
        //public DbSet<ExaminationSymptom> ExaminationSymptoms  { get; set; }
       

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
                        To = new DateTime(2023, 12, 27)
                    },
                    new
                    {
                        WorkingScheduleId = workingSchedule2.Id,
                        From = new DateTime(2022, 10, 27),
                        To = new DateTime(2023, 12, 27)
                    }
                );
            modelBuilder.Entity<WorkingSchedule>()
                .OwnsOne(app => app.DayOfWork)
                .HasData(
                    new
                    {
                        WorkingScheduleId = workingSchedule1.Id,
                        From = new DateTime(2022, 10, 27, 8, 0, 0),
                        To = new DateTime(2023, 12, 27, 14, 0, 0)
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

            Allergen allergen1 = new()
            {
                Id = Guid.NewGuid(),
                Name = "Paracetamol"
            };
            Allergen allergen2 = new()
            {
                Id = Guid.NewGuid(),
                Name = "Brufen"
            };
            modelBuilder.Entity<Allergen>().HasData(
                allergen1,allergen2
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
                Name = "G1",
                BuildingId = floor11.BuildingId,
                Type = RoomType.EXAMINATION
            };
            
            GRoom gRoom1 = new()
            {
                Id = Guid.NewGuid(),
                RoomId = room1.Id,
                PositionX = 0,
                PositionY = 0,
                Lenght = 6,
                Width = 5
            };

            Room room13 = new()
            {
                Id = Guid.NewGuid(),
                FloorId = floor11.Id,
                Name = "G3",
                BuildingId = floor11.BuildingId,
                Type = RoomType.EXAMINATION
            };
            
            GRoom gRoom13 = new()
            {
                Id = Guid.NewGuid(),
                RoomId = room13.Id,
                PositionX = 10,
                PositionY = 0,
                Lenght = 6,
                Width = 5
            };
            Room room14 = new()
            {
                Id = Guid.NewGuid(),
                FloorId = floor11.Id,
                Name = "G4",
                BuildingId = floor11.BuildingId,
                Type = RoomType.EXAMINATION
            };
            
            GRoom gRoom14 = new()
            {
                Id = Guid.NewGuid(),
                RoomId = room14.Id,
                PositionX = 15,
                PositionY = 0,
                Lenght = 6,
                Width = 5
            };
            Room room15 = new()
            {
                Id = Guid.NewGuid(),
                FloorId = floor11.Id,
                Name = "H",
                BuildingId = floor11.BuildingId,
                Type = RoomType.EXAMINATION
            };
            
            GRoom gRoom15 = new()
            {
                Id = Guid.NewGuid(),
                RoomId = room15.Id,
                PositionX = 0,
                PositionY = 6,
                Lenght = 3,
                Width = 20
            };
            
            Room room16 = new()
            {
                Id = Guid.NewGuid(),
                FloorId = floor11.Id,
                Name = "C1",
                BuildingId = floor11.BuildingId,
                Type = RoomType.EXAMINATION
            };
            
            GRoom gRoom16 = new()
            {
                Id = Guid.NewGuid(),
                RoomId = room16.Id,
                PositionX = 0,
                PositionY = 9,
                Lenght = 6,
                Width = 20
            };
            room1.GRoomId = gRoom1.Id;
            room13.GRoomId = gRoom13.Id;
            room14.GRoomId = gRoom14.Id;
            room15.GRoomId = gRoom15.Id;
            room16.GRoomId = gRoom16.Id;
            Room room2 = new()
            {
                Id = Guid.NewGuid(),
                FloorId = floor11.Id,
                Name = "G21",
                BuildingId = floor11.BuildingId,
                Type = RoomType.EXAMINATION
            };
            GRoom gRoom2 = new()
            {
                Id = Guid.NewGuid(),
                RoomId = room2.Id,
                PositionX = 5,
                PositionY = 0,
                Lenght = 6,
                Width = 5
            };
            room2.GRoomId = gRoom2.Id;
            ////
            Room room3 = new()
            {
                Id = Guid.NewGuid(),
                FloorId = floor12.Id,
                Name = "F1",
                BuildingId = floor12.BuildingId,
                Type = RoomType.EXAMINATION
            };
            GRoom gRoom3 = new()
            {
                Id = Guid.NewGuid(),
                RoomId = room3.Id,
                PositionX = 0,
                PositionY  = 0,
                Lenght = 10,
                Width = 8
            };
            room3.GRoomId = gRoom3.Id;
            
            Room room31 = new()
            {
                Id = Guid.NewGuid(),
                FloorId = floor12.Id,
                Name = "F2",
                BuildingId = floor12.BuildingId,
                Type = RoomType.EXAMINATION
            };
            GRoom gRoom31 = new()
            {
                Id = Guid.NewGuid(),
                RoomId = room31.Id,
                PositionX = 0,
                PositionY  = 10,
                Lenght = 5,
                Width = 8
            };
            room31.GRoomId = gRoom31.Id;
            
            
            Room room32 = new()
            {
                Id = Guid.NewGuid(),
                FloorId = floor12.Id,
                Name = "F3",
                BuildingId = floor12.BuildingId,
                Type = RoomType.EXAMINATION
            };
            GRoom gRoom32 = new()
            {
                Id = Guid.NewGuid(),
                RoomId = room32.Id,
                PositionX = 12,
                PositionY  = 0,
                Lenght = 10,
                Width = 8
            };
            room32.GRoomId = gRoom32.Id;
            Room room33 = new()
            {
                Id = Guid.NewGuid(),
                FloorId = floor12.Id,
                Name = "F4",
                BuildingId = floor12.BuildingId,
                Type = RoomType.EXAMINATION
            };
            GRoom gRoom33 = new()
            {
                Id = Guid.NewGuid(),
                RoomId = room33.Id,
                PositionX = 12,
                PositionY  = 10,
                Lenght = 5,
                Width = 8
            };
            room33.GRoomId = gRoom33.Id;
            
            Room room34 = new()
            {
                Id = Guid.NewGuid(),
                FloorId = floor12.Id,
                Name = "H",
                BuildingId = floor12.BuildingId,
                Type = RoomType.EXAMINATION
            };
            GRoom gRoom34 = new()
            {
                Id = Guid.NewGuid(),
                RoomId = room34.Id,
                PositionX = 8,
                PositionY  = 0,
                Lenght = 15,
                Width = 4
            };
            room34.GRoomId = gRoom34.Id;

            
            
            ///
            Room room4 = new()
            {
                Id = Guid.NewGuid(),
                FloorId = floor13.Id,
                Name = "A13",
                BuildingId = floor13.BuildingId,
                Type = RoomType.EXAMINATION
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
                BuildingId = floor21.BuildingId,
                Type = RoomType.MEETING_ROOM
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
                BuildingId = floor21.BuildingId,
                Type = RoomType.MEETING_ROOM
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
                BuildingId = floor22.BuildingId,
                Type = RoomType.MEETING_ROOM
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
                BuildingId = floor23.BuildingId,
                Type = RoomType.MEETING_ROOM
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
                BuildingId = floor23.BuildingId,
                Type = RoomType.MEETING_ROOM
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
            
            
            Room room10 = new()
            {
                Id = Guid.NewGuid(),
                Name = "C23",
                FloorId = floor23.Id,
                BuildingId = floor23.BuildingId,
                Type = RoomType.MEETING_ROOM
            };
            GRoom gRoom10 = new()
            {
                Id = Guid.NewGuid(),
                RoomId = room10.Id,
                PositionX = 15,
                PositionY  = 0,
                Lenght = 10,
                Width = 5
            };
            room10.GRoomId = gRoom10.Id;
            
            modelBuilder.Entity<Room>().HasData(
                room1,room2,room3,room4,room5,room6,room7,room8,room9,room10,room13,room14,room15,room16,
                room31,room32,room33,room34
            );
            modelBuilder.Entity<GRoom>().HasData(
                gRoom1,gRoom2,gRoom3,gRoom4,gRoom5,gRoom6,gRoom7,gRoom8,gRoom9,gRoom10,
                gRoom13,gRoom14,gRoom15,gRoom16,gRoom31,gRoom32,gRoom33,gRoom34
            );

            RoomEquipment roomEquipment1 = new()
            {
                RoomId = room1.Id,   
                RoomEquipmentId =Guid.NewGuid(),
                Amount = 15,
                EquipmentName = "SURGICAL_TABLES"
            };
            RoomEquipment roomEquipment11 = new()
            {
                RoomId = room1.Id,   
                RoomEquipmentId =Guid.NewGuid(),
                Amount = 5,
                EquipmentName = "ANESTHESIA"
            };
            
            RoomEquipment roomEquipment111 = new()
            {
                RoomId = room1.Id,   
                RoomEquipmentId =Guid.NewGuid(),
                Amount = 13,
                EquipmentName = "SYRINGE"
            };
            RoomEquipment roomEquipment2 = new()
            {
                RoomId = room2.Id,   
                RoomEquipmentId =Guid.NewGuid(),
                Amount = 10,
                EquipmentName = "ANESTHESIA"
            };
            RoomEquipment roomEquipment22 = new()
            {
                RoomId = room2.Id,   
                RoomEquipmentId =Guid.NewGuid(),
                Amount = 5,
                EquipmentName = "EKG_MACHINE"
            };
            RoomEquipment roomEquipment3 = new()
            {
                RoomId = room3.Id,   
                RoomEquipmentId =Guid.NewGuid(),
                Amount = 3,
                EquipmentName = "EKG_MACHINE"
            };
            RoomEquipment roomEquipment33 = new()
            {
                RoomId = room3.Id,   
                RoomEquipmentId =Guid.NewGuid(),
                Amount = 23,
                EquipmentName = "SURGICAL_TABLES"
            };
            RoomEquipment roomEquipment4 = new()
            {
                RoomId = room4.Id,   
                RoomEquipmentId =Guid.NewGuid(),
                Amount = 11,
                EquipmentName = "ANESTHESIA"
            };
            
            RoomEquipment roomEquipment44 = new()
            {
                RoomId = room4.Id,   
                RoomEquipmentId =Guid.NewGuid(),
                Amount = 4,
                EquipmentName = "BANDAGE"
            };

            RoomEquipment roomEquipment5 = new()
            {
                RoomId = room5.Id,   
                RoomEquipmentId =Guid.NewGuid(),
                Amount = 22,
                EquipmentName = "EKG_MACHINE"
            };
            
            RoomEquipment roomEquipment55 = new()
            {
                RoomId = room5.Id,   
                RoomEquipmentId =Guid.NewGuid(),
                Amount = 6,
                EquipmentName = "SYRINGE"
            };
            
            RoomEquipment roomEquipment555 = new()
            {
                RoomId = room5.Id,   
                RoomEquipmentId =Guid.NewGuid(),
                Amount = 15,
                EquipmentName = "SURGICAL_TABLES"
            };
            
            RoomEquipment roomEquipment6 = new()
            {
                RoomId = room6.Id,   
                RoomEquipmentId =Guid.NewGuid(),
                Amount = 14,
                EquipmentName = "SURGICAL_TABLES"
            };
            
            RoomEquipment roomEquipment66 = new()
            {
                RoomId = room6.Id,   
                RoomEquipmentId =Guid.NewGuid(),
                Amount = 7,
                EquipmentName = "SYRINGE"
            };
            
            RoomEquipment roomEquipment7 = new()
            {
                RoomId = room7.Id,   
                RoomEquipmentId =Guid.NewGuid(),
                Amount = 6,
                EquipmentName = "BANDAGE"
            };
            
            RoomEquipment roomEquipment8 = new()
            {
                RoomId = room8.Id,   
                RoomEquipmentId =Guid.NewGuid(),
                Amount = 3,
                EquipmentName = "ANESTHESIA"
            };
            
            RoomEquipment roomEquipment88 = new()
            {
                RoomId = room8.Id,   
                RoomEquipmentId =Guid.NewGuid(),
                Amount = 13,
                EquipmentName = "BANDAGE"
            };
            
            RoomEquipment roomEquipment9 = new()
            {
                RoomId = room9.Id, 
                RoomEquipmentId =Guid.NewGuid(),
                Amount = 9,
                EquipmentName = "SURGICAL_TABLES"
            };
            
            RoomEquipment roomEquipment99 = new()
            {
                RoomId = room9.Id, 
                RoomEquipmentId =Guid.NewGuid(),
                Amount = 9,
                EquipmentName = "ANESTHESIA"
            };
            
            //room9.Equipments.Add(roomEquipment9);
            
            modelBuilder.Entity<RoomEquipment>().HasData(
                roomEquipment1,roomEquipment2,roomEquipment3,roomEquipment4,roomEquipment5,roomEquipment6,roomEquipment7,roomEquipment8,roomEquipment9,
                roomEquipment11,roomEquipment111,roomEquipment22,roomEquipment33,roomEquipment44,roomEquipment55,roomEquipment555,roomEquipment66,
                roomEquipment88,roomEquipment99
                
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
                UserRole = UserRole.Doctor,
                Enabled = true,
                IsBlocked = false
            };
            Doctor doctor1 = new()
            {
                Id = new Guid("c94fcdfd-5ce3-4133-ab82-2ac292bcd0e2"),
                SpecializationId = specializationGeneral.Id,
                AddressId = address.Id,
                WorkingScheduleId = workingSchedule1.Id,
                RoomId = room1.Id,
                Username = "Ilija",
                Password = "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy",
                Name = "Ilija",
                Surname = "Maric",
                Email = "Cajons@gmail.com",
                UserRole = UserRole.Doctor,
                Enabled = true,
                IsBlocked = false
            };
            
            Doctor doctor2 = new()
            {
                Id = Guid.NewGuid(),
                SpecializationId = specializationGeneral.Id,
                AddressId = address.Id,
                WorkingScheduleId = workingSchedule1.Id,
                RoomId = room7.Id,
                Username = "Milos",
                Password = "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy",
                Name = "Milos",
                Surname = "Milosevic",
                Email = "Cajons@gmail.com",
                UserRole = UserRole.Doctor,
                Enabled = true,
                IsBlocked = false
            }; 
            Doctor doctor3 = new()
            {
                Id = Guid.NewGuid(),
                SpecializationId = specializationSurgeon.Id,
                AddressId = address.Id,
                WorkingScheduleId = workingSchedule2.Id,
                RoomId = room4.Id,
                Username = "Jakov",
                Password = "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy",
                Name = "Jakov",
                Surname = "Milosevic",
                Email = "Cajons@gmail.com",
                UserRole = UserRole.Doctor,
                Enabled = true
            };
            modelBuilder.Entity<Doctor>().HasData(
                doctor,doctor1,doctor2,doctor3
            );

            Medicine medicine1 = new()
            {
                Id = Guid.NewGuid(),
                Name = "Brufen 300",
                Amount = 30
            };
            
            Medicine medicine2 = new()
            {
                Id = Guid.NewGuid(),
                Name = "Aspirin",
                Amount = 1
            };
            modelBuilder.Entity<Medicine>().HasData(
                medicine1,medicine2
            );

            List<Allergen> allergens = new List<Allergen>();
            allergens.Add(allergen1);
            allergens.Add(allergen2);

            Patient patient1 = new()
            {
                Id = new Guid("0bcf5d99-617e-48f4-a04d-c6ee2baa23cd"),
                AddressId = address.Id,
                Username = "Sale",
                Password = "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy",
                Name = "Sale",
                Surname = "Lave",
                Email = "psw.isa.mail@gmail.com",
                UserRole = UserRole.Patient,
                Enabled = true,
                DoctorId = doctor1.Id,
                DateOfBirth = new DateTime(2007,10,12),
                Gender = Gender.MALE,
                Age = 15,
                Phone = "021420420",
                Jmbg = "131213121312",
                BloodType = BloodType.ABpos,
                Allergies = new List<Allergen>(),
                IsBlocked = false
            };
            Patient patient2 = new()
            {
                Id = new Guid("dd3959fa-ed05-43a0-b549-95c40cb6bd04"),
                Phone = "021420420",
                AddressId = address2.Id,
                Jmbg = "131213121312",
                Username = "Miki",
                Password = "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy",
                Name = "Miki",
                Surname = "Djuricic",
                Email = "psw.isa.mail@gmail.com",
                UserRole = UserRole.Patient,
                Enabled = true,
                DoctorId = doctor1.Id,
                DateOfBirth = new DateTime(1990,10,12),
                Gender = Gender.MALE,
                Age = 32,
                BloodType = BloodType.Aneg,
                Allergies = new List<Allergen>(),
                IsBlocked = false
            };
            
            Patient patient3 = new()
            {
                Id = new Guid("f85589c1-7405-4afd-9e47-c54e30c168dd"),
                Phone = "021420420",
                Jmbg = "131213121312",
                AddressId = address2.Id,
                Username = "Nina",
                Password = "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy",
                Name = "Nina",
                Surname = "Minic",
                Email = "psw.isa.mail@gmail.com",
                UserRole = UserRole.Patient,
                Enabled = true,
                DoctorId = doctor1.Id,
                Gender = Gender.FEMALE,
                Age = 5,
                BloodType = BloodType.Aneg,
                IsBlocked = false
            };
            Patient patient4 = new()
            {
                Id = new Guid("01bf0f4d-3ccb-422b-9c6b-5612f0ecf5a7"),
                Phone = "021420420",
                AddressId = address2.Id,
                Jmbg = "131213121312",
                Username = "Mina",
                Password = "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy",
                Name = "Mina",
                Surname = "Minic",
                Email = "psw.isa.mail@gmail.com",
                UserRole = UserRole.Patient,
                Enabled = true,
                DoctorId = doctor2.Id,
                Gender = Gender.FEMALE,
                Age = 9,
                BloodType = BloodType.Aneg,
                IsBlocked = false
            };
            Patient patient5 = new()
            {
                Id = new Guid("4e0ffa00-c738-4ba0-9445-815325262b5b"),
                Phone = "021420420",
                AddressId = address2.Id,
                Jmbg = "131213121312",
                Username = "Nikola",
                Password = "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy",
                Name = "Nikola",
                Surname = "Nikolic",
                Email = "psw.isa.mail@gmail.com",
                UserRole = UserRole.Patient,
                Enabled = true,
                DoctorId = doctor1.Id,
                Gender = Gender.OTHER,
                Age = 18,
                BloodType = BloodType.Aneg,
                IsBlocked = false
            };
            Patient patient6 = new()
            {
                Id = new Guid("ce4b4c0f-4db2-4565-832b-19aa7425ea6b"),
                Phone = "021420420",
                AddressId = address2.Id,
                Username = "Marko",
                Password = "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy",
                Name = "Marko",
                Surname = "Markovic",
                Email = "psw.isa.mail@gmail.com",
                UserRole = UserRole.Patient,
                Enabled = true,
                Jmbg = "131213121312",
                DoctorId = doctor2.Id,
                Gender = Gender.MALE,
                Age = 65,
                BloodType = BloodType.Aneg,
                IsBlocked = false
            };
            Patient patient7 = new()
            {
                Id = new Guid("ff37875b-73c6-44bd-b8a4-b53d7fce7bd3"),
                Phone = "021420420",
                AddressId = address2.Id,
                Username = "Manja",
                Password = "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy",
                Name = "Manja",
                Surname = "Maric",
                Email = "psw.isa.mail@gmail.com",
                UserRole = UserRole.Patient,
                Enabled = true,
                Jmbg = "131213121312",
                DoctorId = doctor2.Id,
                Gender = Gender.FEMALE,
                Age = 50,
                BloodType = BloodType.Aneg,
                IsBlocked = false
            };
            Patient patient8 = new()
            {
                Id = new Guid("eae6cae2-2d8c-4689-8902-21d2fbeb57bb"),
                Phone = "021420420",
                AddressId = address2.Id,
                Username = "Darko",
                Password = "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy",
                Name = "Darko",
                Surname = "Darkovic",
                Email = "psw.isa.mail@gmail.com",
                UserRole = UserRole.Patient,
                Jmbg = "131213121312",
                Enabled = true,
                DoctorId = doctor.Id,
                Gender = Gender.MALE,
                Age = 70,
                BloodType = BloodType.Aneg,
                IsBlocked = false
            };
            Patient patient9 = new()
            {
                Id = new Guid("659d588f-e141-437e-8d9f-232a537b6c74"),
                Phone = "021420420",
                AddressId = address2.Id,
                Username = "Filip",
                Password = "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy",
                Name = "Filip",
                Surname = "Filipic",
                Email = "psw.isa.mail@gmail.com",
                Jmbg = "131213121312",
                UserRole = UserRole.Patient,
                Enabled = true,
                DoctorId = doctor.Id,
                Gender = Gender.MALE,
                Age = 56,
                BloodType = BloodType.Aneg,
                IsBlocked = false
            };
            Patient patient10 = new()
            {
                Id = new Guid("f748eecd-22bb-43c6-943c-1800e392da97"),
                AddressId = address2.Id,
                Phone = "021420420",
                Username = "Tara",
                Password = "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy",
                Name = "Tara",
                Surname = "Markovic",
                Email = "psw.isa.mail@gmail.com",
                Jmbg = "131213121312",
                UserRole = UserRole.Patient,
                Enabled = true,
                DoctorId = doctor2.Id,
                Gender = Gender.FEMALE,
                Age = 61,
                BloodType = BloodType.Aneg,
                IsBlocked = false
            };
            modelBuilder.Entity<Patient>().HasData(
                patient1,patient2,patient3, patient4, patient5, patient6,patient7,patient8,patient9,patient10
            );
            ApplicationUser applicationUser = new()
            {
                Id = Guid.NewGuid(),
                AddressId = address2.Id,
                Username = "BloodBank",
                Password = "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy",
                Name = "Moja Banka Krvi",
                Surname = "Moja Banka Krvi",
                Email = "psw.isa.mail@gmail.com",
                UserRole = UserRole.BloodBankCenter,
                Enabled = true,
                IsBlocked = false
            };
            modelBuilder.Entity<ApplicationUser>().HasData(
                applicationUser
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
                UserRole = UserRole.Manager,
                Enabled = true,
                IsBlocked = false
            };
            //modelBuilder.Entity<Manager>().HasData(
            //    manager
            //);
            Manager manager1 = new()
            {
                Id = Guid.NewGuid(),
                AddressId = address2.Id,
                Username = "ManagerBB",
                Password = "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy",
                Name = "Manager",
                Surname = "Blood Bank",
                Email = "psw.isa.mail@gmail.com",
                UserRole = UserRole.BloodBank,
                Enabled = true,
                IsBlocked = false
            };
            modelBuilder.Entity<Manager>().HasData(
               manager, manager1
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
            Appointment appointment = new(new Guid("c0576733-b7fa-4974-b60c-d3d7e8c9f216"))
            {
                Emergent = false,
                PatientId = patient1.Id,
                DoctorId = doctor1.Id,
                AppointmentType = AppointmentType.Examination,
                AppointmentState = AppointmentState.Pending
            };
            Appointment appointment1 = new(Guid.NewGuid())
            { 
                Emergent = false,
                PatientId = patient4.Id,
                DoctorId = doctor1.Id,
                AppointmentType = AppointmentType.Examination,
                AppointmentState = AppointmentState.Pending
            };
            Appointment appointment2 = new(Guid.NewGuid())
            {
                Emergent = false,
                PatientId = patient1.Id,
                DoctorId = doctor1.Id,
                AppointmentType = AppointmentType.Examination,
                AppointmentState = AppointmentState.Pending
            };
            Appointment appointment3 = new(Guid.NewGuid())
            {
                Emergent = false,
                PatientId = patient3.Id,
                DoctorId = doctor1.Id,
                AppointmentType = AppointmentType.Examination,
                AppointmentState = AppointmentState.Pending
            };
            Appointment appointment4 = new(Guid.NewGuid())
            {
                Emergent = false,
                PatientId = patient2.Id,
                DoctorId = doctor1.Id,
                AppointmentType = AppointmentType.Examination,
                AppointmentState = AppointmentState.Pending
            };
            Appointment appointment5 = new(Guid.NewGuid())
            {
                Emergent = false,
                PatientId = patient2.Id,
                DoctorId = doctor1.Id,
                AppointmentType = AppointmentType.Examination,
                AppointmentState = AppointmentState.Pending
            };
            Appointment appointment6 = new(Guid.NewGuid())
            {
                Emergent = false,
                PatientId = patient6.Id,
                DoctorId = doctor1.Id,
                AppointmentType = AppointmentType.Examination,
                AppointmentState = AppointmentState.Pending
            };
            Appointment appointment7 = new(Guid.NewGuid())
            {
                Emergent = false,
                PatientId = patient4.Id,
                DoctorId = doctor1.Id,
                AppointmentType = AppointmentType.Examination,
                AppointmentState = AppointmentState.Pending
            };
            Appointment appointment8 = new(Guid.NewGuid())
            {
                Emergent = false,
                PatientId = patient3.Id,
                DoctorId = doctor1.Id,
                AppointmentType = AppointmentType.Examination,
                AppointmentState = AppointmentState.Pending
            };
            Appointment appointment9 = new(Guid.NewGuid())
            {
                Emergent = false,
                PatientId = patient1.Id,
                DoctorId = doctor1.Id,
                AppointmentType = AppointmentType.Examination,
                AppointmentState = AppointmentState.Pending
            };
            Appointment appointment10 = new(Guid.NewGuid())
            {
                Emergent = false,
                PatientId = patient2.Id,
                DoctorId = doctor1.Id,
                AppointmentType = AppointmentType.Examination,
                AppointmentState = AppointmentState.Pending
            };
            Appointment appointment11 = new(Guid.NewGuid())
            {
                Emergent = false,
                PatientId = patient1.Id,
                DoctorId = doctor1.Id,
                AppointmentType = AppointmentType.Examination,
                AppointmentState = AppointmentState.Pending
            };
            Appointment appointment12 = new(Guid.NewGuid())
            {
                Emergent = false,
                PatientId = patient4.Id,
                DoctorId = doctor1.Id,
                AppointmentType = AppointmentType.Examination,
                AppointmentState = AppointmentState.Pending
            };
            Appointment appointment13 = new(Guid.NewGuid())
            {
                Emergent = false,
                PatientId = patient7.Id,
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
                        From = new DateTime(2023, 7, 27, 10, 0, 0),
                        To = new DateTime(2023, 7, 27, 10, 30, 0)
                    },
                    new
                    {
                        AppointmentId = appointment1.Id,
                        From = DateTime.Now.AddMinutes(-60),
                        To = DateTime.Now.AddMinutes(-30)
                    },
                    new
                    {
                        AppointmentId = appointment2.Id,
                        From = DateTime.Now.AddMinutes(-120),
                        To = DateTime.Now.AddMinutes(-90)
                    },
                    new
                    {
                        AppointmentId = appointment3.Id,
                        From = DateTime.Now.AddMinutes(-200),
                        To = DateTime.Now.AddMinutes(-170)
                    },
                    new
                    {
                        AppointmentId = appointment4.Id,
                        From = DateTime.Now.AddMinutes(-240),
                        To = DateTime.Now.AddMinutes(-200)
                    },
                    new
                    {
                        AppointmentId = appointment5.Id,
                        From = DateTime.Now.AddMinutes(-350),
                        To = DateTime.Now.AddMinutes(-250)
                    },
                    new
                    {
                        AppointmentId = appointment6.Id,
                        From = DateTime.Now.AddMinutes(-425),
                        To = DateTime.Now.AddMinutes(-365)
                    },
                    new
                    {
                        AppointmentId = appointment7.Id,
                        From = new DateTime(2022, 7, 27, 10, 0, 0),
                        To = new DateTime(2022, 7, 27, 10, 30, 0)
                    },
                    new
                    {
                        AppointmentId = appointment8.Id,
                        From = new DateTime(2022, 8, 27, 10, 0, 0),
                        To = new DateTime(2022, 8, 27, 10, 30, 0)
                    },
                    new
                    {
                        AppointmentId = appointment9.Id,
                        From = new DateTime(2022, 9, 27, 10, 0, 0),
                        To = new DateTime(2022, 9, 27, 10, 30, 0)
                    },
                    new
                    {
                        AppointmentId = appointment10.Id,
                        From = new DateTime(2022, 10, 27, 10, 0, 0),
                        To = new DateTime(2022, 10, 27, 10, 30, 0)
                    },
                    new
                    {
                        AppointmentId = appointment11.Id,
                        From = new DateTime(2022, 11, 27, 10, 0, 0),
                        To = new DateTime(2022, 11, 27, 10, 30, 0)
                    },
                    new
                    {
                        AppointmentId = appointment12.Id,
                        From = new DateTime(2022, 12, 27, 10, 0, 0),
                        To = new DateTime(2022, 12, 27, 10, 30, 0)
                    }
                );
            modelBuilder.Entity<Appointment>().HasData(
                appointment,appointment1,appointment2,appointment3,appointment4,appointment5,appointment6,appointment7,
                appointment8,appointment9,appointment10,appointment11,appointment12
            );

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
                        From = new DateTime(2022, 10, 27, 10, 0, 0),
                        To = new DateTime(2022, 10, 27, 10, 30, 0)
                    }
                    );
            modelBuilder.Entity<Holiday>().HasData(holiday1);

            BloodUnit unit1 = new(Guid.NewGuid(), BloodType.Aneg, 7, "Moja Banka Krvi");
            BloodUnit unit2 = new(Guid.NewGuid(),BloodType.Oneg,10,"Moja Banka Krvi");
            BloodUnit unit3 = new(Guid.NewGuid(),BloodType.Aneg,4,"Moja Banka Krvi");
            BloodUnit unit4 = new(5,Guid.NewGuid(), BloodType.Aneg, "Moja Banka Krvi", new DateTime(), "URGENT");


            modelBuilder.Entity<BloodUnit>().HasData(
                unit1, unit2,unit3, unit4
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
            RoomEvent roomEvent1 = new RoomEvent()
            {
                Id = Guid.NewGuid(),
                EventName = "SessionStarted",
                TimeStamp = DateTime.Now,
                UserId = manager.Id,
                Value = "null"
            };

            modelBuilder.Entity<RoomEvent>().HasData(roomEvent1);

        }
    }
}
