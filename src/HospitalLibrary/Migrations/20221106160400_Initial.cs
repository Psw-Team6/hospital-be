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
                    { new Guid("51561636-7d8f-49b6-9192-29fe749a657a"), "Novi Sad", "Serbia", 21000, "Kosovska", "23A" },
                    { new Guid("a4e8f397-44ce-4ff9-86a3-20b413abf962"), "Novi Sad", "Serbia", 21000, "Partizanska", "33" },
                    { new Guid("1bc87db4-ebae-4c3c-a69e-d822c0801704"), "Novi Sad", "Serbia", 21000, "JNA", "33" }
                });

            migrationBuilder.InsertData(
                table: "Buildings",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("8de75908-eec2-4768-a4f4-d6b75e27e50e"), "Stara bolnica" },
                    { new Guid("12fa33b4-e393-4328-be7c-921237aab187"), "Nova bolnica" }
                });

            migrationBuilder.InsertData(
                table: "FloorPlanViews",
                columns: new[] { "Id", "Lenght", "PosX", "PosY", "Width" },
                values: new object[,]
                {
                    { new Guid("493d37e4-98d4-4119-9797-ad859171da34"), 5, 0, 0, 5 },
                    { new Guid("eb147559-e72d-4553-9daf-edd83eb39f2e"), 5, 5, 0, 5 },
                    { new Guid("7bf3737f-8337-42bb-bd9f-9b123ac8f926"), 5, 0, 5, 5 }
                });

            migrationBuilder.InsertData(
                table: "Specializations",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("9fb10f03-5d5b-4af2-a28d-c75e660ab68b"), "Surgeon" },
                    { new Guid("4bb49eb7-9bf4-4a5e-bb9e-76214c4f113f"), "General" },
                    { new Guid("f0986efa-335b-48e3-817b-b3d151693d2d"), "Dermatology" }
                });

            migrationBuilder.InsertData(
                table: "WorkingSchedules",
                columns: new[] { "Id", "DayOfWorkFrom", "DayOfWorkTo", "ExpirationFrom", "ExpirationTo" },
                values: new object[,]
                {
                    { new Guid("3937c0a9-ac06-45ba-a1d7-656f8687596b"), new DateTime(2022, 10, 27, 8, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 27, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("5be2491b-0fb1-4e77-a201-8f903b957cb6"), new DateTime(2022, 10, 27, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 27, 22, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Floors",
                columns: new[] { "Id", "BuildingId", "FloorNumber", "Name" },
                values: new object[,]
                {
                    { new Guid("b9028eb5-382a-4761-84f5-cd2ae8003127"), new Guid("8de75908-eec2-4768-a4f4-d6b75e27e50e"), 0, "F0" },
                    { new Guid("a9f0e448-0c07-4f47-a19b-df63cc8a3039"), new Guid("8de75908-eec2-4768-a4f4-d6b75e27e50e"), 1, "F1" },
                    { new Guid("c8a7e635-458e-4050-abfd-76fb9039947b"), new Guid("8de75908-eec2-4768-a4f4-d6b75e27e50e"), 2, "F2" },
                    { new Guid("76fdb478-ded4-4e50-8387-035c0763775e"), new Guid("12fa33b4-e393-4328-be7c-921237aab187"), 0, "F0" },
                    { new Guid("eb55f925-c730-4b07-b468-521ea147de37"), new Guid("12fa33b4-e393-4328-be7c-921237aab187"), 1, "F1" },
                    { new Guid("617ef46b-12d8-45dd-b9bd-cbf144ce5d80"), new Guid("12fa33b4-e393-4328-be7c-921237aab187"), 2, "F2" }
                });

            migrationBuilder.InsertData(
                table: "Patient",
                columns: new[] { "Id", "AddressId", "Email", "Jmbg", "Name", "Password", "Phone", "Surname", "Username" },
                values: new object[,]
                {
                    { new Guid("3e8a319d-b198-4728-b750-fc2b664603ec"), new Guid("51561636-7d8f-49b6-9192-29fe749a657a"), "psw.isa.mail@gmail.com", "99999999", "Sale", "sale1312", "+612222222", "Lave", "Sale" },
                    { new Guid("b0854648-b406-47b7-8ea2-95d6f19b4b00"), new Guid("1bc87db4-ebae-4c3c-a69e-d822c0801704"), "psw.isa.mail@gmail.com", "99999999", "Miki", "sale1312", "+612222222", "Djuricic", "Miki" }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "BuildingId", "BuildingName", "FloorId", "FloorName", "Lenght", "Number", "PositionX", "PositionY", "Width" },
                values: new object[,]
                {
                    { new Guid("a1dd080a-d477-4fa1-9271-574da66dc569"), new Guid("8de75908-eec2-4768-a4f4-d6b75e27e50e"), "Stara zgrada", new Guid("b9028eb5-382a-4761-84f5-cd2ae8003127"), "Prvi", 5, "11A", 0, 0, 5 },
                    { new Guid("90723cfc-5486-4d70-9bc4-87c1de4bb8f0"), new Guid("8de75908-eec2-4768-a4f4-d6b75e27e50e"), "Stara zgrada", new Guid("b9028eb5-382a-4761-84f5-cd2ae8003127"), "Prvi", 5, "12A", 5, 0, 5 },
                    { new Guid("332583ce-f2c5-4b49-a372-f9731b2b3c72"), new Guid("8de75908-eec2-4768-a4f4-d6b75e27e50e"), "Stara zgrada", new Guid("a9f0e448-0c07-4f47-a19b-df63cc8a3039"), "Drugi", 5, "13A", 10, 0, 5 },
                    { new Guid("1c9ddd14-7c3e-4f91-a2d2-30ba63d600ab"), new Guid("8de75908-eec2-4768-a4f4-d6b75e27e50e"), "Stara zgrada", new Guid("c8a7e635-458e-4050-abfd-76fb9039947b"), "Treci", 5, "14A", 0, 5, 5 },
                    { new Guid("6055370d-a08d-4928-8f5c-2240a29e4c6c"), new Guid("12fa33b4-e393-4328-be7c-921237aab187"), "Nova zgrada", new Guid("76fdb478-ded4-4e50-8387-035c0763775e"), "Prvi", 5, "11B", 0, 10, 5 },
                    { new Guid("da60fd73-5068-45d6-902b-edf9874443d0"), new Guid("12fa33b4-e393-4328-be7c-921237aab187"), "Nova zgrada", new Guid("76fdb478-ded4-4e50-8387-035c0763775e"), "Prvi", 5, "12B", 5, 5, 5 },
                    { new Guid("14ce1d91-f11d-4c06-b5d0-9c192c567d25"), new Guid("12fa33b4-e393-4328-be7c-921237aab187"), "Nova zgrada", new Guid("eb55f925-c730-4b07-b468-521ea147de37"), "Drugi", 5, "13B", 10, 5, 5 },
                    { new Guid("3e02d330-c450-4236-8c29-b61fbd1bc6ec"), new Guid("12fa33b4-e393-4328-be7c-921237aab187"), "Nova zgrada", new Guid("617ef46b-12d8-45dd-b9bd-cbf144ce5d80"), "Treci", 10, "14B", 0, 0, 20 },
                    { new Guid("de443497-0784-4f0e-956f-526920e4a15a"), new Guid("12fa33b4-e393-4328-be7c-921237aab187"), "Nova zgrada", new Guid("617ef46b-12d8-45dd-b9bd-cbf144ce5d80"), "Treci", 5, "15B", 0, 10, 20 }
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "AddressId", "Email", "Jmbg", "Name", "Password", "Phone", "RoomId", "SpecializationId", "Surname", "Username", "WorkingScheduleId" },
                values: new object[,]
                {
                    { new Guid("6e528822-1bcb-47c1-8ffd-040c2452e405"), new Guid("51561636-7d8f-49b6-9192-29fe749a657a"), "Cajons@gmail.com", "99999999", "Ilija", "miki123", "+612222222", new Guid("a1dd080a-d477-4fa1-9271-574da66dc569"), new Guid("f0986efa-335b-48e3-817b-b3d151693d2d"), "Maric", "Ilija", new Guid("5be2491b-0fb1-4e77-a201-8f903b957cb6") },
                    { new Guid("29154a3a-e297-4bd4-a89f-b6694220f76c"), new Guid("a4e8f397-44ce-4ff9-86a3-20b413abf962"), "DjordjeLopov@gmail.com", "99999999", "Djordje", "miki123", "+612222222", new Guid("90723cfc-5486-4d70-9bc4-87c1de4bb8f0"), new Guid("f0986efa-335b-48e3-817b-b3d151693d2d"), "Vuckovic", "Tadjo", new Guid("3937c0a9-ac06-45ba-a1d7-656f8687596b") }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "AppointmentState", "AppointmentType", "DoctorId", "Emergent", "PatientId", "Duration_From", "Duration_To" },
                values: new object[] { new Guid("9e8cc086-bfd4-49bb-9cec-b3779175e00d"), 0, 0, new Guid("6e528822-1bcb-47c1-8ffd-040c2452e405"), false, new Guid("3e8a319d-b198-4728-b750-fc2b664603ec"), new DateTime(2022, 10, 27, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 27, 15, 15, 0, 0, DateTimeKind.Unspecified) });

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
