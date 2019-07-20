using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Futbal.Mng.Infrastructure.Migrations
{
    public partial class NewMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "address",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Street = table.Column<string>(nullable: true),
                    Number = table.Column<int>(nullable: false),
                    GameId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_address", x => x.Id);
                });

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
                });

            migrationBuilder.CreateTable(
                name: "Attendee",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    OccuringEventId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendee", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Attendee_occuring_event_OccuringEventId",
                        column: x => x.OccuringEventId,
                        principalTable: "occuring_event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    GameId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "game",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    OwnerId = table.Column<Guid>(nullable: true),
                    GameDate = table.Column<DateTime>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_game", x => x.Id);
                    table.ForeignKey(
                        name: "FK_game_users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_address_GameId",
                table: "address",
                column: "GameId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attendee_OccuringEventId",
                table: "Attendee",
                column: "OccuringEventId");

            migrationBuilder.CreateIndex(
                name: "IX_game_OwnerId",
                table: "game",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_occuring_event_GameId",
                table: "occuring_event",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_users_GameId",
                table: "users",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_address_game_GameId",
                table: "address",
                column: "GameId",
                principalTable: "game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_occuring_event_game_GameId",
                table: "occuring_event",
                column: "GameId",
                principalTable: "game",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_game_GameId",
                table: "users");

            migrationBuilder.DropTable(
                name: "address");

            migrationBuilder.DropTable(
                name: "Attendee");

            migrationBuilder.DropTable(
                name: "occuring_event");

            migrationBuilder.DropTable(
                name: "game");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
