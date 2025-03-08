using System.Text.RegularExpressions;

namespace MusicNotification.Common.Helpers;

public static partial class StringHelper
{
    public static string RemoveBracketText(string text)
    {
        var result = RemoveBracketRegex().Replace(text, "");
        if (string.IsNullOrEmpty(result))
            return text;
        return result;
    }

    [GeneratedRegex(@"\s*\([^)]*\)")]
    private static partial Regex RemoveBracketRegex();
}
