using MusicNotification.Catalogs.Artists.Domain;
using MusicNotification.Common.Interfaces;

namespace MusicNotification.Catalogs.Artists.Repositories;

public class ArtistQueryOptions
{
    public bool IncludeAlbums { get; set; }
    public bool IncludeCountry { get; set; }
}

public interface IArtistRepository : IRepository<ArtistEntity>
{
    IQueryable<ArtistEntity> Get(ArtistQueryOptions options);
    Task<ArtistEntity?> FoundArtistByName(string artistName);
    Task<ArtistEntity?> FoundArtistByNameAndCountry(string artistName, string country);
}
