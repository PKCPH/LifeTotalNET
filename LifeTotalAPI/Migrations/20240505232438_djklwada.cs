using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LifeTotalAPI.Migrations
{
    /// <inheritdoc />
    public partial class djklwada : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PlayerName",
                table: "GamematchPlayers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlayerName",
                table: "GamematchPlayers");
        }
    }
}
