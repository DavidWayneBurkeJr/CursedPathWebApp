using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CursedPathWebApp.Data.Migrations
{
    public partial class newMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Venue_Schedule_ScheduleListViewModelShowId",
                table: "Venue");

            migrationBuilder.DropIndex(
                name: "IX_Venue_ScheduleListViewModelShowId",
                table: "Venue");

            migrationBuilder.DropColumn(
                name: "ScheduleListViewModelShowId",
                table: "Venue");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Venue",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "Venue",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VenueId",
                table: "Schedule",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_VenueId",
                table: "Schedule",
                column: "VenueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_Venue_VenueId",
                table: "Schedule",
                column: "VenueId",
                principalTable: "Venue",
                principalColumn: "VenueId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_Venue_VenueId",
                table: "Schedule");

            migrationBuilder.DropIndex(
                name: "IX_Schedule_VenueId",
                table: "Schedule");

            migrationBuilder.DropColumn(
                name: "VenueId",
                table: "Schedule");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Venue",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "Venue",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "ScheduleListViewModelShowId",
                table: "Venue",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Venue_ScheduleListViewModelShowId",
                table: "Venue",
                column: "ScheduleListViewModelShowId");

            migrationBuilder.AddForeignKey(
                name: "FK_Venue_Schedule_ScheduleListViewModelShowId",
                table: "Venue",
                column: "ScheduleListViewModelShowId",
                principalTable: "Schedule",
                principalColumn: "ShowId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
