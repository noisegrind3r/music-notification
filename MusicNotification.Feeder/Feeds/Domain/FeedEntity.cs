using MusicNotification.Common.Entities;
using MusicNotification.Common.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicNotification.Feeder.Feeds.Domain;

public enum FeedType
{
    Metalarea = 0,
    Rutracker = 1,
};

[Table("feed")]
public class FeedEntity: BaseAuditableEntity, IAggregateRoot
{
    [Column("name")]
    public string? Name { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    [Column("url")]
    public string? Url { get; set; }

    [Column("type")]
    public FeedType? Type { get; set; }

    [Column("is_active")]
    public bool? IsActive { get; set; }

    public List<FeedItemEntity> Items { get; set; } = [];
}
