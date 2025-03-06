using Swashbuckle.AspNetCore.Annotations;
using MusicNotification.Common.Entities;

namespace MusicNotification.Common.Dtos;

public abstract class BaseQueryDto<TEntity>
    where TEntity : BaseEntity
{

    [SwaggerSchema("Идентификатор записи")]
    public int Id { get; set; }

    [SwaggerSchema("Дата обновления записи")]
    public DateTimeOffset UpdatedAt { get; set; }

    [SwaggerSchema("Дата создания записи")]
    public DateTimeOffset CreatedAt { get; set; }
}
