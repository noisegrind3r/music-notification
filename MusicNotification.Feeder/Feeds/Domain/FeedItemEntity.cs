using Microsoft.EntityFrameworkCore;
using MusicNotification.Common.Entities;
using MusicNotification.Common.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicNotification.Feeder.Feeds.Domain;

[Table("feed_item")]
public class FeedItemEntity : BaseAuditableEntity
{
    [Column("uid")]
    public string? Uid { get; set; }

    [Column("title")]
    public string? Title { get; set; }

    [Column("content")]
    public string? Content { get; set; }

    [Column("feed_id")]
    public int FeedId { get; set; } = default;
    public FeedEntity Feed { get; set; } = default!;
}
