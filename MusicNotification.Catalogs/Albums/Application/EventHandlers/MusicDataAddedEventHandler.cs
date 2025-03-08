using MusicNotification.Catalogs.Albums.Application.Services;
using MusicNotification.Catalogs.Albums.Domain;
using MusicNotification.Catalogs.Albums.Repositories;
using MusicNotification.Catalogs.Artists.Application.Dtos;
using MusicNotification.Catalogs.Countries.Application.Dtos;
using MusicNotification.Catalogs.Genries.Application.Dtos;
using MusicNotification.Common.Interfaces;
using MusicNotification.Events.Events;

namespace MusicNotification.Catalogs.Albums.Application.EventHandlers
{
    public class MusicDataAddedEventHandler(IAlbumService albumService, IAlbumRepository albumRepository) : IMusicDataAddedEventHandler
    {
        private readonly IUnitOfWork _unitOfWork = albumRepository.UnitOfWork;
        public async Task Handle(MusicDataAddedEvent notification, CancellationToken cancellationToken)
        {
            var data = notification.Data;

            await _unitOfWork.BeginTransactionAsync(cancellationToken: cancellationToken);

            foreach (var item in data)
            {
                var album = await albumService.CreateOrUpdateAlbumEntity(new Dtos.AlbumCommandDto
                {
                    Artist = new ArtistCommandDto
                    { 
                        Name = item.ArtistName,
                        Country = new CountryCommandDto
                        { 
                            Name = item.CountryName,
                        }
                    },
                    Genre = new GenreCommandDto
                    { 
                        Name= item.GenreName,
                    },
                    Bitrate = item.Bitrate,
                    Name = item.Album,
                    Time = item.Time,
                    Size = item.Size,
                    Year = item.Year,

                }, new AlbumEntity());

                var isExisted = await albumService.CheckDuplicate(album);
                if (!isExisted)
                    await albumRepository.AddAsync(album, cancellationToken);

            }
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitTransactionAsync(cancellationToken);
        }
    }
}
