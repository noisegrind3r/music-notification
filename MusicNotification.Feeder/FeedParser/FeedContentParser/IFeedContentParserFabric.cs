using MusicNotification.Feeder.Feeds.Domain;

namespace MusicNotification.Feeder.FeedParser.FeedContentParser;

public interface IFeedContentParserFabric
{
    Task<IEnumerable<FeedData>> Parse(string url, FeedType type, CancellationToken cancellationToken = default);
}
