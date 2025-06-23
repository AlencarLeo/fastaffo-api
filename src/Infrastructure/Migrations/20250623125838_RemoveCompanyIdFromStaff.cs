using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fastaffo_api.src.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCompanyIdFromStaff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Staffs_Companies_CompanyId",
                table: "Staffs");

            migrationBuilder.DropForeignKey(
                name: "FK_Staffs_ContactInfos_ContactInfoId",
                table: "Staffs");

            migrationBuilder.DropIndex(
                name: "IX_Staffs_CompanyId",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Staffs");

            migrationBuilder.AlterColumn<Guid>(
                name: "ContactInfoId",
                table: "Staffs",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Staffs_ContactInfos_ContactInfoId",
                table: "Staffs",
                column: "ContactInfoId",
                principalTable: "ContactInfos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Staffs_ContactInfos_ContactInfoId",
                table: "Staffs");

            migrationBuilder.AlterColumn<Guid>(
                name: "ContactInfoId",
                table: "Staffs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "Staffs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_CompanyId",
                table: "Staffs",
                column: "CompanyId");

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
    }
}
