using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataPostgresqlLibrary.Migrations
{
    public partial class Modifiedlineitemaddedsoftwaretype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SoftwareType",
                table: "InvoiceLineItem",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SoftwareType",
                table: "InvoiceLineItem");
        }
    }
}
