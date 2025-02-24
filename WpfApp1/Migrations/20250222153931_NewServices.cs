using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WpfApp1.Migrations
{
    /// <inheritdoc />
    public partial class NewServices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_Employees_EmployeesId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_EmployeesId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "Cost",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "NameServices",
                table: "Services");

            migrationBuilder.RenameColumn(
                name: "TypeServices",
                table: "Services",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "EmployeesId",
                table: "Services",
                newName: "UserId");

            migrationBuilder.AddColumn<DateTime>(
                name: "RequestDate",
                table: "Services",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestDate",
                table: "Services");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Services",
                newName: "EmployeesId");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Services",
                newName: "TypeServices");

            migrationBuilder.AddColumn<decimal>(
                name: "Cost",
                table: "Services",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "NameServices",
                table: "Services",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Services_EmployeesId",
                table: "Services",
                column: "EmployeesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Employees_EmployeesId",
                table: "Services",
                column: "EmployeesId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
