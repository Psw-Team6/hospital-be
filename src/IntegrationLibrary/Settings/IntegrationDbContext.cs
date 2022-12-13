using IntegrationLibrary.ConfigureGenerateAndSend.Model;
﻿using IntegrationLibrary.BloodRequests.Model;
using IntegrationLibrary.Tender.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using IntegrationLibrary.BloodBank;
using System.Collections.Generic;
using Amqp.Types;
using System.Linq;

namespace IntegrationLibrary.Settings
{
    public class IntegrationDbContext: DbContext
    {
        public DbSet<BloodBank.BloodBank> BloodBanks { get; set; }

        public DbSet<ConfigureGenerateAndSend.Model.ConfigureGenerateAndSend> ConfigureGenerateAndSend { get; set; }

        public DbSet<BloodRequest> BloodRequests { get; set; }


        public DbSet<BloodUnitAmount> BloodUnitAmounts { get; set; }

        public DbSet<NewsFromBloodBank.Model.NewsFromBloodBank> NewsFromBloodBank { get; set; }
        public DbSet<IntegrationLibrary.Tender.Model.Tender> Tenders { get; set; }

        public IntegrationDbContext(DbContextOptions<IntegrationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            
            modelBuilder.Entity<IntegrationLibrary.Tender.Model.Tender>().Property(d => d.TenderOffer).HasColumnType("jsonb");

            //Start data for Blood requests

            BloodRequest request1 = new()
            {
                Id = Guid.NewGuid(),
                Type = BloodType.ABneg,
                Amount = 10.0,
                Reason = "Operacija",
                Date = new DateTime(2022,12,10),
                DoctorUsername = "Ilija",
                Status = Status.PENDING,
                Comment = ""
            };

            BloodRequest request2 = new()
            {
                Id = Guid.NewGuid(),
                Type = BloodType.Bneg,
                Amount = 20.0,
                Reason = "Transfuzija",
                Date = new DateTime(2022, 12, 20),
                DoctorUsername = "Ilija",
                Status = Status.PENDING,
                Comment = ""
            };

            BloodRequest request3 = new()
            {
                Id = Guid.NewGuid(),
                Type = BloodType.Apos,
                Amount = 20.0,
                Reason = "Transfuzija",
                Date = new DateTime(2023, 1, 20),
                DoctorUsername = "Ilija",
                Status = Status.PENDING,
                Comment = ""
            };


            BloodRequest request4 = new()
            {
                Id = Guid.NewGuid(),
                Type = BloodType.Opos,
                Amount = 5.0,
                Reason = "Zalihe",
                Date = new DateTime(2023, 1, 20),
                DoctorUsername = "Ilija",
                Status = Status.PENDING,
                Comment = ""
            };

            modelBuilder.Entity<BloodRequest>().HasData(
                request1,
                request2,
                request3,
                request4
            );
            
            base.OnModelCreating(modelBuilder);
            BloodBank.BloodBank bloodBank = new()
            {
                Id = Guid.NewGuid(),
                Name = "BloodBank",
                ServerAddress = "localhost",
                Email = "aas@gmail.com",
                Password = "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy",
                ApiKey = "x"
            };
            modelBuilder.Entity<BloodBank.BloodBank>().HasData(bloodBank);



            ConfigureGenerateAndSend.Model.ConfigureGenerateAndSend configuration1 = new()
            {
                Id = Guid.NewGuid(),
                BloodBankName = "Moja Banka Krvi",
                GeneratePeriod = "ONE_MONTH",
                SendPeriod = "EVERY_TWO_MINUT",
                NextDateForSending = DateTime.Now,

            };
            ConfigureGenerateAndSend.Model.ConfigureGenerateAndSend configuration2 = new()
            {
                Id = Guid.NewGuid(),
                BloodBankName = "Nova banka",
                GeneratePeriod = "TWO_MONTH",
                SendPeriod = "ONE_MONTH",
                NextDateForSending = DateTime.Now,
            };

            modelBuilder.Entity<ConfigureGenerateAndSend.Model.ConfigureGenerateAndSend>().HasData(
               configuration1,
               configuration2
            );



            Tender.Model.Tender tender1 = new()
            {
                Id= Guid.NewGuid(),
                HasDeadline = true,
                DeadlineDate= DateTime.Now.AddDays(20),
                PublishedDate= DateTime.Now,
                Status=Enums.StatusTender.Open,
              //  TenderOffer = TenderOffer1,
            };
            modelBuilder.Entity<Tender.Model.Tender>().HasData(tender1);

            BloodUnitAmount bloodUnitAmount1 = new()
            {
                Id = Guid.NewGuid(),
                BloodType = BloodType.Apos,
                Amount = 10,
                TenderId = tender1.Id
            };
            BloodUnitAmount bloodUnitAmount2 = new()
            {
                Id = Guid.NewGuid(),
                BloodType = BloodType.Aneg,
                Amount = 0,
                TenderId = tender1.Id
            };
            BloodUnitAmount bloodUnitAmount3 = new()
            {
                Id = Guid.NewGuid(),
                BloodType = BloodType.Bpos,
                Amount = 5,
                TenderId = tender1.Id
            };
            BloodUnitAmount bloodUnitAmount4 = new()
            {
                Id = Guid.NewGuid(),
                BloodType = BloodType.Bneg,
                Amount = 0,
                TenderId = tender1.Id
            };
            BloodUnitAmount bloodUnitAmount5 = new()
            {
                Id = Guid.NewGuid(),
                BloodType = BloodType.ABpos,
                Amount = 12,
                TenderId = tender1.Id
            };
            BloodUnitAmount bloodUnitAmount6 = new()
            {
                Id = Guid.NewGuid(),
                BloodType = BloodType.ABneg,
                Amount = 7,
                TenderId = tender1.Id
            };
            BloodUnitAmount bloodUnitAmount7 = new()
            {
                Id = Guid.NewGuid(),
                BloodType = BloodType.Opos,
                Amount = 10,
                TenderId = tender1.Id
            };
            BloodUnitAmount bloodUnitAmount8 = new()
            {
                Id = Guid.NewGuid(),
                BloodType = BloodType.Oneg,
                Amount = 0,
                TenderId = tender1.Id
            };
            modelBuilder.Entity<BloodUnitAmount>().HasData(
                bloodUnitAmount1,
                bloodUnitAmount2,
                bloodUnitAmount3,
                bloodUnitAmount4,
                bloodUnitAmount5,
                bloodUnitAmount6,
                bloodUnitAmount7,
                bloodUnitAmount8);


            base.OnModelCreating(modelBuilder);

        }


    }
}
