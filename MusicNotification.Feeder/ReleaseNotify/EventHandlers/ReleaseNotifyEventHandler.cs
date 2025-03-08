using MusicNotification.Events.Events;

namespace MusicNotification.Feeder.ReleaseNotify.EventHandlers;

internal class ReleaseNotifyEventHandler(IReleaseNotification releaseNotification) : IReleaseNotifyEventHandler
{
    public async Task Handle(ReleaseNotifyEvent notification, CancellationToken cancellationToken)
    {
        await releaseNotification.ProcessAndNotificateAllFeeds();
    }
}
