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
                name: "Medicine",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicine", x => x.Id);
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
                name: "Symptoms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Symptoms", x => x.Id);
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
                    EquipmentId = table.Column<Guid>(type: "uuid", nullable: false),
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
                    DateOfBirth = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uuid", nullable: false),
                    BloodType = table.Column<int>(type: "integer", nullable: true)
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
                name: "Examinations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AppointmentId = table.Column<Guid>(type: "uuid", nullable: true),
                    Anamnesis = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Examinations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Examinations_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "ExaminationPrescription",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Usage = table.Column<string>(type: "text", nullable: true),
                    ExaminationId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExaminationPrescription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExaminationPrescription_Examinations_ExaminationId",
                        column: x => x.ExaminationId,
                        principalTable: "Examinations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExaminationSymptom",
                columns: table => new
                {
                    ExaminationsId = table.Column<Guid>(type: "uuid", nullable: false),
                    SymptomsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExaminationSymptom", x => new { x.ExaminationsId, x.SymptomsId });
                    table.ForeignKey(
                        name: "FK_ExaminationSymptom_Examinations_ExaminationsId",
                        column: x => x.ExaminationsId,
                        principalTable: "Examinations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExaminationSymptom_Symptoms_SymptomsId",
                        column: x => x.SymptomsId,
                        principalTable: "Symptoms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BloodPrescription",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BloodType = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    TreatmentReportId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodPrescription", x => x.Id);
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
                    Description = table.Column<string>(type: "text", nullable: true),
                    TreatmentReportId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicinePrescription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicinePrescription_Medicine_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "Medicine",
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
                name: "ExaminationPrescriptionMedicine",
                columns: table => new
                {
                    ExaminationPrescriptionsId = table.Column<Guid>(type: "uuid", nullable: false),
                    MedicinesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExaminationPrescriptionMedicine", x => new { x.ExaminationPrescriptionsId, x.MedicinesId });
                    table.ForeignKey(
                        name: "FK_ExaminationPrescriptionMedicine_ExaminationPrescription_Exa~",
                        column: x => x.ExaminationPrescriptionsId,
                        principalTable: "ExaminationPrescription",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExaminationPrescriptionMedicine_Medicine_MedicinesId",
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
                    { new Guid("c34e9b64-77d5-43ff-b830-7c7f6fac4ee3"), "Novi Sad", "Serbia", 21000, "JNA", "33" },
                    { new Guid("0b131acf-6d8c-482d-886e-1735ca24ef97"), "Novi Sad", "Serbia", 21000, "Partizanska", "33" },
                    { new Guid("fb242af9-6adc-4e45-82ba-68d880cf604c"), "Novi Sad", "Serbia", 21000, "Kosovska", "23A" }
                });

            migrationBuilder.InsertData(
                table: "Allergens",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("9b66b762-7e23-4a08-aaa7-dc39b1fd69fa"), "Brufen" },
                    { new Guid("8882f0e0-4d29-47d1-9507-ef1007cb93e5"), "Paracetamol" }
                });

            migrationBuilder.InsertData(
                table: "BloodUnits",
                columns: new[] { "Id", "Amount", "BloodBankName", "BloodType" },
                values: new object[,]
                {
                    { new Guid("42c4376a-a04d-4dc3-a3f4-43d6c9eb5489"), 7, "Moja Banka Krvi", 0 },
                    { new Guid("69695cf8-6ff3-4b52-8935-dddf4831321d"), 10, "Moja Banka Krvi", 7 },
                    { new Guid("1e9e229b-0099-4f1d-bd8d-2fef5b596922"), 4, "Moja Banka Krvi", 0 }
                });

            migrationBuilder.InsertData(
                table: "Buildings",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("316c8a5f-574f-4c82-8d13-ee43adf05df0"), "Stara bolnica" },
                    { new Guid("2c9826f3-b7ab-406c-9d2f-5194750af822"), "Nova bolnica" }
                });

            migrationBuilder.InsertData(
                table: "GRooms",
                columns: new[] { "Id", "Lenght", "PositionX", "PositionY", "RoomId", "Width" },
                values: new object[,]
                {
                    { new Guid("82fa10d4-f780-44f3-b6ad-ca1459421d30"), 5, 5, 0, new Guid("1fbf573f-2a35-45cb-8d51-31d741aa020b"), 5 },
                    { new Guid("9d643916-6720-46a1-bb92-937fe07cdf34"), 5, 0, 0, new Guid("5822ac5e-ee1b-49a1-ba39-edfb59ac1796"), 5 },
                    { new Guid("6d6b9eac-75e7-4679-8837-19c999d6fb54"), 5, 5, 0, new Guid("a0541e4e-f66f-4cc7-ab53-d8b925cb9c6f"), 5 },
                    { new Guid("fed76441-4be7-4fbc-810a-4ed33065c91d"), 5, 5, 0, new Guid("f676e1d5-6cae-455d-ae3a-0f980a591993"), 5 },
                    { new Guid("cff8d720-3c95-4c13-9458-23760aebd14b"), 5, 5, 0, new Guid("e65c57d1-5a6e-4e36-bfa2-8377fd9b94ca"), 5 },
                    { new Guid("7e1a0f35-4573-44e7-93c1-4718ab309d2f"), 5, 5, 0, new Guid("982ff669-899a-40a0-ab52-329f33a67144"), 5 },
                    { new Guid("2de17ebe-5279-4542-84fa-58b73a9a895f"), 5, 5, 0, new Guid("e35adbfc-0e2e-44dd-914f-26e4f48e4627"), 5 },
                    { new Guid("b8635b03-cb24-4522-8393-5a3ed5af387a"), 5, 5, 0, new Guid("de96badc-d131-4dc8-8634-fa34ac162f3c"), 5 },
                    { new Guid("857efb03-bbc6-4037-9b2d-46319c4eeded"), 5, 5, 0, new Guid("1acf32b4-2e32-4477-876d-858ffaa57985"), 5 }
                });

            migrationBuilder.InsertData(
                table: "Medicine",
                columns: new[] { "Id", "Amount", "Name" },
                values: new object[,]
                {
                    { new Guid("ae1ba087-b238-478d-9726-e9434a8c0fb1"), 1000, "Medicine4" },
                    { new Guid("c86b588c-d498-4136-9a0b-bf1f652887fb"), 1000, "Medicine3" },
                    { new Guid("4200d440-821d-4aff-9af5-94f4b031b7d4"), 1000, "Medicine5" },
                    { new Guid("c35444a1-0454-4abd-b976-8c37965637d8"), 1000, "Medicine6" },
                    { new Guid("6e8543b0-f350-4f3e-885e-28e53b42c9dd"), 1000, "Medicine1" },
                    { new Guid("c88cfa7b-eb10-492e-9f6f-c6e366aa991e"), 1, "Aspirin" },
                    { new Guid("b8a5a098-e2a9-4337-a16b-3fa255e46a80"), 1000, "Medicine2" },
                    { new Guid("fd3c2ee0-b15a-4eef-9dd0-15c26e99dc71"), 30, "Brufen 300" }
                });

            migrationBuilder.InsertData(
                table: "Specializations",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("f2041a62-f207-4011-a88c-0444db4313f0"), "General" },
                    { new Guid("e50fc52c-406d-45f8-8446-31fa1e5e7885"), "Surgeon" },
                    { new Guid("91197d78-962c-4ce5-8162-cc1f89198e62"), "Dermatology" }
                });

            migrationBuilder.InsertData(
                table: "Symptoms",
                columns: new[] { "Id", "Description" },
                values: new object[,]
                {
                    { new Guid("32415b16-74de-4881-aca5-fa607f7cbe09"), "Stomach Aches" },
                    { new Guid("442b01c3-ac23-4e87-8f9d-6e557818524d"), "Nausea" },
                    { new Guid("58764845-3350-4172-96fe-28cea27ee627"), "Eye irritation" },
                    { new Guid("5a032ed6-5426-4e2a-aa9b-23123916a061"), "Runny nose" },
                    { new Guid("0f5bd449-ae50-4717-89a7-c48c6bd30d40"), "Stuffy nose" },
                    { new Guid("e2e3f03f-57f8-4091-9680-86e1a309befc"), "Puffy, watery eyes" },
                    { new Guid("5f5a90e4-3c72-4e08-922a-fd6d009b1dd8"), "Sneezing" },
                    { new Guid("0b0008d7-f780-43c9-86c4-a43ac83bb927"), "High temperature" },
                    { new Guid("9fa38511-b5d1-4539-8ac6-afdd403c0934"), "Difficulty breathing" },
                    { new Guid("9f6f1468-e212-40b3-95d1-0799ca87d13d"), "Cold" },
                    { new Guid("913bd815-424c-4e40-a59e-6e16760ff265"), "Vomiting" },
                    { new Guid("f4e63bee-441c-49c3-8bb7-6bf74e9885df"), "Flu" },
                    { new Guid("987c2226-1076-4062-bee3-054ef47de7c4"), "Headache" },
                    { new Guid("d4c8dda1-7184-4af6-93e1-e572e412f9b2"), "Eye irritation" },
                    { new Guid("e692f5f0-a4ef-4441-81a5-e0d8e951cb23"), "More intense pain and fatigue" },
                    { new Guid("ae89ea76-b54a-495b-b4be-2b3edfc99e17"), "Dry cough" },
                    { new Guid("29f2e1ce-c56f-492d-9c28-1f687575e7fb"), "Sore throat" },
                    { new Guid("c5c1ff91-0ec1-4f8e-adb2-4ded14e89e81"), "Abdominal pain" },
                    { new Guid("63d67c06-dd56-4fe9-859a-6ad9c18de391"), "Diarrhea" },
                    { new Guid("a15da672-a12e-407f-b6b2-51581379b066"), "Mononucleosis" },
                    { new Guid("1ae88643-d685-445e-8b5d-f8d28e813d3d"), "Fever" }
                });

            migrationBuilder.InsertData(
                table: "WorkingSchedules",
                columns: new[] { "Id", "DayOfWorkFrom", "DayOfWorkTo", "ExpirationFrom", "ExpirationTo" },
                values: new object[,]
                {
                    { new Guid("0aa2fc48-0fc1-4269-bf2d-076a6fd4957a"), new DateTime(2022, 10, 27, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 27, 22, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44b3ae2f-7bed-434d-9151-d59045fdbaa4"), new DateTime(2022, 10, 27, 8, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 12, 27, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "ApplicationUsers",
                columns: new[] { "Id", "AddressId", "Email", "Enabled", "Jmbg", "Name", "Password", "Phone", "Surname", "UserRole", "Username" },
                values: new object[,]
                {
                    { new Guid("f5d76e1b-86c7-441f-99f0-3d9e00340167"), new Guid("fb242af9-6adc-4e45-82ba-68d880cf604c"), "psw.isa.mail@gmail.com", true, "99999999", "Sale", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "+612222222", "Lave", 2, "Sale" },
                    { new Guid("805a0a8d-4a0c-4bb2-9eda-819790234be3"), new Guid("c34e9b64-77d5-43ff-b830-7c7f6fac4ee3"), "psw.isa.mail@gmail.com", true, "99999999", "Darko", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "+612222222", "Darkovic", 2, "Darko" },
                    { new Guid("3218b827-4252-43f5-9d38-c48f36a006b9"), new Guid("c34e9b64-77d5-43ff-b830-7c7f6fac4ee3"), "psw.isa.mail@gmail.com", true, "99999999", "Manja", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "+612222222", "Maric", 2, "Manja" },
                    { new Guid("c77663eb-0220-453d-a03e-50b32d04b4ef"), new Guid("c34e9b64-77d5-43ff-b830-7c7f6fac4ee3"), "psw.isa.mail@gmail.com", true, "99999999", "Marko", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "+612222222", "Markovic", 2, "Marko" },
                    { new Guid("732a2989-5de0-4010-87ca-05e01950d1b7"), new Guid("c34e9b64-77d5-43ff-b830-7c7f6fac4ee3"), "psw.isa.mail@gmail.com", true, "99999999", "Nikola", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "+612222222", "Nikolic", 2, "Nikola" },
                    { new Guid("5dc557aa-56fb-4147-9620-dedaf292af07"), new Guid("c34e9b64-77d5-43ff-b830-7c7f6fac4ee3"), "psw.isa.mail@gmail.com", true, "99999999", "Mina", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "+612222222", "Minic", 2, "Mina" },
                    { new Guid("d2c9fe07-ae96-4adf-bfca-4ac11bb2041d"), new Guid("c34e9b64-77d5-43ff-b830-7c7f6fac4ee3"), "psw.isa.mail@gmail.com", true, "99999999", "Nina", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "+612222222", "Minic", 2, "Nina" },
                    { new Guid("b5769636-3262-4bf3-866a-fbbb3e132047"), new Guid("c34e9b64-77d5-43ff-b830-7c7f6fac4ee3"), "psw.isa.mail@gmail.com", true, "99999999", "Miki", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "+612222222", "Djuricic", 2, "Miki" },
                    { new Guid("5ceffbb5-ac1f-4648-8e31-28edd59bcf4a"), new Guid("c34e9b64-77d5-43ff-b830-7c7f6fac4ee3"), "psw.isa.mail@gmail.com", true, "99999999", "Manager", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "+612222222", "Manger", 1, "Manager" },
                    { new Guid("ddedb9ec-d8ca-47de-8888-e5bf974ba27c"), new Guid("0b131acf-6d8c-482d-886e-1735ca24ef97"), "DjordjeLopov@gmail.com", true, "99999999", "Djordje", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "+612222222", "Vuckovic", 0, "Tadjo" },
                    { new Guid("9db9673a-23bb-4f7e-9da8-1df8d7fdc0e9"), new Guid("c34e9b64-77d5-43ff-b830-7c7f6fac4ee3"), "psw.isa.mail@gmail.com", true, "99999999", "Tara", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "+612222222", "Markovic", 2, "Tara" },
                    { new Guid("260fefbd-cf30-49c5-bc01-a38208e0c8e7"), new Guid("fb242af9-6adc-4e45-82ba-68d880cf604c"), "Cajons@gmail.com", true, "99999999", "Milos", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "+612222222", "Milosevic", 0, "Milos" },
                    { new Guid("f67e9046-2c6d-49ec-b85b-deed1ec71018"), new Guid("fb242af9-6adc-4e45-82ba-68d880cf604c"), "Cajons@gmail.com", true, "99999999", "Ilija", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "+612222222", "Maric", 0, "Ilija" },
                    { new Guid("e05adc55-a568-4070-ba67-add9ec9fc455"), new Guid("c34e9b64-77d5-43ff-b830-7c7f6fac4ee3"), "psw.isa.mail@gmail.com", true, "99999999", "Filip", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "+612222222", "Filipic", 2, "Filip" }
                });

            migrationBuilder.InsertData(
                table: "BloodConsumptions",
                columns: new[] { "Id", "Amount", "BloodUnitId", "Date", "DoctorId", "Purpose" },
                values: new object[,]
                {
                    { new Guid("ed7b1d7c-03c7-4bd9-bbfa-36d589cd5c81"), 4, new Guid("42c4376a-a04d-4dc3-a3f4-43d6c9eb5489"), new DateTime(2022, 11, 14, 15, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ddedb9ec-d8ca-47de-8888-e5bf974ba27c"), "operation" },
                    { new Guid("44c7f7f3-a50a-451c-8bac-8e8846f78b22"), 2, new Guid("42c4376a-a04d-4dc3-a3f4-43d6c9eb5489"), new DateTime(2022, 10, 27, 15, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ddedb9ec-d8ca-47de-8888-e5bf974ba27c"), "operation" }
                });

            migrationBuilder.InsertData(
                table: "Floors",
                columns: new[] { "Id", "BuildingId", "FloorNumber", "Name" },
                values: new object[,]
                {
                    { new Guid("3c779b2c-19cc-47f2-bf09-cfc125fba07c"), new Guid("2c9826f3-b7ab-406c-9d2f-5194750af822"), 2, "F2" },
                    { new Guid("940c2cf4-c760-4bab-ae05-4d5864353da3"), new Guid("2c9826f3-b7ab-406c-9d2f-5194750af822"), 1, "F1" },
                    { new Guid("a903ad19-ff23-48fd-855e-dc2e1cd57dcc"), new Guid("2c9826f3-b7ab-406c-9d2f-5194750af822"), 0, "F0" },
                    { new Guid("ce598158-d365-4125-abc0-8f63003b0ea2"), new Guid("316c8a5f-574f-4c82-8d13-ee43adf05df0"), 2, "F2" },
                    { new Guid("fe01c60e-41ef-4e50-8666-a708ecedd030"), new Guid("316c8a5f-574f-4c82-8d13-ee43adf05df0"), 1, "F1" },
                    { new Guid("b692050e-cdbd-4485-a9d7-b0ae3da70c41"), new Guid("316c8a5f-574f-4c82-8d13-ee43adf05df0"), 0, "F0" }
                });

            migrationBuilder.InsertData(
                table: "Managers",
                column: "Id",
                value: new Guid("5ceffbb5-ac1f-4648-8e31-28edd59bcf4a"));

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "BuildingId", "FloorId", "GRoomId", "Name" },
                values: new object[,]
                {
                    { new Guid("5822ac5e-ee1b-49a1-ba39-edfb59ac1796"), new Guid("316c8a5f-574f-4c82-8d13-ee43adf05df0"), new Guid("b692050e-cdbd-4485-a9d7-b0ae3da70c41"), new Guid("9d643916-6720-46a1-bb92-937fe07cdf34"), "A11" },
                    { new Guid("1fbf573f-2a35-45cb-8d51-31d741aa020b"), new Guid("316c8a5f-574f-4c82-8d13-ee43adf05df0"), new Guid("b692050e-cdbd-4485-a9d7-b0ae3da70c41"), new Guid("82fa10d4-f780-44f3-b6ad-ca1459421d30"), "B11" },
                    { new Guid("a0541e4e-f66f-4cc7-ab53-d8b925cb9c6f"), new Guid("316c8a5f-574f-4c82-8d13-ee43adf05df0"), new Guid("fe01c60e-41ef-4e50-8666-a708ecedd030"), new Guid("6d6b9eac-75e7-4679-8837-19c999d6fb54"), "A12" },
                    { new Guid("f676e1d5-6cae-455d-ae3a-0f980a591993"), new Guid("316c8a5f-574f-4c82-8d13-ee43adf05df0"), new Guid("ce598158-d365-4125-abc0-8f63003b0ea2"), new Guid("fed76441-4be7-4fbc-810a-4ed33065c91d"), "A13" },
                    { new Guid("e65c57d1-5a6e-4e36-bfa2-8377fd9b94ca"), new Guid("2c9826f3-b7ab-406c-9d2f-5194750af822"), new Guid("a903ad19-ff23-48fd-855e-dc2e1cd57dcc"), new Guid("cff8d720-3c95-4c13-9458-23760aebd14b"), "A21" },
                    { new Guid("982ff669-899a-40a0-ab52-329f33a67144"), new Guid("2c9826f3-b7ab-406c-9d2f-5194750af822"), new Guid("a903ad19-ff23-48fd-855e-dc2e1cd57dcc"), new Guid("7e1a0f35-4573-44e7-93c1-4718ab309d2f"), "B21" },
                    { new Guid("e35adbfc-0e2e-44dd-914f-26e4f48e4627"), new Guid("2c9826f3-b7ab-406c-9d2f-5194750af822"), new Guid("940c2cf4-c760-4bab-ae05-4d5864353da3"), new Guid("2de17ebe-5279-4542-84fa-58b73a9a895f"), "A22" },
                    { new Guid("de96badc-d131-4dc8-8634-fa34ac162f3c"), new Guid("2c9826f3-b7ab-406c-9d2f-5194750af822"), new Guid("3c779b2c-19cc-47f2-bf09-cfc125fba07c"), new Guid("b8635b03-cb24-4522-8393-5a3ed5af387a"), "C23" },
                    { new Guid("1acf32b4-2e32-4477-876d-858ffaa57985"), new Guid("2c9826f3-b7ab-406c-9d2f-5194750af822"), new Guid("3c779b2c-19cc-47f2-bf09-cfc125fba07c"), new Guid("857efb03-bbc6-4037-9b2d-46319c4eeded"), "B23" }
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "RoomId", "SpecializationId", "WorkingScheduleId" },
                values: new object[,]
                {
                    { new Guid("f67e9046-2c6d-49ec-b85b-deed1ec71018"), new Guid("5822ac5e-ee1b-49a1-ba39-edfb59ac1796"), new Guid("f2041a62-f207-4011-a88c-0444db4313f0"), new Guid("44b3ae2f-7bed-434d-9151-d59045fdbaa4") },
                    { new Guid("260fefbd-cf30-49c5-bc01-a38208e0c8e7"), new Guid("e35adbfc-0e2e-44dd-914f-26e4f48e4627"), new Guid("f2041a62-f207-4011-a88c-0444db4313f0"), new Guid("44b3ae2f-7bed-434d-9151-d59045fdbaa4") },
                    { new Guid("ddedb9ec-d8ca-47de-8888-e5bf974ba27c"), new Guid("1fbf573f-2a35-45cb-8d51-31d741aa020b"), new Guid("91197d78-962c-4ce5-8162-cc1f89198e62"), new Guid("44b3ae2f-7bed-434d-9151-d59045fdbaa4") }
                });

            migrationBuilder.InsertData(
                table: "RoomBeds",
                columns: new[] { "Id", "IsFree", "Number", "RoomId" },
                values: new object[,]
                {
                    { new Guid("26b1ca6d-9903-4322-adac-3d965b1eb269"), true, "12A5", new Guid("1fbf573f-2a35-45cb-8d51-31d741aa020b") },
                    { new Guid("5a89443c-aadc-44a8-9b39-4a815da77d3c"), true, "12A4", new Guid("1fbf573f-2a35-45cb-8d51-31d741aa020b") },
                    { new Guid("63390cf4-46de-407c-823b-2184ce2bd646"), true, "12A3", new Guid("1fbf573f-2a35-45cb-8d51-31d741aa020b") },
                    { new Guid("b099867d-d3f3-4626-b029-d6eea4fda412"), true, "12A2", new Guid("1fbf573f-2a35-45cb-8d51-31d741aa020b") },
                    { new Guid("7662422a-5222-4a22-ba40-30a466882218"), true, "12A1", new Guid("1fbf573f-2a35-45cb-8d51-31d741aa020b") },
                    { new Guid("a8c2afc1-fabd-45b8-bba1-d56ad58357bf"), true, "11A4", new Guid("5822ac5e-ee1b-49a1-ba39-edfb59ac1796") },
                    { new Guid("29079aa6-a1ca-4d3e-9df7-8e59ab52441e"), true, "11A3", new Guid("5822ac5e-ee1b-49a1-ba39-edfb59ac1796") },
                    { new Guid("72e0f33a-161c-48ea-b515-4ebefd25173a"), true, "11A2", new Guid("5822ac5e-ee1b-49a1-ba39-edfb59ac1796") },
                    { new Guid("5b7a45e3-7e95-4806-b23e-b3838d1efdba"), true, "11A1", new Guid("5822ac5e-ee1b-49a1-ba39-edfb59ac1796") }
                });

            migrationBuilder.InsertData(
                table: "RoomEquipment",
                columns: new[] { "RoomEquipmentId", "Amount", "EquipmentName", "RoomId" },
                values: new object[,]
                {
                    { new Guid("2038fe8b-82cf-45d9-8c7f-f1228afa1c15"), 3, "ANESTHESIA", new Guid("de96badc-d131-4dc8-8634-fa34ac162f3c") },
                    { new Guid("637840b4-7df0-440c-ab84-5d940089e95f"), 6, "SYRINGE", new Guid("e65c57d1-5a6e-4e36-bfa2-8377fd9b94ca") },
                    { new Guid("3fa17e21-35fd-48a1-86ce-c26f88820eef"), 13, "BANDAGE", new Guid("de96badc-d131-4dc8-8634-fa34ac162f3c") },
                    { new Guid("212a1c34-a4cb-47bb-bba5-1f6b30170717"), 7, "SYRINGE", new Guid("982ff669-899a-40a0-ab52-329f33a67144") },
                    { new Guid("84ad2ac2-b371-4bca-8b1b-d82a62d26714"), 14, "SURGICAL_TABLES", new Guid("982ff669-899a-40a0-ab52-329f33a67144") },
                    { new Guid("47e7f8d1-94fa-4f1f-8cee-a97f003e0e43"), 15, "SURGICAL_TABLES", new Guid("e65c57d1-5a6e-4e36-bfa2-8377fd9b94ca") },
                    { new Guid("3464c2c5-4fcc-4d18-a268-6e088618dd05"), 6, "BANDAGE", new Guid("e35adbfc-0e2e-44dd-914f-26e4f48e4627") },
                    { new Guid("b3ddb903-e73b-4885-a308-0c90eed7b613"), 22, "EKG_MACHINE", new Guid("e65c57d1-5a6e-4e36-bfa2-8377fd9b94ca") },
                    { new Guid("4d41fe40-82ee-4e9d-96fa-bfd7a1104773"), 5, "EKG_MACHINE", new Guid("1fbf573f-2a35-45cb-8d51-31d741aa020b") },
                    { new Guid("727f2b1c-e158-41e7-b51d-dcdaea2b60e6"), 11, "ANESTHESIA", new Guid("f676e1d5-6cae-455d-ae3a-0f980a591993") },
                    { new Guid("f6e85fc9-52f0-42b7-a3bf-066bec361388"), 23, "SURGICAL_TABLES", new Guid("a0541e4e-f66f-4cc7-ab53-d8b925cb9c6f") },
                    { new Guid("eed2d824-bf34-4bd6-9911-c79ba2b9dce3"), 3, "EKG_MACHINE", new Guid("a0541e4e-f66f-4cc7-ab53-d8b925cb9c6f") },
                    { new Guid("6baa1caa-b808-4016-813e-efdbc297b2c1"), 9, "SURGICAL_TABLES", new Guid("1acf32b4-2e32-4477-876d-858ffaa57985") },
                    { new Guid("86730800-6e9e-4c09-99ad-155b73139882"), 10, "ANESTHESIA", new Guid("1fbf573f-2a35-45cb-8d51-31d741aa020b") },
                    { new Guid("8fcd9244-6363-4fed-8c27-db29f8a7e862"), 13, "SYRINGE", new Guid("5822ac5e-ee1b-49a1-ba39-edfb59ac1796") },
                    { new Guid("f60b5699-a07c-42f1-afa1-4e105aa8abd1"), 5, "ANESTHESIA", new Guid("5822ac5e-ee1b-49a1-ba39-edfb59ac1796") },
                    { new Guid("6619a15c-e0b3-4403-ade4-c2c6cd326330"), 15, "SURGICAL_TABLES", new Guid("5822ac5e-ee1b-49a1-ba39-edfb59ac1796") },
                    { new Guid("c5ef8617-826e-4acc-864d-3ebae03abab3"), 4, "BANDAGE", new Guid("f676e1d5-6cae-455d-ae3a-0f980a591993") },
                    { new Guid("43513a83-d9b4-40d7-8fa3-7d5d9db95a95"), 9, "ANESTHESIA", new Guid("1acf32b4-2e32-4477-876d-858ffaa57985") }
                });

            migrationBuilder.InsertData(
                table: "Holidays",
                columns: new[] { "Id", "Description", "DoctorId", "HolidayStatus", "IsUrgent", "DateRange_From", "DateRange_To" },
                values: new object[] { new Guid("f685b335-0e1a-4b41-b6e1-b72f3ed33388"), "I want to go to Paralia", new Guid("f67e9046-2c6d-49ec-b85b-deed1ec71018"), 0, false, new DateTime(2022, 10, 27, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 27, 10, 30, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "Age", "BloodType", "DateOfBirth", "DoctorId", "Gender" },
                values: new object[,]
                {
                    { new Guid("f5d76e1b-86c7-441f-99f0-3d9e00340167"), 15, 4, new DateTime(2007, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f67e9046-2c6d-49ec-b85b-deed1ec71018"), 0 },
                    { new Guid("b5769636-3262-4bf3-866a-fbbb3e132047"), 32, 0, new DateTime(1990, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f67e9046-2c6d-49ec-b85b-deed1ec71018"), 0 },
                    { new Guid("d2c9fe07-ae96-4adf-bfca-4ac11bb2041d"), 5, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f67e9046-2c6d-49ec-b85b-deed1ec71018"), 1 },
                    { new Guid("732a2989-5de0-4010-87ca-05e01950d1b7"), 18, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f67e9046-2c6d-49ec-b85b-deed1ec71018"), 2 },
                    { new Guid("805a0a8d-4a0c-4bb2-9eda-819790234be3"), 70, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ddedb9ec-d8ca-47de-8888-e5bf974ba27c"), 0 },
                    { new Guid("e05adc55-a568-4070-ba67-add9ec9fc455"), 56, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ddedb9ec-d8ca-47de-8888-e5bf974ba27c"), 0 },
                    { new Guid("5dc557aa-56fb-4147-9620-dedaf292af07"), 9, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("260fefbd-cf30-49c5-bc01-a38208e0c8e7"), 1 },
                    { new Guid("c77663eb-0220-453d-a03e-50b32d04b4ef"), 65, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("260fefbd-cf30-49c5-bc01-a38208e0c8e7"), 0 },
                    { new Guid("3218b827-4252-43f5-9d38-c48f36a006b9"), 50, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("260fefbd-cf30-49c5-bc01-a38208e0c8e7"), 1 },
                    { new Guid("9db9673a-23bb-4f7e-9da8-1df8d7fdc0e9"), 61, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("260fefbd-cf30-49c5-bc01-a38208e0c8e7"), 1 }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "AppointmentState", "AppointmentType", "DoctorId", "Emergent", "PatientId", "Duration_From", "Duration_To" },
                values: new object[] { new Guid("eee3ef6c-c4b3-4d42-b0e3-3ebe5de68f21"), 0, 0, new Guid("f67e9046-2c6d-49ec-b85b-deed1ec71018"), false, new Guid("f5d76e1b-86c7-441f-99f0-3d9e00340167"), new DateTime(2023, 7, 27, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 27, 10, 30, 0, 0, DateTimeKind.Unspecified) });

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
                name: "IX_ExaminationPrescription_ExaminationId",
                table: "ExaminationPrescription",
                column: "ExaminationId");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationPrescriptionMedicine_MedicinesId",
                table: "ExaminationPrescriptionMedicine",
                column: "MedicinesId");

            migrationBuilder.CreateIndex(
                name: "IX_Examinations_AppointmentId",
                table: "Examinations",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationSymptom_SymptomsId",
                table: "ExaminationSymptom",
                column: "SymptomsId");

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
                name: "IX_MedicinePrescription_MedicineId",
                table: "MedicinePrescription",
                column: "MedicineId");

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
                name: "BloodConsumptions");

            migrationBuilder.DropTable(
                name: "BloodPrescription");

            migrationBuilder.DropTable(
                name: "EquipmentMovementAppointment");

            migrationBuilder.DropTable(
                name: "ExaminationPrescriptionMedicine");

            migrationBuilder.DropTable(
                name: "ExaminationSymptom");

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
                name: "MedicinePrescription");

            migrationBuilder.DropTable(
                name: "RoomEquipment");

            migrationBuilder.DropTable(
                name: "Allergens");

            migrationBuilder.DropTable(
                name: "BloodUnits");

            migrationBuilder.DropTable(
                name: "ExaminationPrescription");

            migrationBuilder.DropTable(
                name: "Symptoms");

            migrationBuilder.DropTable(
                name: "Ingredient");

            migrationBuilder.DropTable(
                name: "Medicine");

            migrationBuilder.DropTable(
                name: "TreatmentReport");

            migrationBuilder.DropTable(
                name: "Examinations");

            migrationBuilder.DropTable(
                name: "PatientAdmissions");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "RoomBeds");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Doctors");

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
