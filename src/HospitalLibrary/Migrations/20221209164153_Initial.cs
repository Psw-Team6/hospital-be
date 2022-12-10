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
                    { new Guid("289bf081-fafe-4c30-919b-8f68b34f6187"), "Novi Sad", "Serbia", 21000, "JNA", "33" },
                    { new Guid("1897cb35-3aef-4aee-842d-69365210e93b"), "Novi Sad", "Serbia", 21000, "Partizanska", "33" },
                    { new Guid("fa8eedaf-835b-4f61-bc79-73a9f5a15ab7"), "Novi Sad", "Serbia", 21000, "Kosovska", "23A" }
                });

            migrationBuilder.InsertData(
                table: "Allergens",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("59ec3516-fd6a-4649-b8cf-9eb061e97861"), "Brufen" },
                    { new Guid("4db83b89-7892-4d78-bef8-114f85135769"), "Paracetamol" }
                });

            migrationBuilder.InsertData(
                table: "BloodUnits",
                columns: new[] { "Id", "Amount", "BloodBankName", "BloodType" },
                values: new object[,]
                {
                    { new Guid("429ad103-7afc-4a9e-bead-e939949a3539"), 7, "Moja Banka Krvi", 0 },
                    { new Guid("722a9005-ce74-4d09-b1ec-639a79e6ba6e"), 10, "Moja Banka Krvi", 7 },
                    { new Guid("898c9a19-2550-41f2-a3c9-a5beec491067"), 4, "Moja Banka Krvi", 0 }
                });

            migrationBuilder.InsertData(
                table: "Buildings",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("2e4de800-a5de-4594-9ecd-8596bd993fc7"), "Stara bolnica" },
                    { new Guid("01bd0c4b-19f0-4337-ba6e-61eed651ca0b"), "Nova bolnica" }
                });

            migrationBuilder.InsertData(
                table: "GRooms",
                columns: new[] { "Id", "Lenght", "PositionX", "PositionY", "RoomId", "Width" },
                values: new object[,]
                {
                    { new Guid("3279ddb6-30b9-4538-b5fd-2ed07d815289"), 5, 5, 0, new Guid("667ee3a7-e5f9-4610-967e-72332e89c7f0"), 5 },
                    { new Guid("8b23d5bd-c7c0-4779-83ad-35ac5fea1015"), 5, 0, 0, new Guid("b63e0855-a020-44f7-93ec-73a784344b58"), 5 },
                    { new Guid("563974eb-4c44-44ad-bb5e-5d2547fa5cdc"), 5, 5, 0, new Guid("99ba3bb6-5d7b-4c0d-9bb4-2fe3a05f064d"), 5 },
                    { new Guid("a512d2c3-a01b-461b-a1ac-4194839855d0"), 5, 5, 0, new Guid("24eb497a-0069-4a84-aa23-8d8177b72884"), 5 },
                    { new Guid("6d0df734-aa91-4893-b501-d8a9a6e6e8d5"), 5, 5, 0, new Guid("e2b4d338-8e6a-4fed-b972-e8d32a07bf25"), 5 },
                    { new Guid("7152eefe-d507-4bf4-bd97-e585cad7be5f"), 5, 5, 0, new Guid("78e3535f-d042-4de0-ba63-77c3fc9f5227"), 5 },
                    { new Guid("2aa45fd8-59d1-4f2d-9f76-87853abe46af"), 5, 5, 0, new Guid("34ada0ef-1541-4799-b71e-446f1e6e77e0"), 5 },
                    { new Guid("d22a31e3-d0df-4ba4-b13e-c750ff349713"), 5, 5, 0, new Guid("bdd61d81-a29c-4e4c-9b56-c9e49e53ad9f"), 5 },
                    { new Guid("2e3303b3-2a1c-4e6f-8e5f-6306faa140d3"), 5, 5, 0, new Guid("09385bde-5598-48c5-8dc8-ac51829ecb9b"), 5 }
                });

            migrationBuilder.InsertData(
                table: "Medicine",
                columns: new[] { "Id", "Amount", "Name" },
                values: new object[,]
                {
                    { new Guid("ed31648d-b472-4877-a4e3-076b940f5609"), 1000, "Medicine4" },
                    { new Guid("bd0fd758-2d17-4ffe-ac92-c960d2d878fd"), 1000, "Medicine3" },
                    { new Guid("bd574e72-20dd-49db-becb-16ed19ec1a0a"), 1000, "Medicine5" },
                    { new Guid("7c9ec619-55af-4159-819a-9888bb152db6"), 1000, "Medicine6" },
                    { new Guid("f413625e-5c7f-4614-9249-03a1f4c1b6ab"), 1000, "Medicine1" },
                    { new Guid("875a9eac-b5bc-48dd-9bfd-5366d5d12112"), 1, "Aspirin" },
                    { new Guid("1347f958-1c93-4f6b-b6ee-95aef29e336f"), 1000, "Medicine2" },
                    { new Guid("0157a5f1-fb72-478d-ae8a-8d1444f455e7"), 30, "Brufen 300" }
                });

            migrationBuilder.InsertData(
                table: "Specializations",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("4e4d0d85-77b8-4845-a9df-a57c6e7bb73e"), "General" },
                    { new Guid("0c38c737-8fe0-4cf8-8d22-543733ba2758"), "Surgeon" },
                    { new Guid("c2102b83-6fa4-4284-9590-4fd8246b5f86"), "Dermatology" }
                });

            migrationBuilder.InsertData(
                table: "Symptoms",
                columns: new[] { "Id", "Description" },
                values: new object[,]
                {
                    { new Guid("c8a4852d-87df-4001-bb02-6f6203a1e098"), "Stomach Aches" },
                    { new Guid("ae57f2eb-8efe-4df0-b992-e7a8609d526f"), "Nausea" },
                    { new Guid("da40e2a4-14c0-4737-9be8-03063a651d8d"), "Eye irritation" },
                    { new Guid("e8557884-6d46-4b10-a55d-ccc1e584d353"), "Runny nose" },
                    { new Guid("c06d697e-0eb0-4322-8a39-0399d83c18ec"), "Stuffy nose" },
                    { new Guid("bca3e2af-aeaa-4904-bda3-8a2d8d728ef2"), "Puffy, watery eyes" },
                    { new Guid("59d4be1a-53f2-4e1c-9a6a-58338775ead5"), "Sneezing" },
                    { new Guid("bbd070f1-37e8-49ad-8a6e-8ea318f4b474"), "High temperature" },
                    { new Guid("91be5772-4998-4c04-818d-95659998b1f0"), "Difficulty breathing" },
                    { new Guid("96265d53-2a77-442f-8afb-f4198181cde3"), "Cold" },
                    { new Guid("292a5c4c-84ee-48a6-abce-d94edde5d1a7"), "Vomiting" },
                    { new Guid("30c86ef0-82ca-4431-8bed-065393a5586f"), "Flu" },
                    { new Guid("10cb12e7-dd4f-4358-902a-abba7ed73abb"), "Headache" },
                    { new Guid("1217f0bc-4b0e-4dc9-a2a7-d3837f3c810a"), "Eye irritation" },
                    { new Guid("1c576995-cfad-4a3d-a68f-269627efdc6e"), "More intense pain and fatigue" },
                    { new Guid("ecb98a15-e943-4fbd-b628-407179a990be"), "Dry cough" },
                    { new Guid("71fd2e0c-fa78-4330-b8fc-235f5711f875"), "Sore throat" },
                    { new Guid("b9285681-b97b-4744-a309-e2a291582088"), "Abdominal pain" },
                    { new Guid("9380f144-4ffd-4c6e-9ea9-a0fabcf89475"), "Diarrhea" },
                    { new Guid("f8c56092-418d-47ec-b7d1-9ad28903493f"), "Mononucleosis" },
                    { new Guid("501e4cf4-e47c-4c95-aa01-c601d4ae8364"), "Fever" }
                });

            migrationBuilder.InsertData(
                table: "WorkingSchedules",
                columns: new[] { "Id", "DayOfWorkFrom", "DayOfWorkTo", "ExpirationFrom", "ExpirationTo" },
                values: new object[,]
                {
                    { new Guid("89eb9da1-220b-4407-8e85-7c2edb5e906d"), new DateTime(2022, 10, 27, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 27, 22, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("d6ce3d73-981a-4781-b61b-60c2e1421bf4"), new DateTime(2022, 10, 27, 8, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 12, 27, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "ApplicationUsers",
                columns: new[] { "Id", "AddressId", "Email", "Enabled", "Jmbg", "Name", "Password", "Phone", "Surname", "UserRole", "Username" },
                values: new object[,]
                {
                    { new Guid("bc7cc2d4-f23b-498a-9928-2e1825d44ba7"), new Guid("fa8eedaf-835b-4f61-bc79-73a9f5a15ab7"), "psw.isa.mail@gmail.com", true, "99999999", "Sale", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "+612222222", "Lave", 2, "Sale" },
                    { new Guid("619578ef-3c28-41a1-8ada-1e6589632003"), new Guid("289bf081-fafe-4c30-919b-8f68b34f6187"), "psw.isa.mail@gmail.com", true, "99999999", "Darko", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "+612222222", "Darkovic", 2, "Darko" },
                    { new Guid("7629d54a-c5d5-47ad-a8be-4b4bcdb0b6a5"), new Guid("289bf081-fafe-4c30-919b-8f68b34f6187"), "psw.isa.mail@gmail.com", true, "99999999", "Manja", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "+612222222", "Maric", 2, "Manja" },
                    { new Guid("0275d337-ad70-481b-9bbc-ee79df427259"), new Guid("289bf081-fafe-4c30-919b-8f68b34f6187"), "psw.isa.mail@gmail.com", true, "99999999", "Marko", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "+612222222", "Markovic", 2, "Marko" },
                    { new Guid("2613925b-da57-4a10-abaa-7455f70200f8"), new Guid("289bf081-fafe-4c30-919b-8f68b34f6187"), "psw.isa.mail@gmail.com", true, "99999999", "Nikola", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "+612222222", "Nikolic", 2, "Nikola" },
                    { new Guid("15e20d8e-a393-413f-b473-54dc7c7509a7"), new Guid("289bf081-fafe-4c30-919b-8f68b34f6187"), "psw.isa.mail@gmail.com", true, "99999999", "Mina", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "+612222222", "Minic", 2, "Mina" },
                    { new Guid("34dd85e8-cc50-4a2c-bfc5-b0ab0fb87619"), new Guid("289bf081-fafe-4c30-919b-8f68b34f6187"), "psw.isa.mail@gmail.com", true, "99999999", "Nina", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "+612222222", "Minic", 2, "Nina" },
                    { new Guid("9ab30021-d784-497a-81d0-b1242d59d9a2"), new Guid("289bf081-fafe-4c30-919b-8f68b34f6187"), "psw.isa.mail@gmail.com", true, "99999999", "Miki", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "+612222222", "Djuricic", 2, "Miki" },
                    { new Guid("bc5cfcfc-c672-41f2-9b50-1f8a00b9dc8f"), new Guid("289bf081-fafe-4c30-919b-8f68b34f6187"), "psw.isa.mail@gmail.com", true, "99999999", "Manager", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "+612222222", "Manger", 1, "Manager" },
                    { new Guid("925c76f4-7154-4fbe-9b86-3c4d2756c1e7"), new Guid("1897cb35-3aef-4aee-842d-69365210e93b"), "DjordjeLopov@gmail.com", true, "99999999", "Djordje", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "+612222222", "Vuckovic", 0, "Tadjo" },
                    { new Guid("d51e8aff-ef89-4d4a-af96-2600293b15dd"), new Guid("289bf081-fafe-4c30-919b-8f68b34f6187"), "psw.isa.mail@gmail.com", true, "99999999", "Tara", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "+612222222", "Markovic", 2, "Tara" },
                    { new Guid("87eaca5e-ccef-4560-9918-45aef4a583da"), new Guid("fa8eedaf-835b-4f61-bc79-73a9f5a15ab7"), "Cajons@gmail.com", true, "99999999", "Milos", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "+612222222", "Milosevic", 0, "Milos" },
                    { new Guid("752913c5-85cf-45d8-b16d-cd90e0155525"), new Guid("fa8eedaf-835b-4f61-bc79-73a9f5a15ab7"), "Cajons@gmail.com", true, "99999999", "Ilija", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "+612222222", "Maric", 0, "Ilija" },
                    { new Guid("01b8af2e-6cd7-49b7-a969-94ea95fad128"), new Guid("289bf081-fafe-4c30-919b-8f68b34f6187"), "psw.isa.mail@gmail.com", true, "99999999", "Filip", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "+612222222", "Filipic", 2, "Filip" }
                });

            migrationBuilder.InsertData(
                table: "BloodConsumptions",
                columns: new[] { "Id", "Amount", "BloodUnitId", "Date", "DoctorId", "Purpose" },
                values: new object[,]
                {
                    { new Guid("b88cef23-3448-434e-9ae5-ada56853e2ed"), 4, new Guid("429ad103-7afc-4a9e-bead-e939949a3539"), new DateTime(2022, 11, 14, 15, 0, 0, 0, DateTimeKind.Unspecified), new Guid("925c76f4-7154-4fbe-9b86-3c4d2756c1e7"), "operation" },
                    { new Guid("aa4fac16-aa89-498b-88f2-498cd4ba7e73"), 2, new Guid("429ad103-7afc-4a9e-bead-e939949a3539"), new DateTime(2022, 10, 27, 15, 0, 0, 0, DateTimeKind.Unspecified), new Guid("925c76f4-7154-4fbe-9b86-3c4d2756c1e7"), "operation" }
                });

            migrationBuilder.InsertData(
                table: "Floors",
                columns: new[] { "Id", "BuildingId", "FloorNumber", "Name" },
                values: new object[,]
                {
                    { new Guid("9f52b86a-9ceb-42c9-bd8f-6dcc583aa56d"), new Guid("01bd0c4b-19f0-4337-ba6e-61eed651ca0b"), 2, "F2" },
                    { new Guid("eb0e46b0-61e7-48d3-b874-54770d0405a2"), new Guid("01bd0c4b-19f0-4337-ba6e-61eed651ca0b"), 1, "F1" },
                    { new Guid("a4985846-29d2-4993-933a-6442e876cf27"), new Guid("01bd0c4b-19f0-4337-ba6e-61eed651ca0b"), 0, "F0" },
                    { new Guid("68f00be2-35f7-4c9b-a9cb-4c5dcd351c77"), new Guid("2e4de800-a5de-4594-9ecd-8596bd993fc7"), 2, "F2" },
                    { new Guid("7099a7da-3120-4b40-a6f8-526a400f03e9"), new Guid("2e4de800-a5de-4594-9ecd-8596bd993fc7"), 1, "F1" },
                    { new Guid("753d8f7e-99a7-4c26-b818-bdfb9c667c72"), new Guid("2e4de800-a5de-4594-9ecd-8596bd993fc7"), 0, "F0" }
                });

            migrationBuilder.InsertData(
                table: "Managers",
                column: "Id",
                value: new Guid("bc5cfcfc-c672-41f2-9b50-1f8a00b9dc8f"));

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "BuildingId", "FloorId", "GRoomId", "Name" },
                values: new object[,]
                {
                    { new Guid("b63e0855-a020-44f7-93ec-73a784344b58"), new Guid("2e4de800-a5de-4594-9ecd-8596bd993fc7"), new Guid("753d8f7e-99a7-4c26-b818-bdfb9c667c72"), new Guid("8b23d5bd-c7c0-4779-83ad-35ac5fea1015"), "A11" },
                    { new Guid("667ee3a7-e5f9-4610-967e-72332e89c7f0"), new Guid("2e4de800-a5de-4594-9ecd-8596bd993fc7"), new Guid("753d8f7e-99a7-4c26-b818-bdfb9c667c72"), new Guid("3279ddb6-30b9-4538-b5fd-2ed07d815289"), "B11" },
                    { new Guid("99ba3bb6-5d7b-4c0d-9bb4-2fe3a05f064d"), new Guid("2e4de800-a5de-4594-9ecd-8596bd993fc7"), new Guid("7099a7da-3120-4b40-a6f8-526a400f03e9"), new Guid("563974eb-4c44-44ad-bb5e-5d2547fa5cdc"), "A12" },
                    { new Guid("24eb497a-0069-4a84-aa23-8d8177b72884"), new Guid("2e4de800-a5de-4594-9ecd-8596bd993fc7"), new Guid("68f00be2-35f7-4c9b-a9cb-4c5dcd351c77"), new Guid("a512d2c3-a01b-461b-a1ac-4194839855d0"), "A13" },
                    { new Guid("e2b4d338-8e6a-4fed-b972-e8d32a07bf25"), new Guid("01bd0c4b-19f0-4337-ba6e-61eed651ca0b"), new Guid("a4985846-29d2-4993-933a-6442e876cf27"), new Guid("6d0df734-aa91-4893-b501-d8a9a6e6e8d5"), "A21" },
                    { new Guid("78e3535f-d042-4de0-ba63-77c3fc9f5227"), new Guid("01bd0c4b-19f0-4337-ba6e-61eed651ca0b"), new Guid("a4985846-29d2-4993-933a-6442e876cf27"), new Guid("7152eefe-d507-4bf4-bd97-e585cad7be5f"), "B21" },
                    { new Guid("34ada0ef-1541-4799-b71e-446f1e6e77e0"), new Guid("01bd0c4b-19f0-4337-ba6e-61eed651ca0b"), new Guid("eb0e46b0-61e7-48d3-b874-54770d0405a2"), new Guid("2aa45fd8-59d1-4f2d-9f76-87853abe46af"), "A22" },
                    { new Guid("bdd61d81-a29c-4e4c-9b56-c9e49e53ad9f"), new Guid("01bd0c4b-19f0-4337-ba6e-61eed651ca0b"), new Guid("9f52b86a-9ceb-42c9-bd8f-6dcc583aa56d"), new Guid("d22a31e3-d0df-4ba4-b13e-c750ff349713"), "C23" },
                    { new Guid("09385bde-5598-48c5-8dc8-ac51829ecb9b"), new Guid("01bd0c4b-19f0-4337-ba6e-61eed651ca0b"), new Guid("9f52b86a-9ceb-42c9-bd8f-6dcc583aa56d"), new Guid("2e3303b3-2a1c-4e6f-8e5f-6306faa140d3"), "B23" }
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "RoomId", "SpecializationId", "WorkingScheduleId" },
                values: new object[,]
                {
                    { new Guid("752913c5-85cf-45d8-b16d-cd90e0155525"), new Guid("b63e0855-a020-44f7-93ec-73a784344b58"), new Guid("4e4d0d85-77b8-4845-a9df-a57c6e7bb73e"), new Guid("d6ce3d73-981a-4781-b61b-60c2e1421bf4") },
                    { new Guid("87eaca5e-ccef-4560-9918-45aef4a583da"), new Guid("34ada0ef-1541-4799-b71e-446f1e6e77e0"), new Guid("4e4d0d85-77b8-4845-a9df-a57c6e7bb73e"), new Guid("d6ce3d73-981a-4781-b61b-60c2e1421bf4") },
                    { new Guid("925c76f4-7154-4fbe-9b86-3c4d2756c1e7"), new Guid("667ee3a7-e5f9-4610-967e-72332e89c7f0"), new Guid("c2102b83-6fa4-4284-9590-4fd8246b5f86"), new Guid("d6ce3d73-981a-4781-b61b-60c2e1421bf4") }
                });

            migrationBuilder.InsertData(
                table: "RoomBeds",
                columns: new[] { "Id", "IsFree", "Number", "RoomId" },
                values: new object[,]
                {
                    { new Guid("57775e63-c8d7-41aa-97c3-5a88ee441f01"), true, "12A5", new Guid("667ee3a7-e5f9-4610-967e-72332e89c7f0") },
                    { new Guid("c0e38ee6-4de1-4df0-b1c5-389605e23579"), true, "12A4", new Guid("667ee3a7-e5f9-4610-967e-72332e89c7f0") },
                    { new Guid("7c2e734d-89ba-4015-bf2c-57b2dc39742e"), true, "12A3", new Guid("667ee3a7-e5f9-4610-967e-72332e89c7f0") },
                    { new Guid("487ff11c-8495-4faa-8166-f28692f0f363"), true, "12A2", new Guid("667ee3a7-e5f9-4610-967e-72332e89c7f0") },
                    { new Guid("93990043-39ba-4a6c-b8ba-a14ef9b04bd9"), true, "12A1", new Guid("667ee3a7-e5f9-4610-967e-72332e89c7f0") },
                    { new Guid("19dbcb96-f7d0-49ed-91d6-d614df7798c6"), true, "11A4", new Guid("b63e0855-a020-44f7-93ec-73a784344b58") },
                    { new Guid("3fc4a6e4-62c8-43aa-b722-529302ca993c"), true, "11A3", new Guid("b63e0855-a020-44f7-93ec-73a784344b58") },
                    { new Guid("bba99c74-0a67-435f-b05b-d820c04476e3"), true, "11A2", new Guid("b63e0855-a020-44f7-93ec-73a784344b58") },
                    { new Guid("1a2e0b88-eabd-4118-8737-53d183b47914"), true, "11A1", new Guid("b63e0855-a020-44f7-93ec-73a784344b58") }
                });

            migrationBuilder.InsertData(
                table: "RoomEquipment",
                columns: new[] { "RoomEquipmentId", "Amount", "EquipmentName", "RoomId" },
                values: new object[,]
                {
                    { new Guid("88283c11-96db-483c-8842-cc2332d5bd52"), 3, "ANESTHESIA", new Guid("bdd61d81-a29c-4e4c-9b56-c9e49e53ad9f") },
                    { new Guid("b296ef72-df1e-4cda-8440-f90cdf819257"), 6, "SYRINGE", new Guid("e2b4d338-8e6a-4fed-b972-e8d32a07bf25") },
                    { new Guid("ebd40e43-9a99-40f6-8d07-b8fbf234caa8"), 13, "BANDAGE", new Guid("bdd61d81-a29c-4e4c-9b56-c9e49e53ad9f") },
                    { new Guid("493f3c60-7047-41c4-9de6-07f36b9ab185"), 7, "SYRINGE", new Guid("78e3535f-d042-4de0-ba63-77c3fc9f5227") },
                    { new Guid("c43fea23-19d3-48e1-aa69-9f07f5690558"), 14, "SURGICAL_TABLES", new Guid("78e3535f-d042-4de0-ba63-77c3fc9f5227") },
                    { new Guid("c85bc4b1-15e8-40fb-8b13-89b68b414442"), 15, "SURGICAL_TABLES", new Guid("e2b4d338-8e6a-4fed-b972-e8d32a07bf25") },
                    { new Guid("0002f5f7-a808-4903-b5aa-30ae40972fd8"), 6, "BANDAGE", new Guid("34ada0ef-1541-4799-b71e-446f1e6e77e0") },
                    { new Guid("69faa0c5-5e30-4f9c-9288-826c9d2bd846"), 22, "EKG_MACHINE", new Guid("e2b4d338-8e6a-4fed-b972-e8d32a07bf25") },
                    { new Guid("91111489-dcb2-4dc2-b1f3-50d000967901"), 5, "EKG_MACHINE", new Guid("667ee3a7-e5f9-4610-967e-72332e89c7f0") },
                    { new Guid("3c6e8129-cd65-4175-9d72-f3bd9ab8df03"), 11, "ANESTHESIA", new Guid("24eb497a-0069-4a84-aa23-8d8177b72884") },
                    { new Guid("79c4a4ba-6a30-4436-a488-0b3ad0b5941f"), 23, "SURGICAL_TABLES", new Guid("99ba3bb6-5d7b-4c0d-9bb4-2fe3a05f064d") },
                    { new Guid("0929a294-65f3-4578-9653-35f6730cc5d2"), 3, "EKG_MACHINE", new Guid("99ba3bb6-5d7b-4c0d-9bb4-2fe3a05f064d") },
                    { new Guid("4126117a-b2b2-4b6c-909c-a45d9f2795f2"), 9, "SURGICAL_TABLES", new Guid("09385bde-5598-48c5-8dc8-ac51829ecb9b") },
                    { new Guid("db3cb2cf-ec4f-416a-bd71-9977d237e00e"), 10, "ANESTHESIA", new Guid("667ee3a7-e5f9-4610-967e-72332e89c7f0") },
                    { new Guid("3417263e-68af-4fe2-8c68-1ce8d63174a7"), 13, "SYRINGE", new Guid("b63e0855-a020-44f7-93ec-73a784344b58") },
                    { new Guid("d2e4e48f-7656-45a4-b149-b29186a25291"), 5, "ANESTHESIA", new Guid("b63e0855-a020-44f7-93ec-73a784344b58") },
                    { new Guid("7be9c4da-0af8-448e-a4ef-db6c9d840ea1"), 15, "SURGICAL_TABLES", new Guid("b63e0855-a020-44f7-93ec-73a784344b58") },
                    { new Guid("23647ce5-0678-45a2-a917-a1ab00085792"), 4, "BANDAGE", new Guid("24eb497a-0069-4a84-aa23-8d8177b72884") },
                    { new Guid("0b120cc6-a3c8-4e7e-9996-36af118f6894"), 9, "ANESTHESIA", new Guid("09385bde-5598-48c5-8dc8-ac51829ecb9b") }
                });

            migrationBuilder.InsertData(
                table: "Holidays",
                columns: new[] { "Id", "Description", "DoctorId", "HolidayStatus", "IsUrgent", "DateRange_From", "DateRange_To" },
                values: new object[] { new Guid("ede11f4d-cc98-4788-a89c-9eb93a0f9807"), "I want to go to Paralia", new Guid("752913c5-85cf-45d8-b16d-cd90e0155525"), 0, false, new DateTime(2022, 10, 27, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 27, 10, 30, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "Age", "BloodType", "DateOfBirth", "DoctorId", "Gender" },
                values: new object[,]
                {
                    { new Guid("bc7cc2d4-f23b-498a-9928-2e1825d44ba7"), 15, 4, new DateTime(2007, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("752913c5-85cf-45d8-b16d-cd90e0155525"), 0 },
                    { new Guid("9ab30021-d784-497a-81d0-b1242d59d9a2"), 32, 0, new DateTime(1990, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("752913c5-85cf-45d8-b16d-cd90e0155525"), 0 },
                    { new Guid("34dd85e8-cc50-4a2c-bfc5-b0ab0fb87619"), 5, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("752913c5-85cf-45d8-b16d-cd90e0155525"), 1 },
                    { new Guid("2613925b-da57-4a10-abaa-7455f70200f8"), 18, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("752913c5-85cf-45d8-b16d-cd90e0155525"), 2 },
                    { new Guid("619578ef-3c28-41a1-8ada-1e6589632003"), 70, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("925c76f4-7154-4fbe-9b86-3c4d2756c1e7"), 0 },
                    { new Guid("01b8af2e-6cd7-49b7-a969-94ea95fad128"), 56, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("925c76f4-7154-4fbe-9b86-3c4d2756c1e7"), 0 },
                    { new Guid("15e20d8e-a393-413f-b473-54dc7c7509a7"), 9, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("87eaca5e-ccef-4560-9918-45aef4a583da"), 1 },
                    { new Guid("0275d337-ad70-481b-9bbc-ee79df427259"), 65, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("87eaca5e-ccef-4560-9918-45aef4a583da"), 0 },
                    { new Guid("7629d54a-c5d5-47ad-a8be-4b4bcdb0b6a5"), 50, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("87eaca5e-ccef-4560-9918-45aef4a583da"), 1 },
                    { new Guid("d51e8aff-ef89-4d4a-af96-2600293b15dd"), 61, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("87eaca5e-ccef-4560-9918-45aef4a583da"), 1 }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "AppointmentState", "AppointmentType", "DoctorId", "Emergent", "PatientId", "Duration_From", "Duration_To" },
                values: new object[] { new Guid("8cb2ee95-2737-479a-a9c5-784ffe61000e"), 0, 0, new Guid("752913c5-85cf-45d8-b16d-cd90e0155525"), false, new Guid("bc7cc2d4-f23b-498a-9928-2e1825d44ba7"), new DateTime(2023, 7, 27, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 27, 10, 30, 0, 0, DateTimeKind.Unspecified) });

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
