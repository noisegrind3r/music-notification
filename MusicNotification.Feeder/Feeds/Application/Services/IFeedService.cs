using MusicNotification.Common.Interfaces;
using MusicNotification.Feeder.FeedParser;
using MusicNotification.Feeder.Feeds.Application.Dtos;
using MusicNotification.Feeder.Feeds.Domain;

namespace MusicNotification.Feeder.Feeds.Application.Services;

public interface IFeedService : IBaseService<FeedEntity, FeedQueryDto, FeedCommandDto>
{
    Task<List<FeedEntity>> GetAllActiveFeeds();
    Task<string> ProcessFeedById(int id, CancellationToken cancellationToken = default);
    Task<string> ProcessAllActiveFeeds(CancellationToken cancellationToken = default);
    Task WriteFeedDataToFeedItems(FeedEntity feedEntity, IEnumerable<FeedData> feedData, CancellationToken cancellationToken = default);
    Task ReleaseNotification();
}
