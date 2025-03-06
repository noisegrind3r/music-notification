using MusicNotification.Catalogs.Artists.Application.Dtos;
using MusicNotification.Catalogs.Artists.Domain;
using MusicNotification.Common.Interfaces;

namespace MusicNotification.Catalogs.Artists.Application.Services;

public interface IArtistService : IBaseService<ArtistEntity, ArtistQueryDto, ArtistCommandDto>
{
    Task<ArtistEntity> GetArtistEntityByIdAsync(int id);
    Task<ArtistEntity?> GetArtistByDtoProperties(ArtistCommandDto dto);
    Task<ArtistEntity> CreateOrUpdateArtistEntity(ArtistCommandDto dto, ArtistEntity entity);
}
