using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicNotification.Catalogs.Artists.Application.Dtos;
using MusicNotification.Catalogs.Artists.Application.Services;
using MusicNotification.Catalogs.Artists.Domain;
using MusicNotification.Common.Controllers;
using Swashbuckle.AspNetCore.Annotations;

namespace MusicNotification.Catalogs.Artists.Application.Controllers;

[ApiController]
[Route("artist")]
[SwaggerTag("Настройка исполнителей")]

public class ArtistController(ILogger<ArtistController> logger, IArtistService service) : BaseController<ArtistEntity, ArtistQueryDto, ArtistCommandDto>(logger, service)
{

}
