using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalLibrary.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
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
                    table.PrimaryKey("PK_Address", x => x.Id);
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
                    UserRole = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationUsers_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
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
                table: "Address",
                columns: new[] { "Id", "City", "Country", "Postcode", "Street", "StreetNumber" },
                values: new object[,]
                {
                    { new Guid("09f8f2bb-6d3c-4a19-a7ad-58d4680b941f"), "Novi Sad", "Serbia", 21000, "JNA", "33" },
                    { new Guid("5f669c55-3db7-4c4a-a99a-4b50919ac427"), "Novi Sad", "Serbia", 21000, "Kosovska", "23A" },
                    { new Guid("e0ab0ea2-fe64-4e17-b4df-508939c32aed"), "Novi Sad", "Serbia", 21000, "Partizanska", "33" }
                });

            migrationBuilder.InsertData(
                table: "BloodUnits",
                columns: new[] { "Id", "Amount", "BloodBankName", "BloodType" },
                values: new object[,]
                {
                    { new Guid("c1d7c382-891e-455e-87ff-36d758573a6b"), 7, "Moja Banka Krvi", 0 },
                    { new Guid("045135e8-1268-402c-8388-5f2fb182568f"), 10, "Moja Banka Krvi", 7 },
                    { new Guid("cb2ff9a2-8c01-4a61-9c61-bdc97473593f"), 1, "Moja Banka Krvi", 0 }
                });

            migrationBuilder.InsertData(
                table: "Buildings",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("f532dcb3-e38a-4617-bdb3-7cb68891bc14"), "Stara bolnica" },
                    { new Guid("6cb2ebef-e91f-4d7b-a6ac-837ab07c0e3f"), "Nova bolnica" }
                });

            migrationBuilder.InsertData(
                table: "GRooms",
                columns: new[] { "Id", "Lenght", "PositionX", "PositionY", "RoomId", "Width" },
                values: new object[,]
                {
                    { new Guid("14b1ecbc-8a03-4007-bfa7-9d4534f345ba"), 5, 5, 0, new Guid("f071ce5f-53d5-4ba1-9d9a-cde0f04f0fe3"), 5 },
                    { new Guid("c3616d6c-29c7-464e-a304-e33c92c5c6e5"), 5, 5, 0, new Guid("9baefd49-053c-42db-be9d-ba0a1609ce4c"), 5 },
                    { new Guid("1361935e-cf8c-49d9-8145-0b04a07ceffb"), 5, 5, 0, new Guid("4af1d3f6-c1bb-4f0c-ad4c-a623e7dd73e5"), 5 },
                    { new Guid("45c14dc6-723f-4c78-8985-0a9d2849f7c6"), 5, 5, 0, new Guid("67db4cfd-9410-4d8a-9a93-b4925e6eb846"), 5 },
                    { new Guid("2f47914e-d895-4af5-b061-244c203eb39b"), 5, 5, 0, new Guid("97568eab-887b-4b70-b849-59046ac7dd4a"), 5 },
                    { new Guid("121ebb75-44be-4724-a5b9-b7b39de53892"), 5, 5, 0, new Guid("6e7f0520-2ff3-4f29-817e-4625e1437308"), 5 },
                    { new Guid("4a7ada5f-a9d1-4f37-8976-476a4c321194"), 5, 0, 0, new Guid("3fb3fb24-e02c-42d4-9b14-117c9219e424"), 5 },
                    { new Guid("93314a8c-0ab0-4040-8cd6-a31f03aad471"), 5, 5, 0, new Guid("8645aaad-7b80-4769-bb9f-ab9683c5dab6"), 5 },
                    { new Guid("d94ccf7a-825f-464d-a5e2-bf3fe266a428"), 5, 5, 0, new Guid("62eba6bc-9995-4570-b461-723027f00d91"), 5 }
                });

            migrationBuilder.InsertData(
                table: "Specializations",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("371c54e1-7bf4-4ebf-922e-bce27617942d"), "Surgeon" },
                    { new Guid("4e03d7ab-08f7-4b0a-add9-4108a65c0f95"), "Dermatology" },
                    { new Guid("6f6e923a-337d-422e-9f3d-07145b1d63de"), "General" }
                });

            migrationBuilder.InsertData(
                table: "WorkingSchedules",
                columns: new[] { "Id", "DayOfWorkFrom", "DayOfWorkTo", "ExpirationFrom", "ExpirationTo" },
                values: new object[,]
                {
                    { new Guid("96adc6a9-2363-406d-857a-7215012a99dd"), new DateTime(2022, 10, 27, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 27, 22, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("23dd662a-3f31-4e86-b6f9-2763687e24d8"), new DateTime(2022, 10, 27, 8, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 27, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "ApplicationUsers",
                columns: new[] { "Id", "AddressId", "Email", "Jmbg", "Name", "Password", "Phone", "Surname", "UserRole", "Username" },
                values: new object[,]
                {
                    { new Guid("83144b6c-fec1-49d2-ab15-34faa5376d2b"), new Guid("5f669c55-3db7-4c4a-a99a-4b50919ac427"), "Cajons@gmail.com", "99999999", "Ilija", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "+612222222", "Maric", 0, "Ilija" },
                    { new Guid("b42fa8c6-74e4-4739-98be-a428f5fad2ec"), new Guid("5f669c55-3db7-4c4a-a99a-4b50919ac427"), "psw.isa.mail@gmail.com", "99999999", "Sale", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "+612222222", "Lave", 2, "Sale" },
                    { new Guid("0214b47f-6155-4276-ad24-c8aa46dcd36f"), new Guid("e0ab0ea2-fe64-4e17-b4df-508939c32aed"), "DjordjeLopov@gmail.com", "99999999", "Djordje", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "+612222222", "Vuckovic", 0, "Tadjo" },
                    { new Guid("fdd7da27-6590-4744-84b7-409a2b50c1a4"), new Guid("09f8f2bb-6d3c-4a19-a7ad-58d4680b941f"), "psw.isa.mail@gmail.com", "99999999", "Manager", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "+612222222", "Manger", 1, "Manager" },
                    { new Guid("f353adb7-ba9e-4c57-8ad1-bc331a83300e"), new Guid("09f8f2bb-6d3c-4a19-a7ad-58d4680b941f"), "psw.isa.mail@gmail.com", "99999999", "Miki", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "+612222222", "Djuricic", 2, "Miki" }
                });

            migrationBuilder.InsertData(
                table: "BloodConsumptions",
                columns: new[] { "Id", "Amount", "BloodUnitId", "Date", "DoctorId", "Purpose" },
                values: new object[,]
                {
                    { new Guid("ec0951cb-42b7-49c4-879c-7967d2a32e2f"), 2, new Guid("c1d7c382-891e-455e-87ff-36d758573a6b"), new DateTime(2022, 10, 27, 15, 0, 0, 0, DateTimeKind.Unspecified), new Guid("0214b47f-6155-4276-ad24-c8aa46dcd36f"), "operation" },
                    { new Guid("9567d1a2-9dff-4eb0-9663-f1b972622d2b"), 4, new Guid("c1d7c382-891e-455e-87ff-36d758573a6b"), new DateTime(2022, 11, 14, 15, 0, 0, 0, DateTimeKind.Unspecified), new Guid("0214b47f-6155-4276-ad24-c8aa46dcd36f"), "operation" }
                });

            migrationBuilder.InsertData(
                table: "Floors",
                columns: new[] { "Id", "BuildingId", "FloorNumber", "Name" },
                values: new object[,]
                {
                    { new Guid("f57f51a7-6b1d-482e-b727-711f6387402a"), new Guid("f532dcb3-e38a-4617-bdb3-7cb68891bc14"), 0, "F0" },
                    { new Guid("6675413b-d6dd-478d-b829-49afb06cd51d"), new Guid("f532dcb3-e38a-4617-bdb3-7cb68891bc14"), 1, "F1" },
                    { new Guid("d93a9b1d-9089-4172-931d-92aa46fcb830"), new Guid("f532dcb3-e38a-4617-bdb3-7cb68891bc14"), 2, "F2" },
                    { new Guid("d0fe78ed-0894-48d9-a9f6-37c603e1cfa2"), new Guid("6cb2ebef-e91f-4d7b-a6ac-837ab07c0e3f"), 0, "F0" },
                    { new Guid("b981c572-79e5-4ab7-b64f-57d0cab2ba31"), new Guid("6cb2ebef-e91f-4d7b-a6ac-837ab07c0e3f"), 1, "F1" },
                    { new Guid("d6f13c84-27cb-48a4-a72a-be42f389cbb9"), new Guid("6cb2ebef-e91f-4d7b-a6ac-837ab07c0e3f"), 2, "F2" }
                });

            migrationBuilder.InsertData(
                table: "Managers",
                column: "Id",
                value: new Guid("fdd7da27-6590-4744-84b7-409a2b50c1a4"));

            migrationBuilder.InsertData(
                table: "Patients",
                column: "Id",
                values: new object[]
                {
                    new Guid("b42fa8c6-74e4-4739-98be-a428f5fad2ec"),
                    new Guid("f353adb7-ba9e-4c57-8ad1-bc331a83300e")
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "BuildingId", "FloorId", "GRoomId", "Name" },
                values: new object[,]
                {
                    { new Guid("3fb3fb24-e02c-42d4-9b14-117c9219e424"), new Guid("f532dcb3-e38a-4617-bdb3-7cb68891bc14"), new Guid("f57f51a7-6b1d-482e-b727-711f6387402a"), new Guid("4a7ada5f-a9d1-4f37-8976-476a4c321194"), "A11" },
                    { new Guid("8645aaad-7b80-4769-bb9f-ab9683c5dab6"), new Guid("f532dcb3-e38a-4617-bdb3-7cb68891bc14"), new Guid("f57f51a7-6b1d-482e-b727-711f6387402a"), new Guid("93314a8c-0ab0-4040-8cd6-a31f03aad471"), "B11" },
                    { new Guid("f071ce5f-53d5-4ba1-9d9a-cde0f04f0fe3"), new Guid("f532dcb3-e38a-4617-bdb3-7cb68891bc14"), new Guid("6675413b-d6dd-478d-b829-49afb06cd51d"), new Guid("14b1ecbc-8a03-4007-bfa7-9d4534f345ba"), "A12" },
                    { new Guid("62eba6bc-9995-4570-b461-723027f00d91"), new Guid("f532dcb3-e38a-4617-bdb3-7cb68891bc14"), new Guid("d93a9b1d-9089-4172-931d-92aa46fcb830"), new Guid("d94ccf7a-825f-464d-a5e2-bf3fe266a428"), "A13" },
                    { new Guid("6e7f0520-2ff3-4f29-817e-4625e1437308"), new Guid("6cb2ebef-e91f-4d7b-a6ac-837ab07c0e3f"), new Guid("d0fe78ed-0894-48d9-a9f6-37c603e1cfa2"), new Guid("121ebb75-44be-4724-a5b9-b7b39de53892"), "A21" },
                    { new Guid("97568eab-887b-4b70-b849-59046ac7dd4a"), new Guid("6cb2ebef-e91f-4d7b-a6ac-837ab07c0e3f"), new Guid("d0fe78ed-0894-48d9-a9f6-37c603e1cfa2"), new Guid("2f47914e-d895-4af5-b061-244c203eb39b"), "B21" },
                    { new Guid("67db4cfd-9410-4d8a-9a93-b4925e6eb846"), new Guid("6cb2ebef-e91f-4d7b-a6ac-837ab07c0e3f"), new Guid("b981c572-79e5-4ab7-b64f-57d0cab2ba31"), new Guid("45c14dc6-723f-4c78-8985-0a9d2849f7c6"), "A22" },
                    { new Guid("4af1d3f6-c1bb-4f0c-ad4c-a623e7dd73e5"), new Guid("6cb2ebef-e91f-4d7b-a6ac-837ab07c0e3f"), new Guid("d6f13c84-27cb-48a4-a72a-be42f389cbb9"), new Guid("1361935e-cf8c-49d9-8145-0b04a07ceffb"), "C23" },
                    { new Guid("9baefd49-053c-42db-be9d-ba0a1609ce4c"), new Guid("6cb2ebef-e91f-4d7b-a6ac-837ab07c0e3f"), new Guid("d6f13c84-27cb-48a4-a72a-be42f389cbb9"), new Guid("c3616d6c-29c7-464e-a304-e33c92c5c6e5"), "B23" }
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "RoomId", "SpecializationId", "WorkingScheduleId" },
                values: new object[,]
                {
                    { new Guid("83144b6c-fec1-49d2-ab15-34faa5376d2b"), new Guid("3fb3fb24-e02c-42d4-9b14-117c9219e424"), new Guid("4e03d7ab-08f7-4b0a-add9-4108a65c0f95"), new Guid("96adc6a9-2363-406d-857a-7215012a99dd") },
                    { new Guid("0214b47f-6155-4276-ad24-c8aa46dcd36f"), new Guid("8645aaad-7b80-4769-bb9f-ab9683c5dab6"), new Guid("4e03d7ab-08f7-4b0a-add9-4108a65c0f95"), new Guid("23dd662a-3f31-4e86-b6f9-2763687e24d8") }
                });

            migrationBuilder.InsertData(
                table: "RoomBeds",
                columns: new[] { "Id", "IsFree", "Number", "RoomId" },
                values: new object[,]
                {
                    { new Guid("c621db50-f938-4ab5-b32f-b7af656f4315"), true, "11A1", new Guid("3fb3fb24-e02c-42d4-9b14-117c9219e424") },
                    { new Guid("e370c02d-a13c-44e9-b312-327f886b09e3"), true, "11A2", new Guid("3fb3fb24-e02c-42d4-9b14-117c9219e424") },
                    { new Guid("ae1b69d9-3220-478d-806b-37ee7ea63932"), true, "11A3", new Guid("3fb3fb24-e02c-42d4-9b14-117c9219e424") },
                    { new Guid("61c8a205-05b9-4b6c-a183-3156bad444f3"), true, "11A4", new Guid("3fb3fb24-e02c-42d4-9b14-117c9219e424") },
                    { new Guid("d9c4b60d-59cb-4b86-a37d-d2e5529193fb"), true, "12A1", new Guid("8645aaad-7b80-4769-bb9f-ab9683c5dab6") },
                    { new Guid("c9548860-57b8-4e6d-84c5-a7a075bef6ba"), true, "12A2", new Guid("8645aaad-7b80-4769-bb9f-ab9683c5dab6") },
                    { new Guid("5b8837c1-4e3a-4d30-89ec-adefd478f529"), true, "12A3", new Guid("8645aaad-7b80-4769-bb9f-ab9683c5dab6") },
                    { new Guid("b8550f35-f911-46aa-9959-79537bda74af"), true, "12A4", new Guid("8645aaad-7b80-4769-bb9f-ab9683c5dab6") },
                    { new Guid("b6c05e8f-a103-4922-bf49-01e729dac9e3"), true, "12A5", new Guid("8645aaad-7b80-4769-bb9f-ab9683c5dab6") }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "AppointmentState", "AppointmentType", "DoctorId", "Emergent", "PatientId", "Duration_From", "Duration_To" },
                values: new object[] { new Guid("1ca934df-fb67-4365-a3dc-789b3bac11cb"), 0, 0, new Guid("83144b6c-fec1-49d2-ab15-34faa5376d2b"), false, new Guid("b42fa8c6-74e4-4739-98be-a428f5fad2ec"), new DateTime(2022, 10, 27, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 27, 15, 15, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Holidays",
                columns: new[] { "Id", "Description", "DoctorId", "HolidayStatus", "IsUrgent", "DateRange_From", "DateRange_To" },
                values: new object[] { new Guid("323ca7fd-2cf8-4df2-b05c-a50855c5c235"), "I want to go to Paralia", new Guid("83144b6c-fec1-49d2-ab15-34faa5376d2b"), 0, false, new DateTime(2022, 10, 27, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 27, 15, 15, 0, 0, DateTimeKind.Unspecified) });

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
                name: "Address");
        }
    }
}
