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
                values: new object[] { new Guid("7c97663b-0d22-42f0-a173-ef827274cd43"), "aas@gmail.com", "BloodBank", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "localhost", "7Tel9EbXpCU7jOE72IhRaJ1TfyH8v1F2t69r1fiEx1Y=" });

            migrationBuilder.InsertData(
                table: "BloodRequests",
                columns: new[] { "Id", "Amount", "BloodBankId", "Comment", "Date", "DoctorUsername", "Reason", "Status", "Type" },
                values: new object[,]
                {
                    { new Guid("7c44b52a-6180-4e31-8edd-b7d6db360171"), 10.0, new Guid("00000000-0000-0000-0000-000000000000"), "", new DateTime(2022, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ilija", "Operacija", 2, 5 },
                    { new Guid("a25a993e-f7b9-4066-8f47-ad2205d1c951"), 20.0, new Guid("00000000-0000-0000-0000-000000000000"), "", new DateTime(2022, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ilija", "Transfuzija", 2, 3 },
                    { new Guid("17923af3-24f8-41b4-8c41-eeddc716425d"), 20.0, new Guid("00000000-0000-0000-0000-000000000000"), "", new DateTime(2023, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ilija", "Transfuzija", 2, 0 },
                    { new Guid("a349df96-fe11-4a24-9ed7-9e91f8e18078"), 5.0, new Guid("00000000-0000-0000-0000-000000000000"), "", new DateTime(2023, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ilija", "Zalihe", 2, 6 }
                });

            migrationBuilder.InsertData(
                table: "ConfigureGenerateAndSend",
                columns: new[] { "Id", "BloodBankName", "GeneratePeriod", "NextDateForSending", "SendPeriod" },
                values: new object[,]
                {
                    { new Guid("01eca578-9051-40b0-a9d3-adeb2a72641b"), "Moja Banka Krvi", "ONE_MONTH", new DateTime(2023, 1, 9, 21, 6, 55, 196, DateTimeKind.Local).AddTicks(1052), "EVERY_TWO_MINUT" },
                    { new Guid("2853063c-9d3b-4e9b-bf7c-5ae6f875a73d"), "Nova banka", "TWO_MONTH", new DateTime(2023, 1, 9, 21, 6, 55, 205, DateTimeKind.Local).AddTicks(6954), "ONE_MONTH" }
                });

            migrationBuilder.InsertData(
                table: "Tenders",
                columns: new[] { "Id", "DeadlineDate", "HasDeadline", "PublishedDate", "Status", "TenderOffer", "Winner" },
                values: new object[,]
                {
                    { new Guid("1ccfa7fe-1f34-44ce-a604-0da872b767df"), new DateTime(2023, 1, 29, 21, 6, 55, 205, DateTimeKind.Local).AddTicks(8995), true, new DateTime(2023, 1, 9, 21, 6, 55, 205, DateTimeKind.Local).AddTicks(9404), 0, null, null },
                    { new Guid("1b053eea-4af1-4b49-ad6c-8f805d1cd252"), new DateTime(2023, 1, 8, 21, 6, 55, 206, DateTimeKind.Local).AddTicks(71), true, new DateTime(2023, 1, 5, 21, 6, 55, 206, DateTimeKind.Local).AddTicks(85), 2, null, null }
                });

            migrationBuilder.InsertData(
                table: "BloodUnitAmounts",
                columns: new[] { "Id", "Amount", "BloodType", "TenderId" },
                values: new object[,]
                {
                    { new Guid("88da38b4-6900-485e-8792-04c220d36135"), 10, 0, new Guid("1ccfa7fe-1f34-44ce-a604-0da872b767df") },
                    { new Guid("2a8ff205-f6e6-480d-bfdc-0914734c89b4"), 0, 1, new Guid("1ccfa7fe-1f34-44ce-a604-0da872b767df") },
                    { new Guid("f2b5ced3-3f0c-4b0f-8bb5-9ffe76c85702"), 5, 2, new Guid("1ccfa7fe-1f34-44ce-a604-0da872b767df") },
                    { new Guid("c056da1e-fef7-4ee9-8c7b-7ac45bce034f"), 0, 3, new Guid("1ccfa7fe-1f34-44ce-a604-0da872b767df") },
                    { new Guid("7febea96-1656-484c-9eaa-f6ef44cd59e1"), 12, 4, new Guid("1ccfa7fe-1f34-44ce-a604-0da872b767df") },
                    { new Guid("71cedd53-10ac-4290-b2a7-76749ce5d3a7"), 7, 5, new Guid("1ccfa7fe-1f34-44ce-a604-0da872b767df") },
                    { new Guid("4e349442-baf3-4db4-8014-de277a96516a"), 10, 6, new Guid("1ccfa7fe-1f34-44ce-a604-0da872b767df") },
                    { new Guid("d3ffea5b-86e2-4390-b358-32f6dae3c53c"), 0, 7, new Guid("1ccfa7fe-1f34-44ce-a604-0da872b767df") },
                    { new Guid("ec58d53c-bd76-4464-b075-82e6f6ff5b35"), 7, 5, new Guid("1b053eea-4af1-4b49-ad6c-8f805d1cd252") },
                    { new Guid("f61c854e-3724-4284-86f6-5da12e165ec8"), 10, 6, new Guid("1b053eea-4af1-4b49-ad6c-8f805d1cd252") },
                    { new Guid("1215f951-04a9-4567-be9e-3d7e4ddce9ff"), 0, 7, new Guid("1b053eea-4af1-4b49-ad6c-8f805d1cd252") }
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
