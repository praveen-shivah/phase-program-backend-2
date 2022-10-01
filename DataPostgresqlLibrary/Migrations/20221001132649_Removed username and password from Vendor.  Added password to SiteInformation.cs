using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataPostgresqlLibrary.Migrations
{
    public partial class RemovedusernameandpasswordfromVendorAddedpasswordtoSiteInformation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Vendor");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "SiteInformation",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "SiteInformation");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Vendor",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Vendor",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
