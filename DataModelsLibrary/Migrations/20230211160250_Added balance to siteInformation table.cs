using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataModelsLibrary.Migrations
{
    public partial class AddedbalancetositeInformationtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResellerVendorBalance");

            migrationBuilder.AddColumn<int>(
                name: "Balance",
                table: "SiteInformation",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Balance",
                table: "SiteInformation");

            migrationBuilder.CreateTable(
                name: "ResellerVendorBalance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrganizationId = table.Column<int>(type: "integer", nullable: false),
                    ResellerId = table.Column<int>(type: "integer", nullable: false),
                    VendorId = table.Column<int>(type: "integer", nullable: false),
                    Balance = table.Column<int>(type: "integer", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResellerVendorBalance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResellerVendorBalance_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResellerVendorBalance_Reseller_ResellerId",
                        column: x => x.ResellerId,
                        principalTable: "Reseller",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResellerVendorBalance_Vendor_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ResellerVendorBalance_OrganizationId",
                table: "ResellerVendorBalance",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_ResellerVendorBalance_ResellerId",
                table: "ResellerVendorBalance",
                column: "ResellerId");

            migrationBuilder.CreateIndex(
                name: "IX_ResellerVendorBalance_VendorId",
                table: "ResellerVendorBalance",
                column: "VendorId");
        }
    }
}
