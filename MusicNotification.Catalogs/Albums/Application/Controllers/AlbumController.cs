using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicNotification.Catalogs.Albums.Application.Dtos;
using MusicNotification.Catalogs.Albums.Application.Services;
using MusicNotification.Catalogs.Albums.Domain;
using MusicNotification.Common.Controllers;
using Swashbuckle.AspNetCore.Annotations;

namespace MusicNotification.Catalogs.Albums.Application.Controllers;

[ApiController]
[Route("album")]
[SwaggerTag("Настройка альбомов")]
public class AlbumController(ILogger<AlbumController> logger, IAlbumService service) : BaseController<AlbumEntity, AlbumQueryDto, AlbumCommandDto>(logger, service)
{

}
