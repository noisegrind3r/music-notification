using System.ServiceModel.Syndication;
using System.Text.RegularExpressions;
using System.Xml;

namespace MusicNotification.Feeder.FeedParser.FeedContentParser.Implementation;

public class RutrackerContentParser: IFeedContentParser
{
    public Task<List<FeedData>> Parse(string url, CancellationToken cancellationToken = default)
    {
        XmlReader reader = XmlReader.Create(url);

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

        return Task.FromResult(result);
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
