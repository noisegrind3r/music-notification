using MusicNotification.Catalogs.Genries.Domain;
using MusicNotification.Common.Dtos;
using Swashbuckle.AspNetCore.Annotations;

namespace MusicNotification.Catalogs.Genries.Application.Dtos;

public class GenreQueryDto : BaseQueryDto<GenreEntity>
{
    [SwaggerSchema("Наименование стиля")]
    public string? Name { get; set; }

    [SwaggerSchema("Описание стиля")]
    public string? Description { get; set; }

}
