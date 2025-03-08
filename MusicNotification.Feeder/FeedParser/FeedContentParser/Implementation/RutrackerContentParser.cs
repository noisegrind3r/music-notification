namespace MusicNotification.Feeder.FeedParser.FeedContentParser.Implementation;

public class RutrackerContentParser: IFeedContentParser
{
    public Task<IEnumerable<FeedData>?>? Parse(string url, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
