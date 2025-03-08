using MusicNotification.Common.Interfaces;
namespace MusicNotification.Events.Events;

public enum MessageProviderType
{
    Telegram = 0,
}

public class SendNotificationEvent : INotificationEvent
{
    public string Title { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public MessageProviderType ProviderType { get; set; } = MessageProviderType.Telegram;
    public string Recepient { get; set; } = string.Empty;
}
