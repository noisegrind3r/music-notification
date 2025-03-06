using MusicNotification.Catalogs.Albums.Domain;
using MusicNotification.Catalogs.Countries.Domain;
using MusicNotification.Catalogs.Genries.Domain;
using MusicNotification.Common.Entities;
using MusicNotification.Common.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicNotification.Catalogs.Artists.Domain;

[Table("artist")]
public class ArtistEntity: BaseAuditableEntity, IAggregateRoot
{
    [Column("name")]
    public string? Name { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    [Column("country_id")]
    public int CountryId { get; set; } = default;
    public CountryEntity Country { get; set; } = default!;

    public List<AlbumEntity> Albums { get; set; } = [];

}
