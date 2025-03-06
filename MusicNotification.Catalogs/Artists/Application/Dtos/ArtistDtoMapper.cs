using MusicNotification.Catalogs.Albums.Application.Dtos;
using MusicNotification.Catalogs.Artists.Domain;
using MusicNotification.Catalogs.Countries.Application.Dtos;

namespace MusicNotification.Catalogs.Artists.Application.Dtos;

public class ArtistDtoMapper(ICountryDtoMapper countryDtoMapper, IAlbumDtoMapper albumDtoMapper) : IArtistDtoMapper
{
    public ArtistQueryDto? ToQueryDto(ArtistEntity entity)
    {
        if (entity is null)
        {
            return null;
        }

        return new ArtistQueryDto
        {
            Id = entity.Id,
            Description = entity.Description,
            Name = entity.Name,
            Country = countryDtoMapper.ToQueryDto(entity.Country),
            Albums = entity.Albums.Select(albumDtoMapper.ToQueryDto)?.ToList() ?? [],
            UpdatedAt = entity.UpdatedAt,
            CreatedAt = entity.CreatedAt,
        };
    }

    public ArtistEntity? FromCommandDto(ArtistEntity entity, ArtistCommandDto dto)
    {
        if (dto == null)
        {
            return null;
        }

        entity.Name = dto.Name;
        entity.Description = dto.Description;

        return entity;
    }
}
