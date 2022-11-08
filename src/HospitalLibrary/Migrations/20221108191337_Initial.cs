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
                        name: "FK_Patient_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
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
                        name: "FK_Doctors_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
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
                    table.ForeignKey(
                        name: "FK_GRooms_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
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
                table: "Address",
                columns: new[] { "Id", "City", "Country", "Postcode", "Street", "StreetNumber" },
                values: new object[,]
                {
                    { new Guid("368c3c3f-a67e-41fe-8d32-ad48120f9cd1"), "Novi Sad", "Serbia", 21000, "Kosovska", "23A" },
                    { new Guid("d740efda-6f4e-4301-a02e-a6a7ec00eaca"), "Novi Sad", "Serbia", 21000, "Partizanska", "33" },
                    { new Guid("5815143a-c898-4d2b-9cff-56a197cb6056"), "Novi Sad", "Serbia", 21000, "JNA", "33" }
                });

            migrationBuilder.InsertData(
                table: "Buildings",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("047993d2-ff75-40fa-9c3d-5a27190186dc"), "Stara bolnica" },
                    { new Guid("c1b57334-6e99-4eda-9212-463b0949f690"), "Nova bolnica" }
                });

            migrationBuilder.InsertData(
                table: "FloorPlanViews",
                columns: new[] { "Id", "Lenght", "PosX", "PosY", "Width" },
                values: new object[,]
                {
                    { new Guid("4f2cbca1-7a23-4666-8408-b58fdeae0a6c"), 5, 0, 0, 5 },
                    { new Guid("48e962e4-d14e-4bee-9ed3-da0428e3f9a7"), 5, 5, 0, 5 },
                    { new Guid("202a34c7-cfa2-4aba-bd40-a401d085ec0e"), 5, 0, 5, 5 }
                });

            migrationBuilder.InsertData(
                table: "Specializations",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("3f6ea556-e5da-4464-a149-44a1cb28a96a"), "Surgeon" },
                    { new Guid("033c9f4d-522f-417f-845a-957dd0b31678"), "General" },
                    { new Guid("db77985c-8711-49af-b1ed-0e3a2e7fcde5"), "Dermatology" }
                });

            migrationBuilder.InsertData(
                table: "WorkingSchedules",
                columns: new[] { "Id", "DayOfWorkFrom", "DayOfWorkTo", "ExpirationFrom", "ExpirationTo" },
                values: new object[,]
                {
                    { new Guid("ede26ee9-3c29-4e4e-928d-1683917ff3ba"), new DateTime(2022, 10, 27, 8, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 27, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("c78f6e52-fe35-46d9-9c2c-6721a0eeebe3"), new DateTime(2022, 10, 27, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 27, 22, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Floors",
                columns: new[] { "Id", "BuildingId", "FloorNumber", "Name" },
                values: new object[,]
                {
                    { new Guid("904dba39-ac06-4a4f-bdc9-10ad06e2e0ad"), new Guid("047993d2-ff75-40fa-9c3d-5a27190186dc"), 0, "F0" },
                    { new Guid("1a8d83a1-c314-43bc-b47f-7d68fc929630"), new Guid("047993d2-ff75-40fa-9c3d-5a27190186dc"), 1, "F1" },
                    { new Guid("7612d881-ac40-4137-ab59-f70de196fffd"), new Guid("047993d2-ff75-40fa-9c3d-5a27190186dc"), 2, "F2" },
                    { new Guid("4bae7ea0-68c2-4dc3-8f2e-91ac85dee0cf"), new Guid("c1b57334-6e99-4eda-9212-463b0949f690"), 0, "F0" },
                    { new Guid("ed5f12d1-08a7-40d3-9e1e-756b63411a3f"), new Guid("c1b57334-6e99-4eda-9212-463b0949f690"), 1, "F1" },
                    { new Guid("97c12a19-bf14-43dd-a171-575c62fec804"), new Guid("c1b57334-6e99-4eda-9212-463b0949f690"), 2, "F2" }
                });

            migrationBuilder.InsertData(
                table: "Patient",
                columns: new[] { "Id", "AddressId", "Email", "Jmbg", "Name", "Password", "Phone", "Surname", "Username" },
                values: new object[,]
                {
                    { new Guid("89b205bf-d61b-4ba6-a1c7-30c858b28ef5"), new Guid("368c3c3f-a67e-41fe-8d32-ad48120f9cd1"), "psw.isa.mail@gmail.com", "99999999", "Sale", "sale1312", "+612222222", "Lave", "Sale" },
                    { new Guid("b642a6f6-5492-48d9-b21c-3f5d4d019f44"), new Guid("5815143a-c898-4d2b-9cff-56a197cb6056"), "psw.isa.mail@gmail.com", "99999999", "Miki", "sale1312", "+612222222", "Djuricic", "Miki" }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "BuildingName", "FloorId", "FloorName", "Lenght", "Number", "PositionX", "PositionY", "Width" },
                values: new object[,]
                {
                    { new Guid("e158ba7c-a07d-4231-ab57-3ebb09740bff"), "Stara zgrada", new Guid("904dba39-ac06-4a4f-bdc9-10ad06e2e0ad"), "Prvi", 5, "11A", 0, 0, 5 },
                    { new Guid("8360e6d1-5801-4cc7-b506-6239b48f66c9"), "Stara zgrada", new Guid("904dba39-ac06-4a4f-bdc9-10ad06e2e0ad"), "Prvi", 5, "12A", 5, 0, 5 },
                    { new Guid("60f952a5-dd61-4f74-bdd9-e89aec91b3d9"), "Stara zgrada", new Guid("1a8d83a1-c314-43bc-b47f-7d68fc929630"), "Drugi", 5, "13A", 10, 0, 5 },
                    { new Guid("df7ba662-02d9-4838-8fbd-1c10a37ef6f6"), "Stara zgrada", new Guid("7612d881-ac40-4137-ab59-f70de196fffd"), "Treci", 5, "14A", 0, 5, 5 },
                    { new Guid("2b76cbe4-c4b6-4597-b88b-e7f242db5957"), "Nova zgrada", new Guid("4bae7ea0-68c2-4dc3-8f2e-91ac85dee0cf"), "Prvi", 5, "11B", 0, 10, 5 },
                    { new Guid("bf5f81b2-354f-4023-9453-4d142147dd8d"), "Nova zgrada", new Guid("4bae7ea0-68c2-4dc3-8f2e-91ac85dee0cf"), "Prvi", 5, "12B", 5, 5, 5 },
                    { new Guid("8bedba9f-0c67-4f93-8ffd-8dec0500123e"), "Nova zgrada", new Guid("ed5f12d1-08a7-40d3-9e1e-756b63411a3f"), "Drugi", 5, "13B", 10, 5, 5 },
                    { new Guid("6558e425-a5f4-44ca-8df0-931be9d2d19c"), "Nova zgrada", new Guid("97c12a19-bf14-43dd-a171-575c62fec804"), "Treci", 10, "14B", 0, 0, 20 },
                    { new Guid("6e5bed25-8060-4e7b-8cb3-81314084bc5c"), "Nova zgrada", new Guid("97c12a19-bf14-43dd-a171-575c62fec804"), "Treci", 5, "15B", 0, 10, 20 }
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "AddressId", "Email", "Jmbg", "Name", "Password", "Phone", "RoomId", "SpecializationId", "Surname", "Username", "WorkingScheduleId" },
                values: new object[,]
                {
                    { new Guid("d30fc010-72e2-4ea7-b862-aaa4fc4adeea"), new Guid("368c3c3f-a67e-41fe-8d32-ad48120f9cd1"), "Cajons@gmail.com", "99999999", "Ilija", "miki123", "+612222222", new Guid("e158ba7c-a07d-4231-ab57-3ebb09740bff"), new Guid("db77985c-8711-49af-b1ed-0e3a2e7fcde5"), "Maric", "Ilija", new Guid("c78f6e52-fe35-46d9-9c2c-6721a0eeebe3") },
                    { new Guid("63f85104-47ee-4fb6-9b79-eab1ffa2a786"), new Guid("d740efda-6f4e-4301-a02e-a6a7ec00eaca"), "DjordjeLopov@gmail.com", "99999999", "Djordje", "miki123", "+612222222", new Guid("8360e6d1-5801-4cc7-b506-6239b48f66c9"), new Guid("db77985c-8711-49af-b1ed-0e3a2e7fcde5"), "Vuckovic", "Tadjo", new Guid("ede26ee9-3c29-4e4e-928d-1683917ff3ba") }
                });

            migrationBuilder.InsertData(
                table: "GRooms",
                columns: new[] { "Id", "Lenght", "PositionX", "PositionY", "RoomId", "Width" },
                values: new object[,]
                {
                    { new Guid("f4545953-d8c0-4bab-8a0c-91cf3933bcc8"), 5, 0, 0, new Guid("e158ba7c-a07d-4231-ab57-3ebb09740bff"), 5 },
                    { new Guid("164898c6-f779-4f13-a876-3cc467200fa3"), 5, 5, 0, new Guid("8360e6d1-5801-4cc7-b506-6239b48f66c9"), 5 }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "AppointmentState", "AppointmentType", "DoctorId", "Emergent", "PatientId", "Duration_From", "Duration_To" },
                values: new object[] { new Guid("a4276b64-cdf9-42c2-9c66-1dc752a8b4d3"), 0, 0, new Guid("d30fc010-72e2-4ea7-b862-aaa4fc4adeea"), false, new Guid("89b205bf-d61b-4ba6-a1c7-30c858b28ef5"), new DateTime(2022, 10, 27, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 27, 15, 15, 0, 0, DateTimeKind.Unspecified) });

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
                name: "IX_GRooms_RoomId",
                table: "GRooms",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Holiday_DoctorId",
                table: "Holiday",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_AddressId",
                table: "Patient",
                column: "AddressId");

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
                name: "GRooms");

            migrationBuilder.DropTable(
                name: "Holiday");

            migrationBuilder.DropTable(
                name: "Patient");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Address");

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
