using MusicNotification.Catalogs.Albums.Domain;
using MusicNotification.Common.Interfaces;

namespace MusicNotification.Catalogs.Albums.Application.Dtos;

public interface IAlbumDtoMapper : IDtoMapper<AlbumEntity, AlbumQueryDto, AlbumCommandDto>
{
}
