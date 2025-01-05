using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fastaffo_api.src.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewJobsReqRelationBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "IX_JobRequests_StaffId",
                table: "JobRequests");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_JobStaffs_StaffId",
                table: "JobStaffs",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_JobRequests_StaffId",
                table: "JobRequests",
                column: "StaffId");

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
    }
}
