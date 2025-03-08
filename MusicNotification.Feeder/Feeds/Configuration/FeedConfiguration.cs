using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicNotification.Common.Configuration;
using MusicNotification.Feeder.Feeds.Domain;

namespace MusicNotification.Feeder.Feeds.Configuration;

internal class FeedConfiguration : BaseAudiatableEntityConfiguration<FeedEntity>
{
    public override void Configure(EntityTypeBuilder<FeedEntity> builder)
    {
        builder.HasMany(x => x.Items)
            .WithOne(x => x.Feed)
            .HasForeignKey(x => x.FeedId)
            .OnDelete(DeleteBehavior.Cascade);

        base.Configure(builder);
    }
}
