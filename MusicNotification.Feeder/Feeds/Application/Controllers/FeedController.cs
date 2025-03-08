using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicNotification.Common.Controllers;
using MusicNotification.Common.Validation;
using MusicNotification.Feeder.Feeds.Application.Dtos;
using MusicNotification.Feeder.Feeds.Application.Services;
using MusicNotification.Feeder.Feeds.Domain;
using Swashbuckle.AspNetCore.Annotations;

namespace MusicNotification.Feeder.Feeds.Application.Controllers;

[ApiController]
[Route("feed")]
[SwaggerTag("Настройка фидов")]

public class FeedController(ILogger<FeedController> logger, IFeedService service) : BaseController<FeedEntity, FeedQueryDto, FeedCommandDto>(logger, service)
{
    [HttpPost("process/{id}")]
    [SwaggerOperation(Summary = "Обработать один фид")]
    public async Task<IActionResult> ProcessFeedById(int id)
    {
        var result = await service.ProcessFeedById(id);
        return Ok(result);
    }

    [HttpPost("process")]
    [SwaggerOperation(Summary = "Обработать все активные фид")]
    public async Task<IActionResult> ProcessAllActiveFeeds()
    {
        var result = await service.ProcessAllActiveFeeds();
        return Ok(result);
    }

    [HttpPost("release")]
    [SwaggerOperation(Summary = "Обработать все активные фид")]
    public async Task<IActionResult> ReleaseNotification()
    {
        await service.ReleaseNotification();
        return Ok();
    }
}
