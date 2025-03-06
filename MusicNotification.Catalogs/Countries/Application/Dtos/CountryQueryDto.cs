using MusicNotification.Catalogs.Countries.Domain;
using MusicNotification.Common.Dtos;
using Swashbuckle.AspNetCore.Annotations;

namespace MusicNotification.Catalogs.Countries.Application.Dtos;

public class CountryQueryDto: BaseQueryDto<CountryEntity>
{
    [SwaggerSchema("Наименование страны")]
    public string? Name { get; set; }

    [SwaggerSchema("Описание страны")]
    public string? Description { get; set; }

}
