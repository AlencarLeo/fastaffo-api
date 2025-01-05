using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fastaffo_api.src.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewJobRelationsEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobRequest_Jobs_JobId",
                table: "JobRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_JobRequest_Staffs_StaffId",
                table: "JobRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_JobStaff_Jobs_JobId",
                table: "JobStaff");

            migrationBuilder.DropForeignKey(
                name: "FK_JobStaff_Staffs_StaffId",
                table: "JobStaff");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobStaff",
                table: "JobStaff");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobRequest",
                table: "JobRequest");

            migrationBuilder.RenameTable(
                name: "JobStaff",
                newName: "JobStaffs");

            migrationBuilder.RenameTable(
                name: "JobRequest",
                newName: "JobRequests");

            migrationBuilder.RenameIndex(
                name: "IX_JobStaff_StaffId",
                table: "JobStaffs",
                newName: "IX_JobStaffs_StaffId");

            migrationBuilder.RenameIndex(
                name: "IX_JobStaff_JobId",
                table: "JobStaffs",
                newName: "IX_JobStaffs_JobId");

            migrationBuilder.RenameIndex(
                name: "IX_JobRequest_StaffId",
                table: "JobRequests",
                newName: "IX_JobRequests_StaffId");

            migrationBuilder.RenameIndex(
                name: "IX_JobRequest_JobId",
                table: "JobRequests",
                newName: "IX_JobRequests_JobId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobStaffs",
                table: "JobStaffs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobRequests",
                table: "JobRequests",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobRequests_Jobs_JobId",
                table: "JobRequests",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobRequests_Staffs_StaffId",
                table: "JobRequests",
                column: "StaffId",
                principalTable: "Staffs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobStaffs_Jobs_JobId",
                table: "JobStaffs",
                column: "JobId",
                principalTable: "Jobs",
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
                name: "FK_JobRequests_Jobs_JobId",
                table: "JobRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_JobRequests_Staffs_StaffId",
                table: "JobRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_JobStaffs_Jobs_JobId",
                table: "JobStaffs");

            migrationBuilder.DropForeignKey(
                name: "FK_JobStaffs_Staffs_StaffId",
                table: "JobStaffs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobStaffs",
                table: "JobStaffs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobRequests",
                table: "JobRequests");

            migrationBuilder.RenameTable(
                name: "JobStaffs",
                newName: "JobStaff");

            migrationBuilder.RenameTable(
                name: "JobRequests",
                newName: "JobRequest");

            migrationBuilder.RenameIndex(
                name: "IX_JobStaffs_StaffId",
                table: "JobStaff",
                newName: "IX_JobStaff_StaffId");

            migrationBuilder.RenameIndex(
                name: "IX_JobStaffs_JobId",
                table: "JobStaff",
                newName: "IX_JobStaff_JobId");

            migrationBuilder.RenameIndex(
                name: "IX_JobRequests_StaffId",
                table: "JobRequest",
                newName: "IX_JobRequest_StaffId");

            migrationBuilder.RenameIndex(
                name: "IX_JobRequests_JobId",
                table: "JobRequest",
                newName: "IX_JobRequest_JobId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobStaff",
                table: "JobStaff",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobRequest",
                table: "JobRequest",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobRequest_Jobs_JobId",
                table: "JobRequest",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobRequest_Staffs_StaffId",
                table: "JobRequest",
                column: "StaffId",
                principalTable: "Staffs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobStaff_Jobs_JobId",
                table: "JobStaff",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobStaff_Staffs_StaffId",
                table: "JobStaff",
                column: "StaffId",
                principalTable: "Staffs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
