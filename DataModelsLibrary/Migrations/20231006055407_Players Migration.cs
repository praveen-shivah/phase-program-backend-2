using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataModelsLibrary.Migrations
{
    public partial class PlayersMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayersInformation_Organization_OrganizationId",
                table: "PlayersInformation");

            migrationBuilder.DropIndex(
                name: "IX_PlayersInformation_OrganizationId",
                table: "PlayersInformation");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "PlayersInformation");

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrganizationId = table.Column<int>(type: "integer", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ResellerId = table.Column<int>(type: "integer", nullable: false),
                    VendorId = table.Column<int>(type: "integer", nullable: false),
                    LoginUsername = table.Column<int>(type: "integer", nullable: false),
                    LoginPassword = table.Column<int>(type: "integer", nullable: false),
                    Balance = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Players_Reseller_ResellerId",
                        column: x => x.ResellerId,
                        principalTable: "Reseller",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Players_Vendor_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Players_OrganizationId",
                table: "Players",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_ResellerId",
                table: "Players",
                column: "ResellerId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_VendorId",
                table: "Players",
                column: "VendorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "PlayersInformation",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PlayersInformation_OrganizationId",
                table: "PlayersInformation",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayersInformation_Organization_OrganizationId",
                table: "PlayersInformation",
                column: "OrganizationId",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
