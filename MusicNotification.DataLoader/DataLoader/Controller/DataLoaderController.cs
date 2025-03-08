using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicNotification.Common.Validation;
using MusicNotification.DataLoader.DataLoader.Service;
using Swashbuckle.AspNetCore.Annotations;

namespace MusicNotification.DataLoader.DataLoader.Controller
{
    [ApiController]
    [Route("loader")]
    [SwaggerTag("Загрузка данных")]
    public class DataLoaderController(IDataLoaderService service): ControllerBase
    {
        [HttpPost("excel")]
        [AllowedExtensions(
        [
            ".xls", ".xlsx",
        ])]
        [SwaggerOperation(Summary = "Загрузка данных справочника через файл excel")]
        public async Task<IActionResult> ImportDataFromExcel(IFormFile file)
        {
            var result = await service.ImportDataFromExcel(file);
            return Ok(result);
        }
    }
}
