using MusicNotification.Catalogs.Albums.Application.Dtos;
using MusicNotification.Catalogs.Albums.Domain;
using MusicNotification.Common.Interfaces;

namespace MusicNotification.Catalogs.Albums.Application.Services;

public interface IAlbumService : IBaseService<AlbumEntity, AlbumQueryDto, AlbumCommandDto>
{
}
