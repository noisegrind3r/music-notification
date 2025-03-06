using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using MusicNotification.Common.Dtos;
using MusicNotification.Common.Entities;
using MusicNotification.Common.Interfaces;

namespace MusicNotification.Common.Controllers;

[ApiController]
public class BaseController<TEntity, TQueryDto, TCommandDto>(
    ILogger<BaseController<TEntity, TQueryDto, TCommandDto>> logger,
    IBaseService<TEntity, TQueryDto, TCommandDto> service) : ControllerBase
    where TEntity : BaseAuditableEntity, IAggregateRoot, new()
    where TQueryDto : BaseQueryDto<TEntity>, new()
    where TCommandDto : BaseCommandDto<TEntity>
{
    protected readonly ILogger<BaseController<TEntity, TQueryDto, TCommandDto>> _logger = logger;
    private readonly IBaseService<TEntity, TQueryDto, TCommandDto> _service = service;

    [HttpGet]
    [SwaggerOperation("Получение списка сущности")]
    public async Task<ActionResult<IEnumerable<TQueryDto>>> GetEntityAsync()
    {
        var entities = await _service.GetAllAsync();
        return Ok(entities);
    }

    [HttpGet("{id}")]
    [SwaggerOperation("Получение записи сущности по его id")]
    public async Task<ActionResult<TQueryDto>> GetEntityByIdAsync(int id)
    {
        var result = await _service.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpPost]
    [SwaggerResponse(StatusCodes.Status201Created)]
    [SwaggerOperation("Создание новой записи")]
    public async Task<ActionResult<TQueryDto>> CreateEntityAsync([FromBody] TCommandDto dto)
    {
        var created = await _service.AddAsync(dto);
        return Created(String.Empty, created);
    }

    [HttpPut("{id}")]
    [SwaggerOperation("Обновление записи по его id")]
    public async Task<ActionResult<TQueryDto>> UpdateEntityByIdAsync(int id, [FromBody] TCommandDto dto)
    {
        var updatedEntity = await _service.UpdateAsync(id, dto);
        return Ok(updatedEntity);
    }

    [SwaggerOperation("Удаление записи сущности по его id")]
    [HttpDelete("{id}")]
    public async Task<ActionResult<TQueryDto>> DeleteEntityByIdAsync(int id)
    {
        await _service.DeleteAsync(id);
        return Ok();
    }
}
