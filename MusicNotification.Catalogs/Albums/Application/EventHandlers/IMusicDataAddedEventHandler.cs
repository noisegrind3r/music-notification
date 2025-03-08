using MusicNotification.Common.Interfaces;
using MusicNotification.Events.Events;

namespace MusicNotification.Catalogs.Albums.Application.EventHandlers;

public interface IMusicDataAddedEventHandler : INotificationEventHandler<MusicDataAddedEvent>
{
}
