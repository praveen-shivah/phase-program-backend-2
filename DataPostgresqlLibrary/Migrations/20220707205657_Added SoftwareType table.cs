using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataPostgresqlLibrary.Migrations
{
    public partial class AddedSoftwareTypetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SoftwareType",
                table: "Vendor",
                newName: "SoftwareTypeId");

            migrationBuilder.CreateTable(
                name: "SoftwareType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoftwareType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_SoftwareTypeId",
                table: "Vendor",
                column: "SoftwareTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendor_SoftwareType_SoftwareTypeId",
                table: "Vendor",
                column: "SoftwareTypeId",
                principalTable: "SoftwareType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendor_SoftwareType_SoftwareTypeId",
                table: "Vendor");

            migrationBuilder.DropTable(
                name: "SoftwareType");

            migrationBuilder.DropIndex(
                name: "IX_Vendor_SoftwareTypeId",
                table: "Vendor");

            migrationBuilder.RenameColumn(
                name: "SoftwareTypeId",
                table: "Vendor",
                newName: "SoftwareType");
        }
    }
}
