using Telegram.Bot;

namespace MusicNotification.Notification.Application.MessageSender.Telegram;

public class MessageSenderTelegram(string telegramToken) : IMessageSender
{
    public async Task SendNotification(MessageToSend message, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(telegramToken) || string.IsNullOrEmpty(message.Recepient))
            return;

        var bot = new TelegramBotClient(telegramToken);
        await bot.SendMessage(message.Recepient, $@"{message.Title}
{message.Text}", cancellationToken: cancellationToken);

    }
}