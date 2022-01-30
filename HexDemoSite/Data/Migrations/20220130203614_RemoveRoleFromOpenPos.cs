using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HexDemoSite.Data.Migrations
{
    public partial class RemoveRoleFromOpenPos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OpenPositions_Roles_RoleId",
                table: "OpenPositions");

            migrationBuilder.DropIndex(
                name: "IX_OpenPositions_RoleId",
                table: "OpenPositions");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "OpenPositions");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateFilled",
                table: "OpenPositions",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateFilled",
                table: "OpenPositions");

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "OpenPositions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OpenPositions_RoleId",
                table: "OpenPositions",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_OpenPositions_Roles_RoleId",
                table: "OpenPositions",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
