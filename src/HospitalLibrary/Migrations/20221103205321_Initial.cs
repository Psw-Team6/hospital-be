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
                name: "FloorPlanViews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PosX = table.Column<int>(type: "integer", nullable: false),
                    PosY = table.Column<int>(type: "integer", nullable: false),
                    Lenght = table.Column<int>(type: "integer", nullable: false),
                    Width = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FloorPlanViews", x => x.Id);
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
                name: "Patient",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    AddressId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Surname = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Jmbg = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patient_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
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
                        name: "FK_Feedback_Patient_patientId",
                        column: x => x.patientId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Number = table.Column<string>(type: "text", nullable: false),
                    BuildingId = table.Column<Guid>(type: "uuid", nullable: false),
                    FloorId = table.Column<Guid>(type: "uuid", nullable: false),
                    BuildingName = table.Column<string>(type: "text", nullable: true),
                    FloorName = table.Column<string>(type: "text", nullable: true),
                    PositionX = table.Column<int>(type: "integer", nullable: false),
                    PositionY = table.Column<int>(type: "integer", nullable: false),
                    Lenght = table.Column<int>(type: "integer", nullable: false),
                    Width = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rooms_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Buildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    WorkingScheduleId = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: true),
                    AddressId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Surname = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Jmbg = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doctors_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
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
                        name: "FK_Appointments_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
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
                    DateRange_To = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
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

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "City", "Country", "Postcode", "Street", "StreetNumber" },
                values: new object[,]
                {
                    { new Guid("b6d52d91-c32c-4170-b857-92975c443dbc"), "Novi Sad", "Serbia", 21000, "Kosovska", "23A" },
                    { new Guid("3612b672-2244-4759-a876-cf433d108348"), "Novi Sad", "Serbia", 21000, "Partizanska", "33" },
                    { new Guid("775d55a9-b05f-40e1-bb08-f0a30e14ef1c"), "Novi Sad", "Serbia", 21000, "JNA", "33" }
                });

            migrationBuilder.InsertData(
                table: "Buildings",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("87f26d57-759c-4ddd-a885-0ffd06690ce7"), "Stara bolnica" },
                    { new Guid("e5fa172e-eb14-4c11-a266-aa5df850fd56"), "Nova bolnica" }
                });

            migrationBuilder.InsertData(
                table: "FloorPlanViews",
                columns: new[] { "Id", "Lenght", "PosX", "PosY", "Width" },
                values: new object[,]
                {
                    { new Guid("6ec16f32-6f18-4e07-aa16-ddda03f3dd3d"), 5, 0, 0, 5 },
                    { new Guid("af743ae6-9b3b-4b8f-9ffd-309a33e4a7ed"), 5, 5, 0, 5 },
                    { new Guid("bde53023-fcae-4a65-b62e-0f25277aa6a2"), 5, 0, 5, 5 }
                });

            migrationBuilder.InsertData(
                table: "Specializations",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("296e1ab9-ef2a-46b7-9c14-1b838d1afcb7"), "Surgeon" },
                    { new Guid("359eee5d-9b85-47f7-910f-f30d2e6a1179"), "General" },
                    { new Guid("db264b7d-0444-48fe-819b-1ddf8e0e88d1"), "Dermatology" }
                });

            migrationBuilder.InsertData(
                table: "WorkingSchedules",
                columns: new[] { "Id", "DayOfWorkFrom", "DayOfWorkTo", "ExpirationFrom", "ExpirationTo" },
                values: new object[,]
                {
                    { new Guid("7f077ba3-35f4-4dba-b6f9-68834e9f85b2"), new DateTime(2022, 10, 27, 8, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 27, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("14328999-79f0-468a-8f71-6da9a4d6cb30"), new DateTime(2022, 10, 27, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 27, 22, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Floors",
                columns: new[] { "Id", "BuildingId", "FloorNumber", "Name" },
                values: new object[,]
                {
                    { new Guid("a98fe57d-5efa-4a6f-87aa-9508764bb64e"), new Guid("87f26d57-759c-4ddd-a885-0ffd06690ce7"), 0, "F0" },
                    { new Guid("2ef8e7ad-d6d5-4330-8047-e5f254ae56ec"), new Guid("87f26d57-759c-4ddd-a885-0ffd06690ce7"), 1, "F1" },
                    { new Guid("1436d557-77e5-40da-87a6-b52b0d71fe5a"), new Guid("87f26d57-759c-4ddd-a885-0ffd06690ce7"), 2, "F2" },
                    { new Guid("e1d0d410-37a4-4df9-bb84-3acdb5b7417b"), new Guid("e5fa172e-eb14-4c11-a266-aa5df850fd56"), 0, "F0" },
                    { new Guid("9ecbc275-6ec1-48b7-bfa4-664e8e41a272"), new Guid("e5fa172e-eb14-4c11-a266-aa5df850fd56"), 1, "F1" },
                    { new Guid("239a7928-6162-4f40-8a4c-f53200c7cf24"), new Guid("e5fa172e-eb14-4c11-a266-aa5df850fd56"), 2, "F2" }
                });

            migrationBuilder.InsertData(
                table: "Patient",
                columns: new[] { "Id", "AddressId", "Email", "Jmbg", "Name", "Password", "Phone", "Surname", "Username" },
                values: new object[,]
                {
                    { new Guid("72870c6c-bb9e-41e9-8fa9-97682d330bbb"), new Guid("b6d52d91-c32c-4170-b857-92975c443dbc"), "psw.isa.mail@gmail.com", "99999999", "Sale", "sale1312", "+612222222", "Lave", "Sale" },
                    { new Guid("86e2187b-5132-499d-ad0c-746e397560f1"), new Guid("775d55a9-b05f-40e1-bb08-f0a30e14ef1c"), "psw.isa.mail@gmail.com", "99999999", "Miki", "sale1312", "+612222222", "Djuricic", "Miki" }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "BuildingId", "BuildingName", "FloorId", "FloorName", "Lenght", "Number", "PositionX", "PositionY", "Width" },
                values: new object[,]
                {
                    { new Guid("8fc5d6e4-060a-4cf6-a58e-18b2a8c76b14"), new Guid("87f26d57-759c-4ddd-a885-0ffd06690ce7"), "Stara zgrada", new Guid("a98fe57d-5efa-4a6f-87aa-9508764bb64e"), "Prvi", 5, "11A", 0, 0, 5 },
                    { new Guid("2597f2f6-1af9-4136-9902-de6eb6acfbf2"), new Guid("87f26d57-759c-4ddd-a885-0ffd06690ce7"), "Stara zgrada", new Guid("a98fe57d-5efa-4a6f-87aa-9508764bb64e"), "Prvi", 5, "12A", 5, 0, 5 },
                    { new Guid("c6f0410a-36e1-4611-be27-686bc8a4b16e"), new Guid("87f26d57-759c-4ddd-a885-0ffd06690ce7"), "Stara zgrada", new Guid("2ef8e7ad-d6d5-4330-8047-e5f254ae56ec"), "Drugi", 5, "13A", 10, 0, 5 },
                    { new Guid("6d6533ec-990d-491d-aa27-93c4300fdcd0"), new Guid("87f26d57-759c-4ddd-a885-0ffd06690ce7"), "Stara zgrada", new Guid("1436d557-77e5-40da-87a6-b52b0d71fe5a"), "Treci", 5, "14A", 0, 5, 5 },
                    { new Guid("63b29c78-32a9-4731-8f39-b450ec7fdd01"), new Guid("e5fa172e-eb14-4c11-a266-aa5df850fd56"), "Nova zgrada", new Guid("e1d0d410-37a4-4df9-bb84-3acdb5b7417b"), "Prvi", 5, "11B", 0, 10, 5 },
                    { new Guid("fc5c85e6-8ddd-4dda-a1b1-3dd7461e81b7"), new Guid("e5fa172e-eb14-4c11-a266-aa5df850fd56"), "Nova zgrada", new Guid("e1d0d410-37a4-4df9-bb84-3acdb5b7417b"), "Prvi", 5, "12B", 5, 5, 5 },
                    { new Guid("8a44e626-0a97-4ed5-8803-71c1e1d1727b"), new Guid("e5fa172e-eb14-4c11-a266-aa5df850fd56"), "Nova zgrada", new Guid("9ecbc275-6ec1-48b7-bfa4-664e8e41a272"), "Drugi", 5, "13B", 10, 5, 5 },
                    { new Guid("dab84eb6-9d87-423a-bca8-7c5f70c75a83"), new Guid("e5fa172e-eb14-4c11-a266-aa5df850fd56"), "Nova zgrada", new Guid("239a7928-6162-4f40-8a4c-f53200c7cf24"), "Treci", 10, "14B", 0, 0, 20 },
                    { new Guid("5eb85de2-dc58-4414-a0c9-ca30695983d0"), new Guid("e5fa172e-eb14-4c11-a266-aa5df850fd56"), "Nova zgrada", new Guid("239a7928-6162-4f40-8a4c-f53200c7cf24"), "Treci", 5, "15B", 0, 10, 20 }
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "AddressId", "Email", "Jmbg", "Name", "Password", "Phone", "RoomId", "SpecializationId", "Surname", "Username", "WorkingScheduleId" },
                values: new object[,]
                {
                    { new Guid("c4f1529c-3e9b-48fe-94ae-a828f31e632e"), new Guid("b6d52d91-c32c-4170-b857-92975c443dbc"), "Cajons@gmail.com", "99999999", "Ilija", "miki123", "+612222222", new Guid("8fc5d6e4-060a-4cf6-a58e-18b2a8c76b14"), new Guid("db264b7d-0444-48fe-819b-1ddf8e0e88d1"), "Maric", "Ilija", new Guid("14328999-79f0-468a-8f71-6da9a4d6cb30") },
                    { new Guid("27bcf821-69f4-4239-a1f3-d44b8c24fced"), new Guid("3612b672-2244-4759-a876-cf433d108348"), "DjordjeLopov@gmail.com", "99999999", "Djordje", "miki123", "+612222222", new Guid("2597f2f6-1af9-4136-9902-de6eb6acfbf2"), new Guid("db264b7d-0444-48fe-819b-1ddf8e0e88d1"), "Vuckovic", "Tadjo", new Guid("7f077ba3-35f4-4dba-b6f9-68834e9f85b2") }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "AppointmentState", "AppointmentType", "DoctorId", "Emergent", "PatientId", "Duration_From", "Duration_To" },
                values: new object[] { new Guid("e5e6c192-0072-41d3-9af0-484b8beeb141"), 0, 0, new Guid("c4f1529c-3e9b-48fe-94ae-a828f31e632e"), false, new Guid("72870c6c-bb9e-41e9-8fa9-97682d330bbb"), new DateTime(2022, 10, 27, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 27, 15, 15, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorId",
                table: "Appointments",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_AddressId",
                table: "Doctors",
                column: "AddressId");

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
                name: "IX_Patient_AddressId",
                table: "Patient",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_BuildingId",
                table: "Rooms",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_FloorId",
                table: "Rooms",
                column: "FloorId");

            migrationBuilder.CreateIndex(
                name: "IX_Specializations_Name",
                table: "Specializations",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Feedback");

            migrationBuilder.DropTable(
                name: "FloorPlanViews");

            migrationBuilder.DropTable(
                name: "Holiday");

            migrationBuilder.DropTable(
                name: "Patient");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Specializations");

            migrationBuilder.DropTable(
                name: "WorkingSchedules");

            migrationBuilder.DropTable(
                name: "Floors");

            migrationBuilder.DropTable(
                name: "Buildings");
        }
    }
}
