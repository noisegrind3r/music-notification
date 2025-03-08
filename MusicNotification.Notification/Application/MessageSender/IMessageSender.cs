namespace MusicNotification.Notification.Application.MessageSender;

public interface IMessageSender
{
    Task SendNotification(MessageToSend message, CancellationToken cancellationToken);
}