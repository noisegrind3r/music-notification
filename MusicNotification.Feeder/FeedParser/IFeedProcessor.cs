using MusicNotification.Feeder.Feeds.Domain;

namespace MusicNotification.Feeder.FeedParser;

public interface IFeedProcessor
{
    Task<IEnumerable<FeedData>> Process(FeedEntity feed, CancellationToken cancellationToken = default);
}
