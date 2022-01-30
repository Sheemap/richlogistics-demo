using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HexDemoSite.Data.Migrations
{
    public partial class AddOpenPosToDepPos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OpenPositionId",
                table: "DepartmentPositions",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentPositions_OpenPositionId",
                table: "DepartmentPositions",
                column: "OpenPositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentPositions_OpenPositions_OpenPositionId",
                table: "DepartmentPositions",
                column: "OpenPositionId",
                principalTable: "OpenPositions",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentPositions_OpenPositions_OpenPositionId",
                table: "DepartmentPositions");

            migrationBuilder.DropIndex(
                name: "IX_DepartmentPositions_OpenPositionId",
                table: "DepartmentPositions");

            migrationBuilder.DropColumn(
                name: "OpenPositionId",
                table: "DepartmentPositions");
        }
    }
}
