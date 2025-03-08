namespace MusicNotification.Notification.Application.MessageSender;

public class MessageToSend
{
    public int Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public string Recepient { get; set; } = string.Empty;
}