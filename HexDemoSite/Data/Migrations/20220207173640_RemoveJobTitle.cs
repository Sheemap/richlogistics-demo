using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HexDemoSite.Data.Migrations
{
    public partial class RemoveJobTitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JobTitle",
                table: "DepartmentPositions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "JobTitle",
                table: "DepartmentPositions",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
