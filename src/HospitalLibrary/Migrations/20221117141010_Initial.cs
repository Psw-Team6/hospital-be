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
                name: "Holiday",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uuid", nullable: true),
                    DateRange_From = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DateRange_To = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    isUrgent = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holiday", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Holiday_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    { new Guid("66a0443f-3f6f-487a-82fc-a836e9cc64a9"), "Novi Sad", "Serbia", 21000, "JNA", "33" },
                    { new Guid("17d9414d-2ad4-4074-a08e-b2c7d5a737b4"), "Novi Sad", "Serbia", 21000, "Kosovska", "23A" },
                    { new Guid("1d991563-b62a-4ad6-8c5a-7d5a7fbda19c"), "Novi Sad", "Serbia", 21000, "Partizanska", "33" }
                });

            migrationBuilder.InsertData(
                table: "BloodUnits",
                columns: new[] { "Id", "Amount", "BloodBankName", "BloodType" },
                values: new object[,]
                {
                    { new Guid("ca75331e-b526-4156-81b4-72df9b021ef5"), 7, "Moja Banka Krvi", 0 },
                    { new Guid("a1de60ff-db86-4544-bef1-0e13e6e416c4"), 10, "Moja Banka Krvi", 7 },
                    { new Guid("e7bcaaeb-7813-421f-95dc-81c4a6e53edf"), 1, "Moja Banka Krvi", 0 }
                });

            migrationBuilder.InsertData(
                table: "Buildings",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("8c26101e-2a7e-4ae3-bdae-122b42a04d15"), "Stara bolnica" },
                    { new Guid("c3ceb744-325b-434a-9017-264fb9d84b85"), "Nova bolnica" }
                });

            migrationBuilder.InsertData(
                table: "GRooms",
                columns: new[] { "Id", "Lenght", "PositionX", "PositionY", "RoomId", "Width" },
                values: new object[,]
                {
                    { new Guid("ffd0ab2f-8edf-481d-8f32-b4b22c1f5048"), 5, 5, 0, new Guid("7f904f11-0a43-410b-86ed-82b68fdb4c6d"), 5 },
                    { new Guid("88fa9b8c-869e-4a3c-9a19-7f161a005fa8"), 5, 5, 0, new Guid("8da8f74f-d18d-41dd-9339-938dd0da0da8"), 5 },
                    { new Guid("298e8f17-e2e2-4d2a-8cf1-a31151c068bc"), 5, 5, 0, new Guid("34ab8855-13b4-4adf-92a1-50a351433515"), 5 },
                    { new Guid("8c1f9dcc-a854-4191-bef1-82db2c51f35c"), 5, 5, 0, new Guid("2d08a5ab-a8fe-4f20-ae7f-49d7f0a16e97"), 5 },
                    { new Guid("3d441614-2ad3-45bc-aa03-35243cdbb4a5"), 5, 5, 0, new Guid("ff9369a6-aa42-4fc1-884e-587d8d46d708"), 5 },
                    { new Guid("cdf3740b-cb9b-4760-bfc2-9c3a907fec5b"), 5, 5, 0, new Guid("ce93d705-c92a-4085-b723-d28416023c75"), 5 },
                    { new Guid("78b45e08-90ce-4c42-b1ab-69475dfe735e"), 5, 0, 0, new Guid("edd65f01-43c3-4148-af6b-75f6b5f9bc9b"), 5 },
                    { new Guid("ec0255ba-e610-4c70-85d4-a996cfa79e2e"), 5, 5, 0, new Guid("d79f3b63-3519-4b45-a26c-befaf1d26f3b"), 5 },
                    { new Guid("8c933264-b982-4eeb-b984-db989b4dbf8b"), 5, 5, 0, new Guid("f9afec07-a8e1-41ac-8e67-42be5006eca6"), 5 }
                });

            migrationBuilder.InsertData(
                table: "Specializations",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("dc649fff-afa5-4010-bc43-fccb96d9d504"), "Surgeon" },
                    { new Guid("6cd5f9cd-7524-4d21-a29b-cec2da2329be"), "Dermatology" },
                    { new Guid("8ba7df50-63a6-42a4-812f-1b13401fea3d"), "General" }
                });

            migrationBuilder.InsertData(
                table: "WorkingSchedules",
                columns: new[] { "Id", "DayOfWorkFrom", "DayOfWorkTo", "ExpirationFrom", "ExpirationTo" },
                values: new object[,]
                {
                    { new Guid("0305e713-c903-4572-bffb-e5766659cff9"), new DateTime(2022, 10, 27, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 27, 22, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("073761a7-c258-41ba-a238-e7f7da3c6478"), new DateTime(2022, 10, 27, 8, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 27, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "ApplicationUsers",
                columns: new[] { "Id", "AddressId", "Email", "Jmbg", "Name", "Password", "Phone", "Surname", "UserRole", "Username" },
                values: new object[,]
                {
                    { new Guid("eaa3156c-029d-44df-a4dd-b799a64aaf89"), new Guid("17d9414d-2ad4-4074-a08e-b2c7d5a737b4"), "Cajons@gmail.com", "99999999", "Ilija", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "+612222222", "Maric", 0, "Ilija" },
                    { new Guid("cb15e243-2f60-4207-8427-9189fef0f9ef"), new Guid("17d9414d-2ad4-4074-a08e-b2c7d5a737b4"), "psw.isa.mail@gmail.com", "99999999", "Sale", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "+612222222", "Lave", 2, "Sale" },
                    { new Guid("58483939-e39d-4ac8-8ae3-8070717f5e49"), new Guid("1d991563-b62a-4ad6-8c5a-7d5a7fbda19c"), "DjordjeLopov@gmail.com", "99999999", "Djordje", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "+612222222", "Vuckovic", 0, "Tadjo" },
                    { new Guid("ac1602a4-dc22-40eb-801e-06ae56b5bf53"), new Guid("66a0443f-3f6f-487a-82fc-a836e9cc64a9"), "psw.isa.mail@gmail.com", "99999999", "Manager", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "+612222222", "Manger", 1, "Manager" },
                    { new Guid("62205918-426e-49f2-ba97-6eb93583b5f4"), new Guid("66a0443f-3f6f-487a-82fc-a836e9cc64a9"), "psw.isa.mail@gmail.com", "99999999", "Miki", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "+612222222", "Djuricic", 2, "Miki" }
                });

            migrationBuilder.InsertData(
                table: "BloodConsumptions",
                columns: new[] { "Id", "Amount", "BloodUnitId", "Date", "DoctorId", "Purpose" },
                values: new object[,]
                {
                    { new Guid("bf04c103-a5a9-4182-ab7e-3168a2ca9550"), 2, new Guid("ca75331e-b526-4156-81b4-72df9b021ef5"), new DateTime(2022, 10, 27, 15, 0, 0, 0, DateTimeKind.Unspecified), new Guid("58483939-e39d-4ac8-8ae3-8070717f5e49"), "operation" },
                    { new Guid("64fa0661-f498-442e-8566-ed7ee8deb038"), 4, new Guid("ca75331e-b526-4156-81b4-72df9b021ef5"), new DateTime(2022, 11, 14, 15, 0, 0, 0, DateTimeKind.Unspecified), new Guid("58483939-e39d-4ac8-8ae3-8070717f5e49"), "operation" }
                });

            migrationBuilder.InsertData(
                table: "Floors",
                columns: new[] { "Id", "BuildingId", "FloorNumber", "Name" },
                values: new object[,]
                {
                    { new Guid("6ef3cabc-706c-422a-ad97-ca1c129a5ff1"), new Guid("8c26101e-2a7e-4ae3-bdae-122b42a04d15"), 0, "F0" },
                    { new Guid("ca86dd8c-c30d-49a9-83c4-4a593f8a6e80"), new Guid("8c26101e-2a7e-4ae3-bdae-122b42a04d15"), 1, "F1" },
                    { new Guid("da4a3902-23c6-4076-8f86-e2dddd54fbd6"), new Guid("8c26101e-2a7e-4ae3-bdae-122b42a04d15"), 2, "F2" },
                    { new Guid("08066dcd-09b9-4a78-972c-67589a7d8c68"), new Guid("c3ceb744-325b-434a-9017-264fb9d84b85"), 0, "F0" },
                    { new Guid("91ba6fe2-b9da-4490-a17c-97bd2cbe38c0"), new Guid("c3ceb744-325b-434a-9017-264fb9d84b85"), 1, "F1" },
                    { new Guid("79fbfb16-be05-4bd0-81d9-240f921b6fc9"), new Guid("c3ceb744-325b-434a-9017-264fb9d84b85"), 2, "F2" }
                });

            migrationBuilder.InsertData(
                table: "Managers",
                column: "Id",
                value: new Guid("ac1602a4-dc22-40eb-801e-06ae56b5bf53"));

            migrationBuilder.InsertData(
                table: "Patients",
                column: "Id",
                values: new object[]
                {
                    new Guid("cb15e243-2f60-4207-8427-9189fef0f9ef"),
                    new Guid("62205918-426e-49f2-ba97-6eb93583b5f4")
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "BuildingId", "FloorId", "GRoomId", "Name" },
                values: new object[,]
                {
                    { new Guid("edd65f01-43c3-4148-af6b-75f6b5f9bc9b"), new Guid("8c26101e-2a7e-4ae3-bdae-122b42a04d15"), new Guid("6ef3cabc-706c-422a-ad97-ca1c129a5ff1"), new Guid("78b45e08-90ce-4c42-b1ab-69475dfe735e"), "A11" },
                    { new Guid("d79f3b63-3519-4b45-a26c-befaf1d26f3b"), new Guid("8c26101e-2a7e-4ae3-bdae-122b42a04d15"), new Guid("6ef3cabc-706c-422a-ad97-ca1c129a5ff1"), new Guid("ec0255ba-e610-4c70-85d4-a996cfa79e2e"), "B11" },
                    { new Guid("7f904f11-0a43-410b-86ed-82b68fdb4c6d"), new Guid("8c26101e-2a7e-4ae3-bdae-122b42a04d15"), new Guid("ca86dd8c-c30d-49a9-83c4-4a593f8a6e80"), new Guid("ffd0ab2f-8edf-481d-8f32-b4b22c1f5048"), "A12" },
                    { new Guid("f9afec07-a8e1-41ac-8e67-42be5006eca6"), new Guid("8c26101e-2a7e-4ae3-bdae-122b42a04d15"), new Guid("da4a3902-23c6-4076-8f86-e2dddd54fbd6"), new Guid("8c933264-b982-4eeb-b984-db989b4dbf8b"), "A13" },
                    { new Guid("ce93d705-c92a-4085-b723-d28416023c75"), new Guid("c3ceb744-325b-434a-9017-264fb9d84b85"), new Guid("08066dcd-09b9-4a78-972c-67589a7d8c68"), new Guid("cdf3740b-cb9b-4760-bfc2-9c3a907fec5b"), "A21" },
                    { new Guid("ff9369a6-aa42-4fc1-884e-587d8d46d708"), new Guid("c3ceb744-325b-434a-9017-264fb9d84b85"), new Guid("08066dcd-09b9-4a78-972c-67589a7d8c68"), new Guid("3d441614-2ad3-45bc-aa03-35243cdbb4a5"), "B21" },
                    { new Guid("2d08a5ab-a8fe-4f20-ae7f-49d7f0a16e97"), new Guid("c3ceb744-325b-434a-9017-264fb9d84b85"), new Guid("91ba6fe2-b9da-4490-a17c-97bd2cbe38c0"), new Guid("8c1f9dcc-a854-4191-bef1-82db2c51f35c"), "A22" },
                    { new Guid("34ab8855-13b4-4adf-92a1-50a351433515"), new Guid("c3ceb744-325b-434a-9017-264fb9d84b85"), new Guid("79fbfb16-be05-4bd0-81d9-240f921b6fc9"), new Guid("298e8f17-e2e2-4d2a-8cf1-a31151c068bc"), "C23" },
                    { new Guid("8da8f74f-d18d-41dd-9339-938dd0da0da8"), new Guid("c3ceb744-325b-434a-9017-264fb9d84b85"), new Guid("79fbfb16-be05-4bd0-81d9-240f921b6fc9"), new Guid("88fa9b8c-869e-4a3c-9a19-7f161a005fa8"), "B23" }
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "RoomId", "SpecializationId", "WorkingScheduleId" },
                values: new object[,]
                {
                    { new Guid("eaa3156c-029d-44df-a4dd-b799a64aaf89"), new Guid("edd65f01-43c3-4148-af6b-75f6b5f9bc9b"), new Guid("6cd5f9cd-7524-4d21-a29b-cec2da2329be"), new Guid("0305e713-c903-4572-bffb-e5766659cff9") },
                    { new Guid("58483939-e39d-4ac8-8ae3-8070717f5e49"), new Guid("d79f3b63-3519-4b45-a26c-befaf1d26f3b"), new Guid("6cd5f9cd-7524-4d21-a29b-cec2da2329be"), new Guid("073761a7-c258-41ba-a238-e7f7da3c6478") }
                });

            migrationBuilder.InsertData(
                table: "RoomBeds",
                columns: new[] { "Id", "IsFree", "Number", "RoomId" },
                values: new object[,]
                {
                    { new Guid("c7377d40-433e-44bd-8b32-3000059407ba"), true, "12A5", new Guid("d79f3b63-3519-4b45-a26c-befaf1d26f3b") },
                    { new Guid("0220f179-ac86-46c5-ad43-1b2c0e93c297"), true, "12A4", new Guid("d79f3b63-3519-4b45-a26c-befaf1d26f3b") },
                    { new Guid("592d9a53-c17d-4342-8963-a4fc945cae71"), true, "12A2", new Guid("d79f3b63-3519-4b45-a26c-befaf1d26f3b") },
                    { new Guid("d699ba42-7ce7-41f8-aa1b-dde45b31fe72"), true, "12A1", new Guid("d79f3b63-3519-4b45-a26c-befaf1d26f3b") },
                    { new Guid("7f9eb84e-5220-4833-8b82-46998204c749"), true, "12A3", new Guid("d79f3b63-3519-4b45-a26c-befaf1d26f3b") },
                    { new Guid("4edb239d-be61-4b63-ac73-a8d1293a8b63"), true, "11A4", new Guid("edd65f01-43c3-4148-af6b-75f6b5f9bc9b") },
                    { new Guid("1347b63e-6289-41f7-a537-918a80d0929b"), true, "11A3", new Guid("edd65f01-43c3-4148-af6b-75f6b5f9bc9b") },
                    { new Guid("e4427442-2623-4270-a0fb-c0df3d5ada39"), true, "11A2", new Guid("edd65f01-43c3-4148-af6b-75f6b5f9bc9b") },
                    { new Guid("875315ed-4f0f-4e4d-9bcb-12e68b814e0e"), true, "11A1", new Guid("edd65f01-43c3-4148-af6b-75f6b5f9bc9b") }
                });

            migrationBuilder.InsertData(
                table: "RoomEquipment",
                columns: new[] { "RoomEquipmentId", "Amount", "EquipmentName", "RoomId" },
                values: new object[,]
                {
                    { new Guid("b680e846-748c-4abc-b5aa-30dd06d3fd26"), 3, "ANESTHESIA", new Guid("34ab8855-13b4-4adf-92a1-50a351433515") },
                    { new Guid("c4c6bc9d-f89a-4b61-9074-6e790c6eef8b"), 10, "ANESTHESIA", new Guid("d79f3b63-3519-4b45-a26c-befaf1d26f3b") },
                    { new Guid("18fdf8f5-965a-40a3-88e9-130b77033ccf"), 3, "EKG_MACHINE", new Guid("7f904f11-0a43-410b-86ed-82b68fdb4c6d") },
                    { new Guid("b11be0dc-45b9-4ab6-8d4d-c9b2fafad6f4"), 1, "ANESTHESIA", new Guid("f9afec07-a8e1-41ac-8e67-42be5006eca6") },
                    { new Guid("28fbfda4-0ed0-42fb-aa2e-7a93a76ada58"), 2, "EKG_MACHINE", new Guid("ce93d705-c92a-4085-b723-d28416023c75") },
                    { new Guid("f42cd330-5a2a-4b1e-b595-78b1f76dd110"), 4, "SURGICAL_TABLES", new Guid("ff9369a6-aa42-4fc1-884e-587d8d46d708") },
                    { new Guid("bace83c5-09ff-45ec-94e3-d1f9d97bbdaf"), 6, "BANDAGE", new Guid("2d08a5ab-a8fe-4f20-ae7f-49d7f0a16e97") },
                    { new Guid("61da6744-7409-42ff-9104-450f956323f7"), 15, "SURGICAL_TABLES", new Guid("edd65f01-43c3-4148-af6b-75f6b5f9bc9b") },
                    { new Guid("1fc5a396-1663-4ef2-8483-ef3a76a8f266"), 9, "SURGICAL_TABLES", new Guid("8da8f74f-d18d-41dd-9339-938dd0da0da8") }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "AppointmentState", "AppointmentType", "DoctorId", "Emergent", "PatientId", "Duration_From", "Duration_To" },
                values: new object[] { new Guid("e70399f1-fb6d-4324-bded-488efdd8e209"), 0, 0, new Guid("eaa3156c-029d-44df-a4dd-b799a64aaf89"), false, new Guid("cb15e243-2f60-4207-8427-9189fef0f9ef"), new DateTime(2022, 10, 27, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 27, 15, 15, 0, 0, DateTimeKind.Unspecified) });

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
                name: "IX_Holiday_DoctorId",
                table: "Holiday",
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
                name: "Holiday");

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
                name: "Address");
        }
    }
}
