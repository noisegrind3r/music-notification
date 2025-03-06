using MusicNotification.Catalogs.Albums.Domain;
using MusicNotification.Catalogs.Genries.Application.Dtos;

namespace MusicNotification.Catalogs.Albums.Application.Dtos;

public class AlbumDtoMapper(IGenreDtoMapper genreDtoMapper) : IAlbumDtoMapper
{
    public AlbumQueryDto? ToQueryDto(AlbumEntity entity)
    {
        if (entity is null)
        {
            return null;
        }

        return new AlbumQueryDto
        {
            Id = entity.Id,
            Description = entity.Description,
            Name = entity.Name,
            Bitrate = entity.Bitrate,
            Size = entity.Size,
            Time = entity.Time,
            Year = entity.Year,
            Genre = genreDtoMapper.ToQueryDto(entity.Genre),
            UpdatedAt = entity.UpdatedAt,
            CreatedAt = entity.CreatedAt,
        };
    }

    public AlbumEntity? FromCommandDto(AlbumEntity entity, AlbumCommandDto dto)
    {
        if (dto == null)
        {
            return null;
        }

        entity.Name = dto.Name;
        entity.Description = dto.Description;
        entity.Bitrate = dto.Bitrate;
        entity.Size = dto.Size;
        entity.Year = dto.Year;
        entity.Time = NodaTime.Duration.FromSeconds(dto.Time);

        return entity;
    }
}
