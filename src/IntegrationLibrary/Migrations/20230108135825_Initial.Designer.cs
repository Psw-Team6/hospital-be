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
    [Migration("20230108135825_Initial")]
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
                            Id = new Guid("e8ed024f-0e2f-4883-8bf3-2d3e515596e9"),
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
                            Id = new Guid("46a36b71-db40-40c4-9d46-66d5edca6418"),
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
                            Id = new Guid("cf9f249d-6586-41a7-acf3-0dc58b9a46dc"),
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
                            Id = new Guid("6fd98c90-b4e4-4077-abea-849b03a8cbe4"),
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
                            Id = new Guid("f0533378-620c-4730-9db6-436e50e5cbff"),
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
                            Id = new Guid("a5b80462-03ac-47cc-be95-8a401192e2dc"),
                            BloodBankName = "Moja Banka Krvi",
                            GeneratePeriod = "ONE_MONTH",
                            NextDateForSending = new DateTime(2023, 1, 8, 14, 58, 25, 105, DateTimeKind.Local).AddTicks(6222),
                            SendPeriod = "EVERY_TWO_MINUT"
                        },
                        new
                        {
                            Id = new Guid("b3560622-7f39-4624-9b90-8bfa79514469"),
                            BloodBankName = "Nova banka",
                            GeneratePeriod = "TWO_MONTH",
                            NextDateForSending = new DateTime(2023, 1, 8, 14, 58, 25, 114, DateTimeKind.Local).AddTicks(5101),
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
                            Id = new Guid("9db291fb-82c4-4835-8558-a57a58129221"),
                            Amount = 10,
                            BloodType = 0,
                            TenderId = new Guid("0b429c31-1293-40fc-8679-0faf03dc53b5")
                        },
                        new
                        {
                            Id = new Guid("13adfde8-1f87-404c-ab90-b945ce394a3e"),
                            Amount = 0,
                            BloodType = 1,
                            TenderId = new Guid("0b429c31-1293-40fc-8679-0faf03dc53b5")
                        },
                        new
                        {
                            Id = new Guid("e7298f9c-566f-4b54-9c3f-71f1ba117a40"),
                            Amount = 5,
                            BloodType = 2,
                            TenderId = new Guid("0b429c31-1293-40fc-8679-0faf03dc53b5")
                        },
                        new
                        {
                            Id = new Guid("9189737f-2667-4dfc-b6f5-853f8a049318"),
                            Amount = 0,
                            BloodType = 3,
                            TenderId = new Guid("0b429c31-1293-40fc-8679-0faf03dc53b5")
                        },
                        new
                        {
                            Id = new Guid("b54cafb4-fcde-43f1-8d25-7fadc4ec28da"),
                            Amount = 12,
                            BloodType = 4,
                            TenderId = new Guid("0b429c31-1293-40fc-8679-0faf03dc53b5")
                        },
                        new
                        {
                            Id = new Guid("b0574c8c-60bc-4974-ab51-4f91511f917b"),
                            Amount = 7,
                            BloodType = 5,
                            TenderId = new Guid("0b429c31-1293-40fc-8679-0faf03dc53b5")
                        },
                        new
                        {
                            Id = new Guid("078ad887-fc2b-458b-859f-fa826938d3f3"),
                            Amount = 10,
                            BloodType = 6,
                            TenderId = new Guid("0b429c31-1293-40fc-8679-0faf03dc53b5")
                        },
                        new
                        {
                            Id = new Guid("4bb01b4c-45d7-470d-9fdd-d48c278cce27"),
                            Amount = 0,
                            BloodType = 7,
                            TenderId = new Guid("0b429c31-1293-40fc-8679-0faf03dc53b5")
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
                            Id = new Guid("0b429c31-1293-40fc-8679-0faf03dc53b5"),
                            DeadlineDate = new DateTime(2023, 1, 28, 14, 58, 25, 114, DateTimeKind.Local).AddTicks(7087),
                            HasDeadline = true,
                            PublishedDate = new DateTime(2023, 1, 8, 14, 58, 25, 114, DateTimeKind.Local).AddTicks(7547),
                            Status = 0
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
                                    BloodBankId = new Guid("e8ed024f-0e2f-4883-8bf3-2d3e515596e9"),
                                    Value = "ifhOKBcx7QlkZtib+XwpVGMKsS4kjo/BPOTS26++QsQ="
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