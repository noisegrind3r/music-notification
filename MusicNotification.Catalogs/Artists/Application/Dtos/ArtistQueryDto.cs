using MusicNotification.Catalogs.Albums.Application.Dtos;
using MusicNotification.Catalogs.Artists.Domain;
using MusicNotification.Catalogs.Countries.Application.Dtos;
using MusicNotification.Common.Dtos;
using Swashbuckle.AspNetCore.Annotations;

namespace MusicNotification.Catalogs.Artists.Application.Dtos;

public class ArtistQueryDto : BaseQueryDto<ArtistEntity>
{
    [SwaggerSchema("Наименование исполнителя")]
    public string? Name { get; set; }

    [SwaggerSchema("Страна исполнителя")]
    public CountryQueryDto? Country { get; set; }

    [SwaggerSchema("Описание исполнителя")]
    public string? Description { get; set; }

    [SwaggerSchema("Альбомы исполнителя")]
    public List<AlbumQueryDto?>? Albums { get; set; }

}
