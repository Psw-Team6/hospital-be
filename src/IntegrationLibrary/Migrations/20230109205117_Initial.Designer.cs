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
    [Migration("20230109205117_Initial")]
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
                            Id = new Guid("6fb13bbf-af28-4077-9e72-e22824171a19"),
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
                            Id = new Guid("4a085ac7-bb6d-444e-bde3-60c326827cb7"),
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
                            Id = new Guid("a90ba42f-0954-4c5d-8204-eb177576ad09"),
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
                            Id = new Guid("1b8cf9e6-e145-456d-b13a-3018542e6ddf"),
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
                            Id = new Guid("469639ec-8f7e-422b-aedc-7ce066c670cb"),
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
                            Id = new Guid("a3589015-a26f-4175-8290-599dc2f4b4ac"),
                            BloodBankName = "Moja Banka Krvi",
                            GeneratePeriod = "ONE_MONTH",
                            NextDateForSending = new DateTime(2023, 1, 9, 21, 51, 16, 536, DateTimeKind.Local).AddTicks(4684),
                            SendPeriod = "EVERY_TWO_MINUT"
                        },
                        new
                        {
                            Id = new Guid("2fb91d8d-4ba9-4c7a-8ab3-76c7c69c6691"),
                            BloodBankName = "Nova banka",
                            GeneratePeriod = "TWO_MONTH",
                            NextDateForSending = new DateTime(2023, 1, 9, 21, 51, 16, 544, DateTimeKind.Local).AddTicks(1481),
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
                            Id = new Guid("20e47265-e90a-4a80-b393-aa74583ef96e"),
                            Amount = 10,
                            BloodType = 0,
                            TenderId = new Guid("26825b36-f45d-42cb-9a76-0aa7a1dbc5f2")
                        },
                        new
                        {
                            Id = new Guid("42b5ab8e-7577-4c50-b6f5-c27c5b2ad387"),
                            Amount = 0,
                            BloodType = 1,
                            TenderId = new Guid("26825b36-f45d-42cb-9a76-0aa7a1dbc5f2")
                        },
                        new
                        {
                            Id = new Guid("2b5e879d-8551-42da-bdb4-b50a98a2f458"),
                            Amount = 5,
                            BloodType = 2,
                            TenderId = new Guid("26825b36-f45d-42cb-9a76-0aa7a1dbc5f2")
                        },
                        new
                        {
                            Id = new Guid("d8433979-1214-411d-8d9d-3b1e5c5cb696"),
                            Amount = 0,
                            BloodType = 3,
                            TenderId = new Guid("26825b36-f45d-42cb-9a76-0aa7a1dbc5f2")
                        },
                        new
                        {
                            Id = new Guid("17ea4438-fb9b-4de6-ae8e-0d939fba777e"),
                            Amount = 12,
                            BloodType = 4,
                            TenderId = new Guid("26825b36-f45d-42cb-9a76-0aa7a1dbc5f2")
                        },
                        new
                        {
                            Id = new Guid("877e5c7c-e504-43af-9a35-dccf1893d41c"),
                            Amount = 7,
                            BloodType = 5,
                            TenderId = new Guid("26825b36-f45d-42cb-9a76-0aa7a1dbc5f2")
                        },
                        new
                        {
                            Id = new Guid("fdd8e51e-b948-42e5-9796-0178c889da42"),
                            Amount = 10,
                            BloodType = 6,
                            TenderId = new Guid("26825b36-f45d-42cb-9a76-0aa7a1dbc5f2")
                        },
                        new
                        {
                            Id = new Guid("06ed1ac6-9cca-494b-8d87-ce9b123412c2"),
                            Amount = 0,
                            BloodType = 7,
                            TenderId = new Guid("26825b36-f45d-42cb-9a76-0aa7a1dbc5f2")
                        },
                        new
                        {
                            Id = new Guid("7db49e56-b7e2-4c5d-a629-9b32e94e28a7"),
                            Amount = 7,
                            BloodType = 5,
                            TenderId = new Guid("822bda4c-40c0-4833-9f07-7397cc7627dd")
                        },
                        new
                        {
                            Id = new Guid("63109763-6c86-4076-92f0-50e96048faf6"),
                            Amount = 10,
                            BloodType = 6,
                            TenderId = new Guid("822bda4c-40c0-4833-9f07-7397cc7627dd")
                        },
                        new
                        {
                            Id = new Guid("2f24c7d4-2f6c-473c-8fb8-41ac83a7dadc"),
                            Amount = 0,
                            BloodType = 7,
                            TenderId = new Guid("822bda4c-40c0-4833-9f07-7397cc7627dd")
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
                            Id = new Guid("26825b36-f45d-42cb-9a76-0aa7a1dbc5f2"),
                            DeadlineDate = new DateTime(2023, 1, 29, 21, 51, 16, 544, DateTimeKind.Local).AddTicks(3629),
                            HasDeadline = true,
                            PublishedDate = new DateTime(2023, 1, 9, 21, 51, 16, 544, DateTimeKind.Local).AddTicks(4067),
                            Status = 0
                        },
                        new
                        {
                            Id = new Guid("822bda4c-40c0-4833-9f07-7397cc7627dd"),
                            DeadlineDate = new DateTime(2023, 1, 8, 21, 51, 16, 544, DateTimeKind.Local).AddTicks(4793),
                            HasDeadline = true,
                            PublishedDate = new DateTime(2023, 1, 5, 21, 51, 16, 544, DateTimeKind.Local).AddTicks(4807),
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
                                    BloodBankId = new Guid("6fb13bbf-af28-4077-9e72-e22824171a19"),
                                    Value = "MpXdApexOYVj7lTMXjALZkgjZ3o0XAyYcmV3sGrhFZM="
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