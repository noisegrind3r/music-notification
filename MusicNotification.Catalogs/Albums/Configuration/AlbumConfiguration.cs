using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicNotification.Catalogs.Albums.Domain;
using MusicNotification.Catalogs.Artists.Domain;
using MusicNotification.Common.Configuration;

namespace MusicNotification.Catalogs.Albums.Configuration;

internal class AlbumConfiguration : BaseAudiatableEntityConfiguration<AlbumEntity>
{
    public override void Configure(EntityTypeBuilder<AlbumEntity> builder)
    {
        builder.HasOne(x => x.Genre)
            .WithMany(x => x.Albums)
            .HasForeignKey(x => x.GenreId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(x => x.Artist)
            .WithMany(x => x.Albums)
            .HasForeignKey(x => x.ArtistId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(x => new { x.Name, x.Year, x.ArtistId });

        base.Configure(builder);
    }
}
