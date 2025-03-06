using Microsoft.EntityFrameworkCore;
using MusicNotification.Catalogs.Albums.Domain;
using MusicNotification.Common.Repositories;

namespace MusicNotification.Catalogs.Albums.Repositories;

public class AlbumRepository(CatalogsDbContext dbContext) : BaseRepository<CatalogsDbContext, AlbumEntity>(dbContext), IAlbumRepository
{
    public IQueryable<AlbumEntity> Get(AlbumQueryOptions options)
    {
        var query = GetAll();

        if (options.IncludeGenre)
        {
            query = query.Include(x => x.Genre);
        }

        if (options.IncludeArtist)
        {
            query = query.Include(x => x.Artist)
                .ThenInclude(x => x.Country);
        }

        return query;
    }
}
