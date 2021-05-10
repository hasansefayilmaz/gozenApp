using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gozen.Data.Migrations.GozenDBMigrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "DocumentType",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>("nvarchar(max)", nullable: true),
                    IssueDate = table.Column<DateTime>("datetime2", nullable: false),
                    IsActive = table.Column<bool>("bit", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_DocumentType", x => x.Id); });

            migrationBuilder.CreateTable(
                "PassengerDto",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>("nvarchar(50)", maxLength: 50, nullable: false),
                    Surname = table.Column<string>("nvarchar(50)", maxLength: 50, nullable: false),
                    Gender = table.Column<int>("int", nullable: false),
                    DocumentTypeId = table.Column<int>("int", nullable: false),
                    DocumentNumber = table.Column<int>("int", maxLength: 4, nullable: false),
                    IssueDate = table.Column<DateTime>("datetime2", nullable: false),
                    IsActive = table.Column<bool>("bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passenger", x => x.Id);
                    table.ForeignKey(
                        "FK_Passenger_DocumentType_DocumentTypeId",
                        x => x.DocumentTypeId,
                        "DocumentType",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                "DocumentType",
                new[] {"Id", "IsActive", "IssueDate", "Type"},
                new object[]
                    {1, true, new DateTime(2021, 5, 6, 21, 19, 47, 886, DateTimeKind.Utc).AddTicks(4350), "Pasaport"});

            migrationBuilder.InsertData(
                "DocumentType",
                new[] {"Id", "IsActive", "IssueDate", "Type"},
                new object[]
                    {2, true, new DateTime(2021, 5, 6, 21, 19, 47, 886, DateTimeKind.Utc).AddTicks(7177), "Visa"});

            migrationBuilder.InsertData(
                "DocumentType",
                new[] {"Id", "IsActive", "IssueDate", "Type"},
                new object[]
                    {3, true, new DateTime(2021, 5, 6, 21, 19, 47, 886, DateTimeKind.Utc).AddTicks(7192), "Travel"});

            migrationBuilder.InsertData(
                "PassengerDto",
                new[] {"Id", "DocumentNumber", "DocumentTypeId", "Gender", "IsActive", "IssueDate", "Name", "Surname"},
                new object[,]
                {
                    {
                        1, 1111, 1, 0, true, new DateTime(2021, 5, 6, 21, 19, 47, 908, DateTimeKind.Utc).AddTicks(7513),
                        "Name_1", "Surname_1"
                    },
                    {
                        4, 4444, 1, 1, true, new DateTime(2021, 5, 6, 21, 19, 47, 909, DateTimeKind.Utc).AddTicks(3343),
                        "Name_4", "Surname_4"
                    },
                    {
                        2, 2222, 2, 1, true, new DateTime(2021, 5, 6, 21, 19, 47, 909, DateTimeKind.Utc).AddTicks(3323),
                        "Name_2", "Surname_2"
                    },
                    {
                        5, 5555, 2, 0, true, new DateTime(2021, 5, 6, 21, 19, 47, 909, DateTimeKind.Utc).AddTicks(3346),
                        "Name_5", "Surname_5"
                    },
                    {
                        3, 3333, 3, 0, true, new DateTime(2021, 5, 6, 21, 19, 47, 909, DateTimeKind.Utc).AddTicks(3338),
                        "Name_3", "Surname_3"
                    },
                    {
                        6, 6666, 3, 1, true, new DateTime(2021, 5, 6, 21, 19, 47, 909, DateTimeKind.Utc).AddTicks(3349),
                        "Name_6", "Surname_6"
                    }
                });

            migrationBuilder.CreateIndex(
                "IX_Passenger_DocumentTypeId",
                "PassengerDto",
                "DocumentTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "PassengerDto");

            migrationBuilder.DropTable(
                "DocumentType");
        }
    }
}