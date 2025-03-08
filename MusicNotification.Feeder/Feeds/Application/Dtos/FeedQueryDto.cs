using MusicNotification.Common.Dtos;
using MusicNotification.Feeder.Feeds.Domain;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MusicNotification.Feeder.Feeds.Application.Dtos;

public class FeedQueryDto : BaseQueryDto<FeedEntity>
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

    [SwaggerSchema("Данные фида")]
    public List<FeedItemQueryDto?>? Items { get; set; }

}

public class FeedItemQueryDto: BaseQueryDto<FeedItemEntity>
{
    [SwaggerSchema("Уникальный идентификатор записи")]
    public string? Uid { get; set; }

    [SwaggerSchema("Заголовко записи")]
    public string? Title { get; set; }

    [SwaggerSchema("Содержимое записи")]
    public string? Content { get; set; }
}
