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
                values: new object[] { new Guid("3731ba42-bf0b-49a9-9852-dd1073230711"), "aas@gmail.com", "BloodBank", "VNEXwZIHrujyvlg0wnmHM2FkQ52BKSkUTv5Gobgj4MeeAADy", "localhost", "zrWiV/TOKasMS26rNVIP59IiZjW+7NoxKbhSqjd4m2g=" });

            migrationBuilder.InsertData(
                table: "BloodRequests",
                columns: new[] { "Id", "Amount", "BloodBankId", "Comment", "Date", "DoctorUsername", "Reason", "Status", "Type" },
                values: new object[,]
                {
                    { new Guid("e5175e68-52a2-4451-a732-8c4720d5e6cc"), 10.0, new Guid("00000000-0000-0000-0000-000000000000"), "", new DateTime(2022, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ilija", "Operacija", 2, 5 },
                    { new Guid("564b6121-86f2-44af-9c93-4ba4bac76aaa"), 20.0, new Guid("00000000-0000-0000-0000-000000000000"), "", new DateTime(2022, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ilija", "Transfuzija", 2, 3 },
                    { new Guid("487d5a32-7467-4398-b50f-bbdcd5142a32"), 20.0, new Guid("00000000-0000-0000-0000-000000000000"), "", new DateTime(2023, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ilija", "Transfuzija", 2, 0 },
                    { new Guid("078e971a-ddc8-4252-b121-a733c36d2971"), 5.0, new Guid("00000000-0000-0000-0000-000000000000"), "", new DateTime(2023, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ilija", "Zalihe", 2, 6 }
                });

            migrationBuilder.InsertData(
                table: "ConfigureGenerateAndSend",
                columns: new[] { "Id", "BloodBankName", "GeneratePeriod", "NextDateForSending", "SendPeriod" },
                values: new object[,]
                {
                    { new Guid("02304174-f632-46e3-80c9-a5cf43df1d1c"), "Moja Banka Krvi", "ONE_MONTH", new DateTime(2023, 1, 9, 14, 18, 0, 881, DateTimeKind.Local).AddTicks(6061), "EVERY_TWO_MINUT" },
                    { new Guid("b8ec0c55-8e61-4dce-b5d5-49a8d9b63b75"), "Nova banka", "TWO_MONTH", new DateTime(2023, 1, 9, 14, 18, 0, 888, DateTimeKind.Local).AddTicks(8761), "ONE_MONTH" }
                });

            migrationBuilder.InsertData(
                table: "Tenders",
                columns: new[] { "Id", "DeadlineDate", "HasDeadline", "PublishedDate", "Status", "TenderOffer", "Winner" },
                values: new object[,]
                {
                    { new Guid("2712f390-a199-49fe-8b69-54b21be61e3a"), new DateTime(2023, 1, 29, 14, 18, 0, 889, DateTimeKind.Local).AddTicks(796), true, new DateTime(2023, 1, 9, 14, 18, 0, 889, DateTimeKind.Local).AddTicks(1272), 0, null, null },
                    { new Guid("9570b244-8f3d-412c-8ae8-600139db6c54"), new DateTime(2023, 1, 8, 14, 18, 0, 889, DateTimeKind.Local).AddTicks(2077), true, new DateTime(2023, 1, 5, 14, 18, 0, 889, DateTimeKind.Local).AddTicks(2092), 2, null, null }
                });

            migrationBuilder.InsertData(
                table: "BloodUnitAmounts",
                columns: new[] { "Id", "Amount", "BloodType", "TenderId" },
                values: new object[,]
                {
                    { new Guid("ad71e1b9-7f55-4df7-b0d1-628a3dba2da7"), 10, 0, new Guid("2712f390-a199-49fe-8b69-54b21be61e3a") },
                    { new Guid("a18b4980-d6b4-4cfd-97ed-8fbccc258366"), 0, 1, new Guid("2712f390-a199-49fe-8b69-54b21be61e3a") },
                    { new Guid("f5ecd678-3389-4ed8-8426-065ae2a38f73"), 5, 2, new Guid("2712f390-a199-49fe-8b69-54b21be61e3a") },
                    { new Guid("7d508840-b4eb-46df-a01d-35cff8be0417"), 0, 3, new Guid("2712f390-a199-49fe-8b69-54b21be61e3a") },
                    { new Guid("b71cab99-336a-4123-9e50-e16a11834540"), 12, 4, new Guid("2712f390-a199-49fe-8b69-54b21be61e3a") },
                    { new Guid("adc7c933-b670-4934-9120-94da4053ba4b"), 7, 5, new Guid("2712f390-a199-49fe-8b69-54b21be61e3a") },
                    { new Guid("78c5fe34-c1e6-4073-805a-d143be94dac9"), 10, 6, new Guid("2712f390-a199-49fe-8b69-54b21be61e3a") },
                    { new Guid("d112f038-c8bc-4e63-9451-382da1f4de9c"), 0, 7, new Guid("2712f390-a199-49fe-8b69-54b21be61e3a") },
                    { new Guid("47a8810c-ed3e-4759-97df-5232c2381959"), 7, 5, new Guid("9570b244-8f3d-412c-8ae8-600139db6c54") },
                    { new Guid("9ac8d39e-5fe5-4f5f-bb58-a2f50e68b4e8"), 10, 6, new Guid("9570b244-8f3d-412c-8ae8-600139db6c54") },
                    { new Guid("67b10973-2da7-4bb2-b367-f4be11b1be15"), 0, 7, new Guid("9570b244-8f3d-412c-8ae8-600139db6c54") }
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
