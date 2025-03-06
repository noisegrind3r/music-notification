using MusicNotification.Catalogs.Genries.Domain;
using MusicNotification.Common.Repositories;

namespace MusicNotification.Catalogs.Genries.Repositories;

public class GenreRepository(CatalogsDbContext dbContext) : BaseRepository<CatalogsDbContext, GenreEntity>(dbContext), IGenreRepository
{ 
}
