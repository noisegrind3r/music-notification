using MusicNotification.Common.Interfaces;
using MusicNotification.Feeder.Feeds.Domain;

namespace MusicNotification.Feeder.Feeds.Repositories;

public class FeedQueryOptions
{
    public bool IncludeItems { get; set; }
}

public interface IFeedRepository : IRepository<FeedEntity>
{
    Task<bool> FeedItemEntityExistByFeedIdAndUid(int feedId, string uid);
    IQueryable<FeedEntity> Get(FeedQueryOptions options);
}
