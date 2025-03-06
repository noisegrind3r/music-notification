using MusicNotification.Catalogs.Albums.Domain;
using MusicNotification.Common.Entities;
using MusicNotification.Common.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicNotification.Catalogs.Genries.Domain;

[Table("genre")]
public class GenreEntity: BaseAuditableEntity, IAggregateRoot
{
    [Column("name")]
    public string? Name { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    public List<AlbumEntity> Albums { get; set; } = [];
}
