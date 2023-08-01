using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HrPortal.Migrations
{
    /// <inheritdoc />
    public partial class AddedEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppEmployees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalNumberOfDaysThisYear = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CNP = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    InformationsCI = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rezidence = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SendingAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RelevancePhoneNumber = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    PersonalPhoneNumber = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    HiringDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BirthDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartingSalary = table.Column<int>(type: "int", nullable: false),
                    PaysProgrammerTaxes = table.Column<bool>(type: "bit", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppEmployees", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppEmployees");
        }
    }
}
