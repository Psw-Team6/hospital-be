using System;
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
                name: "Allergens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Allergens", x => x.Id);
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
                name: "EquipmentMovementAppointment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    OriginalRoomId = table.Column<Guid>(type: "uuid", nullable: false),
                    DestinationRoomId = table.Column<Guid>(type: "uuid", nullable: false),
                    Duration_From = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Duration_To = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EquipmentName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentMovementAppointment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquipmentMovementAppointment_Rooms_DestinationRoomId",
                        column: x => x.DestinationRoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipmentMovementAppointment_Rooms_OriginalRoomId",
                        column: x => x.OriginalRoomId,
                        principalTable: "Rooms",
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
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Gender = table.Column<int>(type: "integer", nullable: false),
                    Age = table.Column<int>(type: "integer", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uuid", nullable: false),
                    BloodType = table.Column<int>(type: "integer", nullable: true),
                    IngredientId = table.Column<Guid>(type: "uuid", nullable: true)
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
                    table.ForeignKey(
                        name: "FK_Patients_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Patients_Ingredient_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AllergenPatient",
                columns: table => new
                {
                    AllergiesId = table.Column<Guid>(type: "uuid", nullable: false),
                    PatientsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllergenPatient", x => new { x.AllergiesId, x.PatientsId });
                    table.ForeignKey(
                        name: "FK_AllergenPatient_Allergens_AllergiesId",
                        column: x => x.AllergiesId,
                        principalTable: "Allergens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AllergenPatient_Patients_PatientsId",
                        column: x => x.PatientsId,
                        principalTable: "Patients",
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
                name: "PatientAdmissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DateOfAdmission = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    PatientId = table.Column<Guid>(type: "uuid", nullable: false),
                    SelectedBedId = table.Column<Guid>(type: "uuid", nullable: false),
                    SelectedRoomId = table.Column<Guid>(type: "uuid", nullable: false),
                    Reason = table.Column<string>(type: "text", nullable: true),
                    ReasonOfDischarge = table.Column<string>(type: "text", nullable: true),
                    DateOfDischarge = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
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
                name: "TreatmentReport",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PatientAdmissionId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreatmentReport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TreatmentReport_PatientAdmissions_PatientAdmissionId",
                        column: x => x.PatientAdmissionId,
                        principalTable: "PatientAdmissions",
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
                name: "Medicine",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    AllergenId = table.Column<Guid>(type: "uuid", nullable: true),
                    MedicinePrescriptionId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medicine_Allergens_AllergenId",
                        column: x => x.AllergenId,
                        principalTable: "Allergens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    { new Guid("51814574-dc5f-419a-b306-52d29ab19d15"), "Novi Sad", "Serbia", 21000, "JNA", "33" },
                    { new Guid("15e9ed79-9b35-482e-8cd5-bf2df0fc2e49"), "Novi Sad", "Serbia", 21000, "Partizanska", "33" },
                    { new Guid("848b0367-7cf5-4ec9-afab-7b48da75f37e"), "Novi Sad", "Serbia", 21000, "Kosovska", "23A" }
                });

            migrationBuilder.InsertData(
                table: "Allergens",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("76a98734-5045-4a8b-b1f2-001730494d76"), "Brufen" },
                    { new Guid("5013cfe2-4481-4868-ac85-12726f8ba183"), "Paracetamol" }
                });

            migrationBuilder.InsertData(
                table: "BloodUnits",
                columns: new[] { "Id", "Amount", "BloodBankName", "BloodType" },
                values: new object[,]
                {
                    { new Guid("f814d45a-d30a-41d8-a339-56cd3ab5a8a3"), 7, "Moja Banka Krvi", 0 },
                    { new Guid("292a1add-7260-490c-a055-5026a295d4b4"), 10, "Moja Banka Krvi", 7 },
                    { new Guid("f2e7a8ea-a50f-4cae-9c53-cf9ea9854842"), 4, "Moja Banka Krvi", 0 }
                });

            migrationBuilder.InsertData(
                table: "Buildings",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("3abb1cd6-839c-48d3-afe9-57f1bcb9af97"), "Stara bolnica" },
                    { new Guid("7aa8084a-d532-46bd-8a49-b3a4633d1d96"), "Nova bolnica" }
                });

            migrationBuilder.InsertData(
                table: "GRooms",
                columns: new[] { "Id", "Lenght", "PositionX", "PositionY", "RoomId", "Width" },
                values: new object[,]
                {
                    { new Guid("d571980f-bc2c-4d0d-b11c-d0ae62af50c8"), 5, 5, 0, new Guid("8a00f865-f0f6-4f45-b9d6-7c0ee9e048cf"), 5 },
                    { new Guid("0caad5f2-0197-4552-b161-71ef5bd3d3ce"), 5, 5, 0, new Guid("787b4dc4-502b-4f4f-8c7a-9db197dc238c"), 5 },
                    { new Guid("1d88a81a-15c8-49de-aa4e-c3a4ffaa7304"), 5, 5, 0, new Guid("16be9384-3f9e-49e9-b353-83e8fa1cc2c0"), 5 },
                    { new Guid("7cdb4692-f45b-4634-aa24-ee31510f389b"), 5, 5, 0, new Guid("a4ab6ab8-a04a-4e6c-a680-f8adbfa2e5c9"), 5 },
                    { new Guid("74b01052-ca10-454a-adee-1816369ed128"), 5, 5, 0, new Guid("ca266fb0-7e19-4752-be3e-e96d0e10fce7"), 5 },
                    { new Guid("fa3741e0-894b-46e3-b13e-1349e86ce05f"), 5, 5, 0, new Guid("66ffbaaa-6478-4e73-8e19-1f80e3d6bd2e"), 5 },
                    { new Guid("34fe73f3-74b6-4f83-a540-758684cf4957"), 5, 5, 0, new Guid("d0d468c0-5edd-4a07-a034-9cc4f6b8b795"), 5 },
                    { new Guid("b8e28fc9-6ecf-4acf-a6f5-2c207ee896ee"), 5, 5, 0, new Guid("4ff349b9-292c-4c4c-83b6-a892944ea4dc"), 5 },
                    { new Guid("a7d5ade0-b0e8-42e6-b426-6de2ef6c77e8"), 5, 0, 0, new Guid("585b6294-d572-452d-9f2b-6fe0c7189283"), 5 }
                });

            migrationBuilder.InsertData(
                table: "Specializations",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("e797319c-ada3-4897-bd9a-187feb126309"), "General" },
                    { new Guid("55394b32-c88f-4720-a304-2efd5f80412f"), "Surgeon" },
                    { new Guid("ca2b7d88-bc19-4646-b9e3-3c5d435ec8e2"), "Dermatology" }
                });

            migrationBuilder.InsertData(
                table: "WorkingSchedules",
                columns: new[] { "Id", "DayOfWorkFrom", "DayOfWorkTo", "ExpirationFrom", "ExpirationTo" },
                values: new object[,]
                {
                    { new Guid("801d77bf-2f1c-481d-98de-c6b581b4c404"), new DateTime(2022, 10, 27, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 27, 22, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("da2a8505-cdba-4e8a-8997-563a7b0a3730"), new DateTime(2022, 10, 27, 8, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 12, 27, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "ApplicationUsers",
                columns: new[] { "Id", "AddressId", "Email", "Enabled", "Jmbg", "Name", "Password", "Phone", "Surname", "UserRole", "Username" },
                values: new object[,]
                {
                    { new Guid("f171817a-2e39-4809-a018-ca70ef05c69b"), new Guid("848b0367-7cf5-4ec9-afab-7b48da75f37e"), "Cajons@gmail.com", true, "99999999", "Ilija", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "+612222222", "Maric", 0, "Ilija" },
                    { new Guid("2979259b-f895-4a33-afbc-65cf3b11fb9e"), new Guid("848b0367-7cf5-4ec9-afab-7b48da75f37e"), "psw.isa.mail@gmail.com", true, "99999999", "Sale", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "+612222222", "Lave", 2, "Sale" },
                    { new Guid("982b86fa-fd7c-4175-87c4-b51aa6dfd860"), new Guid("15e9ed79-9b35-482e-8cd5-bf2df0fc2e49"), "DjordjeLopov@gmail.com", true, "99999999", "Djordje", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "+612222222", "Vuckovic", 0, "Tadjo" },
                    { new Guid("b3e65aa3-232d-4330-b783-f487ba6b25bd"), new Guid("51814574-dc5f-419a-b306-52d29ab19d15"), "psw.isa.mail@gmail.com", true, "99999999", "Manager", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "+612222222", "Manger", 1, "Manager" },
                    { new Guid("a4e7fb6f-a4e8-438b-84d9-8b3926de5069"), new Guid("51814574-dc5f-419a-b306-52d29ab19d15"), "psw.isa.mail@gmail.com", true, "99999999", "Miki", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "+612222222", "Djuricic", 2, "Miki" }
                });

            migrationBuilder.InsertData(
                table: "BloodConsumptions",
                columns: new[] { "Id", "Amount", "BloodUnitId", "Date", "DoctorId", "Purpose" },
                values: new object[,]
                {
                    { new Guid("ba78bb6a-93d8-4701-a661-ce6f70d492a8"), 2, new Guid("f814d45a-d30a-41d8-a339-56cd3ab5a8a3"), new DateTime(2022, 10, 27, 15, 0, 0, 0, DateTimeKind.Unspecified), new Guid("982b86fa-fd7c-4175-87c4-b51aa6dfd860"), "operation" },
                    { new Guid("a34d6755-9464-429b-8182-56df25ec0a08"), 4, new Guid("f814d45a-d30a-41d8-a339-56cd3ab5a8a3"), new DateTime(2022, 11, 14, 15, 0, 0, 0, DateTimeKind.Unspecified), new Guid("982b86fa-fd7c-4175-87c4-b51aa6dfd860"), "operation" }
                });

            migrationBuilder.InsertData(
                table: "Floors",
                columns: new[] { "Id", "BuildingId", "FloorNumber", "Name" },
                values: new object[,]
                {
                    { new Guid("e0096484-211f-4c15-a6bd-17b2230b6008"), new Guid("3abb1cd6-839c-48d3-afe9-57f1bcb9af97"), 0, "F0" },
                    { new Guid("75a55f52-204c-4481-8342-6f1a13830d73"), new Guid("3abb1cd6-839c-48d3-afe9-57f1bcb9af97"), 1, "F1" },
                    { new Guid("878e9d33-0b49-4255-aa19-cdbcdccef2c6"), new Guid("3abb1cd6-839c-48d3-afe9-57f1bcb9af97"), 2, "F2" },
                    { new Guid("d507f6da-36bc-4e56-be1b-13301442c42f"), new Guid("7aa8084a-d532-46bd-8a49-b3a4633d1d96"), 0, "F0" },
                    { new Guid("37988b1a-f5dc-4297-8183-48d6e69c612b"), new Guid("7aa8084a-d532-46bd-8a49-b3a4633d1d96"), 1, "F1" },
                    { new Guid("a5ca5189-d607-4f2c-8511-6e1abf5e833e"), new Guid("7aa8084a-d532-46bd-8a49-b3a4633d1d96"), 2, "F2" }
                });

            migrationBuilder.InsertData(
                table: "Managers",
                column: "Id",
                value: new Guid("b3e65aa3-232d-4330-b783-f487ba6b25bd"));

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "BuildingId", "FloorId", "GRoomId", "Name" },
                values: new object[,]
                {
                    { new Guid("585b6294-d572-452d-9f2b-6fe0c7189283"), new Guid("3abb1cd6-839c-48d3-afe9-57f1bcb9af97"), new Guid("e0096484-211f-4c15-a6bd-17b2230b6008"), new Guid("a7d5ade0-b0e8-42e6-b426-6de2ef6c77e8"), "A11" },
                    { new Guid("66ffbaaa-6478-4e73-8e19-1f80e3d6bd2e"), new Guid("3abb1cd6-839c-48d3-afe9-57f1bcb9af97"), new Guid("e0096484-211f-4c15-a6bd-17b2230b6008"), new Guid("fa3741e0-894b-46e3-b13e-1349e86ce05f"), "B11" },
                    { new Guid("4ff349b9-292c-4c4c-83b6-a892944ea4dc"), new Guid("3abb1cd6-839c-48d3-afe9-57f1bcb9af97"), new Guid("75a55f52-204c-4481-8342-6f1a13830d73"), new Guid("b8e28fc9-6ecf-4acf-a6f5-2c207ee896ee"), "A12" },
                    { new Guid("d0d468c0-5edd-4a07-a034-9cc4f6b8b795"), new Guid("3abb1cd6-839c-48d3-afe9-57f1bcb9af97"), new Guid("878e9d33-0b49-4255-aa19-cdbcdccef2c6"), new Guid("34fe73f3-74b6-4f83-a540-758684cf4957"), "A13" },
                    { new Guid("8a00f865-f0f6-4f45-b9d6-7c0ee9e048cf"), new Guid("7aa8084a-d532-46bd-8a49-b3a4633d1d96"), new Guid("d507f6da-36bc-4e56-be1b-13301442c42f"), new Guid("d571980f-bc2c-4d0d-b11c-d0ae62af50c8"), "A21" },
                    { new Guid("ca266fb0-7e19-4752-be3e-e96d0e10fce7"), new Guid("7aa8084a-d532-46bd-8a49-b3a4633d1d96"), new Guid("d507f6da-36bc-4e56-be1b-13301442c42f"), new Guid("74b01052-ca10-454a-adee-1816369ed128"), "B21" },
                    { new Guid("a4ab6ab8-a04a-4e6c-a680-f8adbfa2e5c9"), new Guid("7aa8084a-d532-46bd-8a49-b3a4633d1d96"), new Guid("37988b1a-f5dc-4297-8183-48d6e69c612b"), new Guid("7cdb4692-f45b-4634-aa24-ee31510f389b"), "A22" },
                    { new Guid("16be9384-3f9e-49e9-b353-83e8fa1cc2c0"), new Guid("7aa8084a-d532-46bd-8a49-b3a4633d1d96"), new Guid("a5ca5189-d607-4f2c-8511-6e1abf5e833e"), new Guid("1d88a81a-15c8-49de-aa4e-c3a4ffaa7304"), "C23" },
                    { new Guid("787b4dc4-502b-4f4f-8c7a-9db197dc238c"), new Guid("7aa8084a-d532-46bd-8a49-b3a4633d1d96"), new Guid("a5ca5189-d607-4f2c-8511-6e1abf5e833e"), new Guid("0caad5f2-0197-4552-b161-71ef5bd3d3ce"), "B23" }
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "RoomId", "SpecializationId", "WorkingScheduleId" },
                values: new object[,]
                {
                    { new Guid("f171817a-2e39-4809-a018-ca70ef05c69b"), new Guid("585b6294-d572-452d-9f2b-6fe0c7189283"), new Guid("e797319c-ada3-4897-bd9a-187feb126309"), new Guid("da2a8505-cdba-4e8a-8997-563a7b0a3730") },
                    { new Guid("982b86fa-fd7c-4175-87c4-b51aa6dfd860"), new Guid("66ffbaaa-6478-4e73-8e19-1f80e3d6bd2e"), new Guid("ca2b7d88-bc19-4646-b9e3-3c5d435ec8e2"), new Guid("da2a8505-cdba-4e8a-8997-563a7b0a3730") }
                });

            migrationBuilder.InsertData(
                table: "RoomBeds",
                columns: new[] { "Id", "IsFree", "Number", "RoomId" },
                values: new object[,]
                {
                    { new Guid("f0bc5515-f3b6-4962-a92b-a36c9677546b"), true, "12A5", new Guid("66ffbaaa-6478-4e73-8e19-1f80e3d6bd2e") },
                    { new Guid("d9ae8458-0649-4c67-98eb-a5e5a65c7e0b"), true, "12A4", new Guid("66ffbaaa-6478-4e73-8e19-1f80e3d6bd2e") },
                    { new Guid("cfc16f73-a94e-42ff-a7bf-3d37137f499f"), true, "12A2", new Guid("66ffbaaa-6478-4e73-8e19-1f80e3d6bd2e") },
                    { new Guid("179afba1-30b0-415c-bba1-47d3909eaf5f"), true, "12A1", new Guid("66ffbaaa-6478-4e73-8e19-1f80e3d6bd2e") },
                    { new Guid("f98851f5-60f7-4401-b820-38ceaa79b6e6"), true, "12A3", new Guid("66ffbaaa-6478-4e73-8e19-1f80e3d6bd2e") },
                    { new Guid("a8f4f2d2-ce20-49c1-8ebe-5f6953033201"), true, "11A4", new Guid("585b6294-d572-452d-9f2b-6fe0c7189283") },
                    { new Guid("23892142-6c1d-4b78-ab7b-dc36ed1e3264"), true, "11A3", new Guid("585b6294-d572-452d-9f2b-6fe0c7189283") },
                    { new Guid("0d4223b3-1988-4f8c-b9c9-258e0352fa95"), true, "11A2", new Guid("585b6294-d572-452d-9f2b-6fe0c7189283") },
                    { new Guid("fcd56a4c-e9ae-4671-8e26-4d943cd40951"), true, "11A1", new Guid("585b6294-d572-452d-9f2b-6fe0c7189283") }
                });

            migrationBuilder.InsertData(
                table: "RoomEquipment",
                columns: new[] { "RoomEquipmentId", "Amount", "EquipmentName", "RoomId" },
                values: new object[,]
                {
                    { new Guid("e6186032-1aa0-4e0d-a505-e3a3b711c03b"), 3, "ANESTHESIA", new Guid("16be9384-3f9e-49e9-b353-83e8fa1cc2c0") },
                    { new Guid("b6d36a93-c200-46ab-bad7-d5aacb9ee7c9"), 10, "ANESTHESIA", new Guid("66ffbaaa-6478-4e73-8e19-1f80e3d6bd2e") },
                    { new Guid("e9ed8474-da3a-4252-81a8-d42a51a8f699"), 3, "EKG_MACHINE", new Guid("4ff349b9-292c-4c4c-83b6-a892944ea4dc") },
                    { new Guid("17b32393-e3c1-4454-af9e-661a190ffd43"), 1, "ANESTHESIA", new Guid("d0d468c0-5edd-4a07-a034-9cc4f6b8b795") },
                    { new Guid("fe68eeaf-5615-4f22-a82f-697b7595c9a4"), 2, "EKG_MACHINE", new Guid("8a00f865-f0f6-4f45-b9d6-7c0ee9e048cf") },
                    { new Guid("3dbb1618-fc9d-4162-92cc-19df3d3eecee"), 4, "SURGICAL_TABLES", new Guid("ca266fb0-7e19-4752-be3e-e96d0e10fce7") },
                    { new Guid("ccd974c9-3f00-44dc-bcc4-793a199bdf61"), 6, "BANDAGE", new Guid("a4ab6ab8-a04a-4e6c-a680-f8adbfa2e5c9") },
                    { new Guid("f7ac761b-78ce-4fdb-9693-a899548a9de9"), 15, "SURGICAL_TABLES", new Guid("585b6294-d572-452d-9f2b-6fe0c7189283") },
                    { new Guid("99624840-399b-4ff4-af77-0a7a8cce145e"), 9, "SURGICAL_TABLES", new Guid("787b4dc4-502b-4f4f-8c7a-9db197dc238c") }
                });

            migrationBuilder.InsertData(
                table: "Holidays",
                columns: new[] { "Id", "Description", "DoctorId", "HolidayStatus", "IsUrgent", "DateRange_From", "DateRange_To" },
                values: new object[] { new Guid("c556c567-f5d4-4a35-b594-3995a0aad81b"), "I want to go to Paralia", new Guid("f171817a-2e39-4809-a018-ca70ef05c69b"), 0, false, new DateTime(2022, 10, 27, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 27, 10, 30, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "Age", "BloodType", "DoctorId", "Gender", "IngredientId" },
                values: new object[,]
                {
                    { new Guid("2979259b-f895-4a33-afbc-65cf3b11fb9e"), 15, 4, new Guid("f171817a-2e39-4809-a018-ca70ef05c69b"), 0, null },
                    { new Guid("a4e7fb6f-a4e8-438b-84d9-8b3926de5069"), 32, 0, new Guid("f171817a-2e39-4809-a018-ca70ef05c69b"), 0, null }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "AppointmentState", "AppointmentType", "DoctorId", "Emergent", "PatientId", "Duration_From", "Duration_To" },
                values: new object[] { new Guid("45bdf6d6-f60d-49d5-9f44-df46b3db1057"), 0, 0, new Guid("f171817a-2e39-4809-a018-ca70ef05c69b"), false, new Guid("2979259b-f895-4a33-afbc-65cf3b11fb9e"), new DateTime(2023, 7, 27, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 27, 10, 30, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_AllergenPatient_PatientsId",
                table: "AllergenPatient",
                column: "PatientsId");

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
                name: "IX_EquipmentMovementAppointment_DestinationRoomId",
                table: "EquipmentMovementAppointment",
                column: "DestinationRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentMovementAppointment_OriginalRoomId",
                table: "EquipmentMovementAppointment",
                column: "OriginalRoomId");

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
                name: "IX_Medicine_AllergenId",
                table: "Medicine",
                column: "AllergenId");

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
                name: "IX_Patients_DoctorId",
                table: "Patients",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_IngredientId",
                table: "Patients",
                column: "IngredientId");

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
                name: "IX_TreatmentReport_PatientAdmissionId",
                table: "TreatmentReport",
                column: "PatientAdmissionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllergenPatient");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "BloodConsumptions");

            migrationBuilder.DropTable(
                name: "BloodPrescription");

            migrationBuilder.DropTable(
                name: "EquipmentMovementAppointment");

            migrationBuilder.DropTable(
                name: "Feedback");

            migrationBuilder.DropTable(
                name: "GRooms");

            migrationBuilder.DropTable(
                name: "Holidays");

            migrationBuilder.DropTable(
                name: "IngredientMedicine");

            migrationBuilder.DropTable(
                name: "Managers");

            migrationBuilder.DropTable(
                name: "RoomEquipment");

            migrationBuilder.DropTable(
                name: "BloodUnits");

            migrationBuilder.DropTable(
                name: "Medicine");

            migrationBuilder.DropTable(
                name: "Allergens");

            migrationBuilder.DropTable(
                name: "MedicinePrescription");

            migrationBuilder.DropTable(
                name: "TreatmentReport");

            migrationBuilder.DropTable(
                name: "PatientAdmissions");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "RoomBeds");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Ingredient");

            migrationBuilder.DropTable(
                name: "ApplicationUsers");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Specializations");

            migrationBuilder.DropTable(
                name: "WorkingSchedules");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Floors");

            migrationBuilder.DropTable(
                name: "Buildings");
        }
    }
}
