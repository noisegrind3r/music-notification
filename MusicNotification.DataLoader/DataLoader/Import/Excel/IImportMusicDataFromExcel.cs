namespace MusicNotification.DataLoader.DataLoader.Import.Excel;

public interface IImportMusicDataFromExcel
{
    ImportTemplateResult Import(Stream fileStream);
}
