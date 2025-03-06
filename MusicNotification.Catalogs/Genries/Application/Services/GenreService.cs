using MusicNotification.Catalogs.Genries.Application.Dtos;
using MusicNotification.Catalogs.Genries.Domain;
using MusicNotification.Catalogs.Genries.Repositories;
using MusicNotification.Common.Services;

namespace MusicNotification.Catalogs.Genries.Application.Services;

public class GenreService(IGenreRepository repository, IGenreDtoMapper mapper): BaseService<GenreEntity, GenreQueryDto, GenreCommandDto>(repository, mapper), IGenreService
{
    public async Task<GenreEntity?> GetGenreEntityByIdAsync(int genreId)
    {
        return await repository.GetByIdAsync(genreId);
    }
    public async Task<GenreEntity?> GetGenreEntityByNameAsync(string genreName)
    {
        return await repository.FirstOrDefaultAsync(repository.GetAll().Where(x => x.Name != null && x.Name.Equals(genreName)));
    }
}
