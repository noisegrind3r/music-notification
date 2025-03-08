using MusicNotification.Feeder.Feeds.Domain;

namespace MusicNotification.Feeder.FeedParser.FeedContentParser;

public interface IFeedContentParser
{
    Task<IEnumerable<FeedData>?>? Parse(string url, CancellationToken cancellationToken = default);
}
