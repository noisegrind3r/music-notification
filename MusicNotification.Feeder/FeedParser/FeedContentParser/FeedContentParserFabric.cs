using MusicNotification.Feeder.FeedParser.FeedContentParser.Implementation;
using MusicNotification.Feeder.Feeds.Domain;

namespace MusicNotification.Feeder.FeedParser.FeedContentParser;

public class FeedContentParserFabric : IFeedContentParserFabric
{
    public async Task<IEnumerable<FeedData>> Parse(string url, FeedType type, CancellationToken cancellationToken = default)
    {
        IFeedContentParser contentParser = type switch
        {
            FeedType.Metalarea => new MetalareaContentParser(),
            FeedType.Rutracker => new RutrackerContentParser(),
            _ => throw new NotImplementedException()
        };
        return (await contentParser.Parse(url, cancellationToken) ?? []);
    }
}
