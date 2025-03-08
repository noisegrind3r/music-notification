using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicNotification.Common.Validation;
using MusicNotification.Notification.Application.Dto;
using MusicNotification.Notification.Application.Services;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicNotification.Notification.Application.Controller;

[ApiController]
[Route("notification")]
[SwaggerTag("Загрузка данных")]
public class NotificationController(INotificationService service) : ControllerBase
{
    [HttpPost("send")]
    [SwaggerOperation(Summary = "Отправить оповещение")]
    public async Task<IActionResult> SendNotification([FromBody] SendNotificationCommandDto dto)
    {
        await service.SendNotification(dto);
        return Ok();
    }
}
