using System;
using System.Collections.Generic;
using HospitalLibrary.SharedModel;
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
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    BloodType = table.Column<int>(type: "integer", nullable: false),
                    BloodBankName = table.Column<string>(type: "text", nullable: true),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Source = table.Column<string>(type: "text", nullable: true)
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
                name: "Medicines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoomEvent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EventName = table.Column<string>(type: "text", nullable: true),
                    Value = table.Column<string>(type: "text", nullable: true),
                    TimeStamp = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomEvent", x => x.Id);
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
                    ScheduleFrom = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ScheduleTo = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
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
                    Jmbg = table.Column<Jmbg>(type: "jsonb", nullable: true),
                    Phone = table.Column<Phone>(type: "jsonb", nullable: true),
                    UserRole = table.Column<int>(type: "integer", nullable: false),
                    Enabled = table.Column<bool>(type: "boolean", nullable: false),
                    IsBlocked = table.Column<bool>(type: "boolean", nullable: false)
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
                        name: "FK_IngredientMedicine_Medicines_MedicinesId",
                        column: x => x.MedicinesId,
                        principalTable: "Medicines",
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
                    DateRangeOfMerging_From = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DateRangeOfMerging_To = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
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
                    PatientId = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: true),
                    IsAnonymous = table.Column<bool>(type: "boolean", nullable: false),
                    IsPublic = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedback", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feedback_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaliciousPatients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PatientId = table.Column<Guid>(type: "uuid", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    NumberOfCancellations = table.Column<int>(type: "integer", nullable: false),
                    Malicious = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaliciousPatients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaliciousPatients_Patients_PatientId",
                        column: x => x.PatientId,
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
                name: "PatientHealthState",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BloodPressure_LowerPressure = table.Column<int>(type: "integer", nullable: true),
                    BloodPressure_UpperPressure = table.Column<int>(type: "integer", nullable: true),
                    BloodSugarLevel_SugarLevel = table.Column<int>(type: "integer", nullable: true),
                    Weight = table.Column<int>(type: "integer", nullable: false),
                    SubmissionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    MenstrualCycle_From = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    MenstrualCycle_To = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    BodyFatPercent_Value = table.Column<int>(type: "integer", nullable: true),
                    RootId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientHealthState", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientHealthState_Patients_RootId",
                        column: x => x.RootId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientHealthStateNotification",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PatientId = table.Column<Guid>(type: "uuid", nullable: true),
                    Notifications = table.Column<List<string>>(type: "text[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientHealthStateNotification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientHealthStateNotification_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DomainEvent<EventStoreSchedulingAppointmentType>",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Event = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    AppointmentId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DomainEvent<EventStoreSchedulingAppointmentType>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DomainEvent<EventStoreSchedulingAppointmentType>_Appointmen~",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "EventStoreExaminations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Data = table.Column<int>(type: "integer", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Sequence = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    AggregateId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventStoreExaminations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventStoreExaminations_Examinations_AggregateId",
                        column: x => x.AggregateId,
                        principalTable: "Examinations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExaminationPrescription",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Usage = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExaminationPrescription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExaminationPrescription_Examinations_Id",
                        column: x => x.Id,
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
                        name: "FK_MedicinePrescription_Medicines_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "Medicines",
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
                        name: "FK_ExaminationPrescriptionMedicine_Medicines_MedicinesId",
                        column: x => x.MedicinesId,
                        principalTable: "Medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "City", "Country", "Postcode", "Street", "StreetNumber" },
                values: new object[,]
                {
                    { new Guid("4d517b16-3c54-43db-8014-daba04646b02"), "Novi Sad", "Serbia", 21000, "JNA", "33" },
                    { new Guid("de1d8d16-3e84-4447-b7e3-9b3a64fa4363"), "Novi Sad", "Serbia", 21000, "Partizanska", "33" },
                    { new Guid("78531076-9955-4da1-9601-f4368ea29884"), "Novi Sad", "Serbia", 21000, "Kosovska", "23A" }
                });

            migrationBuilder.InsertData(
                table: "Allergens",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("13122955-1e3d-48cb-9e83-d31d319a4331"), "Brufen" },
                    { new Guid("c56aa480-eb59-4909-8b94-5d5223125862"), "Paracetamol" }
                });

            migrationBuilder.InsertData(
                table: "BloodUnits",
                columns: new[] { "Id", "Amount", "BloodBankName", "BloodType", "Date", "Source" },
                values: new object[,]
                {
                    { new Guid("a2042622-1e03-4030-98df-5b09b26c9ef7"), 7, "Moja Banka Krvi", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("8c700381-8ff6-4e1e-9a5b-d38dcc65d487"), 10, "Moja Banka Krvi", 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("e47de2a2-6c95-4ac3-80f4-17eb4a337439"), 4, "Moja Banka Krvi", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("5166b85e-8984-4d7f-a5c0-aa2842d48cc6"), 5, "Moja Banka Krvi", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "URGENT" }
                });

            migrationBuilder.InsertData(
                table: "Buildings",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("128ab9e5-6d34-4c4f-83a6-52f39f321494"), "Stara bolnica" },
                    { new Guid("0c4c44c2-1684-48b7-a68c-0280f344ce0f"), "Nova bolnica" }
                });

            migrationBuilder.InsertData(
                table: "GRooms",
                columns: new[] { "Id", "Lenght", "PositionX", "PositionY", "RoomId", "Width" },
                values: new object[,]
                {
                    { new Guid("e104979d-3ac4-42dd-b6ef-f45981b9673e"), 5, 5, 0, new Guid("45cd1733-2c1f-48e8-9a61-c16969ae6e0a"), 5 },
                    { new Guid("49503b1b-16fe-4846-8793-2416c9006045"), 5, 0, 0, new Guid("f04b034c-8459-4f80-9595-18974b67622f"), 5 },
                    { new Guid("7e05c47c-b9de-45f8-89d6-755e9a178529"), 5, 5, 0, new Guid("e2a694a2-0fc1-44a7-94dd-ed215518e960"), 5 },
                    { new Guid("9eacd893-7162-455d-88b6-19acc797ceab"), 5, 5, 0, new Guid("e6954c54-f26d-4c3f-8b1b-48236c6289b0"), 5 },
                    { new Guid("612edc5b-6844-4c70-a30d-2f3dfe6e36a9"), 5, 5, 0, new Guid("a48655a4-ff5e-4821-adf8-fe46e9e01c79"), 5 },
                    { new Guid("e52eca54-3ce3-4fb9-b69f-e627837229ee"), 5, 5, 0, new Guid("cf6fc9ac-75e8-4dd3-bfa1-b3a0d4bc541f"), 5 },
                    { new Guid("8ee8bdfd-ac0c-4dc3-b79f-55506121e9c0"), 5, 5, 0, new Guid("87b2c6ed-b5fc-48f6-b20f-efe899fc35a2"), 5 },
                    { new Guid("b4afd009-f768-486f-8e80-80de254e1ef9"), 5, 5, 0, new Guid("97c59b30-1f46-4467-955a-58f4eda9f96f"), 5 },
                    { new Guid("b4e56fe6-d3f0-4823-848d-9b13ee192993"), 5, 5, 0, new Guid("02392c15-8b13-4075-8947-017f3519b777"), 5 }
                });

            migrationBuilder.InsertData(
                table: "Medicines",
                columns: new[] { "Id", "Amount", "Name" },
                values: new object[,]
                {
                    { new Guid("065f8fec-23bd-445d-867e-2817d25a6114"), 1000, "Medicine5" },
                    { new Guid("79581c8e-b480-40e1-bda7-71317c96857f"), 1000, "Medicine4" },
                    { new Guid("4a717373-458c-4bae-95d6-e36d97c16468"), 1000, "Medicine3" },
                    { new Guid("a24fe08e-34b8-4eda-af58-a795972c5cca"), 1000, "Medicine6" },
                    { new Guid("5b41fed9-1020-4aef-8a8a-1007d94cfe15"), 1000, "Medicine1" },
                    { new Guid("42f639da-ffc9-47f3-b36b-616811b41297"), 1, "Aspirin" },
                    { new Guid("a2360d51-9dbf-40f0-9bea-431cb4eacf29"), 1000, "Medicine2" },
                    { new Guid("765c64ca-3e15-4abb-adfe-0365386b51b4"), 30, "Brufen 300" }
                });

            migrationBuilder.InsertData(
                table: "RoomEvent",
                columns: new[] { "Id", "EventName", "TimeStamp", "UserId", "Value" },
                values: new object[] { new Guid("0b688282-7df3-4ee8-a4fe-ed9e463c9fb8"), "SessionStarted", new DateTime(2023, 1, 13, 0, 19, 1, 884, DateTimeKind.Local).AddTicks(254), new Guid("44e5b077-21af-4a4d-8d1d-7fed13b83094"), "null" });

            migrationBuilder.InsertData(
                table: "Specializations",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("5900fe4e-253b-4ec3-94e8-ac33226f53ac"), "General" },
                    { new Guid("90bcbd53-4cf7-4ea2-9eaf-d7015ab50762"), "Dermatology" },
                    { new Guid("40d935bb-90f1-4ad3-984a-aa8b87285921"), "Surgeon" }
                });

            migrationBuilder.InsertData(
                table: "Symptoms",
                columns: new[] { "Id", "Description" },
                values: new object[,]
                {
                    { new Guid("85004393-0793-466b-89f6-dbe8937bd247"), "Nausea" },
                    { new Guid("80adb1ba-f9c4-40b5-a1a6-075630b6c162"), "Eye irritation" },
                    { new Guid("029320d1-693d-44f9-b518-612afda2e7ac"), "Runny nose" },
                    { new Guid("12aeff17-1744-4722-a9cd-baa9f81f2b96"), "Stuffy nose" },
                    { new Guid("b9c61141-98e5-4e0a-a039-6dccbe45984d"), "Puffy, watery eyes" },
                    { new Guid("18cda185-e8de-4fb8-b81e-8a195df842b7"), "Sneezing" },
                    { new Guid("a336538b-7990-4fee-bcf1-8b5cafdf2589"), "High temperature" },
                    { new Guid("eda1554b-436b-46ac-9335-87628aad13c3"), "Difficulty breathing" },
                    { new Guid("d3756a4d-5fc2-47e8-b77e-5e1baef24d29"), "Cold" },
                    { new Guid("785077f5-982f-49ce-b3b6-dfa02ed204c7"), "Vomiting" },
                    { new Guid("cb775309-3450-4da7-89ab-21de2d79f972"), "Flu" },
                    { new Guid("b986129e-c6ba-47ec-9b7b-ea6aea4de066"), "Headache" },
                    { new Guid("d75699a8-358e-480f-bf7b-2221b081bfd1"), "Eye irritation" },
                    { new Guid("f7066d48-07ea-4683-81e8-ea2034d56e38"), "More intense pain and fatigue" },
                    { new Guid("d67d6900-454a-4511-aabf-23dd076c1e8f"), "Dry cough" },
                    { new Guid("91802ffc-77f7-4aca-be23-02ad8d28ae2a"), "Sore throat" },
                    { new Guid("7b7f8f8e-87e8-4ece-9463-d0a920d360ee"), "Abdominal pain" },
                    { new Guid("0862b4d2-d36f-45ee-8a89-f5fa44b2148c"), "Diarrhea" },
                    { new Guid("9f703acd-ba44-420b-a58d-88d0f37aeb52"), "Mononucleosis" },
                    { new Guid("ab39f418-9c8a-4eba-ba28-697ac03e7b2c"), "Fever" },
                    { new Guid("1a8ceade-c1ed-4e0e-a111-1a3d2d3def61"), "Stomach Aches" }
                });

            migrationBuilder.InsertData(
                table: "WorkingSchedules",
                columns: new[] { "Id", "DayOfWorkFrom", "DayOfWorkTo", "ScheduleFrom", "ScheduleTo" },
                values: new object[,]
                {
                    { new Guid("3a3035c9-c574-4699-98b6-bac80701578b"), new DateTime(2022, 10, 27, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 27, 22, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("02efbb74-7c22-4bd7-a66b-15b8561ceecc"), new DateTime(2022, 10, 27, 8, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 12, 27, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "ApplicationUsers",
                columns: new[] { "Id", "AddressId", "Email", "Enabled", "IsBlocked", "Jmbg", "Name", "Password", "Phone", "Surname", "UserRole", "Username" },
                values: new object[,]
                {
                    { new Guid("64feda82-d74a-42a4-9476-0d325f39df8b"), new Guid("de1d8d16-3e84-4447-b7e3-9b3a64fa4363"), "DjordjeLopov@gmail.com", true, false, null, "Djordje", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", null, "Vuckovic", 0, "Tadjo" },
                    { new Guid("111ec5d6-287d-4df9-a06e-ad00e24a1715"), new Guid("4d517b16-3c54-43db-8014-daba04646b02"), "psw.isa.mail@gmail.com", true, false, null, "Darko", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", null, "Darkovic", 2, "Darko" },
                    { new Guid("e0963fea-93a6-4de8-803d-c33d5477795b"), new Guid("4d517b16-3c54-43db-8014-daba04646b02"), "psw.isa.mail@gmail.com", true, false, null, "Manja", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", null, "Maric", 2, "Manja" },
                    { new Guid("7697d0fc-b421-495a-89cd-be8cbeeb6aee"), new Guid("4d517b16-3c54-43db-8014-daba04646b02"), "psw.isa.mail@gmail.com", true, false, null, "Marko", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", null, "Markovic", 2, "Marko" },
                    { new Guid("998d0e87-4611-49e4-a283-5592510e3120"), new Guid("4d517b16-3c54-43db-8014-daba04646b02"), "psw.isa.mail@gmail.com", true, false, null, "Nikola", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", null, "Nikolic", 2, "Nikola" },
                    { new Guid("9c05f79c-859e-42dc-b8f4-8ac11f01350e"), new Guid("4d517b16-3c54-43db-8014-daba04646b02"), "psw.isa.mail@gmail.com", true, false, null, "Mina", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", null, "Minic", 2, "Mina" },
                    { new Guid("9b36c7a8-4fe2-47fc-871f-ca6de6089c74"), new Guid("4d517b16-3c54-43db-8014-daba04646b02"), "psw.isa.mail@gmail.com", true, false, null, "Nina", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", null, "Minic", 2, "Nina" },
                    { new Guid("4ddb71a2-77c7-4cf1-87bb-91e1cd57e7c4"), new Guid("4d517b16-3c54-43db-8014-daba04646b02"), "psw.isa.mail@gmail.com", true, false, null, "Miki", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", null, "Djuricic", 2, "Miki" },
                    { new Guid("86645e61-fe6b-4aa8-b20f-7692cdb8d230"), new Guid("4d517b16-3c54-43db-8014-daba04646b02"), "psw.isa.mail@gmail.com", true, false, null, "Manager", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", null, "Blood Bank", 3, "ManagerBB" },
                    { new Guid("44e5b077-21af-4a4d-8d1d-7fed13b83094"), new Guid("4d517b16-3c54-43db-8014-daba04646b02"), "psw.isa.mail@gmail.com", true, false, null, "Manager", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", null, "Manger", 1, "Manager" },
                    { new Guid("99576e75-ea2b-484f-bd62-512538f7f15b"), new Guid("4d517b16-3c54-43db-8014-daba04646b02"), "psw.isa.mail@gmail.com", true, false, null, "Moja Banka Krvi", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", null, "Moja Banka Krvi", 4, "BloodBank" },
                    { new Guid("b0c6a94f-a9f3-47e1-a96b-afbcf40d6b4d"), new Guid("4d517b16-3c54-43db-8014-daba04646b02"), "psw.isa.mail@gmail.com", true, false, null, "Filip", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", null, "Filipic", 2, "Filip" },
                    { new Guid("72b42a8a-fc4d-4981-8818-e8f867a57095"), new Guid("4d517b16-3c54-43db-8014-daba04646b02"), "psw.isa.mail@gmail.com", true, false, null, "Tara", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", null, "Markovic", 2, "Tara" },
                    { new Guid("222a8b42-2e02-45c7-b53a-b21aefaca00a"), new Guid("78531076-9955-4da1-9601-f4368ea29884"), "Cajons@gmail.com", true, false, null, "Jakov", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", null, "Milosevic", 0, "Jakov" },
                    { new Guid("6c917ac7-3014-4807-b539-e7da0e7ca804"), new Guid("78531076-9955-4da1-9601-f4368ea29884"), "Cajons@gmail.com", true, false, null, "Milos", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", null, "Milosevic", 0, "Milos" },
                    { new Guid("5e04f731-9928-407e-bc2b-9573fe485c5d"), new Guid("78531076-9955-4da1-9601-f4368ea29884"), "Cajons@gmail.com", true, false, null, "Ilija", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", null, "Maric", 0, "Ilija" },
                    { new Guid("652b84fa-fd03-441f-9552-64c87ce8aca6"), new Guid("78531076-9955-4da1-9601-f4368ea29884"), "psw.isa.mail@gmail.com", true, false, null, "Sale", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", null, "Lave", 2, "Sale" }
                });

            migrationBuilder.InsertData(
                table: "BloodConsumptions",
                columns: new[] { "Id", "Amount", "BloodUnitId", "Date", "DoctorId", "Purpose" },
                values: new object[,]
                {
                    { new Guid("62354ab8-de77-4b51-89e2-e510c406b673"), 4, new Guid("a2042622-1e03-4030-98df-5b09b26c9ef7"), new DateTime(2022, 11, 14, 15, 0, 0, 0, DateTimeKind.Unspecified), new Guid("64feda82-d74a-42a4-9476-0d325f39df8b"), "operation" },
                    { new Guid("ca336bea-aec4-4a97-a08e-9d6fbd66afe4"), 2, new Guid("a2042622-1e03-4030-98df-5b09b26c9ef7"), new DateTime(2022, 10, 27, 15, 0, 0, 0, DateTimeKind.Unspecified), new Guid("64feda82-d74a-42a4-9476-0d325f39df8b"), "operation" }
                });

            migrationBuilder.InsertData(
                table: "Floors",
                columns: new[] { "Id", "BuildingId", "FloorNumber", "Name" },
                values: new object[,]
                {
                    { new Guid("7480f1be-b403-4091-86a3-e662b8f032b3"), new Guid("0c4c44c2-1684-48b7-a68c-0280f344ce0f"), 2, "F2" },
                    { new Guid("083458e2-8f33-4b45-9c92-706b1b9c69cc"), new Guid("0c4c44c2-1684-48b7-a68c-0280f344ce0f"), 1, "F1" },
                    { new Guid("a58a4a1f-2757-499d-805d-b80afdb8da45"), new Guid("0c4c44c2-1684-48b7-a68c-0280f344ce0f"), 0, "F0" },
                    { new Guid("43ff2155-685f-4cdc-bb05-5e6827d465fa"), new Guid("128ab9e5-6d34-4c4f-83a6-52f39f321494"), 2, "F2" },
                    { new Guid("ec245cc6-8072-4959-82b9-a18bc46c8720"), new Guid("128ab9e5-6d34-4c4f-83a6-52f39f321494"), 1, "F1" },
                    { new Guid("33e2909a-b11b-4eb6-90ef-38153c9d11c7"), new Guid("128ab9e5-6d34-4c4f-83a6-52f39f321494"), 0, "F0" }
                });

            migrationBuilder.InsertData(
                table: "Managers",
                column: "Id",
                values: new object[]
                {
                    new Guid("44e5b077-21af-4a4d-8d1d-7fed13b83094"),
                    new Guid("86645e61-fe6b-4aa8-b20f-7692cdb8d230")
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "BuildingId", "FloorId", "GRoomId", "Name", "Type" },
                values: new object[,]
                {
                    { new Guid("f04b034c-8459-4f80-9595-18974b67622f"), new Guid("128ab9e5-6d34-4c4f-83a6-52f39f321494"), new Guid("33e2909a-b11b-4eb6-90ef-38153c9d11c7"), new Guid("49503b1b-16fe-4846-8793-2416c9006045"), "A11", 0 },
                    { new Guid("e2a694a2-0fc1-44a7-94dd-ed215518e960"), new Guid("128ab9e5-6d34-4c4f-83a6-52f39f321494"), new Guid("33e2909a-b11b-4eb6-90ef-38153c9d11c7"), new Guid("7e05c47c-b9de-45f8-89d6-755e9a178529"), "B11", 0 },
                    { new Guid("45cd1733-2c1f-48e8-9a61-c16969ae6e0a"), new Guid("128ab9e5-6d34-4c4f-83a6-52f39f321494"), new Guid("ec245cc6-8072-4959-82b9-a18bc46c8720"), new Guid("e104979d-3ac4-42dd-b6ef-f45981b9673e"), "A12", 0 },
                    { new Guid("e6954c54-f26d-4c3f-8b1b-48236c6289b0"), new Guid("128ab9e5-6d34-4c4f-83a6-52f39f321494"), new Guid("43ff2155-685f-4cdc-bb05-5e6827d465fa"), new Guid("9eacd893-7162-455d-88b6-19acc797ceab"), "A13", 0 },
                    { new Guid("02392c15-8b13-4075-8947-017f3519b777"), new Guid("0c4c44c2-1684-48b7-a68c-0280f344ce0f"), new Guid("a58a4a1f-2757-499d-805d-b80afdb8da45"), new Guid("b4e56fe6-d3f0-4823-848d-9b13ee192993"), "A21", 2 },
                    { new Guid("a48655a4-ff5e-4821-adf8-fe46e9e01c79"), new Guid("0c4c44c2-1684-48b7-a68c-0280f344ce0f"), new Guid("a58a4a1f-2757-499d-805d-b80afdb8da45"), new Guid("612edc5b-6844-4c70-a30d-2f3dfe6e36a9"), "B21", 2 },
                    { new Guid("cf6fc9ac-75e8-4dd3-bfa1-b3a0d4bc541f"), new Guid("0c4c44c2-1684-48b7-a68c-0280f344ce0f"), new Guid("083458e2-8f33-4b45-9c92-706b1b9c69cc"), new Guid("e52eca54-3ce3-4fb9-b69f-e627837229ee"), "A22", 2 },
                    { new Guid("87b2c6ed-b5fc-48f6-b20f-efe899fc35a2"), new Guid("0c4c44c2-1684-48b7-a68c-0280f344ce0f"), new Guid("7480f1be-b403-4091-86a3-e662b8f032b3"), new Guid("8ee8bdfd-ac0c-4dc3-b79f-55506121e9c0"), "C23", 2 },
                    { new Guid("97c59b30-1f46-4467-955a-58f4eda9f96f"), new Guid("0c4c44c2-1684-48b7-a68c-0280f344ce0f"), new Guid("7480f1be-b403-4091-86a3-e662b8f032b3"), new Guid("b4afd009-f768-486f-8e80-80de254e1ef9"), "B23", 2 }
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "RoomId", "SpecializationId", "WorkingScheduleId" },
                values: new object[,]
                {
                    { new Guid("5e04f731-9928-407e-bc2b-9573fe485c5d"), new Guid("f04b034c-8459-4f80-9595-18974b67622f"), new Guid("5900fe4e-253b-4ec3-94e8-ac33226f53ac"), new Guid("02efbb74-7c22-4bd7-a66b-15b8561ceecc") },
                    { new Guid("222a8b42-2e02-45c7-b53a-b21aefaca00a"), new Guid("e6954c54-f26d-4c3f-8b1b-48236c6289b0"), new Guid("40d935bb-90f1-4ad3-984a-aa8b87285921"), new Guid("3a3035c9-c574-4699-98b6-bac80701578b") },
                    { new Guid("6c917ac7-3014-4807-b539-e7da0e7ca804"), new Guid("cf6fc9ac-75e8-4dd3-bfa1-b3a0d4bc541f"), new Guid("5900fe4e-253b-4ec3-94e8-ac33226f53ac"), new Guid("02efbb74-7c22-4bd7-a66b-15b8561ceecc") },
                    { new Guid("64feda82-d74a-42a4-9476-0d325f39df8b"), new Guid("e2a694a2-0fc1-44a7-94dd-ed215518e960"), new Guid("90bcbd53-4cf7-4ea2-9eaf-d7015ab50762"), new Guid("02efbb74-7c22-4bd7-a66b-15b8561ceecc") }
                });

            migrationBuilder.InsertData(
                table: "RoomBeds",
                columns: new[] { "Id", "IsFree", "Number", "RoomId" },
                values: new object[,]
                {
                    { new Guid("4fa5d1fc-991b-41a3-bb21-c577015d4d68"), true, "12A5", new Guid("e2a694a2-0fc1-44a7-94dd-ed215518e960") },
                    { new Guid("001ba147-daa5-4d9c-8cc8-ef6e917226b7"), true, "12A4", new Guid("e2a694a2-0fc1-44a7-94dd-ed215518e960") },
                    { new Guid("2f5f9e65-0ce7-4f27-a152-1ebbe4edfbbb"), true, "12A3", new Guid("e2a694a2-0fc1-44a7-94dd-ed215518e960") },
                    { new Guid("1485096e-bc16-48d8-961a-53c6a75cc716"), true, "12A1", new Guid("e2a694a2-0fc1-44a7-94dd-ed215518e960") },
                    { new Guid("6382fabd-082a-45cf-9805-fa15258eb048"), true, "12A2", new Guid("e2a694a2-0fc1-44a7-94dd-ed215518e960") },
                    { new Guid("0345ea12-64e6-47ff-a71b-36d151cf0a35"), true, "11A4", new Guid("f04b034c-8459-4f80-9595-18974b67622f") },
                    { new Guid("b36b656c-dfda-4cfa-a5c8-9bb8b098edb2"), true, "11A3", new Guid("f04b034c-8459-4f80-9595-18974b67622f") },
                    { new Guid("8487d5da-1bc2-4992-8b40-bd80db683987"), true, "11A2", new Guid("f04b034c-8459-4f80-9595-18974b67622f") },
                    { new Guid("86bbc095-9abd-4cf0-b402-4fc5822ca033"), true, "11A1", new Guid("f04b034c-8459-4f80-9595-18974b67622f") }
                });

            migrationBuilder.InsertData(
                table: "RoomEquipment",
                columns: new[] { "RoomEquipmentId", "Amount", "EquipmentName", "RoomId" },
                values: new object[,]
                {
                    { new Guid("bc952f63-46d2-4165-b881-8fad04d8059a"), 3, "ANESTHESIA", new Guid("87b2c6ed-b5fc-48f6-b20f-efe899fc35a2") },
                    { new Guid("b06b0e64-6c67-4e77-b787-1279cd2591ab"), 6, "SYRINGE", new Guid("02392c15-8b13-4075-8947-017f3519b777") },
                    { new Guid("42adaa5b-a579-4084-8cd4-d8a5ec763c2b"), 13, "BANDAGE", new Guid("87b2c6ed-b5fc-48f6-b20f-efe899fc35a2") },
                    { new Guid("771812c1-6efa-4e48-a6ac-2751a9b23d2c"), 7, "SYRINGE", new Guid("a48655a4-ff5e-4821-adf8-fe46e9e01c79") },
                    { new Guid("27b7a452-29ba-4520-be23-20f9b435f451"), 14, "SURGICAL_TABLES", new Guid("a48655a4-ff5e-4821-adf8-fe46e9e01c79") },
                    { new Guid("94185be2-325e-4f03-9b3a-e78ba1f67df7"), 15, "SURGICAL_TABLES", new Guid("02392c15-8b13-4075-8947-017f3519b777") },
                    { new Guid("5580c69d-0f4a-4729-9db4-74815ce086e0"), 6, "BANDAGE", new Guid("cf6fc9ac-75e8-4dd3-bfa1-b3a0d4bc541f") },
                    { new Guid("83b68e9a-737b-48ba-b49f-40490e61577c"), 22, "EKG_MACHINE", new Guid("02392c15-8b13-4075-8947-017f3519b777") },
                    { new Guid("2d319570-c16a-4151-ab87-05e520bfd1ec"), 5, "EKG_MACHINE", new Guid("e2a694a2-0fc1-44a7-94dd-ed215518e960") },
                    { new Guid("6c0a181f-c35e-4d69-ba51-c98a81f514df"), 11, "ANESTHESIA", new Guid("e6954c54-f26d-4c3f-8b1b-48236c6289b0") },
                    { new Guid("da8b837f-3987-4937-8f77-0427f558ec2d"), 23, "SURGICAL_TABLES", new Guid("45cd1733-2c1f-48e8-9a61-c16969ae6e0a") },
                    { new Guid("3ede5fcf-d8b7-4348-8d03-293189ba7742"), 3, "EKG_MACHINE", new Guid("45cd1733-2c1f-48e8-9a61-c16969ae6e0a") },
                    { new Guid("d9ea6d9d-61b0-42cb-9e30-fbde14986c78"), 9, "SURGICAL_TABLES", new Guid("97c59b30-1f46-4467-955a-58f4eda9f96f") },
                    { new Guid("8b1d72fc-6252-4fa1-827b-4c216f067cfb"), 10, "ANESTHESIA", new Guid("e2a694a2-0fc1-44a7-94dd-ed215518e960") },
                    { new Guid("2015bbfc-9e3b-4d95-afa7-a3321463553c"), 13, "SYRINGE", new Guid("f04b034c-8459-4f80-9595-18974b67622f") },
                    { new Guid("10f92c0f-e60e-44e3-a1da-33a377f6c991"), 5, "ANESTHESIA", new Guid("f04b034c-8459-4f80-9595-18974b67622f") },
                    { new Guid("80a633f7-dca3-4e4a-96b4-03602b84772b"), 15, "SURGICAL_TABLES", new Guid("f04b034c-8459-4f80-9595-18974b67622f") },
                    { new Guid("2506a5b3-02a7-477f-b0d6-9078aca27ef9"), 4, "BANDAGE", new Guid("e6954c54-f26d-4c3f-8b1b-48236c6289b0") },
                    { new Guid("3ea73b66-b231-43cb-848e-24279d96d9b8"), 9, "ANESTHESIA", new Guid("97c59b30-1f46-4467-955a-58f4eda9f96f") }
                });

            migrationBuilder.InsertData(
                table: "Holidays",
                columns: new[] { "Id", "Description", "DoctorId", "HolidayStatus", "IsUrgent", "DateRange_From", "DateRange_To" },
                values: new object[] { new Guid("b5522f04-8c98-4b89-90b2-4c334326b337"), "I want to go to Paralia", new Guid("5e04f731-9928-407e-bc2b-9573fe485c5d"), 0, false, new DateTime(2022, 10, 27, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 27, 10, 30, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "Age", "BloodType", "DateOfBirth", "DoctorId", "Gender" },
                values: new object[,]
                {
                    { new Guid("652b84fa-fd03-441f-9552-64c87ce8aca6"), 15, 4, new DateTime(2007, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("5e04f731-9928-407e-bc2b-9573fe485c5d"), 0 },
                    { new Guid("4ddb71a2-77c7-4cf1-87bb-91e1cd57e7c4"), 32, 0, new DateTime(1990, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("5e04f731-9928-407e-bc2b-9573fe485c5d"), 0 },
                    { new Guid("9b36c7a8-4fe2-47fc-871f-ca6de6089c74"), 5, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("5e04f731-9928-407e-bc2b-9573fe485c5d"), 1 },
                    { new Guid("998d0e87-4611-49e4-a283-5592510e3120"), 18, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("5e04f731-9928-407e-bc2b-9573fe485c5d"), 2 },
                    { new Guid("111ec5d6-287d-4df9-a06e-ad00e24a1715"), 70, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("64feda82-d74a-42a4-9476-0d325f39df8b"), 0 },
                    { new Guid("b0c6a94f-a9f3-47e1-a96b-afbcf40d6b4d"), 56, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("64feda82-d74a-42a4-9476-0d325f39df8b"), 0 },
                    { new Guid("9c05f79c-859e-42dc-b8f4-8ac11f01350e"), 9, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("6c917ac7-3014-4807-b539-e7da0e7ca804"), 1 },
                    { new Guid("7697d0fc-b421-495a-89cd-be8cbeeb6aee"), 65, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("6c917ac7-3014-4807-b539-e7da0e7ca804"), 0 },
                    { new Guid("e0963fea-93a6-4de8-803d-c33d5477795b"), 50, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("6c917ac7-3014-4807-b539-e7da0e7ca804"), 1 },
                    { new Guid("72b42a8a-fc4d-4981-8818-e8f867a57095"), 61, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("6c917ac7-3014-4807-b539-e7da0e7ca804"), 1 }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "AppointmentState", "AppointmentType", "DoctorId", "Emergent", "PatientId", "Duration_From", "Duration_To" },
                values: new object[,]
                {
                    { new Guid("c0576733-b7fa-4974-b60c-d3d7e8c9f216"), 0, 0, new Guid("5e04f731-9928-407e-bc2b-9573fe485c5d"), false, new Guid("652b84fa-fd03-441f-9552-64c87ce8aca6"), new DateTime(2023, 7, 27, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 27, 10, 30, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("8fcd8446-f2a7-4001-958c-284441f32e94"), 0, 0, new Guid("5e04f731-9928-407e-bc2b-9573fe485c5d"), false, new Guid("652b84fa-fd03-441f-9552-64c87ce8aca6"), new DateTime(2023, 1, 12, 23, 19, 1, 879, DateTimeKind.Local).AddTicks(6974), new DateTime(2023, 1, 12, 23, 49, 1, 882, DateTimeKind.Local).AddTicks(1433) },
                    { new Guid("b5f5f513-a677-43ff-9bb1-98aeccb67c27"), 0, 0, new Guid("5e04f731-9928-407e-bc2b-9573fe485c5d"), false, new Guid("652b84fa-fd03-441f-9552-64c87ce8aca6"), new DateTime(2023, 1, 12, 22, 19, 1, 882, DateTimeKind.Local).AddTicks(1457), new DateTime(2023, 1, 12, 22, 49, 1, 882, DateTimeKind.Local).AddTicks(1461) },
                    { new Guid("37be3d09-213a-4153-9c6f-b301823162c9"), 0, 0, new Guid("5e04f731-9928-407e-bc2b-9573fe485c5d"), false, new Guid("652b84fa-fd03-441f-9552-64c87ce8aca6"), new DateTime(2023, 1, 12, 20, 59, 1, 882, DateTimeKind.Local).AddTicks(1464), new DateTime(2023, 1, 12, 21, 29, 1, 882, DateTimeKind.Local).AddTicks(1467) },
                    { new Guid("a72016bb-896b-481d-a819-cab0547a0a81"), 0, 0, new Guid("5e04f731-9928-407e-bc2b-9573fe485c5d"), false, new Guid("652b84fa-fd03-441f-9552-64c87ce8aca6"), new DateTime(2023, 1, 12, 19, 49, 1, 882, DateTimeKind.Local).AddTicks(1469), new DateTime(2023, 1, 12, 20, 59, 1, 882, DateTimeKind.Local).AddTicks(1471) },
                    { new Guid("0f77c0ab-9102-4bdd-8e9f-6d1f3bb447d2"), 0, 0, new Guid("5e04f731-9928-407e-bc2b-9573fe485c5d"), false, new Guid("652b84fa-fd03-441f-9552-64c87ce8aca6"), new DateTime(2023, 1, 12, 18, 29, 1, 882, DateTimeKind.Local).AddTicks(1474), new DateTime(2023, 1, 12, 20, 9, 1, 882, DateTimeKind.Local).AddTicks(1477) },
                    { new Guid("053e8a74-2c83-4d58-a868-2b098ed88bb3"), 0, 0, new Guid("5e04f731-9928-407e-bc2b-9573fe485c5d"), false, new Guid("652b84fa-fd03-441f-9552-64c87ce8aca6"), new DateTime(2023, 1, 12, 17, 14, 1, 882, DateTimeKind.Local).AddTicks(1479), new DateTime(2023, 1, 12, 18, 14, 1, 882, DateTimeKind.Local).AddTicks(1481) },
                    { new Guid("607be4f2-9947-415c-a0ea-cc0f937c274a"), 0, 0, new Guid("5e04f731-9928-407e-bc2b-9573fe485c5d"), false, new Guid("652b84fa-fd03-441f-9552-64c87ce8aca6"), new DateTime(2022, 7, 27, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 7, 27, 10, 30, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("bb0b39f7-8792-4c13-a15e-f2e54ee8c273"), 0, 0, new Guid("5e04f731-9928-407e-bc2b-9573fe485c5d"), false, new Guid("652b84fa-fd03-441f-9552-64c87ce8aca6"), new DateTime(2022, 8, 27, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 8, 27, 10, 30, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("5fc058c8-c144-4b6e-bf2d-4771b77ebb5c"), 0, 0, new Guid("5e04f731-9928-407e-bc2b-9573fe485c5d"), false, new Guid("652b84fa-fd03-441f-9552-64c87ce8aca6"), new DateTime(2022, 9, 27, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 27, 10, 30, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("d6951e1c-b3ad-488a-bfac-10f6c93ae186"), 0, 0, new Guid("5e04f731-9928-407e-bc2b-9573fe485c5d"), false, new Guid("652b84fa-fd03-441f-9552-64c87ce8aca6"), new DateTime(2022, 10, 27, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 27, 10, 30, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("3cfd203b-ebb2-458b-bb37-a8da5478bd22"), 0, 0, new Guid("5e04f731-9928-407e-bc2b-9573fe485c5d"), false, new Guid("652b84fa-fd03-441f-9552-64c87ce8aca6"), new DateTime(2022, 11, 27, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 11, 27, 10, 30, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("6de01b45-dceb-40a5-9abb-5726fb91f934"), 0, 0, new Guid("5e04f731-9928-407e-bc2b-9573fe485c5d"), false, new Guid("652b84fa-fd03-441f-9552-64c87ce8aca6"), new DateTime(2022, 12, 27, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 12, 27, 10, 30, 0, 0, DateTimeKind.Unspecified) }
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
                name: "IX_DomainEvent<EventStoreSchedulingAppointmentType>_Appointmen~",
                table: "DomainEvent<EventStoreSchedulingAppointmentType>",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentMovementAppointment_DestinationRoomId",
                table: "EquipmentMovementAppointment",
                column: "DestinationRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentMovementAppointment_OriginalRoomId",
                table: "EquipmentMovementAppointment",
                column: "OriginalRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_EventStoreExaminations_AggregateId",
                table: "EventStoreExaminations",
                column: "AggregateId");

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
                name: "IX_Feedback_PatientId",
                table: "Feedback",
                column: "PatientId");

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
                name: "IX_MaliciousPatients_PatientId",
                table: "MaliciousPatients",
                column: "PatientId");

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
                name: "IX_PatientHealthState_RootId",
                table: "PatientHealthState",
                column: "RootId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientHealthStateNotification_PatientId",
                table: "PatientHealthStateNotification",
                column: "PatientId");

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
                name: "DomainEvent<EventStoreSchedulingAppointmentType>");

            migrationBuilder.DropTable(
                name: "EquipmentMovementAppointment");

            migrationBuilder.DropTable(
                name: "EventStoreExaminations");

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
                name: "MaliciousPatients");

            migrationBuilder.DropTable(
                name: "Managers");

            migrationBuilder.DropTable(
                name: "MedicinePrescription");

            migrationBuilder.DropTable(
                name: "PatientHealthState");

            migrationBuilder.DropTable(
                name: "PatientHealthStateNotification");

            migrationBuilder.DropTable(
                name: "RoomEquipment");

            migrationBuilder.DropTable(
                name: "RoomEvent");

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
                name: "Medicines");

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
