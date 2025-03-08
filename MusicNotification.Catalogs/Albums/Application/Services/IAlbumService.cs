using MusicNotification.Catalogs.Albums.Application.Dtos;
using MusicNotification.Catalogs.Albums.Domain;
using MusicNotification.Common.Interfaces;

namespace MusicNotification.Catalogs.Albums.Application.Services;

public interface IAlbumService : IBaseService<AlbumEntity, AlbumQueryDto, AlbumCommandDto>
{
    Task<AlbumEntity> CreateOrUpdateAlbumEntity(AlbumCommandDto dto, AlbumEntity entity);

    Task<bool> CheckDuplicate(AlbumEntity entity);
}
