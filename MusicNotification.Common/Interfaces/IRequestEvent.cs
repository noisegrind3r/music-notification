using MediatR;

namespace MusicNotification.Common.Interfaces;

public interface IRequestEvent<T> : IEvent, IRequest<T>
{
}
