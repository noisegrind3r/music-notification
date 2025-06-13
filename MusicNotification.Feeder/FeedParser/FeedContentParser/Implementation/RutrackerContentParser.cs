using System.Net;
using System.ServiceModel.Syndication;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace MusicNotification.Feeder.FeedParser.FeedContentParser.Implementation;

public class RutrackerContentParser: IFeedContentParser
{
    public async Task<List<FeedData>> Parse(string url, CancellationToken cancellationToken = default)
    {
        var handler = new HttpClientHandler
        {
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
            AllowAutoRedirect = true,
            MaxAutomaticRedirections = 5,
        };

        using (var httpClient = new HttpClient(handler))
        {
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");
            httpClient.Timeout = TimeSpan.FromSeconds(300);

            var responseBytes = await httpClient.GetByteArrayAsync(url);

            var responseData = Encoding.UTF8.GetString(responseBytes);

            using (var stringReader = new StringReader(responseData))
            using (var reader = XmlReader.Create(stringReader))
            {
                SyndicationFeed feed = SyndicationFeed.Load(reader);

                reader.Close();

                var result = new List<FeedData>();
                foreach (SyndicationItem item in feed.Items)
                {
                    var feedData = new FeedData
                    {
                        FeedDataParsedTitle = ParseTitle(item.Title.Text),
                        Content = item.Summary?.Text ?? string.Empty,
                        Uid = item.Id,
                        Title = item.Title.Text,
                        Link = item.Links.Count != 0 ? item.Links[0].Uri.AbsoluteUri : string.Empty,
                    };
                    result.Add(feedData);
                }
                return result;
            }
        }

    }

    private static FeedDataParsedTitle? ParseTitle(string title)
    {
        var pattern = "\\(([^)]+)\\)\\s*\\[([^]]+)\\]\\s*([^\\-]+(?:-[^\\-]+)*)\\s* - \\s*([^\\-]+)\\s* - \\s*([^,]+),\\s*(.*)";
        var match = Regex.Match(title, pattern);
        if (match != null && !string.IsNullOrEmpty(match.Groups[3].Value))
        {
            return new FeedDataParsedTitle
            {
                ArtistName = match.Groups[3].Value,
                Album = match.Groups[4].Value,
                Genre = match.Groups[1].Value,
                Year = match.Groups[5].Value,
            };
        }
        else
        {
            pattern = "\\(([^)]+)\\)\\s*(?:\\[([^]]+)\\])?\\s*([^\\-]+)\\s* - \\s*([^\\-]+)\\s*";
            match = Regex.Match(title, pattern);
            if (match != null)
            {
                return new FeedDataParsedTitle
                {
                    ArtistName = match.Groups[3].Value,
                    Genre = match.Groups[1].Value,
                };
            }

        }
        return default;
    }
}
