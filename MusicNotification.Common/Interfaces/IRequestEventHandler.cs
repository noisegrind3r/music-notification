using MediatR;

namespace MusicNotification.Common.Interfaces;

public interface IRequestEventHandler<TEvent, TResult> : IRequestHandler<TEvent, TResult> where TEvent : IRequestEvent<TResult>
{

}
