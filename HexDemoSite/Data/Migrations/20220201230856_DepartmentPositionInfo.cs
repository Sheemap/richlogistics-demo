using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HexDemoSite.Data.Migrations
{
    public partial class DepartmentPositionInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "DepartmentPositions",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "JobDescription",
                table: "DepartmentPositions",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "JobTitle",
                table: "DepartmentPositions",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "DepartmentPositions",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Department",
                table: "DepartmentPositions");

            migrationBuilder.DropColumn(
                name: "JobDescription",
                table: "DepartmentPositions");

            migrationBuilder.DropColumn(
                name: "JobTitle",
                table: "DepartmentPositions");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "DepartmentPositions");
        }
    }
}
