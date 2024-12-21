using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fastaffo_api.src.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameJobProps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Staffs",
                table: "Jobs",
                newName: "StaffsId");

            migrationBuilder.RenameColumn(
                name: "Company",
                table: "Jobs",
                newName: "CompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StaffsId",
                table: "Jobs",
                newName: "Staffs");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "Jobs",
                newName: "Company");
        }
    }
}
