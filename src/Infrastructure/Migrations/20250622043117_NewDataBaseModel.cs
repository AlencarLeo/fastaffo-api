using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fastaffo_api.src.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewDataBaseModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admins_Companies_CompanyId",
                table: "Admins");

            migrationBuilder.DropTable(
                name: "JobRequests");

            migrationBuilder.DropTable(
                name: "JobStaffs");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "AcceptingReqs",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "BaseRate",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Client",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "CurrentStaffCount",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "IsClosed",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "JobDuration",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "LocalStartDateTime",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "TotalChargedValue",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "UtcStartDateTime",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Admins");

            migrationBuilder.DropColumn(
                name: "IsOwner",
                table: "Admins");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Staffs",
                newName: "Lastname");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Staffs",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Jobs",
                newName: "JobRef");

            migrationBuilder.RenameColumn(
                name: "StartDateTimeZoneLocation",
                table: "Jobs",
                newName: "EventName");

            migrationBuilder.RenameColumn(
                name: "MaxStaffNumber",
                table: "Jobs",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "JobNumber",
                table: "Jobs",
                newName: "ChargedAmount");

            migrationBuilder.RenameColumn(
                name: "Event",
                table: "Jobs",
                newName: "ClientName");

            migrationBuilder.RenameColumn(
                name: "AllowedForJobStaffIds",
                table: "Jobs",
                newName: "Notes");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Admins",
                newName: "Lastname");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Admins",
                newName: "Name");

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "Staffs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ContactInfoId",
                table: "Staffs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByAdminId",
                table: "Jobs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedById",
                table: "Jobs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ContactInfoId",
                table: "Companies",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WebsiteUrl",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CompanyId",
                table: "Admins",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ContactInfoId",
                table: "Admins",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Admins",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ContactInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhotoLogoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressLine1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressLine2 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExtraRateAmountEntries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtraRateAmountEntries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RatePolicies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    overtime_start_minutes = table.Column<int>(type: "int", nullable: false),
                    overtime_multiplier = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    day_multiplier = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    travel_time_rate = table.Column<int>(type: "int", nullable: false),
                    kilometers_rate = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatePolicies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StaffJobAllowances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StaffJobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExtraRateAmountEntryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffJobAllowances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupervisorStaffId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SupervisorAdminId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teams_Admins_SupervisorAdminId",
                        column: x => x.SupervisorAdminId,
                        principalTable: "Admins",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Teams_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Teams_Staffs_SupervisorStaffId",
                        column: x => x.SupervisorStaffId,
                        principalTable: "Staffs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Target = table.Column<int>(type: "int", nullable: false),
                    JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SupervisorStaffId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SupervisorAdminId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SentById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SentByType = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Requests_Admins_SupervisorAdminId",
                        column: x => x.SupervisorAdminId,
                        principalTable: "Admins",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Requests_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Requests_Staffs_SupervisorStaffId",
                        column: x => x.SupervisorStaffId,
                        principalTable: "Staffs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Requests_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StaffJobs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StaffId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinishTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseRate = table.Column<int>(type: "int", nullable: false),
                    TravelTimeMinutes = table.Column<int>(type: "int", nullable: false),
                    Kilometers = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalAmount = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPersonalJob = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffJobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StaffJobs_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StaffJobs_Staffs_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StaffJobs_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StaffTeams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StaffId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffTeams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StaffTeams_Staffs_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StaffTeams_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_CompanyId",
                table: "Staffs",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_ContactInfoId",
                table: "Staffs",
                column: "ContactInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_CompanyId",
                table: "Jobs",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_CreatedById",
                table: "Jobs",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_ContactInfoId",
                table: "Companies",
                column: "ContactInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Admins_ContactInfoId",
                table: "Admins",
                column: "ContactInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_JobId",
                table: "Requests",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_SupervisorAdminId",
                table: "Requests",
                column: "SupervisorAdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_SupervisorStaffId",
                table: "Requests",
                column: "SupervisorStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_TeamId",
                table: "Requests",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffJobs_JobId",
                table: "StaffJobs",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffJobs_StaffId",
                table: "StaffJobs",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffJobs_TeamId",
                table: "StaffJobs",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffTeams_StaffId",
                table: "StaffTeams",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffTeams_TeamId",
                table: "StaffTeams",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_JobId",
                table: "Teams",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_SupervisorAdminId",
                table: "Teams",
                column: "SupervisorAdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_SupervisorStaffId",
                table: "Teams",
                column: "SupervisorStaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_Admins_Companies_CompanyId",
                table: "Admins",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Admins_ContactInfos_ContactInfoId",
                table: "Admins",
                column: "ContactInfoId",
                principalTable: "ContactInfos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_ContactInfos_ContactInfoId",
                table: "Companies",
                column: "ContactInfoId",
                principalTable: "ContactInfos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Admins_CreatedById",
                table: "Jobs",
                column: "CreatedById",
                principalTable: "Admins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Companies_CompanyId",
                table: "Jobs",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Staffs_Companies_CompanyId",
                table: "Staffs",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Staffs_ContactInfos_ContactInfoId",
                table: "Staffs",
                column: "ContactInfoId",
                principalTable: "ContactInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admins_Companies_CompanyId",
                table: "Admins");

            migrationBuilder.DropForeignKey(
                name: "FK_Admins_ContactInfos_ContactInfoId",
                table: "Admins");

            migrationBuilder.DropForeignKey(
                name: "FK_Companies_ContactInfos_ContactInfoId",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Admins_CreatedById",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Companies_CompanyId",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_Staffs_Companies_CompanyId",
                table: "Staffs");

            migrationBuilder.DropForeignKey(
                name: "FK_Staffs_ContactInfos_ContactInfoId",
                table: "Staffs");

            migrationBuilder.DropTable(
                name: "ContactInfos");

            migrationBuilder.DropTable(
                name: "ExtraRateAmountEntries");

            migrationBuilder.DropTable(
                name: "RatePolicies");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropTable(
                name: "StaffJobAllowances");

            migrationBuilder.DropTable(
                name: "StaffJobs");

            migrationBuilder.DropTable(
                name: "StaffTeams");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Staffs_CompanyId",
                table: "Staffs");

            migrationBuilder.DropIndex(
                name: "IX_Staffs_ContactInfoId",
                table: "Staffs");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_CompanyId",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_CreatedById",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Companies_ContactInfoId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Admins_ContactInfoId",
                table: "Admins");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "ContactInfoId",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "CreatedByAdminId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "ContactInfoId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "WebsiteUrl",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "ContactInfoId",
                table: "Admins");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Admins");

            migrationBuilder.RenameColumn(
                name: "Lastname",
                table: "Staffs",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Staffs",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Jobs",
                newName: "MaxStaffNumber");

            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "Jobs",
                newName: "AllowedForJobStaffIds");

            migrationBuilder.RenameColumn(
                name: "JobRef",
                table: "Jobs",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "EventName",
                table: "Jobs",
                newName: "StartDateTimeZoneLocation");

            migrationBuilder.RenameColumn(
                name: "ClientName",
                table: "Jobs",
                newName: "Event");

            migrationBuilder.RenameColumn(
                name: "ChargedAmount",
                table: "Jobs",
                newName: "JobNumber");

            migrationBuilder.RenameColumn(
                name: "Lastname",
                table: "Admins",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Admins",
                newName: "Phone");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Staffs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "AcceptingReqs",
                table: "Jobs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<float>(
                name: "BaseRate",
                table: "Jobs",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "Client",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.AddColumn<float>(
                name: "JobDuration",
                table: "Jobs",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LocalStartDateTime",
                table: "Jobs",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "TotalChargedValue",
                table: "Jobs",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UtcStartDateTime",
                table: "Jobs",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AlterColumn<Guid>(
                name: "CompanyId",
                table: "Admins",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Admins",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsOwner",
                table: "Admins",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "JobRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdminId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StaffId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResponsedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobRequests_Admins_AdminId",
                        column: x => x.AdminId,
                        principalTable: "Admins",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JobRequests_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobRequests_Staffs_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobStaffs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StaffId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddRate = table.Column<float>(type: "real", nullable: false),
                    AddStartDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobRole = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobStaffs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobStaffs_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobStaffs_Staffs_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobRequests_AdminId",
                table: "JobRequests",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_JobRequests_JobId",
                table: "JobRequests",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_JobRequests_StaffId",
                table: "JobRequests",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_JobStaffs_JobId",
                table: "JobStaffs",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_JobStaffs_StaffId",
                table: "JobStaffs",
                column: "StaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_Admins_Companies_CompanyId",
                table: "Admins",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");
        }
    }
}
