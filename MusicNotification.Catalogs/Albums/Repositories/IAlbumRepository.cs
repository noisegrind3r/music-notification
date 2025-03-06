using MusicNotification.Catalogs.Albums.Domain;
using MusicNotification.Common.Interfaces;

namespace MusicNotification.Catalogs.Albums.Repositories;

public class AlbumQueryOptions
{
    public bool IncludeArtist { get; set; }
    public bool IncludeGenre { get; set; }
}

public interface IAlbumRepository : IRepository<AlbumEntity>
{
    IQueryable<AlbumEntity> Get(AlbumQueryOptions options);
}
