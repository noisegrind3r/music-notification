using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicNotification.Catalogs.Genries.Application.Dtos;
using MusicNotification.Catalogs.Genries.Application.Services;
using MusicNotification.Catalogs.Genries.Domain;
using MusicNotification.Common.Controllers;
using Swashbuckle.AspNetCore.Annotations;

namespace MusicNotification.Catalogs.Genries.Application.Controllers;

[ApiController]
[Route("genre")]
[SwaggerTag("Настройка стилей музыки")]
public class GenreController(ILogger<GenreController> logger, IGenreService service) : BaseController<GenreEntity, GenreQueryDto, GenreCommandDto>(logger, service)
{

}
