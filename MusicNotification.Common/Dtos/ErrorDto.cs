using System.Net;

namespace MusicNotification.Common.Dtos;

public class ErrorDto
{
    public string? Message { get; set; }
    public HttpStatusCode Code { get; set; }
}
