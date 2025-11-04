using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taller.Backend.Migrations
{
    /// <inheritdoc />
    public partial class paisC : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Employee",
                table: "Employee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Country",
                table: "Country");

            migrationBuilder.RenameTable(
                name: "Employee",
                newName: "Employees");

            migrationBuilder.RenameTable(
                name: "Country",
                newName: "Countries");

            migrationBuilder.RenameIndex(
                name: "IX_Employee_FirstName_LastName",
                table: "Employees",
                newName: "IX_Employees_FirstName_LastName");

            migrationBuilder.RenameIndex(
                name: "IX_Country_Name",
                table: "Countries",
                newName: "IX_Countries_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Countries",
                table: "Countries",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Countries",
                table: "Countries");

            migrationBuilder.RenameTable(
                name: "Employees",
                newName: "Employee");

            migrationBuilder.RenameTable(
                name: "Countries",
                newName: "Country");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_FirstName_LastName",
                table: "Employee",
                newName: "IX_Employee_FirstName_LastName");

            migrationBuilder.RenameIndex(
                name: "IX_Countries_Name",
                table: "Country",
                newName: "IX_Country_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employee",
                table: "Employee",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Country",
                table: "Country",
                column: "Id");
        }
    }
}
