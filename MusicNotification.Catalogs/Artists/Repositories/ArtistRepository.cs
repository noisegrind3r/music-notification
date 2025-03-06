using Microsoft.EntityFrameworkCore;
using MusicNotification.Catalogs.Artists.Domain;
using MusicNotification.Common.Repositories;

namespace MusicNotification.Catalogs.Artists.Repositories;

public class ArtistRepository(CatalogsDbContext dbContext) : BaseRepository<CatalogsDbContext, ArtistEntity>(dbContext), IArtistRepository
{
    public IQueryable<ArtistEntity> Get(ArtistQueryOptions options)
    {
        var query = GetAll();

        if (options.IncludeAlbums)
        {
            query = query.Include(x => x.Albums);
        }

        if (options.IncludeCountry)
        {
            query = query.Include(x => x.Country);
        }

        return query;
    }
}
