using System.ComponentModel.DataAnnotations.Schema;

namespace MusicNotification.Common.Entities;

public class BaseAuditableEntity: BaseEntity
{
    [Column("created_at")]
    public DateTimeOffset CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTimeOffset UpdatedAt { get; set; }

    [Column("deleted_at")]
    public DateTimeOffset? DeletedAt { get; set; }
}
