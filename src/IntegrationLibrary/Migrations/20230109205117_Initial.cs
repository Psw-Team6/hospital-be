using System;
using System.Collections.Generic;
using IntegrationLibrary.BloodSubscription.Model;
using IntegrationLibrary.Tender.Model;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IntegrationLibrary.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AmountOfBloodType",
                columns: table => new
                {
                    bloodType = table.Column<int>(type: "integer", nullable: false),
                    amount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "BloodBanks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    ServerAddress = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    ApiKey_Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodBanks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BloodRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<double>(type: "double precision", nullable: false),
                    Reason = table.Column<string>(type: "text", nullable: true),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DoctorUsername = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    BloodBankId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BloodSubscriptions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    bloodBankId = table.Column<Guid>(type: "uuid", nullable: false),
                    dateAndTimeOfSubscription = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    amountOfBloodTypes = table.Column<List<AmountOfBloodType>>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodSubscriptions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ConfigureGenerateAndSend",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BloodBankName = table.Column<string>(type: "text", nullable: true),
                    GeneratePeriod = table.Column<string>(type: "text", nullable: true),
                    SendPeriod = table.Column<string>(type: "text", nullable: true),
                    NextDateForSending = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigureGenerateAndSend", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NewsFromBloodBank",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "text", nullable: true),
                    content = table.Column<string>(type: "text", nullable: true),
                    apiKey = table.Column<string>(type: "text", nullable: true),
                    newsStatus = table.Column<int>(type: "integer", nullable: false),
                    base64image = table.Column<string>(type: "text", nullable: true),
                    bloodBankName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsFromBloodBank", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TenderOffer",
                columns: table => new
                {
                    BloodBankName = table.Column<string>(type: "text", nullable: true),
                    RealizationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Tenders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    HasDeadline = table.Column<bool>(type: "boolean", nullable: false),
                    DeadlineDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    PublishedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Winner = table.Column<TenderOffer>(type: "jsonb", nullable: true),
                    TenderOffer = table.Column<IEnumerable<TenderOffer>>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BloodUnitAmounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BloodType = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    TenderId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodUnitAmounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BloodUnitAmounts_Tenders_TenderId",
                        column: x => x.TenderId,
                        principalTable: "Tenders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "BloodBanks",
                columns: new[] { "Id", "Email", "Name", "Password", "ServerAddress", "ApiKey_Value" },
                values: new object[] { new Guid("6fb13bbf-af28-4077-9e72-e22824171a19"), "aas@gmail.com", "BloodBank", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "localhost", "MpXdApexOYVj7lTMXjALZkgjZ3o0XAyYcmV3sGrhFZM=" });

            migrationBuilder.InsertData(
                table: "BloodRequests",
                columns: new[] { "Id", "Amount", "BloodBankId", "Comment", "Date", "DoctorUsername", "Reason", "Status", "Type" },
                values: new object[,]
                {
                    { new Guid("4a085ac7-bb6d-444e-bde3-60c326827cb7"), 10.0, new Guid("00000000-0000-0000-0000-000000000000"), "", new DateTime(2022, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ilija", "Operacija", 2, 5 },
                    { new Guid("a90ba42f-0954-4c5d-8204-eb177576ad09"), 20.0, new Guid("00000000-0000-0000-0000-000000000000"), "", new DateTime(2022, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ilija", "Transfuzija", 2, 3 },
                    { new Guid("1b8cf9e6-e145-456d-b13a-3018542e6ddf"), 20.0, new Guid("00000000-0000-0000-0000-000000000000"), "", new DateTime(2023, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ilija", "Transfuzija", 2, 0 },
                    { new Guid("469639ec-8f7e-422b-aedc-7ce066c670cb"), 5.0, new Guid("00000000-0000-0000-0000-000000000000"), "", new DateTime(2023, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ilija", "Zalihe", 2, 6 }
                });

            migrationBuilder.InsertData(
                table: "ConfigureGenerateAndSend",
                columns: new[] { "Id", "BloodBankName", "GeneratePeriod", "NextDateForSending", "SendPeriod" },
                values: new object[,]
                {
                    { new Guid("a3589015-a26f-4175-8290-599dc2f4b4ac"), "Moja Banka Krvi", "ONE_MONTH", new DateTime(2023, 1, 9, 21, 51, 16, 536, DateTimeKind.Local).AddTicks(4684), "EVERY_TWO_MINUT" },
                    { new Guid("2fb91d8d-4ba9-4c7a-8ab3-76c7c69c6691"), "Nova banka", "TWO_MONTH", new DateTime(2023, 1, 9, 21, 51, 16, 544, DateTimeKind.Local).AddTicks(1481), "ONE_MONTH" }
                });

            migrationBuilder.InsertData(
                table: "Tenders",
                columns: new[] { "Id", "DeadlineDate", "HasDeadline", "PublishedDate", "Status", "TenderOffer", "Winner" },
                values: new object[,]
                {
                    { new Guid("26825b36-f45d-42cb-9a76-0aa7a1dbc5f2"), new DateTime(2023, 1, 29, 21, 51, 16, 544, DateTimeKind.Local).AddTicks(3629), true, new DateTime(2023, 1, 9, 21, 51, 16, 544, DateTimeKind.Local).AddTicks(4067), 0, null, null },
                    { new Guid("822bda4c-40c0-4833-9f07-7397cc7627dd"), new DateTime(2023, 1, 8, 21, 51, 16, 544, DateTimeKind.Local).AddTicks(4793), true, new DateTime(2023, 1, 5, 21, 51, 16, 544, DateTimeKind.Local).AddTicks(4807), 2, null, null }
                });

            migrationBuilder.InsertData(
                table: "BloodUnitAmounts",
                columns: new[] { "Id", "Amount", "BloodType", "TenderId" },
                values: new object[,]
                {
                    { new Guid("20e47265-e90a-4a80-b393-aa74583ef96e"), 10, 0, new Guid("26825b36-f45d-42cb-9a76-0aa7a1dbc5f2") },
                    { new Guid("42b5ab8e-7577-4c50-b6f5-c27c5b2ad387"), 0, 1, new Guid("26825b36-f45d-42cb-9a76-0aa7a1dbc5f2") },
                    { new Guid("2b5e879d-8551-42da-bdb4-b50a98a2f458"), 5, 2, new Guid("26825b36-f45d-42cb-9a76-0aa7a1dbc5f2") },
                    { new Guid("d8433979-1214-411d-8d9d-3b1e5c5cb696"), 0, 3, new Guid("26825b36-f45d-42cb-9a76-0aa7a1dbc5f2") },
                    { new Guid("17ea4438-fb9b-4de6-ae8e-0d939fba777e"), 12, 4, new Guid("26825b36-f45d-42cb-9a76-0aa7a1dbc5f2") },
                    { new Guid("877e5c7c-e504-43af-9a35-dccf1893d41c"), 7, 5, new Guid("26825b36-f45d-42cb-9a76-0aa7a1dbc5f2") },
                    { new Guid("fdd8e51e-b948-42e5-9796-0178c889da42"), 10, 6, new Guid("26825b36-f45d-42cb-9a76-0aa7a1dbc5f2") },
                    { new Guid("06ed1ac6-9cca-494b-8d87-ce9b123412c2"), 0, 7, new Guid("26825b36-f45d-42cb-9a76-0aa7a1dbc5f2") },
                    { new Guid("7db49e56-b7e2-4c5d-a629-9b32e94e28a7"), 7, 5, new Guid("822bda4c-40c0-4833-9f07-7397cc7627dd") },
                    { new Guid("63109763-6c86-4076-92f0-50e96048faf6"), 10, 6, new Guid("822bda4c-40c0-4833-9f07-7397cc7627dd") },
                    { new Guid("2f24c7d4-2f6c-473c-8fb8-41ac83a7dadc"), 0, 7, new Guid("822bda4c-40c0-4833-9f07-7397cc7627dd") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BloodUnitAmounts_TenderId",
                table: "BloodUnitAmounts",
                column: "TenderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AmountOfBloodType");

            migrationBuilder.DropTable(
                name: "BloodBanks");

            migrationBuilder.DropTable(
                name: "BloodRequests");

            migrationBuilder.DropTable(
                name: "BloodSubscriptions");

            migrationBuilder.DropTable(
                name: "BloodUnitAmounts");

            migrationBuilder.DropTable(
                name: "ConfigureGenerateAndSend");

            migrationBuilder.DropTable(
                name: "NewsFromBloodBank");

            migrationBuilder.DropTable(
                name: "TenderOffer");

            migrationBuilder.DropTable(
                name: "Tenders");
        }
    }
}
