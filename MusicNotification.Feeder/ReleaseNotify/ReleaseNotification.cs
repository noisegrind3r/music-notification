using MusicNotification.Common.Interfaces;
using MusicNotification.Events.Events;
using MusicNotification.Feeder.FeedParser;
using MusicNotification.Feeder.Feeds.Application.Services;
using MusicNotification.Feeder.Feeds.Domain;

namespace MusicNotification.Feeder.ReleaseNotify;

public class ReleaseNotification(
    string telegramRecepient,
    string favoriteTags,
    IEventPublisher eventPublisher,
    IFeedService feedService,
    IFeedProcessor feedProcessor): IReleaseNotification
{
    private string[] _favoriteTagsArray = favoriteTags.ToLower().Split(';');

    public async Task ProcessAndNotificateAllFeeds()
    {
        var feeds = await feedService.GetAllActiveFeeds();

        foreach (var feed in feeds)
        {
            try
            {
                var newFeedItems = await feedProcessor.Process(feed);
                if (newFeedItems is null || !newFeedItems.Any())
                    continue;

                foreach (var feedItem in newFeedItems)
                {
                    if (feedItem.FeedDataParsedTitle is null)
                        continue;

                    var existedArtist = new GetArtistByPropertiesResponse();
                    if (!string.IsNullOrEmpty(feedItem.FeedDataParsedTitle.ArtistName))
                        existedArtist = await eventPublisher.SendRequestEvent<GetArtistByPropertiesRequestEvent, GetArtistByPropertiesResponse>(new GetArtistByPropertiesRequestEvent
                        {
                            ArtistName = feedItem.FeedDataParsedTitle.ArtistName ?? string.Empty,
                            Country = feedItem.FeedDataParsedTitle.Country ?? string.Empty,
                        });

                    var favoriteGenre = false;
                    if (!existedArtist.IsFound && !string.IsNullOrEmpty(feedItem.FeedDataParsedTitle.Genre))
                    {
                        if (_favoriteTagsArray.Any(x => feedItem.FeedDataParsedTitle.Genre.Contains(x, StringComparison.CurrentCultureIgnoreCase)))
                        {
                            existedArtist.IsFound = true;
                            favoriteGenre = true;
                        }
                    }

                    if (existedArtist.IsFound)
                    {
                        await eventPublisher.SendNotificationEvent<SendNotificationEvent>(new SendNotificationEvent
                        {
                            ProviderType = MessageProviderType.Telegram,
                            Recepient = telegramRecepient,
                            Title = $"Новый альбом {feedItem.Title}",
                            Text = $@"Url: {feedItem.Link}
Feed: {feed.Name}
{(favoriteGenre ? "Любимый жанр" : (!existedArtist.IsCountryValid ? "Страна не совпадает" : string.Empty))}"
                        });
                    }

                }

                await feedService.WriteFeedDataToFeedItems(feed, newFeedItems);
            }
            catch (Exception ex)
            {
                await eventPublisher.SendNotificationEvent<SendNotificationEvent>(new SendNotificationEvent
                {
                    ProviderType = MessageProviderType.Telegram,
                    Recepient = telegramRecepient,
                    Title = $"Что-то пошло не так:",
                    Text = $@"{ex.Message}"
                });
            }

        }
    }

}
