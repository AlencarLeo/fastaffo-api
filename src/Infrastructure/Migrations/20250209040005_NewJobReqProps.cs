using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fastaffo_api.src.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewJobReqProps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AdminId",
                table: "JobRequests",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ResponsedAt",
                table: "JobRequests",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobStaffs_StaffId",
                table: "JobStaffs",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_JobRequests_AdminId",
                table: "JobRequests",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_JobRequests_StaffId",
                table: "JobRequests",
                column: "StaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobRequests_Admins_AdminId",
                table: "JobRequests",
                column: "AdminId",
                principalTable: "Admins",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobRequests_Staffs_StaffId",
                table: "JobRequests",
                column: "StaffId",
                principalTable: "Staffs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobStaffs_Staffs_StaffId",
                table: "JobStaffs",
                column: "StaffId",
                principalTable: "Staffs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobRequests_Admins_AdminId",
                table: "JobRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_JobRequests_Staffs_StaffId",
                table: "JobRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_JobStaffs_Staffs_StaffId",
                table: "JobStaffs");

            migrationBuilder.DropIndex(
                name: "IX_JobStaffs_StaffId",
                table: "JobStaffs");

            migrationBuilder.DropIndex(
                name: "IX_JobRequests_AdminId",
                table: "JobRequests");

            migrationBuilder.DropIndex(
                name: "IX_JobRequests_StaffId",
                table: "JobRequests");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "JobRequests");

            migrationBuilder.DropColumn(
                name: "ResponsedAt",
                table: "JobRequests");
        }
    }
}
