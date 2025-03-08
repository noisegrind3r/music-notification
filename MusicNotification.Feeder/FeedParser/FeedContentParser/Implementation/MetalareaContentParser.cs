
using System.ServiceModel.Syndication;
using System.Text.RegularExpressions;
using System.Xml;

namespace MusicNotification.Feeder.FeedParser.FeedContentParser.Implementation;

public class MetalareaContentParser : IFeedContentParser
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
                Content = item.Summary.Text,
                Uid = item.Id,
                Title = item.Title.Text,
                Link = item.Links.Count != 0 ? item.Links[0].Uri.AbsoluteUri : string.Empty,
            };
            var country = ParseContent(item.Summary.Text);

            if (feedData.FeedDataParsedTitle != null)
                feedData.FeedDataParsedTitle.Country = country;
            result.Add(feedData);
        }

        return Task.FromResult(result);
    }

    private static FeedDataParsedTitle? ParseTitle(string title)
    {
        var pattern = "^(.*?)\\s*-\\s*(.*?)\\s*\\((\\d{4})\\)\\s*\\|\\s*(.*)$";
        var match = Regex.Match(title, pattern);
        if (match != null)
        {
            return new FeedDataParsedTitle
            {
                ArtistName = match.Groups[1].Value,
                Album = match.Groups[2].Value,
                Genre = match.Groups[4].Value,
                Year = match.Groups[3].Value,
            };
        }
        return default;
    }

    private static string? ParseContent(string content)
    {
        var pattern = "<b>Country<\\/b>:\\s*([^<]+)";
        var match = Regex.Match(content, pattern);
        if (match != null)
        {
            return match.Groups[1].Value;
        }
        return default;
    }
}
