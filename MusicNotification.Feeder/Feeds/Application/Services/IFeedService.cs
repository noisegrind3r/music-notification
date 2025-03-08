using MusicNotification.Common.Interfaces;
using MusicNotification.Feeder.Feeds.Application.Dtos;
using MusicNotification.Feeder.Feeds.Domain;

namespace MusicNotification.Feeder.Feeds.Application.Services;

public interface IFeedService : IBaseService<FeedEntity, FeedQueryDto, FeedCommandDto>
{
    Task<string> ProcessFeedById(int id, CancellationToken cancellationToken = default);
    Task<string> ProcessAllActiveFeeds(int id, CancellationToken cancellationToken = default);
}
