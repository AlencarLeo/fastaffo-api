using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fastaffo_api.src.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewJobReqAdminProps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Client",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Event",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "FinishDateTime",
                table: "Jobs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "JobDuration",
                table: "Jobs",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "JobNumber",
                table: "Jobs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "TotalChargedValue",
                table: "Jobs",
                type: "real",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Client",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Event",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "FinishDateTime",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "JobDuration",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "JobNumber",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "TotalChargedValue",
                table: "Jobs");
        }
    }
}
