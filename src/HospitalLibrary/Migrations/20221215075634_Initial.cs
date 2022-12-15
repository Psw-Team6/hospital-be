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
                    Type = table.Column<int>(type: "integer", nullable: false),
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
                name: "Consiliums",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Theme = table.Column<string>(type: "text", nullable: true),
                    TimeRange_From = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    TimeRange_To = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    TimeRange_Duration = table.Column<int>(type: "integer", nullable: true),
                    RoomId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consiliums", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Consiliums_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "RoomMerging",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Room1Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Room2Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DatesForSearch_From = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DatesForSearch_To = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Duration = table.Column<TimeSpan>(type: "interval", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomMerging", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomMerging_Rooms_Room1Id",
                        column: x => x.Room1Id,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoomMerging_Rooms_Room2Id",
                        column: x => x.Room2Id,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoomSpliting",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RoomId = table.Column<Guid>(type: "uuid", nullable: false),
                    DatesForSearch_From = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DatesForSearch_To = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Duration = table.Column<TimeSpan>(type: "interval", nullable: false),
                    newRoomName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomSpliting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomSpliting_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConsiliumDoctor",
                columns: table => new
                {
                    ConsiliumsId = table.Column<Guid>(type: "uuid", nullable: false),
                    DoctorsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsiliumDoctor", x => new { x.ConsiliumsId, x.DoctorsId });
                    table.ForeignKey(
                        name: "FK_ConsiliumDoctor_Consiliums_ConsiliumsId",
                        column: x => x.ConsiliumsId,
                        principalTable: "Consiliums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConsiliumDoctor_Doctors_DoctorsId",
                        column: x => x.DoctorsId,
                        principalTable: "Doctors",
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
                    ExaminationId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExaminationPrescription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExaminationPrescription_Examinations_ExaminationId",
                        column: x => x.ExaminationId,
                        principalTable: "Examinations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    { new Guid("135e0e89-2203-4120-a2d2-4d2b0e55416e"), "Novi Sad", "Serbia", 21000, "JNA", "33" },
                    { new Guid("14621e59-7575-4c53-b304-30124c345c82"), "Novi Sad", "Serbia", 21000, "Partizanska", "33" },
                    { new Guid("6ee5e219-5fc0-4603-afe3-548b52b6b3d2"), "Novi Sad", "Serbia", 21000, "Kosovska", "23A" }
                });

            migrationBuilder.InsertData(
                table: "Allergens",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("a588f944-7da5-4e7b-8df9-9825cc3efe55"), "Brufen" },
                    { new Guid("59183a76-c927-4cc4-987f-b161efde8041"), "Paracetamol" }
                });

            migrationBuilder.InsertData(
                table: "BloodUnits",
                columns: new[] { "Id", "Amount", "BloodBankName", "BloodType" },
                values: new object[,]
                {
                    { new Guid("08df4ac0-09a1-4b28-b13c-184f06948d23"), 7, "Moja Banka Krvi", 0 },
                    { new Guid("888b6838-5349-44b7-9ab3-28bb7f1d25f1"), 10, "Moja Banka Krvi", 7 },
                    { new Guid("4c6e8dd5-df25-41a6-86fe-551357a3dcf7"), 4, "Moja Banka Krvi", 0 }
                });

            migrationBuilder.InsertData(
                table: "Buildings",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("a7f68475-6e67-4800-82d9-94ee7bd6f79f"), "Stara bolnica" },
                    { new Guid("e0290505-e2c2-4473-b343-011f3a371222"), "Nova bolnica" }
                });

            migrationBuilder.InsertData(
                table: "GRooms",
                columns: new[] { "Id", "Lenght", "PositionX", "PositionY", "RoomId", "Width" },
                values: new object[,]
                {
                    { new Guid("7370e734-f02d-491f-86b7-7095613ed318"), 5, 5, 0, new Guid("280d3b2a-8908-4480-96ec-b0d986453ffd"), 5 },
                    { new Guid("a4c675ae-2a6f-454b-94c5-40d213986329"), 5, 0, 0, new Guid("5ffabde1-ac61-4bd4-8c85-afb6147d7a22"), 5 },
                    { new Guid("b554632f-50b5-4d94-85ae-f27459fd5ba1"), 5, 5, 0, new Guid("436703e2-0312-4765-8aab-39c97c04f3fc"), 5 },
                    { new Guid("4385db90-984d-4af9-83d6-9097850a3e1c"), 5, 5, 0, new Guid("e8dc9482-21f3-41b2-ab71-2e78eab48ffd"), 5 },
                    { new Guid("7893bf95-f000-495c-86d8-009b7d21b8cb"), 5, 5, 0, new Guid("1164ecd9-2cd9-40c1-81d7-ea38b3592316"), 5 },
                    { new Guid("ee6abb4f-9f45-4463-845b-b15861715081"), 5, 5, 0, new Guid("43ec95b8-37f5-4f93-ab1a-1c1cedfb9d9d"), 5 },
                    { new Guid("51e0011d-b0aa-47e8-8f39-31a93e66dab0"), 5, 5, 0, new Guid("538b1f17-2db8-476e-aa65-cfbb2735e469"), 5 },
                    { new Guid("efe4a995-6177-4318-b711-1e6b5987f0c2"), 5, 5, 0, new Guid("61107f8e-6c3d-441c-bd2c-9499b222d1cb"), 5 },
                    { new Guid("0a75998e-cbbc-4c01-a909-a053771b6f9e"), 5, 5, 0, new Guid("1915f736-18c6-4942-a641-e662bb179959"), 5 }
                });

            migrationBuilder.InsertData(
                table: "Medicine",
                columns: new[] { "Id", "Amount", "Name" },
                values: new object[,]
                {
                    { new Guid("a4ca9e69-5a9d-46e3-a265-c26fc27701d8"), 1000, "Medicine4" },
                    { new Guid("9bd68902-79e4-41fc-839b-b967c25983bd"), 1000, "Medicine3" },
                    { new Guid("bbe3a785-20fb-47af-9adc-cf639a7eabf8"), 1000, "Medicine5" },
                    { new Guid("015491f9-920f-4a75-bc5f-ec514b2a7d34"), 1000, "Medicine6" },
                    { new Guid("7834957a-88ec-465d-ac4a-9498053d0952"), 1000, "Medicine1" },
                    { new Guid("0997c283-f0c0-4dfe-8fb8-d683ec0f1413"), 1, "Aspirin" },
                    { new Guid("96b2459e-580b-4cf6-9072-855dafe594ec"), 1000, "Medicine2" },
                    { new Guid("aae0f5b1-ece9-439b-9ad4-86ad26599f41"), 30, "Brufen 300" }
                });

            migrationBuilder.InsertData(
                table: "Specializations",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("f06ba0e2-a08f-4084-af37-d2243e621668"), "General" },
                    { new Guid("da99077f-91d3-4475-8a39-342da25882a3"), "Surgeon" },
                    { new Guid("5f05b40b-50e1-4478-a023-0da60f1d6bd4"), "Dermatology" }
                });

            migrationBuilder.InsertData(
                table: "Symptoms",
                columns: new[] { "Id", "Description" },
                values: new object[,]
                {
                    { new Guid("4fe9778c-5c17-43ec-b19c-dfaa16643d9d"), "Stomach Aches" },
                    { new Guid("49326071-4942-4974-aa9b-881e70654e0a"), "Nausea" },
                    { new Guid("ccfde46d-13b7-49a3-b4ca-6e639e55179e"), "Eye irritation" },
                    { new Guid("ec26bbaa-cd63-46a8-aa11-aea68efbc289"), "Runny nose" },
                    { new Guid("0a9a0ce3-587a-4989-8e63-4a7b10081980"), "Stuffy nose" },
                    { new Guid("8c366a1f-98f6-4fb8-a997-61356308abf3"), "Puffy, watery eyes" },
                    { new Guid("b059874f-bb48-40ec-abc6-272d91d749d7"), "Sneezing" },
                    { new Guid("5879511f-1b7b-4a68-8ef8-d67a8ae906a5"), "High temperature" },
                    { new Guid("b9134bc5-eca8-4af3-99f4-ed92f2b90078"), "Difficulty breathing" },
                    { new Guid("bcefc434-6501-42ff-a07f-c5552654cd08"), "Cold" },
                    { new Guid("1c01cc4c-e54b-4d6c-acd4-5e75fe5e53fa"), "Vomiting" },
                    { new Guid("0c351542-f024-49cc-a8ac-66d1fdeb5735"), "Flu" },
                    { new Guid("bb85edba-1858-4688-8aab-ed409a772393"), "Headache" },
                    { new Guid("d4d22ef9-cfcd-48ea-81bc-ea82c8d52fd8"), "Eye irritation" },
                    { new Guid("304c136f-eafe-4048-8074-9b6c488127b6"), "More intense pain and fatigue" },
                    { new Guid("7ab0d00c-7c75-4024-b211-31953940edf2"), "Dry cough" },
                    { new Guid("6f3b6bea-dcbf-4830-9bc4-e0087a1ea4a5"), "Sore throat" },
                    { new Guid("e82086a0-9f3c-4c3f-be25-b5e8530e94fe"), "Abdominal pain" },
                    { new Guid("d8b065b1-6a11-425e-ba6d-0549e900ecc9"), "Diarrhea" },
                    { new Guid("72f2f3c9-c04d-4a4b-961f-a4f5636cdfad"), "Mononucleosis" },
                    { new Guid("6fc7e366-f3c9-40f0-9b86-569b6d186a04"), "Fever" }
                });

            migrationBuilder.InsertData(
                table: "WorkingSchedules",
                columns: new[] { "Id", "DayOfWorkFrom", "DayOfWorkTo", "ExpirationFrom", "ExpirationTo" },
                values: new object[,]
                {
                    { new Guid("906d8f00-0f78-4998-8223-0a78c2224335"), new DateTime(2022, 10, 27, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 27, 22, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("eeff5627-d874-483f-9ac4-1eeb38fbd681"), new DateTime(2022, 10, 27, 8, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 12, 27, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "ApplicationUsers",
                columns: new[] { "Id", "AddressId", "Email", "Enabled", "Jmbg", "Name", "Password", "Phone", "Surname", "UserRole", "Username" },
                values: new object[,]
                {
                    { new Guid("7d0a485f-c62e-461d-9c30-4485ac1dcf17"), new Guid("14621e59-7575-4c53-b304-30124c345c82"), "DjordjeLopov@gmail.com", true, "99999999", "Djordje", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "+612222222", "Vuckovic", 0, "Tadjo" },
                    { new Guid("901fb5d3-a91d-406e-bc52-2691b1523c5c"), new Guid("135e0e89-2203-4120-a2d2-4d2b0e55416e"), "psw.isa.mail@gmail.com", true, "99999999", "Darko", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "+612222222", "Darkovic", 2, "Darko" },
                    { new Guid("a109dc55-0793-4110-9e73-5cbe0f07a302"), new Guid("135e0e89-2203-4120-a2d2-4d2b0e55416e"), "psw.isa.mail@gmail.com", true, "99999999", "Manja", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "+612222222", "Maric", 2, "Manja" },
                    { new Guid("4796e4e2-8426-4baa-b155-415a91ad7866"), new Guid("135e0e89-2203-4120-a2d2-4d2b0e55416e"), "psw.isa.mail@gmail.com", true, "99999999", "Marko", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "+612222222", "Markovic", 2, "Marko" },
                    { new Guid("9258216a-a5dd-42bc-a4dd-e868262567aa"), new Guid("135e0e89-2203-4120-a2d2-4d2b0e55416e"), "psw.isa.mail@gmail.com", true, "99999999", "Nikola", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "+612222222", "Nikolic", 2, "Nikola" },
                    { new Guid("572136d7-ee96-4a5e-a54a-c05a48e0b6ad"), new Guid("135e0e89-2203-4120-a2d2-4d2b0e55416e"), "psw.isa.mail@gmail.com", true, "99999999", "Mina", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "+612222222", "Minic", 2, "Mina" },
                    { new Guid("23db877e-6fbc-4cb8-8f7d-bfbc9347950b"), new Guid("135e0e89-2203-4120-a2d2-4d2b0e55416e"), "psw.isa.mail@gmail.com", true, "99999999", "Nina", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "+612222222", "Minic", 2, "Nina" },
                    { new Guid("0fe87c5f-ef07-4102-840f-7d6f57ea9730"), new Guid("135e0e89-2203-4120-a2d2-4d2b0e55416e"), "psw.isa.mail@gmail.com", true, "99999999", "Miki", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "+612222222", "Djuricic", 2, "Miki" },
                    { new Guid("522de892-9ec7-4dd4-b48a-d78af6b046ed"), new Guid("135e0e89-2203-4120-a2d2-4d2b0e55416e"), "psw.isa.mail@gmail.com", true, "99999999", "Manager", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "+612222222", "Blood Bank", 3, "ManagerBB" },
                    { new Guid("678be9c5-86a7-402e-a72b-6bd3b68c93cb"), new Guid("135e0e89-2203-4120-a2d2-4d2b0e55416e"), "psw.isa.mail@gmail.com", true, "99999999", "Manager", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "+612222222", "Manger", 1, "Manager" },
                    { new Guid("27c3fcba-9ac9-4009-a7fb-7ff2c2372b7a"), new Guid("135e0e89-2203-4120-a2d2-4d2b0e55416e"), "psw.isa.mail@gmail.com", true, "99999999", "Moja Banka Krvi", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "+612222222", "Moja Banka Krvi", 4, "BloodBank" },
                    { new Guid("948f9997-53a4-4b39-935d-c9f41b75fa62"), new Guid("135e0e89-2203-4120-a2d2-4d2b0e55416e"), "psw.isa.mail@gmail.com", true, "99999999", "Tara", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "+612222222", "Markovic", 2, "Tara" },
                    { new Guid("5ffbd263-5867-42e4-8254-d5a6e7e47375"), new Guid("6ee5e219-5fc0-4603-afe3-548b52b6b3d2"), "psw.isa.mail@gmail.com", true, "99999999", "Sale", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "+612222222", "Lave", 2, "Sale" },
                    { new Guid("c0afd842-88f3-4eb0-b5c4-5b1af426af9c"), new Guid("6ee5e219-5fc0-4603-afe3-548b52b6b3d2"), "Cajons@gmail.com", true, "99999999", "Milos", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "+612222222", "Milosevic", 0, "Milos" },
                    { new Guid("fd70357e-13eb-44e6-9cd7-335b868fdc8b"), new Guid("6ee5e219-5fc0-4603-afe3-548b52b6b3d2"), "Cajons@gmail.com", true, "99999999", "Ilija", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "+612222222", "Maric", 0, "Ilija" },
                    { new Guid("8d9f2fb8-d5b8-43f2-8937-9f59c5c26547"), new Guid("135e0e89-2203-4120-a2d2-4d2b0e55416e"), "psw.isa.mail@gmail.com", true, "99999999", "Filip", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "+612222222", "Filipic", 2, "Filip" }
                });

            migrationBuilder.InsertData(
                table: "BloodConsumptions",
                columns: new[] { "Id", "Amount", "BloodUnitId", "Date", "DoctorId", "Purpose" },
                values: new object[,]
                {
                    { new Guid("1c65d527-6875-45e1-a10a-3b349ae16947"), 4, new Guid("08df4ac0-09a1-4b28-b13c-184f06948d23"), new DateTime(2022, 11, 14, 15, 0, 0, 0, DateTimeKind.Unspecified), new Guid("7d0a485f-c62e-461d-9c30-4485ac1dcf17"), "operation" },
                    { new Guid("58a7b730-4786-4602-9ccc-c4156b5676b9"), 2, new Guid("08df4ac0-09a1-4b28-b13c-184f06948d23"), new DateTime(2022, 10, 27, 15, 0, 0, 0, DateTimeKind.Unspecified), new Guid("7d0a485f-c62e-461d-9c30-4485ac1dcf17"), "operation" }
                });

            migrationBuilder.InsertData(
                table: "Floors",
                columns: new[] { "Id", "BuildingId", "FloorNumber", "Name" },
                values: new object[,]
                {
                    { new Guid("8d274cd9-5f23-4edf-9d28-18ac7b9f301b"), new Guid("e0290505-e2c2-4473-b343-011f3a371222"), 2, "F2" },
                    { new Guid("0fcc3ba0-5e90-4476-92a5-cd3724fb71fc"), new Guid("e0290505-e2c2-4473-b343-011f3a371222"), 1, "F1" },
                    { new Guid("c45998e1-250b-4427-a299-9f12fa7e45cb"), new Guid("e0290505-e2c2-4473-b343-011f3a371222"), 0, "F0" },
                    { new Guid("037c56a1-08e0-4511-9456-651367a736a9"), new Guid("a7f68475-6e67-4800-82d9-94ee7bd6f79f"), 2, "F2" },
                    { new Guid("60acb236-11d7-46a3-9b85-5ad43eb2671f"), new Guid("a7f68475-6e67-4800-82d9-94ee7bd6f79f"), 1, "F1" },
                    { new Guid("8730109f-6877-4e68-82cc-cbdd88f417bc"), new Guid("a7f68475-6e67-4800-82d9-94ee7bd6f79f"), 0, "F0" }
                });

            migrationBuilder.InsertData(
                table: "Managers",
                column: "Id",
                values: new object[]
                {
                    new Guid("678be9c5-86a7-402e-a72b-6bd3b68c93cb"),
                    new Guid("522de892-9ec7-4dd4-b48a-d78af6b046ed")
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "BuildingId", "FloorId", "GRoomId", "Name", "Type" },
                values: new object[,]
                {
                    { new Guid("5ffabde1-ac61-4bd4-8c85-afb6147d7a22"), new Guid("a7f68475-6e67-4800-82d9-94ee7bd6f79f"), new Guid("8730109f-6877-4e68-82cc-cbdd88f417bc"), new Guid("a4c675ae-2a6f-454b-94c5-40d213986329"), "A11", 0 },
                    { new Guid("280d3b2a-8908-4480-96ec-b0d986453ffd"), new Guid("a7f68475-6e67-4800-82d9-94ee7bd6f79f"), new Guid("8730109f-6877-4e68-82cc-cbdd88f417bc"), new Guid("7370e734-f02d-491f-86b7-7095613ed318"), "B11", 0 },
                    { new Guid("436703e2-0312-4765-8aab-39c97c04f3fc"), new Guid("a7f68475-6e67-4800-82d9-94ee7bd6f79f"), new Guid("60acb236-11d7-46a3-9b85-5ad43eb2671f"), new Guid("b554632f-50b5-4d94-85ae-f27459fd5ba1"), "A12", 0 },
                    { new Guid("e8dc9482-21f3-41b2-ab71-2e78eab48ffd"), new Guid("a7f68475-6e67-4800-82d9-94ee7bd6f79f"), new Guid("037c56a1-08e0-4511-9456-651367a736a9"), new Guid("4385db90-984d-4af9-83d6-9097850a3e1c"), "A13", 0 },
                    { new Guid("1164ecd9-2cd9-40c1-81d7-ea38b3592316"), new Guid("e0290505-e2c2-4473-b343-011f3a371222"), new Guid("c45998e1-250b-4427-a299-9f12fa7e45cb"), new Guid("7893bf95-f000-495c-86d8-009b7d21b8cb"), "A21", 2 },
                    { new Guid("43ec95b8-37f5-4f93-ab1a-1c1cedfb9d9d"), new Guid("e0290505-e2c2-4473-b343-011f3a371222"), new Guid("c45998e1-250b-4427-a299-9f12fa7e45cb"), new Guid("ee6abb4f-9f45-4463-845b-b15861715081"), "B21", 2 },
                    { new Guid("538b1f17-2db8-476e-aa65-cfbb2735e469"), new Guid("e0290505-e2c2-4473-b343-011f3a371222"), new Guid("0fcc3ba0-5e90-4476-92a5-cd3724fb71fc"), new Guid("51e0011d-b0aa-47e8-8f39-31a93e66dab0"), "A22", 2 },
                    { new Guid("61107f8e-6c3d-441c-bd2c-9499b222d1cb"), new Guid("e0290505-e2c2-4473-b343-011f3a371222"), new Guid("8d274cd9-5f23-4edf-9d28-18ac7b9f301b"), new Guid("efe4a995-6177-4318-b711-1e6b5987f0c2"), "C23", 2 },
                    { new Guid("1915f736-18c6-4942-a641-e662bb179959"), new Guid("e0290505-e2c2-4473-b343-011f3a371222"), new Guid("8d274cd9-5f23-4edf-9d28-18ac7b9f301b"), new Guid("0a75998e-cbbc-4c01-a909-a053771b6f9e"), "B23", 2 }
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "RoomId", "SpecializationId", "WorkingScheduleId" },
                values: new object[,]
                {
                    { new Guid("fd70357e-13eb-44e6-9cd7-335b868fdc8b"), new Guid("5ffabde1-ac61-4bd4-8c85-afb6147d7a22"), new Guid("f06ba0e2-a08f-4084-af37-d2243e621668"), new Guid("eeff5627-d874-483f-9ac4-1eeb38fbd681") },
                    { new Guid("c0afd842-88f3-4eb0-b5c4-5b1af426af9c"), new Guid("538b1f17-2db8-476e-aa65-cfbb2735e469"), new Guid("f06ba0e2-a08f-4084-af37-d2243e621668"), new Guid("eeff5627-d874-483f-9ac4-1eeb38fbd681") },
                    { new Guid("7d0a485f-c62e-461d-9c30-4485ac1dcf17"), new Guid("280d3b2a-8908-4480-96ec-b0d986453ffd"), new Guid("5f05b40b-50e1-4478-a023-0da60f1d6bd4"), new Guid("eeff5627-d874-483f-9ac4-1eeb38fbd681") }
                });

            migrationBuilder.InsertData(
                table: "RoomBeds",
                columns: new[] { "Id", "IsFree", "Number", "RoomId" },
                values: new object[,]
                {
                    { new Guid("0a6d2b0f-1962-4e31-8183-1a50a101234f"), true, "12A5", new Guid("280d3b2a-8908-4480-96ec-b0d986453ffd") },
                    { new Guid("00f8fd70-c19f-45e4-b37c-cfe098182ec5"), true, "12A4", new Guid("280d3b2a-8908-4480-96ec-b0d986453ffd") },
                    { new Guid("bc2884f7-84bd-4e29-ac33-8c5863575045"), true, "12A3", new Guid("280d3b2a-8908-4480-96ec-b0d986453ffd") },
                    { new Guid("b5fdfda0-7143-41ce-95fe-c5535a0448f9"), true, "12A2", new Guid("280d3b2a-8908-4480-96ec-b0d986453ffd") },
                    { new Guid("6f73253c-6409-4d57-afbc-ad1c684fba97"), true, "12A1", new Guid("280d3b2a-8908-4480-96ec-b0d986453ffd") },
                    { new Guid("844e5bfd-255e-413d-aac6-af2eff2339c6"), true, "11A4", new Guid("5ffabde1-ac61-4bd4-8c85-afb6147d7a22") },
                    { new Guid("6dae95de-1ba1-4c43-96b6-2e0715454725"), true, "11A3", new Guid("5ffabde1-ac61-4bd4-8c85-afb6147d7a22") },
                    { new Guid("37d5cc0c-92ca-4465-860a-5026c3523a42"), true, "11A2", new Guid("5ffabde1-ac61-4bd4-8c85-afb6147d7a22") },
                    { new Guid("c5f06599-bc99-4206-bffb-82518af85a07"), true, "11A1", new Guid("5ffabde1-ac61-4bd4-8c85-afb6147d7a22") }
                });

            migrationBuilder.InsertData(
                table: "RoomEquipment",
                columns: new[] { "RoomEquipmentId", "Amount", "EquipmentName", "RoomId" },
                values: new object[,]
                {
                    { new Guid("49e29ac3-f793-42f6-aa83-2838cb721378"), 3, "ANESTHESIA", new Guid("61107f8e-6c3d-441c-bd2c-9499b222d1cb") },
                    { new Guid("d5cd41f6-57b1-4e8b-ba09-bb7b82638ad5"), 6, "SYRINGE", new Guid("1164ecd9-2cd9-40c1-81d7-ea38b3592316") },
                    { new Guid("7705cd8f-3258-4f6b-8355-35c363022593"), 13, "BANDAGE", new Guid("61107f8e-6c3d-441c-bd2c-9499b222d1cb") },
                    { new Guid("c175e36f-6557-434a-9e15-ee97a858b9a5"), 7, "SYRINGE", new Guid("43ec95b8-37f5-4f93-ab1a-1c1cedfb9d9d") },
                    { new Guid("f23bc42c-b3aa-4460-98de-b9226a615687"), 14, "SURGICAL_TABLES", new Guid("43ec95b8-37f5-4f93-ab1a-1c1cedfb9d9d") },
                    { new Guid("f273aa50-7232-4855-a51d-df8e82aaec9f"), 15, "SURGICAL_TABLES", new Guid("1164ecd9-2cd9-40c1-81d7-ea38b3592316") },
                    { new Guid("baa67a67-d02a-480d-889f-8b1d0fdb6281"), 6, "BANDAGE", new Guid("538b1f17-2db8-476e-aa65-cfbb2735e469") },
                    { new Guid("4a066f3b-0bdd-41d8-ade6-1e63190d55d2"), 22, "EKG_MACHINE", new Guid("1164ecd9-2cd9-40c1-81d7-ea38b3592316") },
                    { new Guid("80d67d4d-24c3-43aa-84a2-d96adbbdbfd6"), 5, "EKG_MACHINE", new Guid("280d3b2a-8908-4480-96ec-b0d986453ffd") },
                    { new Guid("105aa40f-048a-4ece-9f33-50a792846ac3"), 11, "ANESTHESIA", new Guid("e8dc9482-21f3-41b2-ab71-2e78eab48ffd") },
                    { new Guid("e6701ed7-6de5-407d-9928-d206780f79d4"), 23, "SURGICAL_TABLES", new Guid("436703e2-0312-4765-8aab-39c97c04f3fc") },
                    { new Guid("4ba29b50-f1f1-4cef-be06-170964aeedd8"), 3, "EKG_MACHINE", new Guid("436703e2-0312-4765-8aab-39c97c04f3fc") },
                    { new Guid("2bad4ba5-0095-4d1e-8b7a-95efe82954b1"), 9, "SURGICAL_TABLES", new Guid("1915f736-18c6-4942-a641-e662bb179959") },
                    { new Guid("a63990f4-dad8-4377-b863-c54fde6594a1"), 10, "ANESTHESIA", new Guid("280d3b2a-8908-4480-96ec-b0d986453ffd") },
                    { new Guid("6629dff6-4901-47e3-b93f-1f633f488a81"), 13, "SYRINGE", new Guid("5ffabde1-ac61-4bd4-8c85-afb6147d7a22") },
                    { new Guid("9b388e21-e214-4b99-9553-2d979fc30f8b"), 5, "ANESTHESIA", new Guid("5ffabde1-ac61-4bd4-8c85-afb6147d7a22") },
                    { new Guid("2aecfd54-49af-4384-9462-6d73845dbe0d"), 15, "SURGICAL_TABLES", new Guid("5ffabde1-ac61-4bd4-8c85-afb6147d7a22") },
                    { new Guid("67a40e58-e087-49d6-ba87-8d328993d334"), 4, "BANDAGE", new Guid("e8dc9482-21f3-41b2-ab71-2e78eab48ffd") },
                    { new Guid("30426c39-78b2-44d4-8b75-b463a1b31535"), 9, "ANESTHESIA", new Guid("1915f736-18c6-4942-a641-e662bb179959") }
                });

            migrationBuilder.InsertData(
                table: "Holidays",
                columns: new[] { "Id", "Description", "DoctorId", "HolidayStatus", "IsUrgent", "DateRange_From", "DateRange_To" },
                values: new object[] { new Guid("91b02dbe-d30e-4b8d-9cb4-842a48f43067"), "I want to go to Paralia", new Guid("fd70357e-13eb-44e6-9cd7-335b868fdc8b"), 0, false, new DateTime(2022, 10, 27, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 27, 10, 30, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "Age", "BloodType", "DateOfBirth", "DoctorId", "Gender" },
                values: new object[,]
                {
                    { new Guid("5ffbd263-5867-42e4-8254-d5a6e7e47375"), 15, 4, new DateTime(2007, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("fd70357e-13eb-44e6-9cd7-335b868fdc8b"), 0 },
                    { new Guid("0fe87c5f-ef07-4102-840f-7d6f57ea9730"), 32, 0, new DateTime(1990, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("fd70357e-13eb-44e6-9cd7-335b868fdc8b"), 0 },
                    { new Guid("23db877e-6fbc-4cb8-8f7d-bfbc9347950b"), 5, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("fd70357e-13eb-44e6-9cd7-335b868fdc8b"), 1 },
                    { new Guid("9258216a-a5dd-42bc-a4dd-e868262567aa"), 18, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("fd70357e-13eb-44e6-9cd7-335b868fdc8b"), 2 },
                    { new Guid("901fb5d3-a91d-406e-bc52-2691b1523c5c"), 70, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("7d0a485f-c62e-461d-9c30-4485ac1dcf17"), 0 },
                    { new Guid("8d9f2fb8-d5b8-43f2-8937-9f59c5c26547"), 56, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("7d0a485f-c62e-461d-9c30-4485ac1dcf17"), 0 },
                    { new Guid("572136d7-ee96-4a5e-a54a-c05a48e0b6ad"), 9, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c0afd842-88f3-4eb0-b5c4-5b1af426af9c"), 1 },
                    { new Guid("4796e4e2-8426-4baa-b155-415a91ad7866"), 65, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c0afd842-88f3-4eb0-b5c4-5b1af426af9c"), 0 },
                    { new Guid("a109dc55-0793-4110-9e73-5cbe0f07a302"), 50, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c0afd842-88f3-4eb0-b5c4-5b1af426af9c"), 1 },
                    { new Guid("948f9997-53a4-4b39-935d-c9f41b75fa62"), 61, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c0afd842-88f3-4eb0-b5c4-5b1af426af9c"), 1 }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "AppointmentState", "AppointmentType", "DoctorId", "Emergent", "PatientId", "Duration_From", "Duration_To" },
                values: new object[,]
                {
                    { new Guid("ebae98ac-8e07-4f73-9987-e2391ab7ec0e"), 0, 0, new Guid("fd70357e-13eb-44e6-9cd7-335b868fdc8b"), false, new Guid("5ffbd263-5867-42e4-8254-d5a6e7e47375"), new DateTime(2023, 7, 27, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 27, 10, 30, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("bee5921d-a85c-4283-87f5-4e6e1c365368"), 0, 0, new Guid("fd70357e-13eb-44e6-9cd7-335b868fdc8b"), false, new Guid("5ffbd263-5867-42e4-8254-d5a6e7e47375"), new DateTime(2022, 12, 15, 8, 56, 32, 935, DateTimeKind.Local).AddTicks(4309), new DateTime(2022, 12, 15, 9, 26, 32, 937, DateTimeKind.Local).AddTicks(682) },
                    { new Guid("4e6f3625-2ef1-4e39-9d1a-1799e6aea46c"), 0, 0, new Guid("fd70357e-13eb-44e6-9cd7-335b868fdc8b"), false, new Guid("5ffbd263-5867-42e4-8254-d5a6e7e47375"), new DateTime(2022, 12, 15, 9, 41, 32, 937, DateTimeKind.Local).AddTicks(810), new DateTime(2022, 12, 15, 10, 11, 32, 937, DateTimeKind.Local).AddTicks(820) },
                    { new Guid("a157e0dd-6f00-4673-a2a8-0dfea0d335dc"), 0, 0, new Guid("fd70357e-13eb-44e6-9cd7-335b868fdc8b"), false, new Guid("5ffbd263-5867-42e4-8254-d5a6e7e47375"), new DateTime(2022, 12, 15, 10, 26, 32, 937, DateTimeKind.Local).AddTicks(825), new DateTime(2022, 12, 15, 10, 56, 32, 937, DateTimeKind.Local).AddTicks(828) }
                });

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
                name: "IX_ConsiliumDoctor_DoctorsId",
                table: "ConsiliumDoctor",
                column: "DoctorsId");

            migrationBuilder.CreateIndex(
                name: "IX_Consiliums_RoomId",
                table: "Consiliums",
                column: "RoomId");

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
                name: "IX_RoomMerging_Room1Id",
                table: "RoomMerging",
                column: "Room1Id");

            migrationBuilder.CreateIndex(
                name: "IX_RoomMerging_Room2Id",
                table: "RoomMerging",
                column: "Room2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_FloorId",
                table: "Rooms",
                column: "FloorId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomSpliting_RoomId",
                table: "RoomSpliting",
                column: "RoomId");

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
                name: "ConsiliumDoctor");

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
                name: "RoomMerging");

            migrationBuilder.DropTable(
                name: "RoomSpliting");

            migrationBuilder.DropTable(
                name: "Allergens");

            migrationBuilder.DropTable(
                name: "BloodUnits");

            migrationBuilder.DropTable(
                name: "Consiliums");

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
