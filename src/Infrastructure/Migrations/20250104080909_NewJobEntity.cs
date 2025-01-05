using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fastaffo_api.src.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewJobEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StaffsId",
                table: "Jobs",
                newName: "JobStaffsId");

            migrationBuilder.AddColumn<int>(
                name: "CurrentStaffCount",
                table: "Jobs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsClosed",
                table: "Jobs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "JobRequestsId",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaxStaffNumber",
                table: "Jobs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "JobRequest",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StaffId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    RequestedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobRequest_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobRequest_Staffs_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobStaff",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StaffId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobRole = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddRate = table.Column<float>(type: "real", nullable: false),
                    AddStartDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobStaff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobStaff_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobStaff_Staffs_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobRequest_JobId",
                table: "JobRequest",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_JobRequest_StaffId",
                table: "JobRequest",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_JobStaff_JobId",
                table: "JobStaff",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_JobStaff_StaffId",
                table: "JobStaff",
                column: "StaffId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobRequest");

            migrationBuilder.DropTable(
                name: "JobStaff");

            migrationBuilder.DropColumn(
                name: "CurrentStaffCount",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "IsClosed",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "JobRequestsId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "MaxStaffNumber",
                table: "Jobs");

            migrationBuilder.RenameColumn(
                name: "JobStaffsId",
                table: "Jobs",
                newName: "StaffsId");
        }
    }
}
