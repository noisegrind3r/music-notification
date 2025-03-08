using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicNotification.Feeder.Migrations
{
    /// <inheritdoc />
    public partial class AddFeedItemLink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "link",
                table: "feed_item",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "link",
                table: "feed_item");
        }
    }
}
