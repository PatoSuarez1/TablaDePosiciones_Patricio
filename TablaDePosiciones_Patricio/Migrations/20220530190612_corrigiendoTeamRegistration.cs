using Microsoft.EntityFrameworkCore.Migrations;

namespace TablaDePosiciones_Patricio.Migrations
{
    public partial class corrigiendoTeamRegistration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Position",
                table: "TeamRegistration");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "TeamRegistration",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
