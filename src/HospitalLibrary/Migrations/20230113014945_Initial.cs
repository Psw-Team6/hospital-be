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
                    { new Guid("2f19f017-1396-41fe-a9bc-6ee4aa1141d5"), "Novi Sad", "Serbia", 21000, "JNA", "33" },
                    { new Guid("e6e4e8fa-7a88-4b7b-a1b1-31719b0e14bc"), "Novi Sad", "Serbia", 21000, "Partizanska", "33" },
                    { new Guid("460f38ea-304e-497f-9bd2-0e472fac4933"), "Novi Sad", "Serbia", 21000, "Kosovska", "23A" }
                });

            migrationBuilder.InsertData(
                table: "Allergens",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("7dae6817-763f-4c3e-b503-3514d2133b5a"), "Brufen" },
                    { new Guid("e6298f68-8f70-4260-8d68-52ff601fbd4e"), "Paracetamol" }
                });

            migrationBuilder.InsertData(
                table: "BloodUnits",
                columns: new[] { "Id", "Amount", "BloodBankName", "BloodType", "Date", "Source" },
                values: new object[,]
                {
                    { new Guid("4b3c152f-1d67-4612-aa98-a3b9d0e07197"), 7, "Moja Banka Krvi", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("8bf61cff-4d03-4274-ad4c-ae2f94ceec11"), 10, "Moja Banka Krvi", 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("8f65cf75-51e1-4c4a-931e-e72eaf3697e1"), 4, "Moja Banka Krvi", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("15dc23f6-3330-4f87-ab05-4cc965f209ca"), 5, "Moja Banka Krvi", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "URGENT" }
                });

            migrationBuilder.InsertData(
                table: "Buildings",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("5c0be9a4-e4dc-4987-9149-e384ec99babc"), "Stara bolnica" },
                    { new Guid("863e66f4-9c2d-4fce-af89-7bcbf4b0d098"), "Nova bolnica" }
                });

            migrationBuilder.InsertData(
                table: "GRooms",
                columns: new[] { "Id", "Lenght", "PositionX", "PositionY", "RoomId", "Width" },
                values: new object[,]
                {
                    { new Guid("7e652d6f-6834-4223-8dbf-707fc08caec1"), 5, 5, 0, new Guid("5ef0ef71-f8aa-4b1d-a1d5-063f1a2ce08f"), 5 },
                    { new Guid("fd029de9-64a6-4474-99b1-6c9faa22cfe6"), 5, 0, 0, new Guid("67d8f91c-034f-4651-a95d-051d0e0bdaf3"), 5 },
                    { new Guid("d9439d83-590a-47d4-a38d-411f49906f82"), 5, 5, 0, new Guid("647e243c-6952-49a9-a3f8-d6abec992079"), 5 },
                    { new Guid("c9f973ab-db92-4b02-b728-f64c01b7b1c7"), 5, 5, 0, new Guid("81f79380-6053-4692-b577-a1021606b61d"), 5 },
                    { new Guid("246b547c-fd50-4c58-b5cd-af400539f46e"), 5, 5, 0, new Guid("c52aa8ef-c9f9-4068-b04b-83aaec259908"), 5 },
                    { new Guid("d6a56303-0c28-4026-81f3-459dc651c524"), 5, 5, 0, new Guid("ce0e4866-2fce-42ed-99c5-718a1c062ed1"), 5 },
                    { new Guid("efe97c04-61f6-49bc-93f7-1fc8f39e925e"), 5, 5, 0, new Guid("3739420c-1fa8-41ae-b7db-07e8291eb060"), 5 },
                    { new Guid("81a1e206-a87e-4609-bb32-787c62ec3eb9"), 5, 5, 0, new Guid("643bc862-3bb1-445f-b8ee-09783dc8933d"), 5 },
                    { new Guid("ba364358-1a72-460c-8639-c0927d22b09b"), 5, 5, 0, new Guid("162c7f30-c913-457b-a6d7-9e156f96d7ef"), 5 }
                });

            migrationBuilder.InsertData(
                table: "Medicines",
                columns: new[] { "Id", "Amount", "Name" },
                values: new object[,]
                {
                    { new Guid("0bd12c50-ed0b-4d68-98dd-d200c65454bd"), 1000, "Medicine5" },
                    { new Guid("4c8789b5-439a-4c3d-bed8-01596ff57d8c"), 1000, "Medicine4" },
                    { new Guid("33192ea1-ccb3-462f-9280-989e1e1f8345"), 1000, "Medicine3" },
                    { new Guid("998fbcb2-8c1d-48ad-801b-ecd77e6c401f"), 1000, "Medicine6" },
                    { new Guid("cd6bff09-3deb-4ee6-b7c8-630a3e4b9bbe"), 1000, "Medicine1" },
                    { new Guid("4d2f2c07-68b4-4b95-b0d4-58e29a7cb48a"), 1, "Aspirin" },
                    { new Guid("4470a5ac-c964-4c77-81f6-9e7f78283e9e"), 1000, "Medicine2" },
                    { new Guid("ada8de4e-5459-42d3-b909-af0c0cd0d533"), 30, "Brufen 300" }
                });

            migrationBuilder.InsertData(
                table: "RoomEvent",
                columns: new[] { "Id", "EventName", "TimeStamp", "UserId", "Value" },
                values: new object[] { new Guid("2739bfbc-293f-444c-97c1-384943b05a89"), "SessionStarted", new DateTime(2023, 1, 13, 2, 49, 44, 339, DateTimeKind.Local).AddTicks(7288), new Guid("1ec429df-38fc-4087-8e78-a9c0e2f89312"), "null" });

            migrationBuilder.InsertData(
                table: "Specializations",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("876f291a-3f29-4746-912c-78ee4b44726a"), "General" },
                    { new Guid("3a8f27a3-6de0-4fe9-aa5d-89997b968143"), "Dermatology" },
                    { new Guid("152e0cca-29e7-4c2a-86f2-e74f939a990b"), "Surgeon" }
                });

            migrationBuilder.InsertData(
                table: "Symptoms",
                columns: new[] { "Id", "Description" },
                values: new object[,]
                {
                    { new Guid("a204f10e-9c7c-49eb-b8f5-53f1741397c8"), "Nausea" },
                    { new Guid("a18dfa1e-f72c-4796-8a72-d8ee197cf67a"), "Eye irritation" },
                    { new Guid("3ec0cef1-bc20-48b3-a7e5-1bc6521173df"), "Runny nose" },
                    { new Guid("4b3de336-25d1-4811-bce7-f28ea659449a"), "Stuffy nose" },
                    { new Guid("147e7bfb-7d8d-4af3-b988-c1c960c4a466"), "Puffy, watery eyes" },
                    { new Guid("227f4d8c-4f94-4fef-8971-1020182ea6b8"), "Sneezing" },
                    { new Guid("7be130ae-8aad-4662-8151-050aff33a7c7"), "High temperature" },
                    { new Guid("152545d5-786b-4521-a15d-4602267091be"), "Difficulty breathing" },
                    { new Guid("beaf7a6f-a9e6-49b9-af56-76455cce3eed"), "Cold" },
                    { new Guid("75baa9c4-bda6-48b6-94ea-a8df79bf429f"), "Vomiting" },
                    { new Guid("86faf410-1fff-4484-bea3-a4e577ffbd86"), "Flu" },
                    { new Guid("439d4c0d-71ef-4937-9bf9-641e423671f2"), "Headache" },
                    { new Guid("92b336cd-d8ca-414c-b9ec-a86bcf7167ec"), "Eye irritation" },
                    { new Guid("4af9dfd8-af66-43df-8904-a9749d616137"), "More intense pain and fatigue" },
                    { new Guid("7bead2c8-bdcc-4c11-87db-aa90866952eb"), "Dry cough" },
                    { new Guid("588ffe29-fa85-4c28-881f-1f225212b338"), "Sore throat" },
                    { new Guid("06dce648-69fb-42ce-96ff-94e8d7740d71"), "Abdominal pain" },
                    { new Guid("62e3aeb5-9a6d-452f-af8c-a5b647af71bc"), "Diarrhea" },
                    { new Guid("596c4589-dc9f-46f7-b417-abe14346adc2"), "Mononucleosis" },
                    { new Guid("91a53d6e-b901-4a82-8293-02d821d84468"), "Fever" },
                    { new Guid("f4154e3a-2135-4cd1-94aa-67fed7bdb353"), "Stomach Aches" }
                });

            migrationBuilder.InsertData(
                table: "WorkingSchedules",
                columns: new[] { "Id", "DayOfWorkFrom", "DayOfWorkTo", "ScheduleFrom", "ScheduleTo" },
                values: new object[,]
                {
                    { new Guid("541dca18-475b-4e2b-bfc2-34ec8904ec5e"), new DateTime(2022, 10, 27, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 27, 22, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("e241d70d-9ccb-4244-8225-93e027196fe4"), new DateTime(2022, 10, 27, 8, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 12, 27, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "ApplicationUsers",
                columns: new[] { "Id", "AddressId", "Email", "Enabled", "IsBlocked", "Jmbg", "Name", "Password", "Phone", "Surname", "UserRole", "Username" },
                values: new object[,]
                {
                    { new Guid("9608076c-c376-4a41-a4d3-cf81d540b2ba"), new Guid("e6e4e8fa-7a88-4b7b-a1b1-31719b0e14bc"), "DjordjeLopov@gmail.com", true, false, null, "Djordje", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", null, "Vuckovic", 0, "Tadjo" },
                    { new Guid("57c88fd6-8336-4c23-a0d3-37b5a777e9c3"), new Guid("2f19f017-1396-41fe-a9bc-6ee4aa1141d5"), "psw.isa.mail@gmail.com", true, false, null, "Darko", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", null, "Darkovic", 2, "Darko" },
                    { new Guid("d4ebb2e0-666d-4c74-8d47-1478fc649fff"), new Guid("2f19f017-1396-41fe-a9bc-6ee4aa1141d5"), "psw.isa.mail@gmail.com", true, false, null, "Manja", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", null, "Maric", 2, "Manja" },
                    { new Guid("137e882f-b81f-46b8-8d97-23ea0fc78863"), new Guid("2f19f017-1396-41fe-a9bc-6ee4aa1141d5"), "psw.isa.mail@gmail.com", true, false, null, "Marko", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", null, "Markovic", 2, "Marko" },
                    { new Guid("37032b86-5d68-4a3f-a386-a5779e97eaa6"), new Guid("2f19f017-1396-41fe-a9bc-6ee4aa1141d5"), "psw.isa.mail@gmail.com", true, false, null, "Nikola", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", null, "Nikolic", 2, "Nikola" },
                    { new Guid("6b2ed5fc-4b64-46e7-964b-b66430bdc364"), new Guid("2f19f017-1396-41fe-a9bc-6ee4aa1141d5"), "psw.isa.mail@gmail.com", true, false, null, "Mina", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", null, "Minic", 2, "Mina" },
                    { new Guid("001cd202-fc16-4532-a763-b89341d68852"), new Guid("2f19f017-1396-41fe-a9bc-6ee4aa1141d5"), "psw.isa.mail@gmail.com", true, false, null, "Nina", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", null, "Minic", 2, "Nina" },
                    { new Guid("776d830a-2886-4ee8-ad74-af685ad94209"), new Guid("2f19f017-1396-41fe-a9bc-6ee4aa1141d5"), "psw.isa.mail@gmail.com", true, false, null, "Miki", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", null, "Djuricic", 2, "Miki" },
                    { new Guid("5a11ff8b-9039-4335-b767-4c1c4d9ca4c3"), new Guid("2f19f017-1396-41fe-a9bc-6ee4aa1141d5"), "psw.isa.mail@gmail.com", true, false, null, "Manager", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", null, "Blood Bank", 3, "ManagerBB" },
                    { new Guid("1ec429df-38fc-4087-8e78-a9c0e2f89312"), new Guid("2f19f017-1396-41fe-a9bc-6ee4aa1141d5"), "psw.isa.mail@gmail.com", true, false, null, "Manager", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", null, "Manger", 1, "Manager" },
                    { new Guid("2400f61c-df18-4aa2-ba03-971a5de4e36a"), new Guid("2f19f017-1396-41fe-a9bc-6ee4aa1141d5"), "psw.isa.mail@gmail.com", true, false, null, "Moja Banka Krvi", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", null, "Moja Banka Krvi", 4, "BloodBank" },
                    { new Guid("9d8c6232-ae54-4695-8002-df8131613e88"), new Guid("2f19f017-1396-41fe-a9bc-6ee4aa1141d5"), "psw.isa.mail@gmail.com", true, false, null, "Filip", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", null, "Filipic", 2, "Filip" },
                    { new Guid("9b2bc501-e0e2-466c-beac-2ce11b88e364"), new Guid("2f19f017-1396-41fe-a9bc-6ee4aa1141d5"), "psw.isa.mail@gmail.com", true, false, null, "Tara", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", null, "Markovic", 2, "Tara" },
                    { new Guid("4346eef2-ce47-4cd3-a973-89104f8c0ca0"), new Guid("460f38ea-304e-497f-9bd2-0e472fac4933"), "Cajons@gmail.com", true, false, null, "Jakov", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", null, "Milosevic", 0, "Jakov" },
                    { new Guid("1956470c-4059-4587-9231-265ca72502f2"), new Guid("460f38ea-304e-497f-9bd2-0e472fac4933"), "Cajons@gmail.com", true, false, null, "Milos", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", null, "Milosevic", 0, "Milos" },
                    { new Guid("868e4abc-83b0-4c5a-be7e-c43db48c997a"), new Guid("460f38ea-304e-497f-9bd2-0e472fac4933"), "Cajons@gmail.com", true, false, null, "Ilija", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", null, "Maric", 0, "Ilija" },
                    { new Guid("646f05f0-3fe6-43b5-b2d0-0489e2cf69be"), new Guid("460f38ea-304e-497f-9bd2-0e472fac4933"), "psw.isa.mail@gmail.com", true, false, null, "Sale", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", null, "Lave", 2, "Sale" }
                });

            migrationBuilder.InsertData(
                table: "BloodConsumptions",
                columns: new[] { "Id", "Amount", "BloodUnitId", "Date", "DoctorId", "Purpose" },
                values: new object[,]
                {
                    { new Guid("34145882-246f-4ec9-b822-cd27550e258a"), 4, new Guid("4b3c152f-1d67-4612-aa98-a3b9d0e07197"), new DateTime(2022, 11, 14, 15, 0, 0, 0, DateTimeKind.Unspecified), new Guid("9608076c-c376-4a41-a4d3-cf81d540b2ba"), "operation" },
                    { new Guid("96663560-b26f-4ca8-a4ee-da5dfdc9fca2"), 2, new Guid("4b3c152f-1d67-4612-aa98-a3b9d0e07197"), new DateTime(2022, 10, 27, 15, 0, 0, 0, DateTimeKind.Unspecified), new Guid("9608076c-c376-4a41-a4d3-cf81d540b2ba"), "operation" }
                });

            migrationBuilder.InsertData(
                table: "Floors",
                columns: new[] { "Id", "BuildingId", "FloorNumber", "Name" },
                values: new object[,]
                {
                    { new Guid("aebf621e-dcba-4a07-8c73-a6c3fbc2a0de"), new Guid("863e66f4-9c2d-4fce-af89-7bcbf4b0d098"), 2, "F2" },
                    { new Guid("fc5eb75f-5eb2-4bb4-89df-a8c14288c31b"), new Guid("863e66f4-9c2d-4fce-af89-7bcbf4b0d098"), 1, "F1" },
                    { new Guid("7e6a6f46-d7b5-4766-88f8-5266bf9d0807"), new Guid("863e66f4-9c2d-4fce-af89-7bcbf4b0d098"), 0, "F0" },
                    { new Guid("743d85f7-5270-4809-bc6f-7dbe3869af9c"), new Guid("5c0be9a4-e4dc-4987-9149-e384ec99babc"), 2, "F2" },
                    { new Guid("40c7caad-3750-4913-912d-06ab07941339"), new Guid("5c0be9a4-e4dc-4987-9149-e384ec99babc"), 1, "F1" },
                    { new Guid("6ee65446-2ccc-40e9-b4d0-a1fd7272ef90"), new Guid("5c0be9a4-e4dc-4987-9149-e384ec99babc"), 0, "F0" }
                });

            migrationBuilder.InsertData(
                table: "Managers",
                column: "Id",
                values: new object[]
                {
                    new Guid("1ec429df-38fc-4087-8e78-a9c0e2f89312"),
                    new Guid("5a11ff8b-9039-4335-b767-4c1c4d9ca4c3")
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "BuildingId", "FloorId", "GRoomId", "Name", "Type" },
                values: new object[,]
                {
                    { new Guid("67d8f91c-034f-4651-a95d-051d0e0bdaf3"), new Guid("5c0be9a4-e4dc-4987-9149-e384ec99babc"), new Guid("6ee65446-2ccc-40e9-b4d0-a1fd7272ef90"), new Guid("fd029de9-64a6-4474-99b1-6c9faa22cfe6"), "A11", 0 },
                    { new Guid("647e243c-6952-49a9-a3f8-d6abec992079"), new Guid("5c0be9a4-e4dc-4987-9149-e384ec99babc"), new Guid("6ee65446-2ccc-40e9-b4d0-a1fd7272ef90"), new Guid("d9439d83-590a-47d4-a38d-411f49906f82"), "B11", 0 },
                    { new Guid("5ef0ef71-f8aa-4b1d-a1d5-063f1a2ce08f"), new Guid("5c0be9a4-e4dc-4987-9149-e384ec99babc"), new Guid("40c7caad-3750-4913-912d-06ab07941339"), new Guid("7e652d6f-6834-4223-8dbf-707fc08caec1"), "A12", 0 },
                    { new Guid("81f79380-6053-4692-b577-a1021606b61d"), new Guid("5c0be9a4-e4dc-4987-9149-e384ec99babc"), new Guid("743d85f7-5270-4809-bc6f-7dbe3869af9c"), new Guid("c9f973ab-db92-4b02-b728-f64c01b7b1c7"), "A13", 0 },
                    { new Guid("162c7f30-c913-457b-a6d7-9e156f96d7ef"), new Guid("863e66f4-9c2d-4fce-af89-7bcbf4b0d098"), new Guid("7e6a6f46-d7b5-4766-88f8-5266bf9d0807"), new Guid("ba364358-1a72-460c-8639-c0927d22b09b"), "A21", 2 },
                    { new Guid("c52aa8ef-c9f9-4068-b04b-83aaec259908"), new Guid("863e66f4-9c2d-4fce-af89-7bcbf4b0d098"), new Guid("7e6a6f46-d7b5-4766-88f8-5266bf9d0807"), new Guid("246b547c-fd50-4c58-b5cd-af400539f46e"), "B21", 2 },
                    { new Guid("ce0e4866-2fce-42ed-99c5-718a1c062ed1"), new Guid("863e66f4-9c2d-4fce-af89-7bcbf4b0d098"), new Guid("fc5eb75f-5eb2-4bb4-89df-a8c14288c31b"), new Guid("d6a56303-0c28-4026-81f3-459dc651c524"), "A22", 2 },
                    { new Guid("3739420c-1fa8-41ae-b7db-07e8291eb060"), new Guid("863e66f4-9c2d-4fce-af89-7bcbf4b0d098"), new Guid("aebf621e-dcba-4a07-8c73-a6c3fbc2a0de"), new Guid("efe97c04-61f6-49bc-93f7-1fc8f39e925e"), "C23", 2 },
                    { new Guid("643bc862-3bb1-445f-b8ee-09783dc8933d"), new Guid("863e66f4-9c2d-4fce-af89-7bcbf4b0d098"), new Guid("aebf621e-dcba-4a07-8c73-a6c3fbc2a0de"), new Guid("81a1e206-a87e-4609-bb32-787c62ec3eb9"), "B23", 2 }
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "RoomId", "SpecializationId", "WorkingScheduleId" },
                values: new object[,]
                {
                    { new Guid("868e4abc-83b0-4c5a-be7e-c43db48c997a"), new Guid("67d8f91c-034f-4651-a95d-051d0e0bdaf3"), new Guid("876f291a-3f29-4746-912c-78ee4b44726a"), new Guid("e241d70d-9ccb-4244-8225-93e027196fe4") },
                    { new Guid("4346eef2-ce47-4cd3-a973-89104f8c0ca0"), new Guid("81f79380-6053-4692-b577-a1021606b61d"), new Guid("152e0cca-29e7-4c2a-86f2-e74f939a990b"), new Guid("541dca18-475b-4e2b-bfc2-34ec8904ec5e") },
                    { new Guid("1956470c-4059-4587-9231-265ca72502f2"), new Guid("ce0e4866-2fce-42ed-99c5-718a1c062ed1"), new Guid("876f291a-3f29-4746-912c-78ee4b44726a"), new Guid("e241d70d-9ccb-4244-8225-93e027196fe4") },
                    { new Guid("9608076c-c376-4a41-a4d3-cf81d540b2ba"), new Guid("647e243c-6952-49a9-a3f8-d6abec992079"), new Guid("3a8f27a3-6de0-4fe9-aa5d-89997b968143"), new Guid("e241d70d-9ccb-4244-8225-93e027196fe4") }
                });

            migrationBuilder.InsertData(
                table: "RoomBeds",
                columns: new[] { "Id", "IsFree", "Number", "RoomId" },
                values: new object[,]
                {
                    { new Guid("36f6f173-69f5-490a-82b9-07034eb213a2"), true, "12A5", new Guid("647e243c-6952-49a9-a3f8-d6abec992079") },
                    { new Guid("6f7b963c-b908-4a92-9a65-73e46e1b0793"), true, "12A4", new Guid("647e243c-6952-49a9-a3f8-d6abec992079") },
                    { new Guid("723258f3-b792-4026-b6a3-7a7940a86b41"), true, "12A3", new Guid("647e243c-6952-49a9-a3f8-d6abec992079") },
                    { new Guid("d68d43c5-4c5c-4cfe-b72d-0ecaaa0b8f99"), true, "12A1", new Guid("647e243c-6952-49a9-a3f8-d6abec992079") },
                    { new Guid("2ad2e34b-8fdf-4200-82da-a771096d2258"), true, "12A2", new Guid("647e243c-6952-49a9-a3f8-d6abec992079") },
                    { new Guid("fe487883-a675-4efe-8788-835c39852a05"), true, "11A4", new Guid("67d8f91c-034f-4651-a95d-051d0e0bdaf3") },
                    { new Guid("52e29af2-4845-4f75-baba-75379ab351c9"), true, "11A3", new Guid("67d8f91c-034f-4651-a95d-051d0e0bdaf3") },
                    { new Guid("d4911c54-9578-4087-ba6e-9768dc59105f"), true, "11A2", new Guid("67d8f91c-034f-4651-a95d-051d0e0bdaf3") },
                    { new Guid("720a022b-0c70-4d69-8dea-f2674375d202"), true, "11A1", new Guid("67d8f91c-034f-4651-a95d-051d0e0bdaf3") }
                });

            migrationBuilder.InsertData(
                table: "RoomEquipment",
                columns: new[] { "RoomEquipmentId", "Amount", "EquipmentName", "RoomId" },
                values: new object[,]
                {
                    { new Guid("61fdfa73-24da-45bf-a661-82753e034b58"), 3, "ANESTHESIA", new Guid("3739420c-1fa8-41ae-b7db-07e8291eb060") },
                    { new Guid("28d2e9e5-609a-4888-aee4-f37cbf1bd211"), 6, "SYRINGE", new Guid("162c7f30-c913-457b-a6d7-9e156f96d7ef") },
                    { new Guid("ad134e58-2759-4d78-826d-d2a34288cab1"), 13, "BANDAGE", new Guid("3739420c-1fa8-41ae-b7db-07e8291eb060") },
                    { new Guid("3522761e-0d78-4f08-abe1-d102bc168ca5"), 7, "SYRINGE", new Guid("c52aa8ef-c9f9-4068-b04b-83aaec259908") },
                    { new Guid("7e0726da-e57c-4b25-b68c-951241b46ec9"), 14, "SURGICAL_TABLES", new Guid("c52aa8ef-c9f9-4068-b04b-83aaec259908") },
                    { new Guid("c6cc896c-0e18-4ad0-a0a5-9be86e032e73"), 15, "SURGICAL_TABLES", new Guid("162c7f30-c913-457b-a6d7-9e156f96d7ef") },
                    { new Guid("b76f6e4c-ab24-4c6a-af20-127181855997"), 6, "BANDAGE", new Guid("ce0e4866-2fce-42ed-99c5-718a1c062ed1") },
                    { new Guid("fecd3033-3102-4558-848d-37e8e4a0d8c1"), 22, "EKG_MACHINE", new Guid("162c7f30-c913-457b-a6d7-9e156f96d7ef") },
                    { new Guid("36a483ab-3d88-4e2f-bd18-76de869e1f61"), 5, "EKG_MACHINE", new Guid("647e243c-6952-49a9-a3f8-d6abec992079") },
                    { new Guid("d33d1e79-82ff-4c7a-83d7-f5aa8c263523"), 11, "ANESTHESIA", new Guid("81f79380-6053-4692-b577-a1021606b61d") },
                    { new Guid("3ddec615-2a45-4217-9d48-0fdf06522341"), 23, "SURGICAL_TABLES", new Guid("5ef0ef71-f8aa-4b1d-a1d5-063f1a2ce08f") },
                    { new Guid("f176ba08-246c-46bb-af80-22f61670bf82"), 3, "EKG_MACHINE", new Guid("5ef0ef71-f8aa-4b1d-a1d5-063f1a2ce08f") },
                    { new Guid("2a2e90ce-a255-4c15-aec4-0e526ef45987"), 9, "SURGICAL_TABLES", new Guid("643bc862-3bb1-445f-b8ee-09783dc8933d") },
                    { new Guid("2dd6531d-dd09-4685-b190-43a5e75143fe"), 10, "ANESTHESIA", new Guid("647e243c-6952-49a9-a3f8-d6abec992079") },
                    { new Guid("6917d883-6739-46da-ac46-e53802e68495"), 13, "SYRINGE", new Guid("67d8f91c-034f-4651-a95d-051d0e0bdaf3") },
                    { new Guid("37c9548e-e3d2-425a-92e0-d57566947bae"), 5, "ANESTHESIA", new Guid("67d8f91c-034f-4651-a95d-051d0e0bdaf3") },
                    { new Guid("85d28332-be7f-48e9-9670-2a6af3056be8"), 15, "SURGICAL_TABLES", new Guid("67d8f91c-034f-4651-a95d-051d0e0bdaf3") },
                    { new Guid("738a7307-1c44-48f8-8296-81ed3035de3d"), 4, "BANDAGE", new Guid("81f79380-6053-4692-b577-a1021606b61d") },
                    { new Guid("27b6c241-1901-49a7-ab57-aa68e8426df5"), 9, "ANESTHESIA", new Guid("643bc862-3bb1-445f-b8ee-09783dc8933d") }
                });

            migrationBuilder.InsertData(
                table: "Holidays",
                columns: new[] { "Id", "Description", "DoctorId", "HolidayStatus", "IsUrgent", "DateRange_From", "DateRange_To" },
                values: new object[] { new Guid("8c0bcc6e-eae5-48e9-941e-283e09b3de50"), "I want to go to Paralia", new Guid("868e4abc-83b0-4c5a-be7e-c43db48c997a"), 0, false, new DateTime(2022, 10, 27, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 27, 10, 30, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "Age", "BloodType", "DateOfBirth", "DoctorId", "Gender" },
                values: new object[,]
                {
                    { new Guid("646f05f0-3fe6-43b5-b2d0-0489e2cf69be"), 15, 4, new DateTime(2007, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("868e4abc-83b0-4c5a-be7e-c43db48c997a"), 0 },
                    { new Guid("776d830a-2886-4ee8-ad74-af685ad94209"), 32, 0, new DateTime(1990, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("868e4abc-83b0-4c5a-be7e-c43db48c997a"), 0 },
                    { new Guid("001cd202-fc16-4532-a763-b89341d68852"), 5, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("868e4abc-83b0-4c5a-be7e-c43db48c997a"), 1 },
                    { new Guid("37032b86-5d68-4a3f-a386-a5779e97eaa6"), 18, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("868e4abc-83b0-4c5a-be7e-c43db48c997a"), 2 },
                    { new Guid("57c88fd6-8336-4c23-a0d3-37b5a777e9c3"), 70, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("9608076c-c376-4a41-a4d3-cf81d540b2ba"), 0 },
                    { new Guid("9d8c6232-ae54-4695-8002-df8131613e88"), 56, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("9608076c-c376-4a41-a4d3-cf81d540b2ba"), 0 },
                    { new Guid("6b2ed5fc-4b64-46e7-964b-b66430bdc364"), 9, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("1956470c-4059-4587-9231-265ca72502f2"), 1 },
                    { new Guid("137e882f-b81f-46b8-8d97-23ea0fc78863"), 65, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("1956470c-4059-4587-9231-265ca72502f2"), 0 },
                    { new Guid("d4ebb2e0-666d-4c74-8d47-1478fc649fff"), 50, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("1956470c-4059-4587-9231-265ca72502f2"), 1 },
                    { new Guid("9b2bc501-e0e2-466c-beac-2ce11b88e364"), 61, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("1956470c-4059-4587-9231-265ca72502f2"), 1 }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "AppointmentState", "AppointmentType", "DoctorId", "Emergent", "PatientId", "Duration_From", "Duration_To" },
                values: new object[,]
                {
                    { new Guid("c0576733-b7fa-4974-b60c-d3d7e8c9f216"), 0, 0, new Guid("868e4abc-83b0-4c5a-be7e-c43db48c997a"), false, new Guid("646f05f0-3fe6-43b5-b2d0-0489e2cf69be"), new DateTime(2023, 7, 27, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 27, 10, 30, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("aed01124-1953-4b40-93b5-a69afa7c27e9"), 0, 0, new Guid("868e4abc-83b0-4c5a-be7e-c43db48c997a"), false, new Guid("646f05f0-3fe6-43b5-b2d0-0489e2cf69be"), new DateTime(2023, 1, 13, 1, 49, 44, 335, DateTimeKind.Local).AddTicks(7900), new DateTime(2023, 1, 13, 2, 19, 44, 338, DateTimeKind.Local).AddTicks(8100) },
                    { new Guid("b118cc49-59b1-402d-9cd3-c1e75f8af85c"), 0, 0, new Guid("868e4abc-83b0-4c5a-be7e-c43db48c997a"), false, new Guid("646f05f0-3fe6-43b5-b2d0-0489e2cf69be"), new DateTime(2023, 1, 13, 0, 49, 44, 338, DateTimeKind.Local).AddTicks(8131), new DateTime(2023, 1, 13, 1, 19, 44, 338, DateTimeKind.Local).AddTicks(8134) },
                    { new Guid("6505b320-ad94-49a3-b019-0db1d96e24a0"), 0, 0, new Guid("868e4abc-83b0-4c5a-be7e-c43db48c997a"), false, new Guid("646f05f0-3fe6-43b5-b2d0-0489e2cf69be"), new DateTime(2023, 1, 12, 23, 29, 44, 338, DateTimeKind.Local).AddTicks(8138), new DateTime(2023, 1, 12, 23, 59, 44, 338, DateTimeKind.Local).AddTicks(8140) },
                    { new Guid("4d9c39fb-99ee-4a5e-a452-ff1db3518596"), 0, 0, new Guid("868e4abc-83b0-4c5a-be7e-c43db48c997a"), false, new Guid("646f05f0-3fe6-43b5-b2d0-0489e2cf69be"), new DateTime(2023, 1, 12, 22, 19, 44, 338, DateTimeKind.Local).AddTicks(8147), new DateTime(2023, 1, 12, 23, 29, 44, 338, DateTimeKind.Local).AddTicks(8149) },
                    { new Guid("c11d04fb-ffa6-4662-b561-aa4abc73924b"), 0, 0, new Guid("868e4abc-83b0-4c5a-be7e-c43db48c997a"), false, new Guid("646f05f0-3fe6-43b5-b2d0-0489e2cf69be"), new DateTime(2023, 1, 12, 20, 59, 44, 338, DateTimeKind.Local).AddTicks(8152), new DateTime(2023, 1, 12, 22, 39, 44, 338, DateTimeKind.Local).AddTicks(8154) },
                    { new Guid("17fda728-bf1e-410b-b0f2-f8175cdd52da"), 0, 0, new Guid("868e4abc-83b0-4c5a-be7e-c43db48c997a"), false, new Guid("646f05f0-3fe6-43b5-b2d0-0489e2cf69be"), new DateTime(2023, 1, 12, 19, 44, 44, 338, DateTimeKind.Local).AddTicks(8157), new DateTime(2023, 1, 12, 20, 44, 44, 338, DateTimeKind.Local).AddTicks(8160) },
                    { new Guid("c148ec77-d4ff-476b-bef0-e84caa798d1b"), 0, 0, new Guid("868e4abc-83b0-4c5a-be7e-c43db48c997a"), false, new Guid("646f05f0-3fe6-43b5-b2d0-0489e2cf69be"), new DateTime(2022, 7, 27, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 7, 27, 10, 30, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("45729890-49c9-4132-aece-89a316f6bd51"), 0, 0, new Guid("868e4abc-83b0-4c5a-be7e-c43db48c997a"), false, new Guid("646f05f0-3fe6-43b5-b2d0-0489e2cf69be"), new DateTime(2022, 8, 27, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 8, 27, 10, 30, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("76e8dc82-714b-4d11-a48f-65665b03d959"), 0, 0, new Guid("868e4abc-83b0-4c5a-be7e-c43db48c997a"), false, new Guid("646f05f0-3fe6-43b5-b2d0-0489e2cf69be"), new DateTime(2022, 9, 27, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 27, 10, 30, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("a148eb81-17ff-4137-a11a-9b0be8b514ec"), 0, 0, new Guid("868e4abc-83b0-4c5a-be7e-c43db48c997a"), false, new Guid("646f05f0-3fe6-43b5-b2d0-0489e2cf69be"), new DateTime(2022, 10, 27, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 27, 10, 30, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("a3f25837-81ee-4707-864b-dd89ce0880df"), 0, 0, new Guid("868e4abc-83b0-4c5a-be7e-c43db48c997a"), false, new Guid("646f05f0-3fe6-43b5-b2d0-0489e2cf69be"), new DateTime(2022, 11, 27, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 11, 27, 10, 30, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("c4ee42eb-2607-47e6-a826-c79507c84555"), 0, 0, new Guid("868e4abc-83b0-4c5a-be7e-c43db48c997a"), false, new Guid("646f05f0-3fe6-43b5-b2d0-0489e2cf69be"), new DateTime(2022, 12, 27, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 12, 27, 10, 30, 0, 0, DateTimeKind.Unspecified) }
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
