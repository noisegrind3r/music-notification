using Microsoft.AspNetCore.Http;

namespace MusicNotification.DataLoader.DataLoader.Service;

public interface IDataLoaderService
{
    Task<string> ImportDataFromExcel(IFormFile file, CancellationToken cancellationToken = default);
}
