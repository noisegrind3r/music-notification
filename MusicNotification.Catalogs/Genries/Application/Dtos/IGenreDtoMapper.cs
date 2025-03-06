using MusicNotification.Catalogs.Genries.Domain;
using MusicNotification.Common.Interfaces;

namespace MusicNotification.Catalogs.Genries.Application.Dtos;

public interface IGenreDtoMapper: IDtoMapper<GenreEntity, GenreQueryDto, GenreCommandDto>
{
}
