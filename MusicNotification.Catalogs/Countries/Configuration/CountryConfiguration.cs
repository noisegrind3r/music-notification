using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicNotification.Catalogs.Countries.Domain;
using MusicNotification.Common.Configuration;

namespace MusicNotification.Catalogs.Countries.Configuration;

internal class CountryConfiguration : BaseAudiatableEntityConfiguration<CountryEntity>
{
    public override void Configure(EntityTypeBuilder<CountryEntity> builder)
    {
        builder.HasIndex(x => new { x.Name });

        base.Configure(builder);
    }
}