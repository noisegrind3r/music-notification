using MusicNotification.Feeder.FeedParser.FeedContentParser;
using MusicNotification.Feeder.Feeds.Domain;

namespace MusicNotification.Feeder.FeedParser;

public class FeedProcessor(IFeedContentParserFabric contentParserFabric): IFeedProcessor
{

    public async Task<IEnumerable<FeedItemEntity>> Process(FeedEntity feed, CancellationToken cancellationToken = default)
    {
        if (feed.Url is null)
            return [];

        var data = await contentParserFabric.Parse(feed.Url, feed.Type ?? FeedType.Metalarea, cancellationToken);

        return data.Where(x => !feed.Items.Any(y => y.Uid == x.Uid))?.Select(item => new FeedItemEntity
        {
            Content = item.Content,
            Feed = feed,
            Title = item.Title,
            Uid = item.Uid,
        })?.ToList() ?? [];
    }
}
