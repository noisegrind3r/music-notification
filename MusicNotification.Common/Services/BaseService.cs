using MusicNotification.Common.Dtos;
using MusicNotification.Common.Entities;
using MusicNotification.Common.Exceptions;
using MusicNotification.Common.Interfaces;

namespace MusicNotification.Common.Services;

public class BaseService<TEntity, TQueryDto, TCommandDto> : IBaseService<TEntity, TQueryDto, TCommandDto>
    where TEntity : BaseEntity, IAggregateRoot, new()
    where TQueryDto : BaseQueryDto<TEntity>, new()
    where TCommandDto : BaseCommandDto<TEntity>
{
    private readonly IDtoMapper<TEntity, TQueryDto, TCommandDto> _mapper;
    private readonly IRepository<TEntity> _repository;
    private readonly IUnitOfWork _unitOfWork;

    protected BaseService(IRepository<TEntity> repository, IDtoMapper<TEntity, TQueryDto, TCommandDto> mapper)
    {
        _unitOfWork = repository.UnitOfWork;
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TQueryDto?>?> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var data = await _repository.ToListAsync(_repository.GetAll());

        return data?.Select(x => _mapper.ToQueryDto(x));
    }

    public async Task<TQueryDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.FirstOrDefaultAsync(_repository.GetAll().Where(x => x.Id == id))
                     ?? throw new EntityNotFoundException($"Не найдена сущность с id {id}"); ;
        return _mapper.ToQueryDto(entity);
    }

    public async Task<TQueryDto?> AddAsync(TCommandDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            var entity = new TEntity();
            entity = _mapper.FromCommandDto(entity, dto);
            if (entity == null)
                return default;
            await _repository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return _mapper.ToQueryDto(entity);
        }
        catch (Exception)
        {
            throw new EntityProcessException("Ошибка при создании объекта");
        }
    }

    public async Task<TQueryDto?> UpdateAsync(int id, TCommandDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            var entity = await _repository.FirstOrDefaultAsync(_repository.GetAll().Where(x => x.Id == id)) ??
                     throw new EntityNotFoundException($"Не найдена сущность с id {id}");
            entity = _mapper.FromCommandDto(entity, dto);
            if (entity == null)
                return default;
            await _repository.UpdateAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return _mapper.ToQueryDto(entity);
        }
        catch (Exception)
        {
            throw new EntityProcessException("Ошибка при создании объекта");
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.FirstOrDefaultAsync(_repository.GetAll().Where(x => x.Id == id)) ??
                     throw new EntityNotFoundException($"Не найдена сущность с id {id}");
        _repository.Delete(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<TQueryDto?> ToQueryDto(TEntity entity)
    {
        return await Task.FromResult(_mapper.ToQueryDto(entity));
    }
}
