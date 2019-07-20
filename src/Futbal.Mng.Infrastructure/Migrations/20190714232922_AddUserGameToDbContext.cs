using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Futbal.Mng.Infrastructure.Migrations
{
    public partial class AddUserGameToDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OccuringEvents");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OccuringEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    GameId = table.Column<Guid>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OccuringEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OccuringEvents_games_GameId",
                        column: x => x.GameId,
                        principalTable: "games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OccuringEvents_GameId",
                table: "OccuringEvents",
                column: "GameId");
        }
    }
}
