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

    public async Task<ArtistEntity?> FoundArtistByNameAndCountry(string artistName, string country)
    {
        return await GetAll().Include(x => x.Country).FirstOrDefaultAsync(x => 
            x.Name!= null && x.Name.Equals(artistName) && x.Country.Name != null && x.Country.Name.Equals(country)
        );
    }

    public async Task<ArtistEntity?> FoundArtistByName(string artistName)
    {
        return await GetAll().Include(x => x.Country).FirstOrDefaultAsync(x =>
            x.Name != null && x.Name.Equals(artistName)
        );
    }
}
