using MusicNotification.Common.Dtos;
using MusicNotification.Common.Entities;

namespace MusicNotification.Common.Interfaces;

public interface IDtoMapper<TEntity, TQueryDto, TCommandDto>
      where TEntity : BaseEntity, IAggregateRoot, new()
      where TQueryDto : BaseQueryDto<TEntity>, new()
      where TCommandDto : BaseCommandDto<TEntity>
{
    TQueryDto? ToQueryDto(TEntity entity);
    TEntity? FromCommandDto(TEntity entity, TCommandDto dto);
}
