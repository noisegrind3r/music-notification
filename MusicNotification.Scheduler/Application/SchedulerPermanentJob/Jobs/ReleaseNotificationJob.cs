using MusicNotification.Common.Interfaces;
using MusicNotification.Events.Events;
using Quartz;

namespace MusicNotification.Scheduler.Application.SchedulerPermanentJob.Jobs
{
    public class ReleaseNotificationJob(
        IEventPublisher eventPublisher
        ) : ISchedulerWorkerPermanentCronJob
    {
        private readonly IEventPublisher _eventPublisher = eventPublisher;

        public async Task Execute(IJobExecutionContext context)
        {
            await _eventPublisher.SendNotificationEvent(new ReleaseNotifyEvent());
        }
    }
}
