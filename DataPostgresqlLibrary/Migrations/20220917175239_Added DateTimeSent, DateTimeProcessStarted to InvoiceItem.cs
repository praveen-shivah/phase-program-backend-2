using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataPostgresqlLibrary.Migrations
{
    public partial class AddedDateTimeSentDateTimeProcessStartedtoInvoiceItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTimeProcessStarted",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "DateTimeSent",
                table: "Invoice");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTimeProcessStarted",
                table: "InvoiceLineItem",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTimeSent",
                table: "InvoiceLineItem",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TransferPointsQueue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountId = table.Column<string>(type: "text", nullable: false),
                    APIKey = table.Column<string>(type: "text", nullable: false),
                    InvoiceLineItemId = table.Column<int>(type: "integer", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Points = table.Column<int>(type: "integer", nullable: false),
                    SoftwareType = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    DateTimeProcessStarted = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DateTimeSent = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    OrganizationId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferPointsQueue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransferPointsQueue_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransferPointsQueue_OrganizationId",
                table: "TransferPointsQueue",
                column: "OrganizationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransferPointsQueue");

            migrationBuilder.DropColumn(
                name: "DateTimeProcessStarted",
                table: "InvoiceLineItem");

            migrationBuilder.DropColumn(
                name: "DateTimeSent",
                table: "InvoiceLineItem");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTimeProcessStarted",
                table: "Invoice",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTimeSent",
                table: "Invoice",
                type: "timestamp with time zone",
                nullable: true);
        }
    }
}
