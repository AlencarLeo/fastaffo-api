using System;

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fastaffo_api.src.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewRequestEntityModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Admins_SupervisorAdminId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Staffs_SupervisorStaffId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Teams_TeamId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_SupervisorAdminId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_TeamId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "SentByType",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "SupervisorAdminId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "Target",
                table: "Requests");

            migrationBuilder.RenameColumn(
                name: "TeamId",
                table: "Requests",
                newName: "ResponsedById");

            migrationBuilder.RenameColumn(
                name: "SupervisorStaffId",
                table: "Requests",
                newName: "AdminId");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_SupervisorStaffId",
                table: "Requests",
                newName: "IX_Requests_AdminId");

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "Requests",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "ResponseDate",
                table: "Requests",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StaffId",
                table: "Requests",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Requests_CompanyId",
                table: "Requests",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_StaffId",
                table: "Requests",
                column: "StaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Admins_AdminId",
                table: "Requests",
                column: "AdminId",
                principalTable: "Admins",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Companies_CompanyId",
                table: "Requests",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Staffs_StaffId",
                table: "Requests",
                column: "StaffId",
                principalTable: "Staffs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Admins_AdminId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Companies_CompanyId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Staffs_StaffId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_CompanyId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_StaffId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "ResponseDate",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "StaffId",
                table: "Requests");

            migrationBuilder.RenameColumn(
                name: "ResponsedById",
                table: "Requests",
                newName: "TeamId");

            migrationBuilder.RenameColumn(
                name: "AdminId",
                table: "Requests",
                newName: "SupervisorStaffId");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_AdminId",
                table: "Requests",
                newName: "IX_Requests_SupervisorStaffId");

            migrationBuilder.AddColumn<int>(
                name: "SentByType",
                table: "Requests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "SupervisorAdminId",
                table: "Requests",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Target",
                table: "Requests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_SupervisorAdminId",
                table: "Requests",
                column: "SupervisorAdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_TeamId",
                table: "Requests",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Admins_SupervisorAdminId",
                table: "Requests",
                column: "SupervisorAdminId",
                principalTable: "Admins",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Staffs_SupervisorStaffId",
                table: "Requests",
                column: "SupervisorStaffId",
                principalTable: "Staffs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Teams_TeamId",
                table: "Requests",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id");
        }
    }
}
