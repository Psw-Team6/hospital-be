﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalLibrary.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    City = table.Column<string>(type: "text", nullable: true),
                    StreetNumber = table.Column<string>(type: "text", nullable: true),
                    Country = table.Column<string>(type: "text", nullable: true),
                    Street = table.Column<string>(type: "text", nullable: true),
                    Postcode = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BloodUnits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BloodType = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    BloodBankName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodUnits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Buildings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buildings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GRooms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PositionX = table.Column<int>(type: "integer", nullable: false),
                    PositionY = table.Column<int>(type: "integer", nullable: false),
                    Lenght = table.Column<int>(type: "integer", nullable: false),
                    Width = table.Column<int>(type: "integer", nullable: false),
                    RoomId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GRooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ingredient",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredient", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Specializations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specializations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkingSchedules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ExpirationFrom = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ExpirationTo = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DayOfWorkFrom = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DayOfWorkTo = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingSchedules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: true),
                    AddressId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Surname = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Jmbg = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    UserRole = table.Column<int>(type: "integer", nullable: false),
                    Enabled = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationUsers_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BloodConsumptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BloodUnitId = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Purpose = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodConsumptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BloodConsumptions_BloodUnits_BloodUnitId",
                        column: x => x.BloodUnitId,
                        principalTable: "BloodUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Floors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FloorNumber = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    BuildingId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Floors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Floors_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Buildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Managers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Managers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Managers_ApplicationUsers_Id",
                        column: x => x.Id,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Gender = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patients_ApplicationUsers_Id",
                        column: x => x.Id,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    FloorId = table.Column<Guid>(type: "uuid", nullable: false),
                    BuildingId = table.Column<Guid>(type: "uuid", nullable: false),
                    GRoomId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rooms_Floors_FloorId",
                        column: x => x.FloorId,
                        principalTable: "Floors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Feedback",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    patientId = table.Column<Guid>(type: "uuid", nullable: false),
                    date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    text = table.Column<string>(type: "text", nullable: true),
                    isAnonymous = table.Column<bool>(type: "boolean", nullable: false),
                    isPublic = table.Column<bool>(type: "boolean", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedback", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feedback_Patients_patientId",
                        column: x => x.patientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IngredientPatient",
                columns: table => new
                {
                    AllergiesId = table.Column<Guid>(type: "uuid", nullable: false),
                    PatientsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientPatient", x => new { x.AllergiesId, x.PatientsId });
                    table.ForeignKey(
                        name: "FK_IngredientPatient_Ingredient_AllergiesId",
                        column: x => x.AllergiesId,
                        principalTable: "Ingredient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientPatient_Patients_PatientsId",
                        column: x => x.PatientsId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TreatmentReport",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PatientId = table.Column<Guid>(type: "uuid", nullable: false),
                    DateRange_From = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DateRange_To = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ReasonOfHospitalization = table.Column<string>(type: "text", nullable: true),
                    ReasonOfDischarge = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreatmentReport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TreatmentReport_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SpecializationId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoomId = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkingScheduleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doctors_ApplicationUsers_Id",
                        column: x => x.Id,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Doctors_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Doctors_Specializations_SpecializationId",
                        column: x => x.SpecializationId,
                        principalTable: "Specializations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Doctors_WorkingSchedules_WorkingScheduleId",
                        column: x => x.WorkingScheduleId,
                        principalTable: "WorkingSchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoomBeds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IsFree = table.Column<bool>(type: "boolean", nullable: false),
                    Number = table.Column<string>(type: "text", nullable: true),
                    RoomId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomBeds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomBeds_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoomEquipment",
                columns: table => new
                {
                    RoomEquipmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    EquipmentName = table.Column<string>(type: "text", nullable: true),
                    RoomId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomEquipment", x => x.RoomEquipmentId);
                    table.ForeignKey(
                        name: "FK_RoomEquipment_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BloodPrescription",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PatientId = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    TreatmentReportId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodPrescription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BloodPrescription_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BloodPrescription_TreatmentReport_TreatmentReportId",
                        column: x => x.TreatmentReportId,
                        principalTable: "TreatmentReport",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicinePrescription",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MedicineId = table.Column<Guid>(type: "uuid", nullable: false),
                    PatientId = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    TreatmentReportId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicinePrescription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicinePrescription_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicinePrescription_TreatmentReport_TreatmentReportId",
                        column: x => x.TreatmentReportId,
                        principalTable: "TreatmentReport",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Emergent = table.Column<bool>(type: "boolean", nullable: false),
                    Duration_From = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Duration_To = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    PatientId = table.Column<Guid>(type: "uuid", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uuid", nullable: false),
                    AppointmentType = table.Column<int>(type: "integer", nullable: false),
                    AppointmentState = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Holidays",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uuid", nullable: false),
                    DateRange_From = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DateRange_To = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    IsUrgent = table.Column<bool>(type: "boolean", nullable: false),
                    HolidayStatus = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holidays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Holidays_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientAdmissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DateOfAdmission = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    PatientId = table.Column<Guid>(type: "uuid", nullable: false),
                    SelectedBedId = table.Column<Guid>(type: "uuid", nullable: false),
                    SelectedRoomId = table.Column<Guid>(type: "uuid", nullable: false),
                    Reason = table.Column<string>(type: "text", nullable: true),
                    DateOfDischarge = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientAdmissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientAdmissions_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientAdmissions_RoomBeds_SelectedBedId",
                        column: x => x.SelectedBedId,
                        principalTable: "RoomBeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientAdmissions_Rooms_SelectedRoomId",
                        column: x => x.SelectedRoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Medicine",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    MedicinePrescriptionId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medicine_MedicinePrescription_MedicinePrescriptionId",
                        column: x => x.MedicinePrescriptionId,
                        principalTable: "MedicinePrescription",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IngredientMedicine",
                columns: table => new
                {
                    IngredientsId = table.Column<Guid>(type: "uuid", nullable: false),
                    MedicinesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientMedicine", x => new { x.IngredientsId, x.MedicinesId });
                    table.ForeignKey(
                        name: "FK_IngredientMedicine_Ingredient_IngredientsId",
                        column: x => x.IngredientsId,
                        principalTable: "Ingredient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientMedicine_Medicine_MedicinesId",
                        column: x => x.MedicinesId,
                        principalTable: "Medicine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "City", "Country", "Postcode", "Street", "StreetNumber" },
                values: new object[,]
                {
                    { new Guid("fbf157ec-f2bc-4c9e-a975-5ad652d98bd1"), "Novi Sad", "Serbia", 21000, "JNA", "33" },
                    { new Guid("efff96ac-a65a-4438-9ac7-f2054ae39da4"), "Novi Sad", "Serbia", 21000, "Kosovska", "23A" },
                    { new Guid("af07eba6-a31b-401d-9c9c-41cc195611af"), "Novi Sad", "Serbia", 21000, "Partizanska", "33" }
                });

            migrationBuilder.InsertData(
                table: "BloodUnits",
                columns: new[] { "Id", "Amount", "BloodBankName", "BloodType" },
                values: new object[,]
                {
                    { new Guid("1eeeeb46-685b-44d6-8da2-3728f4ded7fe"), 7, "Moja Banka Krvi", 0 },
                    { new Guid("a07cc82d-3b57-43af-939f-51c6f8e4ebb9"), 10, "Moja Banka Krvi", 7 },
                    { new Guid("ef2fe890-ad75-4013-bf9a-dbd6fab618e9"), 4, "Moja Banka Krvi", 0 }
                });

            migrationBuilder.InsertData(
                table: "Buildings",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("4dad6b40-ae4c-4556-acd1-ef591abada2d"), "Stara bolnica" },
                    { new Guid("7463e990-2d5b-4736-a280-edd8055ffe4d"), "Nova bolnica" }
                });

            migrationBuilder.InsertData(
                table: "GRooms",
                columns: new[] { "Id", "Lenght", "PositionX", "PositionY", "RoomId", "Width" },
                values: new object[,]
                {
                    { new Guid("cd23b907-7463-4102-8ee5-71e81b07e5b4"), 5, 5, 0, new Guid("07c9f772-b5d8-4702-a982-6bb1db9f50c0"), 5 },
                    { new Guid("b6e26878-8630-427e-b79c-b4377a845849"), 5, 5, 0, new Guid("0afadb4e-6012-4bef-bac9-7ed9c544cfe6"), 5 },
                    { new Guid("e4ac2a8a-a74c-4534-9d75-880538e8cf43"), 5, 5, 0, new Guid("f1be28ef-3feb-4a3d-babd-6a82da07a6e3"), 5 },
                    { new Guid("1ce1d9ee-8336-406f-85bd-03bd90c3c7ba"), 5, 5, 0, new Guid("8301dbac-f44f-4afc-a80b-954adf024856"), 5 },
                    { new Guid("97bffefc-e5f4-4ea6-8d5c-621ac084e40d"), 5, 5, 0, new Guid("bdc4acf9-3bc1-4d2f-a62d-f9a0788ba8a3"), 5 },
                    { new Guid("1cec57b2-08cc-4243-ba15-4cb34412641d"), 5, 5, 0, new Guid("7b2b3105-25cc-4a86-ab10-4c9fe386937e"), 5 },
                    { new Guid("ef62ba48-5611-493d-9e68-8e46595bf285"), 5, 0, 0, new Guid("2e584efe-7e15-4dee-84ad-6c5665c0f780"), 5 },
                    { new Guid("e901867e-5f97-4a40-a3e1-d949d8e83906"), 5, 5, 0, new Guid("da3fa05a-612b-4e87-92aa-0e24b8af987f"), 5 },
                    { new Guid("187392d3-1ab8-4ce3-a523-02d1c32a6671"), 5, 5, 0, new Guid("cfe67bb5-636c-4c67-90db-8bd4c4ddb1a4"), 5 }
                });

            migrationBuilder.InsertData(
                table: "Specializations",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0b259ebb-b69f-4333-9748-03ac110f4796"), "Surgeon" },
                    { new Guid("a1a8f9af-d4b7-4e0e-a000-99147da2aba6"), "Dermatology" },
                    { new Guid("c0dcd73f-df41-451c-9df8-ab0bef392f5c"), "General" }
                });

            migrationBuilder.InsertData(
                table: "WorkingSchedules",
                columns: new[] { "Id", "DayOfWorkFrom", "DayOfWorkTo", "ExpirationFrom", "ExpirationTo" },
                values: new object[,]
                {
                    { new Guid("f4397a3b-c0fc-45b9-9837-44e64bb4074b"), new DateTime(2022, 10, 27, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 27, 22, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("a8cbddd7-2829-4ef9-bf74-eee6822a6ccf"), new DateTime(2022, 10, 27, 8, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 12, 27, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "ApplicationUsers",
                columns: new[] { "Id", "AddressId", "Email", "Enabled", "Jmbg", "Name", "Password", "Phone", "Surname", "UserRole", "Username" },
                values: new object[,]
                {
                    { new Guid("e8a497f6-4426-40d6-bc16-5cb3b2118095"), new Guid("efff96ac-a65a-4438-9ac7-f2054ae39da4"), "Cajons@gmail.com", true, "99999999", "Ilija", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "+612222222", "Maric", 0, "Ilija" },
                    { new Guid("ba1c63ea-a609-4815-8817-7385418ab048"), new Guid("efff96ac-a65a-4438-9ac7-f2054ae39da4"), "psw.isa.mail@gmail.com", true, "99999999", "Sale", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "+612222222", "Lave", 2, "Sale" },
                    { new Guid("b30c53d4-dc2d-441f-8c8c-58a9cb2f7527"), new Guid("af07eba6-a31b-401d-9c9c-41cc195611af"), "DjordjeLopov@gmail.com", true, "99999999", "Djordje", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "+612222222", "Vuckovic", 0, "Tadjo" },
                    { new Guid("699fd0e8-ba02-4e04-a628-30cf5b2be97e"), new Guid("fbf157ec-f2bc-4c9e-a975-5ad652d98bd1"), "psw.isa.mail@gmail.com", true, "99999999", "Manager", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "+612222222", "Manger", 1, "Manager" },
                    { new Guid("4f228a22-3961-43ea-bdd1-3dfedbb65c21"), new Guid("fbf157ec-f2bc-4c9e-a975-5ad652d98bd1"), "psw.isa.mail@gmail.com", true, "99999999", "Miki", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "+612222222", "Djuricic", 2, "Miki" }
                });

            migrationBuilder.InsertData(
                table: "BloodConsumptions",
                columns: new[] { "Id", "Amount", "BloodUnitId", "Date", "DoctorId", "Purpose" },
                values: new object[,]
                {
                    { new Guid("b71460cf-71ba-40f6-aaa6-2c7428e1852f"), 2, new Guid("1eeeeb46-685b-44d6-8da2-3728f4ded7fe"), new DateTime(2022, 10, 27, 15, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b30c53d4-dc2d-441f-8c8c-58a9cb2f7527"), "operation" },
                    { new Guid("8ce7974c-9aa0-4f92-ad7c-0f5924ecc292"), 4, new Guid("1eeeeb46-685b-44d6-8da2-3728f4ded7fe"), new DateTime(2022, 11, 14, 15, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b30c53d4-dc2d-441f-8c8c-58a9cb2f7527"), "operation" }
                });

            migrationBuilder.InsertData(
                table: "Floors",
                columns: new[] { "Id", "BuildingId", "FloorNumber", "Name" },
                values: new object[,]
                {
                    { new Guid("e5ebaa22-5c75-4103-bf09-652fbb0a93e7"), new Guid("4dad6b40-ae4c-4556-acd1-ef591abada2d"), 0, "F0" },
                    { new Guid("881f3216-3dfb-4287-829c-83d46a4fe96f"), new Guid("4dad6b40-ae4c-4556-acd1-ef591abada2d"), 1, "F1" },
                    { new Guid("61b3ca26-6df4-49df-a959-71837d247b6f"), new Guid("4dad6b40-ae4c-4556-acd1-ef591abada2d"), 2, "F2" },
                    { new Guid("2cb41b8f-89af-4202-a244-eeaa1e585ec7"), new Guid("7463e990-2d5b-4736-a280-edd8055ffe4d"), 0, "F0" },
                    { new Guid("b9e0aa22-b60a-4fef-be7f-5ba80a6129dc"), new Guid("7463e990-2d5b-4736-a280-edd8055ffe4d"), 1, "F1" },
                    { new Guid("36bda2f7-1b57-4f2d-8227-6de2a99a954c"), new Guid("7463e990-2d5b-4736-a280-edd8055ffe4d"), 2, "F2" }
                });

            migrationBuilder.InsertData(
                table: "Managers",
                column: "Id",
                value: new Guid("699fd0e8-ba02-4e04-a628-30cf5b2be97e"));

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "Gender" },
                values: new object[,]
                {
                    { new Guid("ba1c63ea-a609-4815-8817-7385418ab048"), 0 },
                    { new Guid("4f228a22-3961-43ea-bdd1-3dfedbb65c21"), 0 }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "BuildingId", "FloorId", "GRoomId", "Name" },
                values: new object[,]
                {
                    { new Guid("2e584efe-7e15-4dee-84ad-6c5665c0f780"), new Guid("4dad6b40-ae4c-4556-acd1-ef591abada2d"), new Guid("e5ebaa22-5c75-4103-bf09-652fbb0a93e7"), new Guid("ef62ba48-5611-493d-9e68-8e46595bf285"), "A11" },
                    { new Guid("da3fa05a-612b-4e87-92aa-0e24b8af987f"), new Guid("4dad6b40-ae4c-4556-acd1-ef591abada2d"), new Guid("e5ebaa22-5c75-4103-bf09-652fbb0a93e7"), new Guid("e901867e-5f97-4a40-a3e1-d949d8e83906"), "B11" },
                    { new Guid("07c9f772-b5d8-4702-a982-6bb1db9f50c0"), new Guid("4dad6b40-ae4c-4556-acd1-ef591abada2d"), new Guid("881f3216-3dfb-4287-829c-83d46a4fe96f"), new Guid("cd23b907-7463-4102-8ee5-71e81b07e5b4"), "A12" },
                    { new Guid("cfe67bb5-636c-4c67-90db-8bd4c4ddb1a4"), new Guid("4dad6b40-ae4c-4556-acd1-ef591abada2d"), new Guid("61b3ca26-6df4-49df-a959-71837d247b6f"), new Guid("187392d3-1ab8-4ce3-a523-02d1c32a6671"), "A13" },
                    { new Guid("7b2b3105-25cc-4a86-ab10-4c9fe386937e"), new Guid("7463e990-2d5b-4736-a280-edd8055ffe4d"), new Guid("2cb41b8f-89af-4202-a244-eeaa1e585ec7"), new Guid("1cec57b2-08cc-4243-ba15-4cb34412641d"), "A21" },
                    { new Guid("bdc4acf9-3bc1-4d2f-a62d-f9a0788ba8a3"), new Guid("7463e990-2d5b-4736-a280-edd8055ffe4d"), new Guid("2cb41b8f-89af-4202-a244-eeaa1e585ec7"), new Guid("97bffefc-e5f4-4ea6-8d5c-621ac084e40d"), "B21" },
                    { new Guid("8301dbac-f44f-4afc-a80b-954adf024856"), new Guid("7463e990-2d5b-4736-a280-edd8055ffe4d"), new Guid("b9e0aa22-b60a-4fef-be7f-5ba80a6129dc"), new Guid("1ce1d9ee-8336-406f-85bd-03bd90c3c7ba"), "A22" },
                    { new Guid("f1be28ef-3feb-4a3d-babd-6a82da07a6e3"), new Guid("7463e990-2d5b-4736-a280-edd8055ffe4d"), new Guid("36bda2f7-1b57-4f2d-8227-6de2a99a954c"), new Guid("e4ac2a8a-a74c-4534-9d75-880538e8cf43"), "C23" },
                    { new Guid("0afadb4e-6012-4bef-bac9-7ed9c544cfe6"), new Guid("7463e990-2d5b-4736-a280-edd8055ffe4d"), new Guid("36bda2f7-1b57-4f2d-8227-6de2a99a954c"), new Guid("b6e26878-8630-427e-b79c-b4377a845849"), "B23" }
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "RoomId", "SpecializationId", "WorkingScheduleId" },
                values: new object[,]
                {
                    { new Guid("e8a497f6-4426-40d6-bc16-5cb3b2118095"), new Guid("2e584efe-7e15-4dee-84ad-6c5665c0f780"), new Guid("a1a8f9af-d4b7-4e0e-a000-99147da2aba6"), new Guid("a8cbddd7-2829-4ef9-bf74-eee6822a6ccf") },
                    { new Guid("b30c53d4-dc2d-441f-8c8c-58a9cb2f7527"), new Guid("da3fa05a-612b-4e87-92aa-0e24b8af987f"), new Guid("a1a8f9af-d4b7-4e0e-a000-99147da2aba6"), new Guid("a8cbddd7-2829-4ef9-bf74-eee6822a6ccf") }
                });

            migrationBuilder.InsertData(
                table: "RoomBeds",
                columns: new[] { "Id", "IsFree", "Number", "RoomId" },
                values: new object[,]
                {
                    { new Guid("21c45016-91b5-49b2-8b2e-8fc4c3d0bf13"), true, "12A4", new Guid("da3fa05a-612b-4e87-92aa-0e24b8af987f") },
                    { new Guid("0031d6c6-58e0-4946-8600-d6b6bb208159"), true, "12A3", new Guid("da3fa05a-612b-4e87-92aa-0e24b8af987f") },
                    { new Guid("8e6a285b-8ee3-4fd9-aa26-0feaed09acd4"), true, "12A2", new Guid("da3fa05a-612b-4e87-92aa-0e24b8af987f") },
                    { new Guid("bf031e93-898f-42d8-a893-0ce7a555d261"), true, "12A1", new Guid("da3fa05a-612b-4e87-92aa-0e24b8af987f") },
                    { new Guid("dc572b8a-fb4f-4166-8cc4-6b25dafc6d68"), true, "12A5", new Guid("da3fa05a-612b-4e87-92aa-0e24b8af987f") },
                    { new Guid("ef224e2f-ef69-47dd-bf5e-066489b683c8"), true, "11A4", new Guid("2e584efe-7e15-4dee-84ad-6c5665c0f780") },
                    { new Guid("6e98621e-d7b4-40a1-bed6-caa3356ebfc7"), true, "11A3", new Guid("2e584efe-7e15-4dee-84ad-6c5665c0f780") },
                    { new Guid("4189af5f-7ae6-4484-a4a2-cb19aa13fc96"), true, "11A2", new Guid("2e584efe-7e15-4dee-84ad-6c5665c0f780") },
                    { new Guid("d43e162c-5117-423e-bc50-925715b91523"), true, "11A1", new Guid("2e584efe-7e15-4dee-84ad-6c5665c0f780") }
                });

            migrationBuilder.InsertData(
                table: "RoomEquipment",
                columns: new[] { "RoomEquipmentId", "Amount", "EquipmentName", "RoomId" },
                values: new object[,]
                {
                    { new Guid("046eb19d-c9f9-47ff-b154-985f5978da73"), 15, "SURGICAL_TABLES", new Guid("2e584efe-7e15-4dee-84ad-6c5665c0f780") },
                    { new Guid("4ccefbb4-db8c-4f37-8210-009ac2f49ee4"), 3, "ANESTHESIA", new Guid("f1be28ef-3feb-4a3d-babd-6a82da07a6e3") },
                    { new Guid("4ca35b8d-b082-43cc-b224-6f5cce015dcd"), 3, "SYRINGE", new Guid("8301dbac-f44f-4afc-a80b-954adf024856") },
                    { new Guid("3e4edf31-0a7c-4387-a4d6-b1116c1c1954"), 6, "BANDAGE", new Guid("8301dbac-f44f-4afc-a80b-954adf024856") },
                    { new Guid("32d4531b-1c86-4081-9819-2c813a2318db"), 4, "SURGICAL_TABLES", new Guid("bdc4acf9-3bc1-4d2f-a62d-f9a0788ba8a3") },
                    { new Guid("616ce03f-acec-4fba-a86c-c95146e48c37"), 2, "EKG_MACHINE", new Guid("7b2b3105-25cc-4a86-ab10-4c9fe386937e") },
                    { new Guid("91d69c27-ba66-490a-bd3e-64c37af428cb"), 11, "SURGICAL_TABLES", new Guid("cfe67bb5-636c-4c67-90db-8bd4c4ddb1a4") },
                    { new Guid("1e5c3c01-8cce-4c42-89ab-e488d5a49122"), 3, "EKG_MACHINE", new Guid("07c9f772-b5d8-4702-a982-6bb1db9f50c0") },
                    { new Guid("2c2f1909-7a35-4511-8f02-5d7769288de3"), 3, "ANESTHESIA", new Guid("2e584efe-7e15-4dee-84ad-6c5665c0f780") },
                    { new Guid("52dac854-6736-4b78-bd42-646206ce2fbd"), 3, "EKG_MACHINE", new Guid("da3fa05a-612b-4e87-92aa-0e24b8af987f") },
                    { new Guid("7add0278-aa64-4047-a884-dac3dea3158d"), 5, "SURGICAL_TABLES", new Guid("da3fa05a-612b-4e87-92aa-0e24b8af987f") },
                    { new Guid("d1df20c9-0b38-4c77-b241-0219a914dc48"), 10, "ANESTHESIA", new Guid("da3fa05a-612b-4e87-92aa-0e24b8af987f") },
                    { new Guid("84c2a6d0-e35a-453e-a734-ccd943db7f5f"), 9, "SURGICAL_TABLES", new Guid("0afadb4e-6012-4bef-bac9-7ed9c544cfe6") },
                    { new Guid("7e72c473-1804-49b8-ad63-c1152deadb4c"), 1, "ANESTHESIA", new Guid("cfe67bb5-636c-4c67-90db-8bd4c4ddb1a4") },
                    { new Guid("c47d0f64-a417-47d3-9912-c893c74f63e8"), 9, "SYRINGE", new Guid("0afadb4e-6012-4bef-bac9-7ed9c544cfe6") }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "AppointmentState", "AppointmentType", "DoctorId", "Emergent", "PatientId", "Duration_From", "Duration_To" },
                values: new object[] { new Guid("277d8564-f047-4f20-b8c7-d18584bc555c"), 0, 0, new Guid("e8a497f6-4426-40d6-bc16-5cb3b2118095"), false, new Guid("ba1c63ea-a609-4815-8817-7385418ab048"), new DateTime(2023, 7, 27, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 27, 10, 30, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Holidays",
                columns: new[] { "Id", "Description", "DoctorId", "HolidayStatus", "IsUrgent", "DateRange_From", "DateRange_To" },
                values: new object[] { new Guid("91951219-5b9b-453a-bae3-44e2306e06bf"), "I want to go to Paralia", new Guid("e8a497f6-4426-40d6-bc16-5cb3b2118095"), 0, false, new DateTime(2022, 10, 27, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 27, 10, 30, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsers_AddressId",
                table: "ApplicationUsers",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsers_Username",
                table: "ApplicationUsers",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorId",
                table: "Appointments",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_BloodConsumptions_BloodUnitId",
                table: "BloodConsumptions",
                column: "BloodUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_BloodPrescription_PatientId",
                table: "BloodPrescription",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_BloodPrescription_TreatmentReportId",
                table: "BloodPrescription",
                column: "TreatmentReportId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_RoomId",
                table: "Doctors",
                column: "RoomId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_SpecializationId",
                table: "Doctors",
                column: "SpecializationId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_WorkingScheduleId",
                table: "Doctors",
                column: "WorkingScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_patientId",
                table: "Feedback",
                column: "patientId");

            migrationBuilder.CreateIndex(
                name: "IX_Floors_BuildingId",
                table: "Floors",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_Holidays_DoctorId",
                table: "Holidays",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientMedicine_MedicinesId",
                table: "IngredientMedicine",
                column: "MedicinesId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientPatient_PatientsId",
                table: "IngredientPatient",
                column: "PatientsId");

            migrationBuilder.CreateIndex(
                name: "IX_Medicine_MedicinePrescriptionId",
                table: "Medicine",
                column: "MedicinePrescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicinePrescription_PatientId",
                table: "MedicinePrescription",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicinePrescription_TreatmentReportId",
                table: "MedicinePrescription",
                column: "TreatmentReportId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientAdmissions_PatientId",
                table: "PatientAdmissions",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientAdmissions_SelectedBedId",
                table: "PatientAdmissions",
                column: "SelectedBedId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientAdmissions_SelectedRoomId",
                table: "PatientAdmissions",
                column: "SelectedRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomBeds_RoomId",
                table: "RoomBeds",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomEquipment_RoomId",
                table: "RoomEquipment",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_FloorId",
                table: "Rooms",
                column: "FloorId");

            migrationBuilder.CreateIndex(
                name: "IX_Specializations_Name",
                table: "Specializations",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentReport_PatientId",
                table: "TreatmentReport",
                column: "PatientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "BloodConsumptions");

            migrationBuilder.DropTable(
                name: "BloodPrescription");

            migrationBuilder.DropTable(
                name: "Feedback");

            migrationBuilder.DropTable(
                name: "GRooms");

            migrationBuilder.DropTable(
                name: "Holidays");

            migrationBuilder.DropTable(
                name: "IngredientMedicine");

            migrationBuilder.DropTable(
                name: "IngredientPatient");

            migrationBuilder.DropTable(
                name: "Managers");

            migrationBuilder.DropTable(
                name: "PatientAdmissions");

            migrationBuilder.DropTable(
                name: "RoomEquipment");

            migrationBuilder.DropTable(
                name: "BloodUnits");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Medicine");

            migrationBuilder.DropTable(
                name: "Ingredient");

            migrationBuilder.DropTable(
                name: "RoomBeds");

            migrationBuilder.DropTable(
                name: "Specializations");

            migrationBuilder.DropTable(
                name: "WorkingSchedules");

            migrationBuilder.DropTable(
                name: "MedicinePrescription");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "TreatmentReport");

            migrationBuilder.DropTable(
                name: "Floors");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Buildings");

            migrationBuilder.DropTable(
                name: "ApplicationUsers");

            migrationBuilder.DropTable(
                name: "Addresses");
        }
    }
}
