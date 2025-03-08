using ExcelDataReader;
using MusicNotification.Common.Exceptions;
using System.Data;
using System.Reflection;

namespace MusicNotification.Common.Import.Excel;

public class ParseExcelData<T>(Stream fileStream, string sheetName, int startRow, IDictionary<string, string> fieldMapping) : IDisposable
    where T : IParseExcelProperties, new()
{
    private readonly Stream _stream = fileStream;
    private readonly string _sheetName = sheetName;
    private readonly int _startRow = startRow;
    private readonly IDictionary<string, string> _fieldMapping = fieldMapping;

    private DataTable? LoadFile(int startRow, bool useHeaderRow = true)
    {
        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        var reader = ExcelReaderFactory.CreateReader(_stream);
        var dataSet = reader.AsDataSet(new ExcelDataSetConfiguration()
        {
            ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
            {
                UseHeaderRow = useHeaderRow,
                //Пропускаем указанное количество строк для получения заголовков
                ReadHeaderRow = (rowReader) => {
                    for(var k = 0; k < startRow - 1; k++)
                        reader.Read();
                },
            }
        });

        if (dataSet.Tables[_sheetName] is not null)
            return dataSet.Tables[_sheetName];
        
        return default;
    }

    public IEnumerable<T> Parse()
    {
        var data = LoadFile(_startRow);

        if (data?.Columns?.Count == 0)
            throw new BadRequestException("Не удалось разобрать файл");

        var fieldIndex = data?.Columns?.Cast<DataColumn>()
            .Where(x => _fieldMapping.ContainsKey(x.ColumnName))
            .ToDictionary(key => _fieldMapping[key.ColumnName], value => value.Ordinal);

        if (fieldIndex?.Count == 0)
            throw new BadRequestException("Не удалось разобрать файл");

        var result = new List<T>();

        try
        {
            Type parsedType = typeof(T);
            for (var k = 0; k < data?.Rows.Count; k++)
            {
                var row = data.Rows[k];
                var parsedRow = new T();

                foreach (var map in fieldIndex!)
                {
                    PropertyInfo? propInfo = parsedType.GetProperty(map.Key);
                    if (propInfo != null)
                    {
                        var value = row[map.Value] == null || row[map.Value] == DBNull.Value ? String.Empty : row[map.Value];
                        propInfo.SetValue(parsedRow, value.ToString());

                    }
                }
                result.Add(parsedRow);
            }
        }
        catch (Exception ex)
        {
            throw new BadRequestException($"При обработки файла возникла ошибка {ex.Message}");
        }
        return result;
    }

    public TBase? GetSpecifiedCellValue<TBase>(int row, int col)
    {
        //Загружаем весь файл, так как могут быть нужны данные из шапки
        var data = LoadFile(0, false);
        var value = data?.Rows[row][col];
        if (data is null)
            return default;

        return value != DBNull.Value ? (TBase)data.Rows[row][col] : default;
    }

    public void Dispose()
    {
        _stream.Dispose();
        GC.SuppressFinalize(this);
    }
}
