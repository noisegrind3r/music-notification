using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicNotification.Catalogs.Genries.Domain;
using MusicNotification.Common.Configuration;

namespace MusicNotification.Catalogs.Genries.Configuration;

internal class GenreConfiguration : BaseAudiatableEntityConfiguration<GenreEntity>
{
    public override void Configure(EntityTypeBuilder<GenreEntity> builder)
    {
        builder.HasIndex(x => new { x.Name });

        base.Configure(builder);
    }
}
