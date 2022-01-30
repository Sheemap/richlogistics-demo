using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HexDemoSite.Data.Migrations
{
    public partial class AddLeadershipApprovalField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateApproved",
                table: "OpenPositions",
                newName: "LeadershipDateApproved");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "HRDateApproved",
                table: "OpenPositions",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HRDateApproved",
                table: "OpenPositions");

            migrationBuilder.RenameColumn(
                name: "LeadershipDateApproved",
                table: "OpenPositions",
                newName: "DateApproved");
        }
    }
}
