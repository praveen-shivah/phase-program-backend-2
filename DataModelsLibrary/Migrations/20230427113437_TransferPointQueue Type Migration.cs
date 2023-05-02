using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataModelsLibrary.Migrations
{
    public partial class TransferPointQueueTypeMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TransferPointsQueueTypeId",
                table: "TransferPointsQueue",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TransferPointsQueueType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferPointsQueueType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransferPointsQueue_TransferPointsQueueTypeId",
                table: "TransferPointsQueue",
                column: "TransferPointsQueueTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransferPointsQueue_TransferPointsType_TransferPointsTypeId",
                table: "TransferPointsQueue",
                column: "TransferPointsQueueTypeId",
                principalTable: "TransferPointsQueueType",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransferPointsQueue_TransferPointsType_TransferPointsTypeId",
                table: "TransferPointsQueue");

            migrationBuilder.DropTable(
                name: "TransferPointsQueueType");

            migrationBuilder.DropIndex(
                name: "IX_TransferPointsQueue_TransferPointsQueueTypeId",
                table: "TransferPointsQueue");

            migrationBuilder.DropColumn(
                name: "TransferPointsQueueTypeId",
                table: "TransferPointsQueue");
        }
    }
}
