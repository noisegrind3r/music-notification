using MusicNotification.Common.Import.Excel;
namespace MusicNotification.DataLoader.DataLoader.Import.Excel;

public class ParseExcelPropertiesDataMusic : IParseExcelProperties
{
    public string ArtistName { get; set; } = string.Empty;
    public string Album { get; set; } = string.Empty;
    public string Year { get; set; } = string.Empty;
    public string Size { get; set; } = string.Empty;
    public string Time { get; set; } = string.Empty;
    public string Bitrate { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
}
