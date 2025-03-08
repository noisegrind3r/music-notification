using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MusicNotification.Common.EventPublisher;

public static class EventPublisherDependencyInjectio
{
    public static IServiceCollection AddEventPublisherDependencyInjection(this IServiceCollection services)
    {
        services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()); });

        return services;
    }
}
