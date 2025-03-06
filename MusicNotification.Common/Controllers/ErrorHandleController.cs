using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using MusicNotification.Common.Dtos;
using MusicNotification.Common.Exceptions;

namespace MusicNotification.Common.Controllers;

[AllowAnonymous]
[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorHandleController : ControllerBase
{
    [Route("error")]
    public ErrorDto Error()
    {
        var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
        var exception = context?.Error;
        var code = HttpStatusCode.InternalServerError;

        if (exception is EntityNotFoundException) code = HttpStatusCode.NotFound;
        else if (exception is BadRequestException) code = HttpStatusCode.BadRequest;
        else if (exception is EntityProcessException) code = HttpStatusCode.UnprocessableEntity;

        Response.StatusCode = ((int)code);

        return new ErrorDto()
        {
            Message = exception?.Message ?? String.Empty,
            Code = code
        };
    }
}
