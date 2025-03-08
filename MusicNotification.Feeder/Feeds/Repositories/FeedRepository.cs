using Microsoft.EntityFrameworkCore;
using MusicNotification.Common.Repositories;
using MusicNotification.Feeder.Feeds.Domain;

namespace MusicNotification.Feeder.Feeds.Repositories;

public class FeedRepository(FeederDbContext dbContext) : BaseRepository<FeederDbContext, FeedEntity>(dbContext), IFeedRepository
{
    public IQueryable<FeedEntity> Get(FeedQueryOptions options)
    {
        var query = GetAll();

        if (options.IncludeItems)
        {
            query = query.Include(x => x.Items);
        }

        return query;
    }

    public async Task<bool> FeedItemEntityExistByFeedIdAndUid(int feedId, string uid)
    {
        return await GetAll().Include(x => x.Items).AnyAsync(x => x.Id == feedId && x.Items.Any(y => y.Uid == uid));
    }
}
