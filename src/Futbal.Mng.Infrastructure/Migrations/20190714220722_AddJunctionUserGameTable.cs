using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Futbal.Mng.Infrastructure.Migrations
{
    public partial class AddJunctionUserGameTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_games_GameId",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_GameId",
                table: "users");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "users");

            migrationBuilder.CreateTable(
                name: "UserGame",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    GameId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGame", x => new { x.UserId, x.GameId });
                    table.ForeignKey(
                        name: "FK_UserGame_games_GameId",
                        column: x => x.GameId,
                        principalTable: "games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserGame_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserGame_GameId",
                table: "UserGame",
                column: "GameId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserGame");

            migrationBuilder.AddColumn<Guid>(
                name: "GameId",
                table: "users",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_GameId",
                table: "users",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_users_games_GameId",
                table: "users",
                column: "GameId",
                principalTable: "games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
