using MusicNotification.Common.Interfaces;

namespace MusicNotification.Events.Events;

public class MusicDataAddedEvent: INotificationEvent
{
    public List<MusicDataAddedEventData> Data { get; set; } = [];
}

public class MusicDataAddedEventData
{
    public string ArtistName { get; set; } = string.Empty;
    public string Album { get; set; } = string.Empty;
    public int Year { get; set; }
    public decimal Size { get; set; }
    public double Time { get; set; }
    public string Bitrate { get; set; } = string.Empty;
    public string GenreName { get; set; } = string.Empty;
    public string CountryName { get; set; } = string.Empty;
}