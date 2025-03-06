using MusicNotification.Catalogs.Genries.Domain;

namespace MusicNotification.Catalogs.Genries.Application.Dtos;

public class GenreDtoMapper : IGenreDtoMapper
{
    public GenreQueryDto? ToQueryDto(GenreEntity entity)
    {
        if (entity is null)
        {
            return null;
        }

        return new GenreQueryDto
        {
            Id = entity.Id,
            Description = entity.Description,
            Name = entity.Name,
            UpdatedAt = entity.UpdatedAt,
            CreatedAt = entity.CreatedAt,
        };
    }

    public GenreEntity? FromCommandDto(GenreEntity entity, GenreCommandDto dto)
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
