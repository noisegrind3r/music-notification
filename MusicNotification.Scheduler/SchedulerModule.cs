using Microsoft.Extensions.DependencyInjection;
using MusicNotification.Scheduler.Application.SchedulerPermanentJob;
using MusicNotification.Scheduler.Application.SchedulerPermanentJob.Jobs;
using System.Reflection;

namespace MusicNotification.Scheduler;

public static class SchedulerModule
{
    public static IServiceCollection AddSchedulerModule(this IServiceCollection services)
    {
        services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()); });

        return services;
    }

    public static IServiceCollection AddJobsSchedulerModule(this IServiceCollection services)
    {
        SchedulerPermanentJob.AddPermanentJob<ReleaseNotificationJob>(services, "0 0 */1 ? * *");
        return services;
    }
}
