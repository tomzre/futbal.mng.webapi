using Microsoft.EntityFrameworkCore.Migrations;

namespace Futbal.Mng.Infrastructure.Migrations
{
    public partial class rmoccuringevent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_occuring_event_games_GameId",
                table: "occuring_event");

            migrationBuilder.DropPrimaryKey(
                name: "PK_occuring_event",
                table: "occuring_event");

            migrationBuilder.RenameTable(
                name: "occuring_event",
                newName: "OccuringEvents");

            migrationBuilder.RenameIndex(
                name: "IX_occuring_event_GameId",
                table: "OccuringEvents",
                newName: "IX_OccuringEvents_GameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OccuringEvents",
                table: "OccuringEvents",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OccuringEvents_games_GameId",
                table: "OccuringEvents",
                column: "GameId",
                principalTable: "games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OccuringEvents_games_GameId",
                table: "OccuringEvents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OccuringEvents",
                table: "OccuringEvents");

            migrationBuilder.RenameTable(
                name: "OccuringEvents",
                newName: "occuring_event");

            migrationBuilder.RenameIndex(
                name: "IX_OccuringEvents_GameId",
                table: "occuring_event",
                newName: "IX_occuring_event_GameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_occuring_event",
                table: "occuring_event",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_occuring_event_games_GameId",
                table: "occuring_event",
                column: "GameId",
                principalTable: "games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
