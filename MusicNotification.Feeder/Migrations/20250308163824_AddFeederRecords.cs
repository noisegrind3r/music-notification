using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicNotification.Feeder.Migrations
{
    /// <inheritdoc />
    public partial class AddFeederRecords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO feed (name, url, type, is_active, created_at, updated_at, deleted_at)
                VALUES
	                ('Metalarea Format Music', 'https://metalarea.org/forum/index.php?act=rssout&id=6', 0, true, now(), now(), '-infinity'),
	                ('Metalarea Lossless Music', 'https://metalarea.org/forum/index.php?act=rssout&id=4', 0, true, now(), now(), '-infinity'),
	                ('Metalarea Unformat Music', 'https://metalarea.org/forum/index.php?act=rssout&id=2', 0, true, now(), now(), '-infinity'),
	                ('Rutracker Death, Doom (lossless)', 'https://torapi.vercel.app/api/get/rss/rutracker?category=1779', 1, true, now(), now(), '-infinity'),
	                ('Rutracker Gothic Metal (lossless)', 'https://torapi.vercel.app/api/get/rss/rutracker?category=1724', 1, true, now(), now(), '-infinity'),
	                ('Folk, Pagan, Viking (lossless)', 'https://torapi.vercel.app/api/get/rss/rutracker?category=1720', 1, true, now(), now(), '-infinity'),
	                ('Black (lossless)', 'https://torapi.vercel.app/api/get/rss/rutracker?category=1719', 1, true, now(), now(), '-infinity'),
	                ('Grind, Brutal Death (lossless)', 'https://torapi.vercel.app/api/get/rss/rutracker?category=1730', 1, true, now(), now(), '-infinity'),
	                ('Heavy, Power, Progressive (lossless)', 'https://torapi.vercel.app/api/get/rss/rutracker?category=1726', 1, true, now(), now(), '-infinity'),
	                ('Sludge, Stoner, Post-Metal (lossless)', 'https://torapi.vercel.app/api/get/rss/rutracker?category=1815', 1, true, now(), now(), '-infinity'),
	                ('Thrash, Speed (lossless)', 'https://torapi.vercel.app/api/get/rss/rutracker?category=1728', 1, true, now(), now(), '-infinity'),
	                ('Darkwave, Neoclassical, Ethereal, Dungeon Synth (lossless)', 'https://torapi.vercel.app/api/get/rss/rutracker?category=1866', 1, true, now(), now(), '-infinity'),
	                ('Metal (lossless)', 'https://torapi.vercel.app/api/get/rss/rutracker?category=739', 1, true, now(), now(), '-infinity');
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
