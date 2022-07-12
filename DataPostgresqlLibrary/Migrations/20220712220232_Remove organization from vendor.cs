using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataPostgresqlLibrary.Migrations
{
    public partial class Removeorganizationfromvendor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendor_Organization_OrganizationId",
                table: "Vendor");

            migrationBuilder.DropIndex(
                name: "IX_Vendor_OrganizationId",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "Vendor");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "Vendor",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_OrganizationId",
                table: "Vendor",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendor_Organization_OrganizationId",
                table: "Vendor",
                column: "OrganizationId",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
