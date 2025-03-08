using MusicNotification.Feeder.Feeds.Domain;

namespace MusicNotification.Feeder.FeedParser.FeedContentParser;

public interface IFeedContentParser
{
    Task<List<FeedData>> Parse(string url, CancellationToken cancellationToken = default);
}
