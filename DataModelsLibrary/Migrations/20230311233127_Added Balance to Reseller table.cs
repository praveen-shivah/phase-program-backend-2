using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataModelsLibrary.Migrations
{
    public partial class AddedBalancetoResellertable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Balance",
                table: "Reseller",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Balance",
                table: "Reseller");
        }
    }
}
