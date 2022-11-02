using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentOffice.Data.SOMigrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "MO");

            migrationBuilder.CreateTable(
                name: "Conditions",
                schema: "MO",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConditionName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conditions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeRoles",
                schema: "MO",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeRoles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                schema: "MO",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                schema: "MO",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuggestionNumber = table.Column<int>(type: "int", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TeamID = table.Column<int>(type: "int", nullable: false),
                    EmployeeRoleID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Employees_EmployeeRoles_EmployeeRoleID",
                        column: x => x.EmployeeRoleID,
                        principalSchema: "MO",
                        principalTable: "EmployeeRoles",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Employees_Teams_TeamID",
                        column: x => x.TeamID,
                        principalSchema: "MO",
                        principalTable: "Teams",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeConditions",
                schema: "MO",
                columns: table => new
                {
                    ConditionID = table.Column<int>(type: "int", nullable: false),
                    EmployeeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeConditions", x => new { x.ConditionID, x.EmployeeID });
                    table.ForeignKey(
                        name: "FK_EmployeeConditions_Conditions_ConditionID",
                        column: x => x.ConditionID,
                        principalSchema: "MO",
                        principalTable: "Conditions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeConditions_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalSchema: "MO",
                        principalTable: "Employees",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeConditions_EmployeeID",
                schema: "MO",
                table: "EmployeeConditions",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmployeeNumber",
                schema: "MO",
                table: "Employees",
                column: "EmployeeNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmployeeRoleID",
                schema: "MO",
                table: "Employees",
                column: "EmployeeRoleID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_TeamID",
                schema: "MO",
                table: "Employees",
                column: "TeamID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeConditions",
                schema: "MO");

            migrationBuilder.DropTable(
                name: "Conditions",
                schema: "MO");

            migrationBuilder.DropTable(
                name: "Employees",
                schema: "MO");

            migrationBuilder.DropTable(
                name: "EmployeeRoles",
                schema: "MO");

            migrationBuilder.DropTable(
                name: "Teams",
                schema: "MO");
        }
    }
}
