using Microsoft.Extensions.DependencyInjection;
using MusicNotification.Notification.Application.EventHandlers;
using MusicNotification.Notification.Application.MessageSender;
using MusicNotification.Notification.Application.Services;
using System.ComponentModel.Design;
using System.Reflection;

namespace MusicNotification.Notification;

public static class NotificationModule
{
    public static IServiceCollection AddNotificationModule(this IServiceCollection services, Action<NotificationModuleOptions> configureOptions)
    {
        var settings = new NotificationModuleOptions();
        configureOptions(settings);

        services.Configure(configureOptions);

        services.AddScoped<ISendNotificationEventHandler, SendNotificationEventHandler>();
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<IMessageFabric>(x => new MessageFabric(new MessageFabricOptions
        { 
            TelegramToken = settings.TelegramToken ?? string.Empty,
        }));

        services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()); });

        return services;
    }
}
