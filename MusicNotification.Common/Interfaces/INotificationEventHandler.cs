using MediatR;

namespace MusicNotification.Common.Interfaces;

public interface INotificationEventHandler<TEvent> : INotificationHandler<TEvent> where TEvent : INotificationEvent
{
}
