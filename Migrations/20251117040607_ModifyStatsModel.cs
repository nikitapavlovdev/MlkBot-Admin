using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MlkAdmin.Migrations
{
    /// <inheritdoc />
    public partial class ModifyStatsModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Count",
                table: "Messages",
                newName: "TotalMessageCount");

            migrationBuilder.AddColumn<int>(
                name: "BadWordsAmount",
                table: "Messages",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CommandsAmount",
                table: "Messages",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GifsAmount",
                table: "Messages",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PicturesAmount",
                table: "Messages",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BadWordsAmount",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "CommandsAmount",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "GifsAmount",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "PicturesAmount",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "TotalMessageCount",
                table: "Messages",
                newName: "Count");
        }
    }
}
