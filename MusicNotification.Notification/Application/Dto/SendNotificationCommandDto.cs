using MusicNotification.Events.Events;
using Swashbuckle.AspNetCore.Annotations;

namespace MusicNotification.Notification.Application.Dto
{
    public class SendNotificationCommandDto
    {
        [SwaggerSchema("Заголовок сообщения")]
        public string Title { get; set; } = string.Empty;

        [SwaggerSchema("Текст сообщения")]
        public string Text { get; set; } = string.Empty;

        [SwaggerSchema("Адресат сообщения")]
        public string Recepient { get; set; } = string.Empty;
    }
}
