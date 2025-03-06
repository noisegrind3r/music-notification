using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicNotification.Catalogs.Countries.Application.Dtos;
using MusicNotification.Catalogs.Countries.Application.Services;
using MusicNotification.Catalogs.Countries.Domain;
using MusicNotification.Common.Controllers;
using Swashbuckle.AspNetCore.Annotations;

namespace MusicNotification.Catalogs.Countries.Application.Controllers;

[ApiController]
[Route("country")]
[SwaggerTag("Настройка стран исполнителей")]
public class CountryController(ILogger<CountryController> logger, ICountryService service) : BaseController<CountryEntity, CountryQueryDto, CountryCommandDto>(logger, service)
{

}
