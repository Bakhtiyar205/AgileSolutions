using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CompanyEmployee.Migrations
{
    public partial class AddEmployeeProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "Employees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Pin",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "SerialNumber",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<string>(
                name: "Serie",
                table: "Employees",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "Employees",
                type: "nvarchar(155)",
                maxLength: 155,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Pin",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "SerialNumber",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Serie",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "Employees");
        }
    }
}
