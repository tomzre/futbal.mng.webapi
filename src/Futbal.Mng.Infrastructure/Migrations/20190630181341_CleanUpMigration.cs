using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Futbal.Mng.Infrastructure.Migrations
{
    public partial class CleanUpMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_address_game_GameId",
                table: "address");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendee_occuring_event_OccuringEventId",
                table: "Attendee");

            migrationBuilder.DropForeignKey(
                name: "FK_game_users_OwnerId",
                table: "game");

            migrationBuilder.DropForeignKey(
                name: "FK_users_game_GameId",
                table: "users");

            migrationBuilder.DropTable(
                name: "occuring_event");

            migrationBuilder.DropIndex(
                name: "IX_Attendee_OccuringEventId",
                table: "Attendee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_game",
                table: "game");

            migrationBuilder.DropColumn(
                name: "OccuringEventId",
                table: "Attendee");

            migrationBuilder.RenameTable(
                name: "game",
                newName: "games");

            migrationBuilder.RenameIndex(
                name: "IX_game_OwnerId",
                table: "games",
                newName: "IX_games_OwnerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_games",
                table: "games",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_address_games_GameId",
                table: "address",
                column: "GameId",
                principalTable: "games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_games_users_OwnerId",
                table: "games",
                column: "OwnerId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_users_games_GameId",
                table: "users",
                column: "GameId",
                principalTable: "games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_address_games_GameId",
                table: "address");

            migrationBuilder.DropForeignKey(
                name: "FK_games_users_OwnerId",
                table: "games");

            migrationBuilder.DropForeignKey(
                name: "FK_users_games_GameId",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_games",
                table: "games");

            migrationBuilder.RenameTable(
                name: "games",
                newName: "game");

            migrationBuilder.RenameIndex(
                name: "IX_games_OwnerId",
                table: "game",
                newName: "IX_game_OwnerId");

            migrationBuilder.AddColumn<Guid>(
                name: "OccuringEventId",
                table: "Attendee",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_game",
                table: "game",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "occuring_event",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    GameId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_occuring_event", x => x.Id);
                    table.ForeignKey(
                        name: "FK_occuring_event_game_GameId",
                        column: x => x.GameId,
                        principalTable: "game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendee_OccuringEventId",
                table: "Attendee",
                column: "OccuringEventId");

            migrationBuilder.CreateIndex(
                name: "IX_occuring_event_GameId",
                table: "occuring_event",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_address_game_GameId",
                table: "address",
                column: "GameId",
                principalTable: "game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Attendee_occuring_event_OccuringEventId",
                table: "Attendee",
                column: "OccuringEventId",
                principalTable: "occuring_event",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_game_users_OwnerId",
                table: "game",
                column: "OwnerId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_users_game_GameId",
                table: "users",
                column: "GameId",
                principalTable: "game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
