using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataModelsLibrary.Migrations
{
    public partial class AddedLoginUsernameandLoginPasswordtoSiteInformationtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LoginPassword",
                table: "SiteInformation",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValueSql: "''::character varying");

            migrationBuilder.AddColumn<string>(
                name: "LoginUsername",
                table: "SiteInformation",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValueSql: "''::character varying");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoginPassword",
                table: "SiteInformation");

            migrationBuilder.DropColumn(
                name: "LoginUsername",
                table: "SiteInformation");
        }
    }
}
