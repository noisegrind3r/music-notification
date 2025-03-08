using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace MusicNotification.Scheduler.Application.SchedulerPermanentJob;

public static class SchedulerPermanentJob
{
    public static void AddPermanentJob<T>(IServiceCollection services, string cronExpression) where T : ISchedulerWorkerPermanentCronJob
    {
        var name = typeof(T).Name;
        var jobKey = new JobKey(name);
        var triggerName = $"{name}-trigger";

        services.AddQuartz(q =>
        {
            q.AddJob<T>(opts => opts.WithIdentity(jobKey));

            q.AddTrigger(opts => opts
                .ForJob(jobKey)
                .WithIdentity(triggerName)
                .WithCronSchedule(cronExpression)
            );
        });


        services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
    }
}
