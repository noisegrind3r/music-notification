using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicNotification.Common.Configuration;
using MusicNotification.Feeder.Feeds.Domain;

namespace MusicNotification.Feeder.Feeds.Configuration;

internal class FeedItemConfiguration : BaseAudiatableEntityConfiguration<FeedItemEntity>
{
    public override void Configure(EntityTypeBuilder<FeedItemEntity> builder)
    {
        builder.HasIndex(x => new { x.FeedId, x.Uid });
        base.Configure(builder);
    }
}
