using MusicNotification.Common.Interfaces;
using MusicNotification.Events.Events;
using MusicNotification.Notification.Application.Dto;

namespace MusicNotification.Notification.Application.Services;

public class NotificationService(IEventPublisher eventPublisher): INotificationService
{
    public async Task SendNotification(SendNotificationCommandDto dto)
    {
        await eventPublisher.SendNotificationEvent(new SendNotificationEvent
        { 
            Text = dto.Text,
            Title = dto.Title,
            Recepient = dto.Recepient,
        });
    }
}
