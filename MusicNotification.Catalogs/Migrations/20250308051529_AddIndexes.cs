using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicNotification.Catalogs.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_genre_name",
                table: "genre",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "IX_country_name",
                table: "country",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "IX_artist_name_country_id",
                table: "artist",
                columns: new[] { "name", "country_id" });

            migrationBuilder.CreateIndex(
                name: "IX_album_name_year_artist_id",
                table: "album",
                columns: new[] { "name", "year", "artist_id" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_genre_name",
                table: "genre");

            migrationBuilder.DropIndex(
                name: "IX_country_name",
                table: "country");

            migrationBuilder.DropIndex(
                name: "IX_artist_name_country_id",
                table: "artist");

            migrationBuilder.DropIndex(
                name: "IX_album_name_year_artist_id",
                table: "album");
        }
    }
}
