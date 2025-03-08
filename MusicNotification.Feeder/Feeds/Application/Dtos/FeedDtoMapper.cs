using MusicNotification.Feeder.Feeds.Domain;

namespace MusicNotification.Feeder.Feeds.Application.Dtos;

public class FeedDtoMapper() : IFeedDtoMapper
{
    public FeedQueryDto? ToQueryDto(FeedEntity entity)
    {
        if (entity is null)
        {
            return null;
        }

        return new FeedQueryDto
        {
            Id = entity.Id,
            Description = entity.Description,
            Name = entity.Name,
            Url = entity.Url,
            Type = entity.Type,
            Items = entity.Items.Select(ToFeedItemQueryDto)?.ToList() ?? [],
            IsActive = entity.IsActive,
            UpdatedAt = entity.UpdatedAt,
            CreatedAt = entity.CreatedAt,
        };
    }

    private FeedItemQueryDto? ToFeedItemQueryDto(FeedItemEntity entity)
    {
        if (entity is null)
        {
            return null;
        }

        return new FeedItemQueryDto
        {
            Id = entity.Id,
            Title = entity.Title,
            Uid = entity.Uid,
            Content = entity.Content,
            UpdatedAt = entity.UpdatedAt,
            CreatedAt = entity.CreatedAt,
        };
    }

    public FeedEntity? FromCommandDto(FeedEntity entity, FeedCommandDto dto)
    {
        if (dto == null)
        {
            return null;
        }

        entity.Name = dto.Name;
        entity.Description = dto.Description;
        entity.Url = dto.Url;
        entity.Type = dto.Type;
        entity.IsActive = dto.IsActive;
        return entity;
    }
}
