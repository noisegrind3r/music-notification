using Microsoft.AspNetCore.Http;
using MusicNotification.Common.EventPublisher;
using MusicNotification.Common.Exceptions;
using MusicNotification.Common.Helpers;
using MusicNotification.Common.Interfaces;
using MusicNotification.DataLoader.DataLoader.Import.Excel;
using MusicNotification.Events.Events;

namespace MusicNotification.DataLoader.DataLoader.Service
{
    public class DataLoaderService(
        IImportMusicDataFromExcel importMusicDataFromExcel,
        IEventPublisher eventPublisher
    ) : IDataLoaderService
    {
        public async Task<string> ImportDataFromExcel(IFormFile file, CancellationToken cancellationToken = default)
        {
            if (file == null)
                throw new BadRequestException("Не указан файл для импорта");

            using var stream = new MemoryStream();
            await file.CopyToAsync(stream, cancellationToken);

            var data = importMusicDataFromExcel.Import(stream);

            await MusicDataAdded(data);

            var count = data.Data?.Count ?? 0;

            var result = $"Обработанно строк : {count}";

            return result;
        }

        private async Task MusicDataAdded(ImportTemplateResult data)
        {
            await eventPublisher.SendNotificationEvent(new MusicDataAddedEvent()
            {
                Data = data.Data?.Select(x => new MusicDataAddedEventData
                {
                    Album = x.Album,
                    ArtistName = StringHelper.RemoveBracketText(x.ArtistName).Trim(),
                    CountryName = x.Country,
                    GenreName = x.Genre,
                    Year = int.Parse(x.Year),
                    Bitrate = x.Bitrate,
                    Size = decimal.Parse(x.Size),
                    Time = TimeSpan.Parse(x.Time).TotalSeconds
                })?.ToList() ?? []
            });
        }
    }
}
