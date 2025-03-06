using MusicNotification.Common.Entities;

namespace MusicNotification.Common.Dtos;

public class BaseCommandDto<TEntity>
    where TEntity : BaseEntity, new()
{
}
