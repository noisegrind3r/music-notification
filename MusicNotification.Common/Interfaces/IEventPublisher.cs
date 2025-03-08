namespace MusicNotification.Common.Interfaces;

public interface IEventPublisher
{
    Task<TResult> SendRequestEvent<TEvent, TResult>(TEvent eventToPublish) where TEvent : IRequestEvent<TResult>;
    Task SendNotificationEvent<TEvent>(TEvent eventToPublish) where TEvent : INotificationEvent;
}
