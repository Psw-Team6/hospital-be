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
                name: "PDFReportDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PdfName = table.Column<string>(type: "text", nullable: true),
                    StartTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PDFReportDetails", x => x.Id);
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
                values: new object[,]
                {
                    { new Guid("2727aa43-75af-421c-9202-463f1daeebda"), "tasaantic00@gmail.com", "BloodBank", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "localhost", "bHaCSLjsmN+NT6SGY1k81KvTOEW4QVDonP5DF1Y+tyU=" },
                    { new Guid("43336b7c-6b59-403c-a814-e059052e45ed"), "tasaantic00@gmail.com", "TamaraBank", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADi", "localhost", "4GbC6Ss42a7mAJRA0ACwLDQI+9A7r+8CdvyI3zDTMA4=" }
                });

            migrationBuilder.InsertData(
                table: "BloodRequests",
                columns: new[] { "Id", "Amount", "BloodBankId", "Comment", "Date", "DoctorUsername", "Reason", "Status", "Type" },
                values: new object[,]
                {
                    { new Guid("8ebc7418-9db7-457f-8b98-4170f6b66ffd"), 10.0, new Guid("00000000-0000-0000-0000-000000000000"), "", new DateTime(2022, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ilija", "Operacija", 2, 5 },
                    { new Guid("c65df01f-de12-4873-b274-4ba1b7a4c618"), 20.0, new Guid("00000000-0000-0000-0000-000000000000"), "", new DateTime(2022, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ilija", "Transfuzija", 2, 3 },
                    { new Guid("be8082fc-d424-4059-8264-418a52751b84"), 20.0, new Guid("00000000-0000-0000-0000-000000000000"), "", new DateTime(2023, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ilija", "Transfuzija", 2, 0 },
                    { new Guid("92127a78-14e3-4f61-a5f6-cf4c788fe6a3"), 5.0, new Guid("00000000-0000-0000-0000-000000000000"), "", new DateTime(2023, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ilija", "Zalihe", 2, 6 }
                });

            migrationBuilder.InsertData(
                table: "ConfigureGenerateAndSend",
                columns: new[] { "Id", "BloodBankName", "GeneratePeriod", "NextDateForSending", "SendPeriod" },
                values: new object[,]
                {
                    { new Guid("f811589f-942f-4fa7-820c-b4096d44bf92"), "Moja Banka Krvi", "ONE_MONTH", new DateTime(2023, 1, 13, 2, 54, 23, 84, DateTimeKind.Local).AddTicks(2239), "EVERY_TWO_MINUT" },
                    { new Guid("53cea329-8855-4ac5-b748-35f2e942fcef"), "Nova banka", "TWO_MONTH", new DateTime(2023, 1, 13, 2, 54, 23, 86, DateTimeKind.Local).AddTicks(2473), "ONE_MONTH" }
                });

            migrationBuilder.InsertData(
                table: "Tenders",
                columns: new[] { "Id", "DeadlineDate", "HasDeadline", "PublishedDate", "Status", "TenderOffer", "Winner" },
                values: new object[,]
                {
                    { new Guid("19ae9440-401a-4dac-81d2-6139588f8cd5"), new DateTime(2023, 2, 2, 2, 54, 23, 86, DateTimeKind.Local).AddTicks(3769), true, new DateTime(2023, 1, 13, 2, 54, 23, 86, DateTimeKind.Local).AddTicks(3967), 0, null, null },
                    { new Guid("62b76dce-e0d5-4acd-8ac0-359d62fd1515"), new DateTime(2023, 1, 12, 2, 54, 23, 86, DateTimeKind.Local).AddTicks(4261), true, new DateTime(2023, 1, 9, 2, 54, 23, 86, DateTimeKind.Local).AddTicks(4267), 2, null, null },
                    { new Guid("24519c26-78f4-4728-ab9d-bf6a78aa29da"), new DateTime(2023, 1, 9, 2, 54, 23, 86, DateTimeKind.Local).AddTicks(4271), true, new DateTime(2023, 1, 3, 2, 54, 23, 86, DateTimeKind.Local).AddTicks(4273), 2, null, null }
                });

            migrationBuilder.InsertData(
                table: "BloodUnitAmounts",
                columns: new[] { "Id", "Amount", "BloodType", "TenderId" },
                values: new object[,]
                {
                    { new Guid("318194b0-85a6-43ac-b987-463f6d2c5b2d"), 10, 0, new Guid("19ae9440-401a-4dac-81d2-6139588f8cd5") },
                    { new Guid("3b5fa4b8-e790-4efe-a803-80ba26096255"), 15, 1, new Guid("19ae9440-401a-4dac-81d2-6139588f8cd5") },
                    { new Guid("54f3b85d-6997-4075-974b-0a1872f81dad"), 5, 2, new Guid("19ae9440-401a-4dac-81d2-6139588f8cd5") },
                    { new Guid("0c83d6f7-c3e2-48ff-afc3-dfd9c8b84815"), 17, 3, new Guid("19ae9440-401a-4dac-81d2-6139588f8cd5") },
                    { new Guid("87261e8c-ed67-45c6-96e2-e6e2775556f3"), 12, 4, new Guid("19ae9440-401a-4dac-81d2-6139588f8cd5") },
                    { new Guid("171ce85e-6077-4e45-838e-f5ad7a9a1b06"), 7, 5, new Guid("19ae9440-401a-4dac-81d2-6139588f8cd5") },
                    { new Guid("47819be4-36c4-4a2b-b1cf-24b0013b9ef5"), 10, 6, new Guid("19ae9440-401a-4dac-81d2-6139588f8cd5") },
                    { new Guid("5143a7aa-e2f0-470a-9eb4-957973082a89"), 0, 7, new Guid("19ae9440-401a-4dac-81d2-6139588f8cd5") },
                    { new Guid("282ed418-4ba8-4e56-ab39-bfaf1a691545"), 7, 5, new Guid("62b76dce-e0d5-4acd-8ac0-359d62fd1515") },
                    { new Guid("e2834992-a0b2-4f7b-90d4-f8b2722a7175"), 10, 6, new Guid("62b76dce-e0d5-4acd-8ac0-359d62fd1515") },
                    { new Guid("3c08d08b-bcaf-4ac0-a146-7203d498b0a0"), 14, 7, new Guid("62b76dce-e0d5-4acd-8ac0-359d62fd1515") },
                    { new Guid("b26afa32-3862-40e9-81e5-571c76f31d69"), 7, 5, new Guid("24519c26-78f4-4728-ab9d-bf6a78aa29da") },
                    { new Guid("1684c057-1c5b-4a2d-a81c-ded9acf03cb5"), 10, 6, new Guid("24519c26-78f4-4728-ab9d-bf6a78aa29da") },
                    { new Guid("b765584f-f9e5-455c-a3ce-baf514caecf4"), 14, 7, new Guid("24519c26-78f4-4728-ab9d-bf6a78aa29da") }
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
                name: "PDFReportDetails");

            migrationBuilder.DropTable(
                name: "TenderOffer");

            migrationBuilder.DropTable(
                name: "Tenders");
        }
    }
}
