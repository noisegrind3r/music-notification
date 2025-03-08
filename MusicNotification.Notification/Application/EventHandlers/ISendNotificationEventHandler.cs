using MusicNotification.Common.Interfaces;
using MusicNotification.Events.Events;

namespace MusicNotification.Notification.Application.EventHandlers;

internal interface ISendNotificationEventHandler : INotificationEventHandler<SendNotificationEvent>
{
}
