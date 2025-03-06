
using MusicNotification.Catalogs.Countries.Domain;

namespace MusicNotification.Catalogs.Countries.Application.Dtos;

public class CountryDtoMapper : ICountryDtoMapper
{
    public CountryQueryDto? ToQueryDto(CountryEntity entity)
    {
        if (entity is null)
        {
            return null;
        }

        return new CountryQueryDto
        {
            Id = entity.Id,
            Description = entity.Description,
            Name = entity.Name,
            UpdatedAt = entity.UpdatedAt,
            CreatedAt = entity.CreatedAt,
        };
    }

    public CountryEntity? FromCommandDto(CountryEntity entity, CountryCommandDto dto)
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
