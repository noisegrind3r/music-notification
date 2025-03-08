
using System.ServiceModel.Syndication;
using System.Xml;

namespace MusicNotification.Feeder.FeedParser.FeedContentParser.Implementation;

public class MetalareaContentParser : IFeedContentParser
{
    public Task<IEnumerable<FeedData>?>? Parse(string url, CancellationToken cancellationToken = default)
    {
        XmlReader reader = XmlReader.Create(url);

        SyndicationFeed feed = SyndicationFeed.Load(reader);

        reader.Close();
        foreach (SyndicationItem item in feed.Items)
        {
        }

        return default;
    }
}
