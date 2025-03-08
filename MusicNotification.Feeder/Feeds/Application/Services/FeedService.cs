using MusicNotification.Common.Exceptions;
using MusicNotification.Common.Interfaces;
using MusicNotification.Common.Services;
using MusicNotification.Events.Events;
using MusicNotification.Feeder.FeedParser;
using MusicNotification.Feeder.Feeds.Application.Dtos;
using MusicNotification.Feeder.Feeds.Domain;
using MusicNotification.Feeder.Feeds.Repositories;
using System.Text;
using System.Threading;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MusicNotification.Feeder.Feeds.Application.Services;

public class FeedService(
    IFeedRepository repository, 
    IFeedDtoMapper mapper,
    IFeedProcessor feedProcessor,
    IEventPublisher eventPublisher
    ): BaseService<FeedEntity, FeedQueryDto, FeedCommandDto>(repository, mapper), IFeedService
{
    public async new Task<IEnumerable<FeedQueryDto?>?> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var query = GetFeedQuery();
        var data = await repository.ToListAsync(query);

        return data?.Select(x => mapper.ToQueryDto(x));
    }

    public async new Task<FeedQueryDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await GetFeedEntityByIdAsync(id);
        return mapper.ToQueryDto(entity);
    }

    public async new Task<FeedQueryDto?> AddAsync(FeedCommandDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            var entity = new FeedEntity();
            entity = await CreateOrUpdateFeedEntity(dto, entity);
            if (entity == null)
                return default;
            await repository.AddAsync(entity, cancellationToken);
            await repository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.ToQueryDto(entity);
        }
        catch (Exception)
        {
            throw new EntityProcessException("Ошибка при создании объекта");
        }
    }

    public async new Task<FeedQueryDto?> UpdateAsync(int id, FeedCommandDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            var entity = await GetFeedEntityByIdAsync(id);
            entity = await CreateOrUpdateFeedEntity(dto, entity);
            if (entity == null)
                return default;
            await repository.UpdateAsync(entity, cancellationToken);
            await repository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.ToQueryDto(entity);
        }
        catch (Exception)
        {
            throw new EntityProcessException("Ошибка при создании объекта");
        }
    }

    public async Task<string> ProcessFeedById(int id, CancellationToken cancellationToken = default)
    {
        var feedEntity = await GetFeedEntityByIdAsync(id);

        return await ProcessOneFeed(feedEntity, cancellationToken);
    }

    public async Task<List<FeedEntity>> GetAllActiveFeeds()
    {
        var query = GetFeedQuery();

        return await repository.ToListAsync(query.Where(x => x.IsActive ?? false));
    }

    public async Task<string> ProcessAllActiveFeeds(CancellationToken cancellationToken = default)
    {
        var feeds = await GetAllActiveFeeds();

        var result = new StringBuilder();
        foreach (var feed in feeds)
        {
            var oneResult = await ProcessOneFeed(feed, cancellationToken);
            result.AppendLine(oneResult);
        }

        return result.ToString();
    }

    private IQueryable<FeedEntity> GetFeedQuery()
    {
        return repository.Get(new FeedQueryOptions
        {
            IncludeItems = true,
        });
    }

    private async Task<FeedEntity> GetFeedEntityByIdAsync(int id)
    {
        var query = GetFeedQuery();
        return await repository.FirstOrDefaultAsync(query.Where(x => x.Id == id)) ??
                 throw new EntityNotFoundException($"Не найдена сущность с id {id}");
    }

    private async Task<FeedEntity> CreateOrUpdateFeedEntity(FeedCommandDto dto, FeedEntity entity)
    {
        entity = mapper.FromCommandDto(entity, dto) ?? new FeedEntity();
        await Task.CompletedTask;
        return entity;
    }

    private async Task<string> ProcessOneFeed(FeedEntity feedEntity, CancellationToken cancellationToken = default)
    { 
        var data = await ProcessFeed(feedEntity);
        await WriteFeedDataToFeedItems(feedEntity, data, cancellationToken);

        return $"Обработано данных {feedEntity.Name} = {data.Count()}";
    }

    private async Task<IEnumerable<FeedData>> ProcessFeed(FeedEntity feedEntity)
    {
        return await feedProcessor.Process(feedEntity);
    }

    public async Task WriteFeedDataToFeedItems(FeedEntity feedEntity, IEnumerable<FeedData> feedData, CancellationToken cancellationToken = default)
    {
        var newItems = feedData?.Select(x => new FeedItemEntity
        {
            Content = x.Content,
            Feed = feedEntity,
            Title = x.Title,
            Uid = x.Uid,
            Link = x.Link,
        })?.ToList() ?? [];

        if (newItems.Count != 0)
        {
            feedEntity.Items = feedEntity.Items.Concat(newItems)?.ToList() ?? [];
            await repository.UpdateAsync(feedEntity, cancellationToken);
            await repository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task ReleaseNotification()
    {
        await eventPublisher.SendNotificationEvent<ReleaseNotifyEvent>(new ReleaseNotifyEvent());
    }


}
