using MusicNotification.Notification.Application.Dto;

namespace MusicNotification.Notification.Application.Services;

public interface INotificationService
{
    Task SendNotification(SendNotificationCommandDto dto);
}
