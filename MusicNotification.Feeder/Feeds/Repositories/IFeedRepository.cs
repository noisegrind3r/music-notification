using MusicNotification.Common.Interfaces;
using MusicNotification.Feeder.Feeds.Domain;

namespace MusicNotification.Feeder.Feeds.Repositories;

public class FeedQueryOptions
{
    public bool IncludeItems { get; set; }
}

public interface IFeedRepository : IRepository<FeedEntity>
{
    IQueryable<FeedEntity> Get(FeedQueryOptions options);
}
