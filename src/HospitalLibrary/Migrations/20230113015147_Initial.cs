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
                    { new Guid("6b856808-564f-4104-ab1b-eba5ba148cd6"), "Novi Sad", "Serbia", 21000, "JNA", "33" },
                    { new Guid("89602358-aabe-40ce-a9f1-ffbafe0ddbe5"), "Novi Sad", "Serbia", 21000, "Partizanska", "33" },
                    { new Guid("bbd503ae-9092-44aa-8c0c-ea33a07eb08c"), "Novi Sad", "Serbia", 21000, "Kosovska", "23A" }
                });

            migrationBuilder.InsertData(
                table: "Allergens",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("e32acba6-72db-4f89-bb0e-166dc3c8a670"), "Brufen" },
                    { new Guid("68070d19-6175-480b-9eb1-fddc5294cf40"), "Paracetamol" }
                });

            migrationBuilder.InsertData(
                table: "BloodUnits",
                columns: new[] { "Id", "Amount", "BloodBankName", "BloodType", "Date", "Source" },
                values: new object[,]
                {
                    { new Guid("6ab296dd-4e43-4c20-a598-08334de07ddb"), 7, "Moja Banka Krvi", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("710e2969-b40f-417b-81f9-1d6ecfb49a63"), 10, "Moja Banka Krvi", 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6555553f-b095-4e01-bbb3-849f202da976"), 4, "Moja Banka Krvi", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("2aa4b1c3-2373-4c34-9dae-680d7d80e4c9"), 5, "Moja Banka Krvi", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "URGENT" }
                });

            migrationBuilder.InsertData(
                table: "Buildings",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("568380d9-3252-4770-8848-431c82718428"), "Stara bolnica" },
                    { new Guid("1c142059-50c9-46d7-b700-91c0f242e157"), "Nova bolnica" }
                });

            migrationBuilder.InsertData(
                table: "GRooms",
                columns: new[] { "Id", "Lenght", "PositionX", "PositionY", "RoomId", "Width" },
                values: new object[,]
                {
                    { new Guid("66589383-6120-4413-a662-7514eb517243"), 5, 5, 0, new Guid("6d3aa3c6-7083-42da-944f-fac4cf10c040"), 5 },
                    { new Guid("8fb30e0b-180c-48a7-98d1-b33558243ff2"), 5, 0, 0, new Guid("d88c2bf0-653b-4a27-84ea-27b08483527d"), 5 },
                    { new Guid("b7dd6790-fbd0-4968-ac89-143b2d303475"), 5, 5, 0, new Guid("0aa4c1e5-aa74-4a97-8319-457cbe42f691"), 5 },
                    { new Guid("9f682da0-0a5c-41cf-999e-db9c2715a6f2"), 5, 5, 0, new Guid("c1ba9060-59d0-4212-86ce-d5ef73441c75"), 5 },
                    { new Guid("3c7b3406-f1ad-4e4a-9d0a-4565d671686c"), 5, 5, 0, new Guid("b7023d87-6e8e-4d26-80cc-70c543997336"), 5 },
                    { new Guid("e986202e-57bb-4efd-a4c7-00968a1b8742"), 5, 5, 0, new Guid("89038e1f-ca34-4965-b8f8-49fe6d4e00ba"), 5 },
                    { new Guid("46ee8189-6ae6-47e9-a942-95ed51d1f9c6"), 5, 5, 0, new Guid("d315aae0-e2aa-4c1f-b81d-0534c446517b"), 5 },
                    { new Guid("33599b73-fae3-4fc5-ad0d-8a2ca6817aca"), 5, 5, 0, new Guid("54036bee-7da3-4afe-a6d3-f351ccaa4a83"), 5 },
                    { new Guid("84c2dadc-7d7d-4d0a-b44c-dec720ecc730"), 5, 5, 0, new Guid("1b6835b7-c14b-4488-a9fd-42f68470d0c2"), 5 }
                });

            migrationBuilder.InsertData(
                table: "Medicines",
                columns: new[] { "Id", "Amount", "Name" },
                values: new object[,]
                {
                    { new Guid("46541ceb-8847-449e-bc25-0a75b27593d0"), 1000, "Medicine5" },
                    { new Guid("26651aa3-b625-4c13-a32e-25fbb991c413"), 1000, "Medicine4" },
                    { new Guid("89a410e9-17bb-42a9-ab23-d6b0ed2ec7d7"), 1000, "Medicine3" },
                    { new Guid("17f6208d-e451-45b3-b253-60b8e6c2ea7c"), 1000, "Medicine6" },
                    { new Guid("bad64163-531f-4124-9e57-d176690ecc81"), 1000, "Medicine1" },
                    { new Guid("852d3800-9ad9-4802-a885-31a90a121994"), 1, "Aspirin" },
                    { new Guid("da066c33-695f-48c4-90b7-946cbcfb5d3b"), 1000, "Medicine2" },
                    { new Guid("00300b1a-e51b-4cc4-8f57-54896eee64e7"), 30, "Brufen 300" }
                });

            migrationBuilder.InsertData(
                table: "RoomEvent",
                columns: new[] { "Id", "EventName", "TimeStamp", "UserId", "Value" },
                values: new object[] { new Guid("aa1377e8-07a1-46e4-afb0-79585658f2d5"), "SessionStarted", new DateTime(2023, 1, 13, 2, 51, 47, 209, DateTimeKind.Local).AddTicks(917), new Guid("90f13cb3-cf31-469d-bf3c-dffdef88269a"), "null" });

            migrationBuilder.InsertData(
                table: "Specializations",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("fa838465-5f15-4ced-ae08-8f8a1f4991c2"), "General" },
                    { new Guid("cc42df20-363f-4d79-b9d2-5effc6ee4634"), "Dermatology" },
                    { new Guid("1f637627-8b51-4091-b027-21883b61e72c"), "Surgeon" }
                });

            migrationBuilder.InsertData(
                table: "Symptoms",
                columns: new[] { "Id", "Description" },
                values: new object[,]
                {
                    { new Guid("4a017243-3ff0-4e31-9ee5-37f0e7725695"), "Nausea" },
                    { new Guid("d47644c4-80fa-44e9-a4ef-c0f981bdf85c"), "Eye irritation" },
                    { new Guid("891ae072-fc32-4edf-93fe-9ee39582ab2f"), "Runny nose" },
                    { new Guid("68de85e1-1537-464b-bbfc-fc447e46d860"), "Stuffy nose" },
                    { new Guid("b5505320-43e6-467b-86aa-804edd910a88"), "Puffy, watery eyes" },
                    { new Guid("4a0ceb88-73be-44cf-900a-a294c2193397"), "Sneezing" },
                    { new Guid("e1283090-1b19-4d78-9625-3b23ec6845d2"), "High temperature" },
                    { new Guid("226b1b76-c2a4-4d91-a358-8769a86d8fee"), "Difficulty breathing" },
                    { new Guid("290b0ed3-b163-4452-8b53-cb8bf69888c9"), "Cold" },
                    { new Guid("935a4de8-4c4c-409d-b28a-8a742b870d47"), "Vomiting" },
                    { new Guid("e14530af-68b2-41ae-a15b-e5588d64911b"), "Flu" },
                    { new Guid("09f2084c-32a4-42f6-ae38-4d0b82c74d22"), "Headache" },
                    { new Guid("6933faab-293b-4a14-9855-ca8d702c72f0"), "Eye irritation" },
                    { new Guid("c8d9ef86-b116-40d9-b3bb-132f5460a7e5"), "More intense pain and fatigue" },
                    { new Guid("7ff613a0-20dc-43b7-b7be-b16a4df66b0b"), "Dry cough" },
                    { new Guid("0dcd97d3-2bde-4a98-a1c8-0eee780003c4"), "Sore throat" },
                    { new Guid("a054e9b6-7f66-46fa-8778-e941ca21f82c"), "Abdominal pain" },
                    { new Guid("5fab9f92-947e-4404-84d7-0b124fa27168"), "Diarrhea" },
                    { new Guid("0f70eff3-d322-47fa-9c91-eeb362b8f2fd"), "Mononucleosis" },
                    { new Guid("8627701d-c7f5-4e6b-b2e4-8739be146ed2"), "Fever" },
                    { new Guid("a8a67d72-8689-4656-a9bd-0e75c4f26ccc"), "Stomach Aches" }
                });

            migrationBuilder.InsertData(
                table: "WorkingSchedules",
                columns: new[] { "Id", "DayOfWorkFrom", "DayOfWorkTo", "ScheduleFrom", "ScheduleTo" },
                values: new object[,]
                {
                    { new Guid("021f0a2d-c6f8-4a3e-8fec-87f62826a167"), new DateTime(2022, 10, 27, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 27, 22, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("d104ae9b-ee45-4423-b194-488e145621d9"), new DateTime(2022, 10, 27, 8, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 12, 27, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "ApplicationUsers",
                columns: new[] { "Id", "AddressId", "Email", "Enabled", "IsBlocked", "Jmbg", "Name", "Password", "Phone", "Surname", "UserRole", "Username" },
                values: new object[,]
                {
                    { new Guid("106ae974-ce1a-4e21-884f-8e28ab9d0e37"), new Guid("89602358-aabe-40ce-a9f1-ffbafe0ddbe5"), "DjordjeLopov@gmail.com", true, false, null, "Djordje", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", null, "Vuckovic", 0, "Tadjo" },
                    { new Guid("7eb286b3-9c90-4314-9a3e-e71b267e0d83"), new Guid("6b856808-564f-4104-ab1b-eba5ba148cd6"), "psw.isa.mail@gmail.com", true, false, null, "Darko", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", null, "Darkovic", 2, "Darko" },
                    { new Guid("650c5caa-85b9-4b6c-92f9-2d229a490a21"), new Guid("6b856808-564f-4104-ab1b-eba5ba148cd6"), "psw.isa.mail@gmail.com", true, false, null, "Manja", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", null, "Maric", 2, "Manja" },
                    { new Guid("6f0daebc-1391-4b22-bc57-2fc1a25b2dad"), new Guid("6b856808-564f-4104-ab1b-eba5ba148cd6"), "psw.isa.mail@gmail.com", true, false, null, "Marko", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", null, "Markovic", 2, "Marko" },
                    { new Guid("58b497a0-20ff-4cfa-801e-61917b8f5e4a"), new Guid("6b856808-564f-4104-ab1b-eba5ba148cd6"), "psw.isa.mail@gmail.com", true, false, null, "Nikola", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", null, "Nikolic", 2, "Nikola" },
                    { new Guid("ca59216b-8af6-45d8-9620-559f433ba1fe"), new Guid("6b856808-564f-4104-ab1b-eba5ba148cd6"), "psw.isa.mail@gmail.com", true, false, null, "Mina", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", null, "Minic", 2, "Mina" },
                    { new Guid("4ab6d5fc-4765-497f-9cf6-cfdc6e70651f"), new Guid("6b856808-564f-4104-ab1b-eba5ba148cd6"), "psw.isa.mail@gmail.com", true, false, null, "Nina", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", null, "Minic", 2, "Nina" },
                    { new Guid("421f330c-c7b4-4001-b981-d1c89d0afdc5"), new Guid("6b856808-564f-4104-ab1b-eba5ba148cd6"), "psw.isa.mail@gmail.com", true, false, null, "Miki", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", null, "Djuricic", 2, "Miki" },
                    { new Guid("d7fef085-5e2b-496f-87b1-d07cc4313eb6"), new Guid("6b856808-564f-4104-ab1b-eba5ba148cd6"), "psw.isa.mail@gmail.com", true, false, null, "Manager", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", null, "Blood Bank", 3, "ManagerBB" },
                    { new Guid("90f13cb3-cf31-469d-bf3c-dffdef88269a"), new Guid("6b856808-564f-4104-ab1b-eba5ba148cd6"), "psw.isa.mail@gmail.com", true, false, null, "Manager", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", null, "Manger", 1, "Manager" },
                    { new Guid("e60c03d8-1511-41a7-961a-0e60fb9c636c"), new Guid("6b856808-564f-4104-ab1b-eba5ba148cd6"), "psw.isa.mail@gmail.com", true, false, null, "Moja Banka Krvi", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", null, "Moja Banka Krvi", 4, "BloodBank" },
                    { new Guid("a3f79940-bbf5-4389-b285-3ce663437677"), new Guid("6b856808-564f-4104-ab1b-eba5ba148cd6"), "psw.isa.mail@gmail.com", true, false, null, "Filip", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", null, "Filipic", 2, "Filip" },
                    { new Guid("55569299-ba89-40b0-9003-69e2e3abfac9"), new Guid("6b856808-564f-4104-ab1b-eba5ba148cd6"), "psw.isa.mail@gmail.com", true, false, null, "Tara", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", null, "Markovic", 2, "Tara" },
                    { new Guid("c0db68dd-79c2-4c4d-8598-69153d8ed147"), new Guid("bbd503ae-9092-44aa-8c0c-ea33a07eb08c"), "Cajons@gmail.com", true, false, null, "Jakov", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", null, "Milosevic", 0, "Jakov" },
                    { new Guid("c454e084-4d41-4461-8b5d-16c56140daa3"), new Guid("bbd503ae-9092-44aa-8c0c-ea33a07eb08c"), "Cajons@gmail.com", true, false, null, "Milos", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", null, "Milosevic", 0, "Milos" },
                    { new Guid("238b29b1-3025-4349-a3f5-69e7772eecff"), new Guid("bbd503ae-9092-44aa-8c0c-ea33a07eb08c"), "Cajons@gmail.com", true, false, null, "Ilija", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", null, "Maric", 0, "Ilija" },
                    { new Guid("284842dc-f5a9-4558-a408-ba8903dd03f5"), new Guid("bbd503ae-9092-44aa-8c0c-ea33a07eb08c"), "psw.isa.mail@gmail.com", true, false, null, "Sale", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", null, "Lave", 2, "Sale" }
                });

            migrationBuilder.InsertData(
                table: "BloodConsumptions",
                columns: new[] { "Id", "Amount", "BloodUnitId", "Date", "DoctorId", "Purpose" },
                values: new object[,]
                {
                    { new Guid("811f7af9-1f33-4b23-a79c-0ec2da2f1bd5"), 4, new Guid("6ab296dd-4e43-4c20-a598-08334de07ddb"), new DateTime(2022, 11, 14, 15, 0, 0, 0, DateTimeKind.Unspecified), new Guid("106ae974-ce1a-4e21-884f-8e28ab9d0e37"), "operation" },
                    { new Guid("80cd73b0-09c4-43a6-bc28-2c2d10f95c10"), 2, new Guid("6ab296dd-4e43-4c20-a598-08334de07ddb"), new DateTime(2022, 10, 27, 15, 0, 0, 0, DateTimeKind.Unspecified), new Guid("106ae974-ce1a-4e21-884f-8e28ab9d0e37"), "operation" }
                });

            migrationBuilder.InsertData(
                table: "Floors",
                columns: new[] { "Id", "BuildingId", "FloorNumber", "Name" },
                values: new object[,]
                {
                    { new Guid("ec1aaf90-6aa8-4515-b078-4ff72a21eadd"), new Guid("1c142059-50c9-46d7-b700-91c0f242e157"), 2, "F2" },
                    { new Guid("7489e06d-b9ee-4c38-959f-2009f53b4918"), new Guid("1c142059-50c9-46d7-b700-91c0f242e157"), 1, "F1" },
                    { new Guid("a9426238-4726-4d35-becf-36ce525cb1ca"), new Guid("1c142059-50c9-46d7-b700-91c0f242e157"), 0, "F0" },
                    { new Guid("4f74a46e-a341-431d-87b5-cd19f0e00ec5"), new Guid("568380d9-3252-4770-8848-431c82718428"), 2, "F2" },
                    { new Guid("fa80debf-a147-4aa5-be4e-2adc6b2c1794"), new Guid("568380d9-3252-4770-8848-431c82718428"), 1, "F1" },
                    { new Guid("bb3bc660-b5cb-486d-8277-b3ffac62a383"), new Guid("568380d9-3252-4770-8848-431c82718428"), 0, "F0" }
                });

            migrationBuilder.InsertData(
                table: "Managers",
                column: "Id",
                values: new object[]
                {
                    new Guid("90f13cb3-cf31-469d-bf3c-dffdef88269a"),
                    new Guid("d7fef085-5e2b-496f-87b1-d07cc4313eb6")
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "BuildingId", "FloorId", "GRoomId", "Name", "Type" },
                values: new object[,]
                {
                    { new Guid("d88c2bf0-653b-4a27-84ea-27b08483527d"), new Guid("568380d9-3252-4770-8848-431c82718428"), new Guid("bb3bc660-b5cb-486d-8277-b3ffac62a383"), new Guid("8fb30e0b-180c-48a7-98d1-b33558243ff2"), "A11", 0 },
                    { new Guid("0aa4c1e5-aa74-4a97-8319-457cbe42f691"), new Guid("568380d9-3252-4770-8848-431c82718428"), new Guid("bb3bc660-b5cb-486d-8277-b3ffac62a383"), new Guid("b7dd6790-fbd0-4968-ac89-143b2d303475"), "B11", 0 },
                    { new Guid("6d3aa3c6-7083-42da-944f-fac4cf10c040"), new Guid("568380d9-3252-4770-8848-431c82718428"), new Guid("fa80debf-a147-4aa5-be4e-2adc6b2c1794"), new Guid("66589383-6120-4413-a662-7514eb517243"), "A12", 0 },
                    { new Guid("c1ba9060-59d0-4212-86ce-d5ef73441c75"), new Guid("568380d9-3252-4770-8848-431c82718428"), new Guid("4f74a46e-a341-431d-87b5-cd19f0e00ec5"), new Guid("9f682da0-0a5c-41cf-999e-db9c2715a6f2"), "A13", 0 },
                    { new Guid("1b6835b7-c14b-4488-a9fd-42f68470d0c2"), new Guid("1c142059-50c9-46d7-b700-91c0f242e157"), new Guid("a9426238-4726-4d35-becf-36ce525cb1ca"), new Guid("84c2dadc-7d7d-4d0a-b44c-dec720ecc730"), "A21", 2 },
                    { new Guid("b7023d87-6e8e-4d26-80cc-70c543997336"), new Guid("1c142059-50c9-46d7-b700-91c0f242e157"), new Guid("a9426238-4726-4d35-becf-36ce525cb1ca"), new Guid("3c7b3406-f1ad-4e4a-9d0a-4565d671686c"), "B21", 2 },
                    { new Guid("89038e1f-ca34-4965-b8f8-49fe6d4e00ba"), new Guid("1c142059-50c9-46d7-b700-91c0f242e157"), new Guid("7489e06d-b9ee-4c38-959f-2009f53b4918"), new Guid("e986202e-57bb-4efd-a4c7-00968a1b8742"), "A22", 2 },
                    { new Guid("d315aae0-e2aa-4c1f-b81d-0534c446517b"), new Guid("1c142059-50c9-46d7-b700-91c0f242e157"), new Guid("ec1aaf90-6aa8-4515-b078-4ff72a21eadd"), new Guid("46ee8189-6ae6-47e9-a942-95ed51d1f9c6"), "C23", 2 },
                    { new Guid("54036bee-7da3-4afe-a6d3-f351ccaa4a83"), new Guid("1c142059-50c9-46d7-b700-91c0f242e157"), new Guid("ec1aaf90-6aa8-4515-b078-4ff72a21eadd"), new Guid("33599b73-fae3-4fc5-ad0d-8a2ca6817aca"), "B23", 2 }
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "RoomId", "SpecializationId", "WorkingScheduleId" },
                values: new object[,]
                {
                    { new Guid("238b29b1-3025-4349-a3f5-69e7772eecff"), new Guid("d88c2bf0-653b-4a27-84ea-27b08483527d"), new Guid("fa838465-5f15-4ced-ae08-8f8a1f4991c2"), new Guid("d104ae9b-ee45-4423-b194-488e145621d9") },
                    { new Guid("c0db68dd-79c2-4c4d-8598-69153d8ed147"), new Guid("c1ba9060-59d0-4212-86ce-d5ef73441c75"), new Guid("1f637627-8b51-4091-b027-21883b61e72c"), new Guid("021f0a2d-c6f8-4a3e-8fec-87f62826a167") },
                    { new Guid("c454e084-4d41-4461-8b5d-16c56140daa3"), new Guid("89038e1f-ca34-4965-b8f8-49fe6d4e00ba"), new Guid("fa838465-5f15-4ced-ae08-8f8a1f4991c2"), new Guid("d104ae9b-ee45-4423-b194-488e145621d9") },
                    { new Guid("106ae974-ce1a-4e21-884f-8e28ab9d0e37"), new Guid("0aa4c1e5-aa74-4a97-8319-457cbe42f691"), new Guid("cc42df20-363f-4d79-b9d2-5effc6ee4634"), new Guid("d104ae9b-ee45-4423-b194-488e145621d9") }
                });

            migrationBuilder.InsertData(
                table: "RoomBeds",
                columns: new[] { "Id", "IsFree", "Number", "RoomId" },
                values: new object[,]
                {
                    { new Guid("f3cfdd8f-2218-4cfe-84ab-552f511466ef"), true, "12A5", new Guid("0aa4c1e5-aa74-4a97-8319-457cbe42f691") },
                    { new Guid("1a6958c2-f886-46fe-9dd5-ae0b85cb4588"), true, "12A4", new Guid("0aa4c1e5-aa74-4a97-8319-457cbe42f691") },
                    { new Guid("1cf979d3-4040-492e-95ac-0bb82dfa4db4"), true, "12A3", new Guid("0aa4c1e5-aa74-4a97-8319-457cbe42f691") },
                    { new Guid("1d1d86ed-4c14-4d6e-998e-768b199df9b5"), true, "12A1", new Guid("0aa4c1e5-aa74-4a97-8319-457cbe42f691") },
                    { new Guid("0f85a582-0e3a-45b7-9264-f5e7a3106325"), true, "12A2", new Guid("0aa4c1e5-aa74-4a97-8319-457cbe42f691") },
                    { new Guid("3d116f44-265e-44ac-9d66-8e7ec7071c43"), true, "11A4", new Guid("d88c2bf0-653b-4a27-84ea-27b08483527d") },
                    { new Guid("47d0279c-f487-43de-a687-535e3e9b6425"), true, "11A3", new Guid("d88c2bf0-653b-4a27-84ea-27b08483527d") },
                    { new Guid("f1b3f2b1-51e0-42e2-8a7b-1b854f237e7d"), true, "11A2", new Guid("d88c2bf0-653b-4a27-84ea-27b08483527d") },
                    { new Guid("7067a08b-f441-4e82-8f18-77f61bb398e9"), true, "11A1", new Guid("d88c2bf0-653b-4a27-84ea-27b08483527d") }
                });

            migrationBuilder.InsertData(
                table: "RoomEquipment",
                columns: new[] { "RoomEquipmentId", "Amount", "EquipmentName", "RoomId" },
                values: new object[,]
                {
                    { new Guid("396f1abe-4d59-4236-962b-14125b942c8e"), 3, "ANESTHESIA", new Guid("d315aae0-e2aa-4c1f-b81d-0534c446517b") },
                    { new Guid("da919d83-5b5a-4a87-8354-ab85778dc484"), 6, "SYRINGE", new Guid("1b6835b7-c14b-4488-a9fd-42f68470d0c2") },
                    { new Guid("4ca45201-228c-4928-b566-7ccc79323351"), 13, "BANDAGE", new Guid("d315aae0-e2aa-4c1f-b81d-0534c446517b") },
                    { new Guid("6a79acfb-2177-4332-9035-f97845d14b01"), 7, "SYRINGE", new Guid("b7023d87-6e8e-4d26-80cc-70c543997336") },
                    { new Guid("9657ab46-9efc-45a8-a5c3-cea8a15b047c"), 14, "SURGICAL_TABLES", new Guid("b7023d87-6e8e-4d26-80cc-70c543997336") },
                    { new Guid("411450a1-5a94-4e19-80bf-3793f1c09236"), 15, "SURGICAL_TABLES", new Guid("1b6835b7-c14b-4488-a9fd-42f68470d0c2") },
                    { new Guid("5eef24d0-eb2e-48b0-9ea9-0e6bae4c26bb"), 6, "BANDAGE", new Guid("89038e1f-ca34-4965-b8f8-49fe6d4e00ba") },
                    { new Guid("b2b61721-f419-4d6b-b183-d22e1876adf8"), 22, "EKG_MACHINE", new Guid("1b6835b7-c14b-4488-a9fd-42f68470d0c2") },
                    { new Guid("76e772f4-6ab5-4e4d-9d9d-a101b7ffc792"), 5, "EKG_MACHINE", new Guid("0aa4c1e5-aa74-4a97-8319-457cbe42f691") },
                    { new Guid("158761ab-c2c8-4232-a4d3-48a67972e947"), 11, "ANESTHESIA", new Guid("c1ba9060-59d0-4212-86ce-d5ef73441c75") },
                    { new Guid("9dfb229f-2061-4903-9f0e-507d395d0293"), 23, "SURGICAL_TABLES", new Guid("6d3aa3c6-7083-42da-944f-fac4cf10c040") },
                    { new Guid("948a604a-5d28-41f1-803e-6f82f3e94f39"), 3, "EKG_MACHINE", new Guid("6d3aa3c6-7083-42da-944f-fac4cf10c040") },
                    { new Guid("13bb883f-430f-4bf8-b56e-af16c40d3d9d"), 9, "SURGICAL_TABLES", new Guid("54036bee-7da3-4afe-a6d3-f351ccaa4a83") },
                    { new Guid("9cabaaaf-dc5e-481e-ab79-b8e693cb55cc"), 10, "ANESTHESIA", new Guid("0aa4c1e5-aa74-4a97-8319-457cbe42f691") },
                    { new Guid("67e43c44-1b42-4ad9-b976-3451b00f16a5"), 13, "SYRINGE", new Guid("d88c2bf0-653b-4a27-84ea-27b08483527d") },
                    { new Guid("39f720b9-e999-4c8d-bd12-57bf80ad0b31"), 5, "ANESTHESIA", new Guid("d88c2bf0-653b-4a27-84ea-27b08483527d") },
                    { new Guid("6f849664-adf4-43d3-a02b-bb2a0cc797ca"), 15, "SURGICAL_TABLES", new Guid("d88c2bf0-653b-4a27-84ea-27b08483527d") },
                    { new Guid("782e8581-aa2f-4907-bdd3-06969d5026e7"), 4, "BANDAGE", new Guid("c1ba9060-59d0-4212-86ce-d5ef73441c75") },
                    { new Guid("691db9f9-a0d6-4f2c-a86e-740065103fe7"), 9, "ANESTHESIA", new Guid("54036bee-7da3-4afe-a6d3-f351ccaa4a83") }
                });

            migrationBuilder.InsertData(
                table: "Holidays",
                columns: new[] { "Id", "Description", "DoctorId", "HolidayStatus", "IsUrgent", "DateRange_From", "DateRange_To" },
                values: new object[] { new Guid("4a57d6c9-6239-4251-825d-881053873621"), "I want to go to Paralia", new Guid("238b29b1-3025-4349-a3f5-69e7772eecff"), 0, false, new DateTime(2022, 10, 27, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 27, 10, 30, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "Age", "BloodType", "DateOfBirth", "DoctorId", "Gender" },
                values: new object[,]
                {
                    { new Guid("284842dc-f5a9-4558-a408-ba8903dd03f5"), 15, 4, new DateTime(2007, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("238b29b1-3025-4349-a3f5-69e7772eecff"), 0 },
                    { new Guid("421f330c-c7b4-4001-b981-d1c89d0afdc5"), 32, 0, new DateTime(1990, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("238b29b1-3025-4349-a3f5-69e7772eecff"), 0 },
                    { new Guid("4ab6d5fc-4765-497f-9cf6-cfdc6e70651f"), 5, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("238b29b1-3025-4349-a3f5-69e7772eecff"), 1 },
                    { new Guid("58b497a0-20ff-4cfa-801e-61917b8f5e4a"), 18, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("238b29b1-3025-4349-a3f5-69e7772eecff"), 2 },
                    { new Guid("7eb286b3-9c90-4314-9a3e-e71b267e0d83"), 70, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("106ae974-ce1a-4e21-884f-8e28ab9d0e37"), 0 },
                    { new Guid("a3f79940-bbf5-4389-b285-3ce663437677"), 56, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("106ae974-ce1a-4e21-884f-8e28ab9d0e37"), 0 },
                    { new Guid("ca59216b-8af6-45d8-9620-559f433ba1fe"), 9, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c454e084-4d41-4461-8b5d-16c56140daa3"), 1 },
                    { new Guid("6f0daebc-1391-4b22-bc57-2fc1a25b2dad"), 65, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c454e084-4d41-4461-8b5d-16c56140daa3"), 0 },
                    { new Guid("650c5caa-85b9-4b6c-92f9-2d229a490a21"), 50, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c454e084-4d41-4461-8b5d-16c56140daa3"), 1 },
                    { new Guid("55569299-ba89-40b0-9003-69e2e3abfac9"), 61, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c454e084-4d41-4461-8b5d-16c56140daa3"), 1 }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "AppointmentState", "AppointmentType", "DoctorId", "Emergent", "PatientId", "Duration_From", "Duration_To" },
                values: new object[,]
                {
                    { new Guid("c0576733-b7fa-4974-b60c-d3d7e8c9f216"), 0, 0, new Guid("238b29b1-3025-4349-a3f5-69e7772eecff"), false, new Guid("284842dc-f5a9-4558-a408-ba8903dd03f5"), new DateTime(2023, 7, 27, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 27, 10, 30, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("09d8369a-4bcf-4636-a757-d6ab40d59060"), 0, 0, new Guid("238b29b1-3025-4349-a3f5-69e7772eecff"), false, new Guid("284842dc-f5a9-4558-a408-ba8903dd03f5"), new DateTime(2023, 1, 13, 1, 51, 47, 206, DateTimeKind.Local).AddTicks(4588), new DateTime(2023, 1, 13, 2, 21, 47, 208, DateTimeKind.Local).AddTicks(2232) },
                    { new Guid("e2f0f436-e57f-46af-aa16-e629407697c8"), 0, 0, new Guid("238b29b1-3025-4349-a3f5-69e7772eecff"), false, new Guid("284842dc-f5a9-4558-a408-ba8903dd03f5"), new DateTime(2023, 1, 13, 0, 51, 47, 208, DateTimeKind.Local).AddTicks(2257), new DateTime(2023, 1, 13, 1, 21, 47, 208, DateTimeKind.Local).AddTicks(2260) },
                    { new Guid("43f92899-f9c2-4f8a-9441-5b099dd0e26e"), 0, 0, new Guid("238b29b1-3025-4349-a3f5-69e7772eecff"), false, new Guid("284842dc-f5a9-4558-a408-ba8903dd03f5"), new DateTime(2023, 1, 12, 23, 31, 47, 208, DateTimeKind.Local).AddTicks(2263), new DateTime(2023, 1, 13, 0, 1, 47, 208, DateTimeKind.Local).AddTicks(2266) },
                    { new Guid("3fa0b63a-92fb-4428-9c1b-852e79069e1b"), 0, 0, new Guid("238b29b1-3025-4349-a3f5-69e7772eecff"), false, new Guid("284842dc-f5a9-4558-a408-ba8903dd03f5"), new DateTime(2023, 1, 12, 22, 21, 47, 208, DateTimeKind.Local).AddTicks(2269), new DateTime(2023, 1, 12, 23, 31, 47, 208, DateTimeKind.Local).AddTicks(2272) },
                    { new Guid("739820c9-7880-4081-b750-4ce984110fd5"), 0, 0, new Guid("238b29b1-3025-4349-a3f5-69e7772eecff"), false, new Guid("284842dc-f5a9-4558-a408-ba8903dd03f5"), new DateTime(2023, 1, 12, 21, 1, 47, 208, DateTimeKind.Local).AddTicks(2274), new DateTime(2023, 1, 12, 22, 41, 47, 208, DateTimeKind.Local).AddTicks(2276) },
                    { new Guid("dcad6271-0e26-4f94-bd02-947bfd3cb7b8"), 0, 0, new Guid("238b29b1-3025-4349-a3f5-69e7772eecff"), false, new Guid("284842dc-f5a9-4558-a408-ba8903dd03f5"), new DateTime(2023, 1, 12, 19, 46, 47, 208, DateTimeKind.Local).AddTicks(2279), new DateTime(2023, 1, 12, 20, 46, 47, 208, DateTimeKind.Local).AddTicks(2281) },
                    { new Guid("20c1eb71-20d5-4f7c-bfd8-acd432653aab"), 0, 0, new Guid("238b29b1-3025-4349-a3f5-69e7772eecff"), false, new Guid("284842dc-f5a9-4558-a408-ba8903dd03f5"), new DateTime(2022, 7, 27, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 7, 27, 10, 30, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("4b69775d-1c12-4088-8592-d00a87d665f9"), 0, 0, new Guid("238b29b1-3025-4349-a3f5-69e7772eecff"), false, new Guid("284842dc-f5a9-4558-a408-ba8903dd03f5"), new DateTime(2022, 8, 27, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 8, 27, 10, 30, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("2bf1dc7d-bc85-4232-b0b1-e04d11d1cf1a"), 0, 0, new Guid("238b29b1-3025-4349-a3f5-69e7772eecff"), false, new Guid("284842dc-f5a9-4558-a408-ba8903dd03f5"), new DateTime(2022, 9, 27, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 27, 10, 30, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("556e5a69-bf8a-4cc7-97e5-380a059828b7"), 0, 0, new Guid("238b29b1-3025-4349-a3f5-69e7772eecff"), false, new Guid("284842dc-f5a9-4558-a408-ba8903dd03f5"), new DateTime(2022, 10, 27, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 27, 10, 30, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("10b9d9b4-414a-47bb-a3fe-1493174c4447"), 0, 0, new Guid("238b29b1-3025-4349-a3f5-69e7772eecff"), false, new Guid("284842dc-f5a9-4558-a408-ba8903dd03f5"), new DateTime(2022, 11, 27, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 11, 27, 10, 30, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("cfee2e95-9bc9-4835-acd8-72a87f7133fb"), 0, 0, new Guid("238b29b1-3025-4349-a3f5-69e7772eecff"), false, new Guid("284842dc-f5a9-4558-a408-ba8903dd03f5"), new DateTime(2022, 12, 27, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 12, 27, 10, 30, 0, 0, DateTimeKind.Unspecified) }
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
