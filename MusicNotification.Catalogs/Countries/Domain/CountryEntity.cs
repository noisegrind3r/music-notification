using MusicNotification.Catalogs.Artists.Domain;
using MusicNotification.Common.Entities;
using MusicNotification.Common.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicNotification.Catalogs.Countries.Domain;

[Table("country")]
public class CountryEntity: BaseAuditableEntity, IAggregateRoot
{
    [Column("name")]
    public string? Name { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    public List<ArtistEntity> Artists { get; set; } = [];
}
