using MusicNotification.Common.Dtos;
using MusicNotification.Common.Entities;

namespace MusicNotification.Common.Interfaces;

public interface IBaseService<TEntity, TQueryDto, TCommandDto>
    where TEntity : BaseEntity, IAggregateRoot, new()
    where TQueryDto : BaseQueryDto<TEntity>, new()
    where TCommandDto : BaseCommandDto<TEntity>
{
    Task<IEnumerable<TQueryDto?>?> GetAllAsync(CancellationToken cancellationToken = default);

    Task<TQueryDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<TQueryDto?> AddAsync(TCommandDto dto, CancellationToken cancellationToken = default);

    Task<TQueryDto?> UpdateAsync(int id, TCommandDto dto, CancellationToken cancellationToken = default);

    Task DeleteAsync(int id, CancellationToken cancellationToken = default);

    Task<TQueryDto?> ToQueryDto(TEntity entity);
}
