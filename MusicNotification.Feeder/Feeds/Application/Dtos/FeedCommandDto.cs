using MusicNotification.Common.Dtos;
using MusicNotification.Feeder.Feeds.Domain;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace MusicNotification.Feeder.Feeds.Application.Dtos;

public class FeedCommandDto: BaseCommandDto<FeedEntity>
{
    [SwaggerSchema("Наименование фида")]
    public string? Name { get; set; }

    [SwaggerSchema("Описание фида")]
    public string? Description { get; set; }

    [SwaggerSchema("Ссылка на фид")]
    public string? Url { get; set; }

    [SwaggerSchema("Тип фида")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public FeedType? Type { get; set; }

    [SwaggerSchema("Признак активности фида")]
    public bool? IsActive { get; set; }
}
