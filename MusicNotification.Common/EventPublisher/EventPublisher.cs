using MediatR;
using MusicNotification.Common.Interfaces;

namespace MusicNotification.Common.EventPublisher
{
    public class EventPublisher(IMediator mediator) : IEventPublisher
    {
        private readonly IMediator _mediator = mediator;

        public async Task<TResult> SendRequestEvent<TEvent, TResult>(TEvent eventToPublish) where TEvent : IRequestEvent<TResult>
        {
            return await _mediator.Send(eventToPublish);
        }
        public async Task SendNotificationEvent<TEvent>(TEvent eventToPublish) where TEvent : INotificationEvent
        {
            await _mediator.Publish(eventToPublish);
        }
    }
}
