namespace MusicNotification.Feeder.ReleaseNotify;

public interface IReleaseNotification
{
    Task ProcessAndNotificateAllFeeds();
}
