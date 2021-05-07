using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gozen.Data.Migrations.GozenDBMigrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DocumentType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Passenger",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    DocumentTypeId = table.Column<int>(type: "int", nullable: false),
                    DocumentNumber = table.Column<int>(type: "int", maxLength: 4, nullable: false),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passenger", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Passenger_DocumentType_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalTable: "DocumentType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "DocumentType",
                columns: new[] { "Id", "IsActive", "IssueDate", "Type" },
                values: new object[] { 1, true, new DateTime(2021, 5, 6, 21, 19, 47, 886, DateTimeKind.Utc).AddTicks(4350), "Pasaport" });

            migrationBuilder.InsertData(
                table: "DocumentType",
                columns: new[] { "Id", "IsActive", "IssueDate", "Type" },
                values: new object[] { 2, true, new DateTime(2021, 5, 6, 21, 19, 47, 886, DateTimeKind.Utc).AddTicks(7177), "Visa" });

            migrationBuilder.InsertData(
                table: "DocumentType",
                columns: new[] { "Id", "IsActive", "IssueDate", "Type" },
                values: new object[] { 3, true, new DateTime(2021, 5, 6, 21, 19, 47, 886, DateTimeKind.Utc).AddTicks(7192), "Travel" });

            migrationBuilder.InsertData(
                table: "Passenger",
                columns: new[] { "Id", "DocumentNumber", "DocumentTypeId", "Gender", "IsActive", "IssueDate", "Name", "Surname" },
                values: new object[,]
                {
                    { 1, 1111, 1, 0, true, new DateTime(2021, 5, 6, 21, 19, 47, 908, DateTimeKind.Utc).AddTicks(7513), "Name_1", "Surname_1" },
                    { 4, 4444, 1, 1, true, new DateTime(2021, 5, 6, 21, 19, 47, 909, DateTimeKind.Utc).AddTicks(3343), "Name_4", "Surname_4" },
                    { 2, 2222, 2, 1, true, new DateTime(2021, 5, 6, 21, 19, 47, 909, DateTimeKind.Utc).AddTicks(3323), "Name_2", "Surname_2" },
                    { 5, 5555, 2, 0, true, new DateTime(2021, 5, 6, 21, 19, 47, 909, DateTimeKind.Utc).AddTicks(3346), "Name_5", "Surname_5" },
                    { 3, 3333, 3, 0, true, new DateTime(2021, 5, 6, 21, 19, 47, 909, DateTimeKind.Utc).AddTicks(3338), "Name_3", "Surname_3" },
                    { 6, 6666, 3, 1, true, new DateTime(2021, 5, 6, 21, 19, 47, 909, DateTimeKind.Utc).AddTicks(3349), "Name_6", "Surname_6" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Passenger_DocumentTypeId",
                table: "Passenger",
                column: "DocumentTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Passenger");

            migrationBuilder.DropTable(
                name: "DocumentType");
        }
    }
}
