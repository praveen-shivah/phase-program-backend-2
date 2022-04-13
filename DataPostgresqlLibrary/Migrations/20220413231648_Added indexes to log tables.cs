using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataPostgresqlLibrary.Migrations
{
    public partial class Addedindexestologtables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "SignificantEvent",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "LOCALTIMESTAMP AT TIME ZONE 'UTC'",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "ErrorLog",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "LOCALTIMESTAMP AT TIME ZONE 'UTC'",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.CreateTable(
                name: "SignificantEventType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SignificantEventType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SignificantEvent_EventId_CreatedOn",
                table: "SignificantEvent",
                columns: new[] { "Id", "ShortDescription", "CreatedBy", "EventTypeId", "CreatedOn" });

            migrationBuilder.CreateIndex(
                name: "IX_ErrorLog_Hash",
                table: "ErrorLog",
                columns: new[] { "Hash", "CreatedOn" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SignificantEventType");

            migrationBuilder.DropIndex(
                name: "IX_SignificantEvent_EventId_CreatedOn",
                table: "SignificantEvent");

            migrationBuilder.DropIndex(
                name: "IX_ErrorLog_Hash",
                table: "ErrorLog");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "SignificantEvent",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "LOCALTIMESTAMP AT TIME ZONE 'UTC'");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "ErrorLog",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "LOCALTIMESTAMP AT TIME ZONE 'UTC'");
        }
    }
}
