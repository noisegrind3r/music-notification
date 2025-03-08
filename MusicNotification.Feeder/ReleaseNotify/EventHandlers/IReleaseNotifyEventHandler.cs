using MusicNotification.Common.Interfaces;
using MusicNotification.Events.Events;

namespace MusicNotification.Feeder.ReleaseNotify.EventHandlers;

public interface IReleaseNotifyEventHandler: INotificationEventHandler<ReleaseNotifyEvent>
{
}
