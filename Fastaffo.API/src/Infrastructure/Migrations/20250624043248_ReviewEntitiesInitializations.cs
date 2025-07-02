using System;

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fastaffo_api.src.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ReviewEntitiesInitializations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Admins_CreatedById",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_StaffTeams_Teams_TeamId",
                table: "StaffTeams");

            migrationBuilder.DropColumn(
                name: "day_multiplier",
                table: "RatePolicies");

            migrationBuilder.DropColumn(
                name: "kilometers_rate",
                table: "RatePolicies");

            migrationBuilder.DropColumn(
                name: "overtime_multiplier",
                table: "RatePolicies");

            migrationBuilder.DropColumn(
                name: "overtime_start_minutes",
                table: "RatePolicies");

            migrationBuilder.DropColumn(
                name: "travel_time_rate",
                table: "RatePolicies");

            migrationBuilder.AlterColumn<int>(
                name: "TravelTimeMinutes",
                table: "StaffJobs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "TotalAmount",
                table: "StaffJobs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Kilometers",
                table: "StaffJobs",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FinishTime",
                table: "StaffJobs",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Label",
                table: "StaffJobAllowances",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<Guid>(
                name: "ExtraRateAmountEntryId",
                table: "StaffJobAllowances",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<decimal>(
                name: "DayMultiplier",
                table: "RatePolicies",
                type: "decimal(10,4)",
                precision: 10,
                scale: 4,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "KilometersRate",
                table: "RatePolicies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OvertimeMultiplier",
                table: "RatePolicies",
                type: "decimal(10,4)",
                precision: 10,
                scale: 4,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OvertimeStartMinutes",
                table: "RatePolicies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TravelTimeRate",
                table: "RatePolicies",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedById",
                table: "Jobs",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "ExtraRateAmountEntries",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Admins_CreatedById",
                table: "Jobs",
                column: "CreatedById",
                principalTable: "Admins",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StaffTeams_Teams_TeamId",
                table: "StaffTeams",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Admins_CreatedById",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_StaffTeams_Teams_TeamId",
                table: "StaffTeams");

            migrationBuilder.DropColumn(
                name: "DayMultiplier",
                table: "RatePolicies");

            migrationBuilder.DropColumn(
                name: "KilometersRate",
                table: "RatePolicies");

            migrationBuilder.DropColumn(
                name: "OvertimeMultiplier",
                table: "RatePolicies");

            migrationBuilder.DropColumn(
                name: "OvertimeStartMinutes",
                table: "RatePolicies");

            migrationBuilder.DropColumn(
                name: "TravelTimeRate",
                table: "RatePolicies");

            migrationBuilder.AlterColumn<int>(
                name: "TravelTimeMinutes",
                table: "StaffJobs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TotalAmount",
                table: "StaffJobs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Kilometers",
                table: "StaffJobs",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FinishTime",
                table: "StaffJobs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Label",
                table: "StaffJobAllowances",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ExtraRateAmountEntryId",
                table: "StaffJobAllowances",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "day_multiplier",
                table: "RatePolicies",
                type: "decimal(10,4)",
                precision: 10,
                scale: 4,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "kilometers_rate",
                table: "RatePolicies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "overtime_multiplier",
                table: "RatePolicies",
                type: "decimal(10,4)",
                precision: 10,
                scale: 4,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "overtime_start_minutes",
                table: "RatePolicies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "travel_time_rate",
                table: "RatePolicies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedById",
                table: "Jobs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "ExtraRateAmountEntries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Admins_CreatedById",
                table: "Jobs",
                column: "CreatedById",
                principalTable: "Admins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StaffTeams_Teams_TeamId",
                table: "StaffTeams",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
