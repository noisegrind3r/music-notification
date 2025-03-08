using MusicNotification.Common.Classes;

namespace MusicNotification.Feeder;

public class FeederModuleOptions
{
    public string TelegramRecepient { get; set; } = string.Empty;
    public string FavoriteTags {  get; set; } = string.Empty;
    public ConnectionStringsOptions? ConnectionStrings { get; set; }
}
