using MusicNotification.Events.Events;
using MusicNotification.Notification.Application.MessageSender;

namespace MusicNotification.Notification.Application.EventHandlers;

internal class SendNotificationEventHandler(IMessageFabric messageFabric) : ISendNotificationEventHandler
{
    private readonly IMessageFabric _messageFabric = messageFabric;

    public async Task Handle(SendNotificationEvent notification, CancellationToken cancellationToken)
    {
      
        if (string.IsNullOrEmpty(notification.Recepient))
            return;

        await _messageFabric.Send(new MessageToSend()
        {
            Text = notification.Text,
            Title = notification.Title,
            Recepient = notification.Recepient,
            CreatedAt = DateTimeOffset.Now,
            UpdatedAt = DateTimeOffset.Now,
        }, notification.ProviderType, cancellationToken);
    }
}
