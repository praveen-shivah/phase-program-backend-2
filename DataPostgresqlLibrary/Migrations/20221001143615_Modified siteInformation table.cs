using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataPostgresqlLibrary.Migrations
{
    public partial class ModifiedsiteInformationtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VendorCredentialsByOrganization_Organization_OrganizationId",
                table: "VendorCredentialsByOrganization");

            migrationBuilder.DropForeignKey(
                name: "FK_VendorCredentialsByOrganization_Vendor_VendorId",
                table: "VendorCredentialsByOrganization");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VendorCredentialsByOrganization",
                table: "VendorCredentialsByOrganization");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "SiteInformation");

            migrationBuilder.RenameTable(
                name: "VendorCredentialsByOrganization",
                newName: "VendorCredentialsByOrganizations");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "SiteInformation",
                newName: "AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_VendorCredentialsByOrganization_VendorId",
                table: "VendorCredentialsByOrganizations",
                newName: "IX_VendorCredentialsByOrganizations_VendorId");

            migrationBuilder.RenameIndex(
                name: "IX_VendorCredentialsByOrganization_OrganizationId",
                table: "VendorCredentialsByOrganizations",
                newName: "IX_VendorCredentialsByOrganizations_OrganizationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VendorCredentialsByOrganizations",
                table: "VendorCredentialsByOrganizations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VendorCredentialsByOrganizations_Organization_OrganizationId",
                table: "VendorCredentialsByOrganizations",
                column: "OrganizationId",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VendorCredentialsByOrganizations_Vendor_VendorId",
                table: "VendorCredentialsByOrganizations",
                column: "VendorId",
                principalTable: "Vendor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VendorCredentialsByOrganizations_Organization_OrganizationId",
                table: "VendorCredentialsByOrganizations");

            migrationBuilder.DropForeignKey(
                name: "FK_VendorCredentialsByOrganizations_Vendor_VendorId",
                table: "VendorCredentialsByOrganizations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VendorCredentialsByOrganizations",
                table: "VendorCredentialsByOrganizations");

            migrationBuilder.RenameTable(
                name: "VendorCredentialsByOrganizations",
                newName: "VendorCredentialsByOrganization");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "SiteInformation",
                newName: "UserName");

            migrationBuilder.RenameIndex(
                name: "IX_VendorCredentialsByOrganizations_VendorId",
                table: "VendorCredentialsByOrganization",
                newName: "IX_VendorCredentialsByOrganization_VendorId");

            migrationBuilder.RenameIndex(
                name: "IX_VendorCredentialsByOrganizations_OrganizationId",
                table: "VendorCredentialsByOrganization",
                newName: "IX_VendorCredentialsByOrganization_OrganizationId");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "SiteInformation",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VendorCredentialsByOrganization",
                table: "VendorCredentialsByOrganization",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VendorCredentialsByOrganization_Organization_OrganizationId",
                table: "VendorCredentialsByOrganization",
                column: "OrganizationId",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VendorCredentialsByOrganization_Vendor_VendorId",
                table: "VendorCredentialsByOrganization",
                column: "VendorId",
                principalTable: "Vendor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
