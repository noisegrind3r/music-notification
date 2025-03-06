using MusicNotification.Catalogs.Countries.Domain;
using MusicNotification.Common.Repositories;

namespace MusicNotification.Catalogs.Countries.Repositories;

public class CountryRepository(CatalogsDbContext dbContext) : BaseRepository<CatalogsDbContext, CountryEntity>(dbContext), ICountryRepository
{ 
}
