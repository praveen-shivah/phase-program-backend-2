using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataModelsLibrary.Migrations
{
    public partial class PlayersFieldsUpdateMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LoginUsername",
                table: "Players",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "LoginPassword",
                table: "Players",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Players",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Mail",
                table: "Players",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MobileId",
                table: "Players",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Players",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Players",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PlayerId",
                table: "Players",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Mail",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "MobileId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "Players");

            migrationBuilder.AlterColumn<int>(
                name: "LoginUsername",
                table: "Players",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "LoginPassword",
                table: "Players",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
