using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicNotification.Catalogs.Artists.Domain;
using MusicNotification.Common.Configuration;

namespace MusicNotification.Catalogs.Artists.Configuration;

internal class ArtistConfiguration : BaseAudiatableEntityConfiguration<ArtistEntity>
{
    public override void Configure(EntityTypeBuilder<ArtistEntity> builder)
    {
        builder.HasOne(x => x.Country)
            .WithMany(x => x.Artists)
            .HasForeignKey(x => x.CountryId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(x => new { x.Name, x.CountryId });

        base.Configure(builder);
    }
}
