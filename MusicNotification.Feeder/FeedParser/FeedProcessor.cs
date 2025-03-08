using MusicNotification.Feeder.FeedParser.FeedContentParser;
using MusicNotification.Feeder.Feeds.Domain;
using MusicNotification.Feeder.Feeds.Repositories;

namespace MusicNotification.Feeder.FeedParser;

public class FeedProcessor(IFeedContentParserFabric contentParserFabric, IFeedRepository feedRepository): IFeedProcessor
{

    public async Task<IEnumerable<FeedData>> Process(FeedEntity feed, CancellationToken cancellationToken = default)
    {
        if (feed.Url is null)
            return [];

        var data = await contentParserFabric.Parse(feed.Url, feed.Type ?? FeedType.Metalarea, cancellationToken);

        var result = new List<FeedData>();
        foreach (var item in data)
        {
            if (string.IsNullOrEmpty(item.Uid))
                continue;

            var isProcessed = await feedRepository.FeedItemEntityExistByFeedIdAndUid(feed.Id, item.Uid);
            if (isProcessed) continue;

            result.Add(item);
        }

        return result;
    }
}
