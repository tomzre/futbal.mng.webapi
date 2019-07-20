using Microsoft.EntityFrameworkCore.Migrations;

namespace Futbal.Mng.Infrastructure.Migrations
{
    public partial class AddUserIsAvailabe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "users",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "users");
        }
    }
}
