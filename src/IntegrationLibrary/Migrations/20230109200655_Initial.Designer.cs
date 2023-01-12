﻿// <auto-generated />
using System;
using System.Collections.Generic;
using IntegrationLibrary.BloodSubscription.Model;
using IntegrationLibrary.Settings;
using IntegrationLibrary.Tender.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace IntegrationLibrary.Migrations
{
    [DbContext(typeof(IntegrationDbContext))]
    [Migration("20230109200655_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("IntegrationLibrary.BloodBank.BloodBank", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("ServerAddress")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("BloodBanks");

                    b.HasData(
                        new
                        {
                            Id = new Guid("7c97663b-0d22-42f0-a173-ef827274cd43"),
                            Email = "aas@gmail.com",
                            Name = "BloodBank",
                            Password = "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy",
                            ServerAddress = "localhost"
                        });
                });

            modelBuilder.Entity("IntegrationLibrary.BloodRequests.Model.BloodRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<double>("Amount")
                        .HasColumnType("double precision");

                    b.Property<Guid>("BloodBankId")
                        .HasColumnType("uuid");

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("DoctorUsername")
                        .HasColumnType("text");

                    b.Property<string>("Reason")
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("BloodRequests");

                    b.HasData(
                        new
                        {
                            Id = new Guid("7c44b52a-6180-4e31-8edd-b7d6db360171"),
                            Amount = 10.0,
                            BloodBankId = new Guid("00000000-0000-0000-0000-000000000000"),
                            Comment = "",
                            Date = new DateTime(2022, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DoctorUsername = "Ilija",
                            Reason = "Operacija",
                            Status = 2,
                            Type = 5
                        },
                        new
                        {
                            Id = new Guid("a25a993e-f7b9-4066-8f47-ad2205d1c951"),
                            Amount = 20.0,
                            BloodBankId = new Guid("00000000-0000-0000-0000-000000000000"),
                            Comment = "",
                            Date = new DateTime(2022, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DoctorUsername = "Ilija",
                            Reason = "Transfuzija",
                            Status = 2,
                            Type = 3
                        },
                        new
                        {
                            Id = new Guid("17923af3-24f8-41b4-8c41-eeddc716425d"),
                            Amount = 20.0,
                            BloodBankId = new Guid("00000000-0000-0000-0000-000000000000"),
                            Comment = "",
                            Date = new DateTime(2023, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DoctorUsername = "Ilija",
                            Reason = "Transfuzija",
                            Status = 2,
                            Type = 0
                        },
                        new
                        {
                            Id = new Guid("a349df96-fe11-4a24-9ed7-9e91f8e18078"),
                            Amount = 5.0,
                            BloodBankId = new Guid("00000000-0000-0000-0000-000000000000"),
                            Comment = "",
                            Date = new DateTime(2023, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DoctorUsername = "Ilija",
                            Reason = "Zalihe",
                            Status = 2,
                            Type = 6
                        });
                });

            modelBuilder.Entity("IntegrationLibrary.BloodSubscription.Model.AmountOfBloodType", b =>
                {
                    b.Property<int>("amount")
                        .HasColumnType("integer");

                    b.Property<int>("bloodType")
                        .HasColumnType("integer");

                    b.ToTable("AmountOfBloodType");
                });

            modelBuilder.Entity("IntegrationLibrary.BloodSubscription.Model.MounthlyBloodSubscription", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<List<AmountOfBloodType>>("amountOfBloodTypes")
                        .HasColumnType("jsonb");

                    b.Property<Guid>("bloodBankId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("dateAndTimeOfSubscription")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("id");

                    b.ToTable("BloodSubscriptions");
                });

            modelBuilder.Entity("IntegrationLibrary.ConfigureGenerateAndSend.Model.ConfigureGenerateAndSend", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("BloodBankName")
                        .HasColumnType("text");

                    b.Property<string>("GeneratePeriod")
                        .HasColumnType("text");

                    b.Property<DateTime>("NextDateForSending")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("SendPeriod")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ConfigureGenerateAndSend");

                    b.HasData(
                        new
                        {
                            Id = new Guid("01eca578-9051-40b0-a9d3-adeb2a72641b"),
                            BloodBankName = "Moja Banka Krvi",
                            GeneratePeriod = "ONE_MONTH",
                            NextDateForSending = new DateTime(2023, 1, 9, 21, 6, 55, 196, DateTimeKind.Local).AddTicks(1052),
                            SendPeriod = "EVERY_TWO_MINUT"
                        },
                        new
                        {
                            Id = new Guid("2853063c-9d3b-4e9b-bf7c-5ae6f875a73d"),
                            BloodBankName = "Nova banka",
                            GeneratePeriod = "TWO_MONTH",
                            NextDateForSending = new DateTime(2023, 1, 9, 21, 6, 55, 205, DateTimeKind.Local).AddTicks(6954),
                            SendPeriod = "ONE_MONTH"
                        });
                });

            modelBuilder.Entity("IntegrationLibrary.NewsFromBloodBank.Model.NewsFromBloodBank", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("apiKey")
                        .HasColumnType("text");

                    b.Property<string>("base64image")
                        .HasColumnType("text");

                    b.Property<string>("bloodBankName")
                        .HasColumnType("text");

                    b.Property<string>("content")
                        .HasColumnType("text");

                    b.Property<int>("newsStatus")
                        .HasColumnType("integer");

                    b.Property<string>("title")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("NewsFromBloodBank");
                });

            modelBuilder.Entity("IntegrationLibrary.Tender.Model.BloodUnitAmount", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Amount")
                        .HasColumnType("integer");

                    b.Property<int>("BloodType")
                        .HasColumnType("integer");

                    b.Property<Guid>("TenderId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("TenderId");

                    b.ToTable("BloodUnitAmounts");

                    b.HasData(
                        new
                        {
                            Id = new Guid("88da38b4-6900-485e-8792-04c220d36135"),
                            Amount = 10,
                            BloodType = 0,
                            TenderId = new Guid("1ccfa7fe-1f34-44ce-a604-0da872b767df")
                        },
                        new
                        {
                            Id = new Guid("2a8ff205-f6e6-480d-bfdc-0914734c89b4"),
                            Amount = 0,
                            BloodType = 1,
                            TenderId = new Guid("1ccfa7fe-1f34-44ce-a604-0da872b767df")
                        },
                        new
                        {
                            Id = new Guid("f2b5ced3-3f0c-4b0f-8bb5-9ffe76c85702"),
                            Amount = 5,
                            BloodType = 2,
                            TenderId = new Guid("1ccfa7fe-1f34-44ce-a604-0da872b767df")
                        },
                        new
                        {
                            Id = new Guid("c056da1e-fef7-4ee9-8c7b-7ac45bce034f"),
                            Amount = 0,
                            BloodType = 3,
                            TenderId = new Guid("1ccfa7fe-1f34-44ce-a604-0da872b767df")
                        },
                        new
                        {
                            Id = new Guid("7febea96-1656-484c-9eaa-f6ef44cd59e1"),
                            Amount = 12,
                            BloodType = 4,
                            TenderId = new Guid("1ccfa7fe-1f34-44ce-a604-0da872b767df")
                        },
                        new
                        {
                            Id = new Guid("71cedd53-10ac-4290-b2a7-76749ce5d3a7"),
                            Amount = 7,
                            BloodType = 5,
                            TenderId = new Guid("1ccfa7fe-1f34-44ce-a604-0da872b767df")
                        },
                        new
                        {
                            Id = new Guid("4e349442-baf3-4db4-8014-de277a96516a"),
                            Amount = 10,
                            BloodType = 6,
                            TenderId = new Guid("1ccfa7fe-1f34-44ce-a604-0da872b767df")
                        },
                        new
                        {
                            Id = new Guid("d3ffea5b-86e2-4390-b358-32f6dae3c53c"),
                            Amount = 0,
                            BloodType = 7,
                            TenderId = new Guid("1ccfa7fe-1f34-44ce-a604-0da872b767df")
                        },
                        new
                        {
                            Id = new Guid("ec58d53c-bd76-4464-b075-82e6f6ff5b35"),
                            Amount = 7,
                            BloodType = 5,
                            TenderId = new Guid("1b053eea-4af1-4b49-ad6c-8f805d1cd252")
                        },
                        new
                        {
                            Id = new Guid("f61c854e-3724-4284-86f6-5da12e165ec8"),
                            Amount = 10,
                            BloodType = 6,
                            TenderId = new Guid("1b053eea-4af1-4b49-ad6c-8f805d1cd252")
                        },
                        new
                        {
                            Id = new Guid("1215f951-04a9-4567-be9e-3d7e4ddce9ff"),
                            Amount = 0,
                            BloodType = 7,
                            TenderId = new Guid("1b053eea-4af1-4b49-ad6c-8f805d1cd252")
                        });
                });

            modelBuilder.Entity("IntegrationLibrary.Tender.Model.Tender", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DeadlineDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("HasDeadline")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("PublishedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<IEnumerable<TenderOffer>>("TenderOffer")
                        .HasColumnType("jsonb");

                    b.Property<TenderOffer>("Winner")
                        .HasColumnType("jsonb");

                    b.HasKey("Id");

                    b.ToTable("Tenders");

                    b.HasData(
                        new
                        {
                            Id = new Guid("1ccfa7fe-1f34-44ce-a604-0da872b767df"),
                            DeadlineDate = new DateTime(2023, 1, 29, 21, 6, 55, 205, DateTimeKind.Local).AddTicks(8995),
                            HasDeadline = true,
                            PublishedDate = new DateTime(2023, 1, 9, 21, 6, 55, 205, DateTimeKind.Local).AddTicks(9404),
                            Status = 0
                        },
                        new
                        {
                            Id = new Guid("1b053eea-4af1-4b49-ad6c-8f805d1cd252"),
                            DeadlineDate = new DateTime(2023, 1, 8, 21, 6, 55, 206, DateTimeKind.Local).AddTicks(71),
                            HasDeadline = true,
                            PublishedDate = new DateTime(2023, 1, 5, 21, 6, 55, 206, DateTimeKind.Local).AddTicks(85),
                            Status = 2
                        });
                });

            modelBuilder.Entity("IntegrationLibrary.Tender.Model.TenderOffer", b =>
                {
                    b.Property<string>("BloodBankName")
                        .HasColumnType("text");

                    b.Property<double>("Price")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("RealizationDate")
                        .HasColumnType("timestamp without time zone");

                    b.ToTable("TenderOffer");
                });

            modelBuilder.Entity("IntegrationLibrary.BloodBank.BloodBank", b =>
                {
                    b.OwnsOne("IntegrationLibrary.BloodBank.Model.ApiKey", "ApiKey", b1 =>
                        {
                            b1.Property<Guid>("BloodBankId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Value")
                                .HasColumnType("text");

                            b1.HasKey("BloodBankId");

                            b1.ToTable("BloodBanks");

                            b1.WithOwner()
                                .HasForeignKey("BloodBankId");

                            b1.HasData(
                                new
                                {
                                    BloodBankId = new Guid("7c97663b-0d22-42f0-a173-ef827274cd43"),
                                    Value = "7Tel9EbXpCU7jOE72IhRaJ1TfyH8v1F2t69r1fiEx1Y="
                                });
                        });

                    b.Navigation("ApiKey");
                });

            modelBuilder.Entity("IntegrationLibrary.Tender.Model.BloodUnitAmount", b =>
                {
                    b.HasOne("IntegrationLibrary.Tender.Model.Tender", "Tender")
                        .WithMany("BloodUnitAmount")
                        .HasForeignKey("TenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tender");
                });

            modelBuilder.Entity("IntegrationLibrary.Tender.Model.Tender", b =>
                {
                    b.Navigation("BloodUnitAmount");
                });
#pragma warning restore 612, 618
        }
    }
}
