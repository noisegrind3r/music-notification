using MusicNotification.Catalogs.Countries.Domain;
using MusicNotification.Catalogs.Genries.Application.Dtos;
using MusicNotification.Catalogs.Genries.Domain;
using MusicNotification.Common.Interfaces;

namespace MusicNotification.Catalogs.Genries.Application.Services;

public interface IGenreService: IBaseService<GenreEntity, GenreQueryDto, GenreCommandDto>
{
    Task<GenreEntity?> GetGenreEntityByIdAsync(int genreId);
    Task<GenreEntity?> GetGenreEntityByNameAsync(string genreName);
}
