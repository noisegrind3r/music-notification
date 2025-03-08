using MusicNotification.Common.Interfaces;

namespace MusicNotification.Events.Events;

public class GetArtistByPropertiesRequestEvent: IRequestEvent<GetArtistByPropertiesResponse>
{
    public string ArtistName { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
}

public class GetArtistByPropertiesResponse
{
    public int? ArtistId { get; set; }

    public bool IsCountryValid { get; set; } = false;
    public bool IsFound { get; set; } = false;

}