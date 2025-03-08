
using MusicNotification.Events.Events;

namespace MusicNotification.Notification.Application.MessageSender;

public interface IMessageFabric
{
    Task Send(MessageToSend message, MessageProviderType type, CancellationToken cancellationToken = default);
}