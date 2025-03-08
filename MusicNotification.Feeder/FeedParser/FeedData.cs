namespace MusicNotification.Feeder.FeedParser;

public class FeedData
{
    public string? Uid { get; set; }

    public string? Title { get; set; }

    public string? Content { get; set; }
    public string? Link { get; set; }

    public FeedDataParsedTitle? FeedDataParsedTitle { get; set; }
}

public class FeedDataParsedTitle
{
    public string? ArtistName { get; set; }
    public string? Album { get; set; }
    public string? Country { get; set; }
    public string? Genre { get; set; }
    public string? Year { get; set; }

}
