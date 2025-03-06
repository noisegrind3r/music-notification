using MusicNotification.Catalogs.Artists.Domain;
using MusicNotification.Catalogs.Genries.Domain;
using MusicNotification.Common.Entities;
using MusicNotification.Common.Interfaces;
using NodaTime;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicNotification.Catalogs.Albums.Domain;

[Table("album")]
public class AlbumEntity : BaseAuditableEntity, IAggregateRoot
{
    [Column("name")]
    public string? Name { get; set; }

    [Column("year")]
    public int Year { get; set; }

    [Column("size")]
    public decimal Size { get; set; }

    [Column("bitrate")]
    public string Bitrate { get; set; } = string.Empty;

    [Column("time")]
    public Duration Time { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    [Column("genre_id")]
    public int GenreId { get; set; } = default;
    public GenreEntity Genre { get; set; } = default!;

    [Column("artist_id")]
    public int ArtistId { get; set; } = default;
    public ArtistEntity Artist { get; set; } = default!;
}
