using MusicNotification.Events.Events;
using MusicNotification.Notification.Application.MessageSender.Telegram;

namespace MusicNotification.Notification.Application.MessageSender;

public class MessageFabric(MessageFabricOptions options) : IMessageFabric
{
    public async Task Send(MessageToSend message, MessageProviderType type, CancellationToken cancellationToken = default)
    {
#pragma warning disable IDE0079 // Remove unnecessary suppression
#pragma warning disable CA1859 // Use concrete types when possible for improved performance
        IMessageSender messageSender = type switch
        {
            MessageProviderType.Telegram => new MessageSenderTelegram(options.TelegramToken),
            _ => throw new NotImplementedException()
        };
#pragma warning restore CA1859 // Use concrete types when possible for improved performance

        await messageSender.SendNotification(message, cancellationToken);
#pragma warning restore IDE0079 // Remove unnecessary suppression
    }
}