using MusicNotification.Catalogs.Albums.Domain;
using MusicNotification.Catalogs.Artists.Application.Dtos;
using MusicNotification.Catalogs.Genries.Application.Dtos;
using MusicNotification.Common.Dtos;
using NodaTime;
using Swashbuckle.AspNetCore.Annotations;

namespace MusicNotification.Catalogs.Albums.Application.Dtos;

public class AlbumQueryDto : BaseQueryDto<AlbumEntity>
{
    [SwaggerSchema("Наименование стиля")]
    public string? Name { get; set; }

    [SwaggerSchema("Год")]
    public int Year { get; set; }

    [SwaggerSchema("Размер в МБ")]
    public decimal Size { get; set; }

    [SwaggerSchema("Битрейт")]
    public string Bitrate { get; set; } = string.Empty;

    [SwaggerSchema("Продолжительность")]
    public string Time { get; set; } = string.Empty;

    [SwaggerSchema("Описание стиля")]
    public string? Description { get; set; }

    [SwaggerSchema("Стиль альбома")]
    public GenreQueryDto? Genre { get; set; }

}
