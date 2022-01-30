using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HexDemoSite.Data.Migrations
{
    public partial class DeleteSetNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentPositions_OpenPositions_OpenPositionId",
                table: "DepartmentPositions");

            migrationBuilder.DropIndex(
                name: "IX_DepartmentPositions_OpenPositionId",
                table: "DepartmentPositions");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentPositions_OpenPositionId",
                table: "DepartmentPositions",
                column: "OpenPositionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentPositions_OpenPositions_OpenPositionId",
                table: "DepartmentPositions",
                column: "OpenPositionId",
                principalTable: "OpenPositions",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentPositions_OpenPositions_OpenPositionId",
                table: "DepartmentPositions");

            migrationBuilder.DropIndex(
                name: "IX_DepartmentPositions_OpenPositionId",
                table: "DepartmentPositions");

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
    }
}
