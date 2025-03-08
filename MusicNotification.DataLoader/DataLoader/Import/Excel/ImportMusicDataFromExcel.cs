using MusicNotification.Common.Exceptions;
using MusicNotification.Common.Import.Excel;
namespace MusicNotification.DataLoader.DataLoader.Import.Excel;

public class ImportMusicDataFromExcel : IImportMusicDataFromExcel
{
    //Artist (Band)	Album	Year	Size(Mb)	Time	Bitrate	Genre	Country

    public ImportTemplateResult Import(Stream fileStream)
    {
        var fieldMapping = new Dictionary<string, string>
        {
            {
                "Artist (Band)", nameof(ParseExcelPropertiesDataMusic.ArtistName)
            },
            {
                "Album", nameof(ParseExcelPropertiesDataMusic.Album)
            },
            {
                "Year", nameof(ParseExcelPropertiesDataMusic.Year)
            },
            {
                "Size(Mb)", nameof(ParseExcelPropertiesDataMusic.Size)
            },
            {
                "Time", nameof(ParseExcelPropertiesDataMusic.Time)
            },
            {
                "Bitrate", nameof(ParseExcelPropertiesDataMusic.Bitrate)
            },
            {
                "Genre", nameof(ParseExcelPropertiesDataMusic.Genre)
            },
            {
                "Country", nameof(ParseExcelPropertiesDataMusic.Country)
            },
        };

        using var parseExcelData = new ParseExcelData<ParseExcelPropertiesDataMusic>(fileStream, "Music", 1, fieldMapping);
        var data = parseExcelData.Parse();
        if (data is null || !data.Any())
            throw new BadRequestException("Нет данных для загрузки");

        var result = new ImportTemplateResult
        {
            Data = data?.ToList(),
        };

        return result;
    }
}

public class ImportTemplateResult
{
    public List<ParseExcelPropertiesDataMusic>? Data { get; set; }
}
