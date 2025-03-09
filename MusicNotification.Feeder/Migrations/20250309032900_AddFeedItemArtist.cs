using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicNotification.Feeder.Migrations
{
    /// <inheritdoc />
    public partial class AddFeedItemArtist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "artist",
                table: "feed_item",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "artist",
                table: "feed_item");
        }
    }
}
