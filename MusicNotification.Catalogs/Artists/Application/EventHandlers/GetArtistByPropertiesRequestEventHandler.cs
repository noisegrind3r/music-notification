using MusicNotification.Catalogs.Artists.Repositories;
using MusicNotification.Events.Events;

namespace MusicNotification.Catalogs.Artists.Application.EventHandlers;

public class GetArtistByPropertiesRequestEventHandler(IArtistRepository artistRepository) : IGetArtistByPropertiesRequestEventHandler
{
    public async Task<GetArtistByPropertiesResponse> Handle(GetArtistByPropertiesRequestEvent request, CancellationToken cancellationToken)
    {
        var artist = await artistRepository.FoundArtistByNameAndCountry(request.ArtistName, request.Country);
        if (artist is not null)
            return new GetArtistByPropertiesResponse
            {
                ArtistId = artist.Id,
                IsFound = true,
                IsCountryValid = true,
            };

        artist = await artistRepository.FoundArtistByName(request.ArtistName);
        if (artist is not null)
            return new GetArtistByPropertiesResponse
            {
                ArtistId = artist.Id,
                IsFound = true,
                IsCountryValid = false,
            };

        return new GetArtistByPropertiesResponse();

    }
}
