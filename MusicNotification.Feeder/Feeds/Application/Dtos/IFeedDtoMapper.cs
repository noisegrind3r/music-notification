using MusicNotification.Common.Interfaces;
using MusicNotification.Feeder.Feeds.Domain;

namespace MusicNotification.Feeder.Feeds.Application.Dtos;

public interface IFeedDtoMapper : IDtoMapper<FeedEntity, FeedQueryDto, FeedCommandDto>
{
}
