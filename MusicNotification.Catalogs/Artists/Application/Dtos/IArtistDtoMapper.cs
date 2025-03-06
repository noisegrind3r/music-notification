using MusicNotification.Catalogs.Artists.Domain;
using MusicNotification.Common.Interfaces;

namespace MusicNotification.Catalogs.Artists.Application.Dtos;

public interface IArtistDtoMapper : IDtoMapper<ArtistEntity, ArtistQueryDto, ArtistCommandDto>
{
}
