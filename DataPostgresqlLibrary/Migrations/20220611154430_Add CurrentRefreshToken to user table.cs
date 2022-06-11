using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataPostgresqlLibrary.Migrations
{
    public partial class AddCurrentRefreshTokentousertable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CurrentRefreshToken",
                table: "User",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentRefreshToken",
                table: "User");
        }
    }
}
