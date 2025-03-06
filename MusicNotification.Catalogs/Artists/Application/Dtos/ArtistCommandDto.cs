using MusicNotification.Catalogs.Artists.Domain;
using MusicNotification.Catalogs.Countries.Application.Dtos;
using MusicNotification.Common.Dtos;
using Swashbuckle.AspNetCore.Annotations;

namespace MusicNotification.Catalogs.Artists.Application.Dtos;

public class ArtistCommandDto: BaseCommandDto<ArtistEntity>
{
    [SwaggerSchema("Наименование исполнителя")]
    public string? Name { get; set; }

    [SwaggerSchema("Наименование страны")]
    public int? CountryId { get; set; }

    [SwaggerSchema("Параметры для создания страны")]
    public CountryCommandDto? Country { get; set; }

    [SwaggerSchema("Описание исполнителя")]
    public string? Description { get; set; }

}
