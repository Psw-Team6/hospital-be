using System;
using System.Collections.Generic;
using IntegrationLibrary.BloodSubscription.Model;
using IntegrationLibrary.Tender.Model;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IntegrationLibrary.Migrations
{
    public partial class firstone : Migration
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
                values: new object[] { new Guid("f94a1824-1e6a-42f8-89d5-b9ac3ae47f20"), "aas@gmail.com", "BloodBank", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "localhost", "h+vLNKbrWu+bpT5I1/e12asBn1KvUf2735y9DHmUxCA=" });

            migrationBuilder.InsertData(
                table: "BloodRequests",
                columns: new[] { "Id", "Amount", "BloodBankId", "Comment", "Date", "DoctorUsername", "Reason", "Status", "Type" },
                values: new object[,]
                {
                    { new Guid("5805c5f0-d755-4f87-a92f-5b6cbb895439"), 10.0, new Guid("00000000-0000-0000-0000-000000000000"), "", new DateTime(2022, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ilija", "Operacija", 2, 5 },
                    { new Guid("78028f77-7852-42a9-9972-1bf5bda85d57"), 20.0, new Guid("00000000-0000-0000-0000-000000000000"), "", new DateTime(2022, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ilija", "Transfuzija", 2, 3 },
                    { new Guid("6dbc8675-a43b-4194-a6c1-0db13a2aa6cb"), 20.0, new Guid("00000000-0000-0000-0000-000000000000"), "", new DateTime(2023, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ilija", "Transfuzija", 2, 0 },
                    { new Guid("fcd5ad92-e250-4561-ab1f-8ca860aeb09e"), 5.0, new Guid("00000000-0000-0000-0000-000000000000"), "", new DateTime(2023, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ilija", "Zalihe", 2, 6 }
                });

            migrationBuilder.InsertData(
                table: "ConfigureGenerateAndSend",
                columns: new[] { "Id", "BloodBankName", "GeneratePeriod", "NextDateForSending", "SendPeriod" },
                values: new object[,]
                {
                    { new Guid("1a8bea6a-c73d-47e3-a11b-24dc5929c32d"), "Moja Banka Krvi", "ONE_MONTH", new DateTime(2023, 1, 9, 21, 55, 43, 281, DateTimeKind.Local).AddTicks(1718), "EVERY_TWO_MINUT" },
                    { new Guid("d666e43c-3a43-4e50-aa7a-e62d9acf2dec"), "Nova banka", "TWO_MONTH", new DateTime(2023, 1, 9, 21, 55, 43, 291, DateTimeKind.Local).AddTicks(9969), "ONE_MONTH" }
                });

            migrationBuilder.InsertData(
                table: "Tenders",
                columns: new[] { "Id", "DeadlineDate", "HasDeadline", "PublishedDate", "Status", "TenderOffer", "Winner" },
                values: new object[,]
                {
                    { new Guid("3a194651-844c-41c1-acdc-bd4ce478dbb7"), new DateTime(2023, 1, 29, 21, 55, 43, 292, DateTimeKind.Local).AddTicks(2143), true, new DateTime(2023, 1, 9, 21, 55, 43, 292, DateTimeKind.Local).AddTicks(2577), 0, null, null },
                    { new Guid("8da00c33-39b0-453b-ae26-f03e09e234d4"), new DateTime(2023, 1, 8, 21, 55, 43, 292, DateTimeKind.Local).AddTicks(4054), true, new DateTime(2023, 1, 5, 21, 55, 43, 292, DateTimeKind.Local).AddTicks(4086), 2, null, null }
                });

            migrationBuilder.InsertData(
                table: "BloodUnitAmounts",
                columns: new[] { "Id", "Amount", "BloodType", "TenderId" },
                values: new object[,]
                {
                    { new Guid("9cfd69d2-8d31-4198-963a-96018b322d5d"), 10, 0, new Guid("3a194651-844c-41c1-acdc-bd4ce478dbb7") },
                    { new Guid("b7a3c9fb-5c08-43ca-b94f-59b1e3000af1"), 0, 1, new Guid("3a194651-844c-41c1-acdc-bd4ce478dbb7") },
                    { new Guid("ba62ab0b-1462-481d-b9e4-10247c629ad8"), 5, 2, new Guid("3a194651-844c-41c1-acdc-bd4ce478dbb7") },
                    { new Guid("5c29015c-a0d4-47bf-aaad-410d73ca57e5"), 0, 3, new Guid("3a194651-844c-41c1-acdc-bd4ce478dbb7") },
                    { new Guid("ac36e432-f531-492c-b267-9bf71bfff199"), 12, 4, new Guid("3a194651-844c-41c1-acdc-bd4ce478dbb7") },
                    { new Guid("531ec062-8818-4651-8bda-e5a08a7c2fa7"), 7, 5, new Guid("3a194651-844c-41c1-acdc-bd4ce478dbb7") },
                    { new Guid("ed3570d4-51a9-4e8e-8f32-44da70729602"), 10, 6, new Guid("3a194651-844c-41c1-acdc-bd4ce478dbb7") },
                    { new Guid("72dde8c1-2101-419a-8e0e-a0aad5d5c52d"), 0, 7, new Guid("3a194651-844c-41c1-acdc-bd4ce478dbb7") },
                    { new Guid("5f915480-5098-4ef3-9d30-6bd48d00ef47"), 7, 5, new Guid("8da00c33-39b0-453b-ae26-f03e09e234d4") },
                    { new Guid("5107a6fb-0149-4ce1-a7d6-4524958be89c"), 10, 6, new Guid("8da00c33-39b0-453b-ae26-f03e09e234d4") },
                    { new Guid("64cd89b7-570e-4ea9-93dd-f347a71fdc38"), 0, 7, new Guid("8da00c33-39b0-453b-ae26-f03e09e234d4") }
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
