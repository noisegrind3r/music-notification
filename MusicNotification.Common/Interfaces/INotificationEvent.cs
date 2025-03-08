using MediatR;

namespace MusicNotification.Common.Interfaces;

public interface INotificationEvent : IEvent, INotification
{
}
