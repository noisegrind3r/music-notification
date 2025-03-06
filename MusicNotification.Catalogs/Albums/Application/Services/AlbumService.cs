using MusicNotification.Catalogs.Albums.Application.Dtos;
using MusicNotification.Catalogs.Albums.Domain;
using MusicNotification.Catalogs.Albums.Repositories;
using MusicNotification.Catalogs.Artists.Application.Services;
using MusicNotification.Catalogs.Artists.Domain;
using MusicNotification.Catalogs.Genries.Application.Services;
using MusicNotification.Common.Exceptions;
using MusicNotification.Common.Services;

namespace MusicNotification.Catalogs.Albums.Application.Services;

public class AlbumService(
    IAlbumRepository repository, 
    IAlbumDtoMapper mapper,
    IGenreService genreService,
    IArtistService artistService
    ): BaseService<AlbumEntity, AlbumQueryDto, AlbumCommandDto>(repository, mapper), IAlbumService
{
    public async new Task<IEnumerable<AlbumQueryDto?>?> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var query = GetAlbumQuery();
        var data = await repository.ToListAsync(query);

        return data?.Select(x => mapper.ToQueryDto(x));
    }

    public async new Task<AlbumQueryDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await GetAlbumEntityByIdAsync(id);
        return mapper.ToQueryDto(entity);
    }

    public async new Task<AlbumQueryDto?> AddAsync(AlbumCommandDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            var entity = new AlbumEntity();
            entity = await CreateOrUpdateAlbumEntity(dto, entity);
            if (entity == null)
                return default;
            await repository.AddAsync(entity, cancellationToken);
            await repository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.ToQueryDto(entity);
        }
        catch (Exception)
        {
            throw new EntityProcessException("Ошибка при создании объекта");
        }
    }

    public async new Task<AlbumQueryDto?> UpdateAsync(int id, AlbumCommandDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            var entity = await GetAlbumEntityByIdAsync(id);
            entity = await CreateOrUpdateAlbumEntity(dto, entity);
            if (entity == null)
                return default;
            await repository.UpdateAsync(entity, cancellationToken);
            await repository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.ToQueryDto(entity);
        }
        catch (Exception)
        {
            throw new EntityProcessException("Ошибка при создании объекта");
        }
    }

    private IQueryable<AlbumEntity> GetAlbumQuery()
    {
        return repository.Get(new AlbumQueryOptions
        {
            IncludeArtist = true,
            IncludeGenre = true,
        });
    }

    private async Task<AlbumEntity> GetAlbumEntityByIdAsync(int id)
    {
        var query = GetAlbumQuery();
        return await repository.FirstOrDefaultAsync(query.Where(x => x.Id == id)) ??
        throw new EntityNotFoundException($"Не найдена сущность с id {id}");
    }

    private async Task<AlbumEntity> CreateOrUpdateAlbumEntity(AlbumCommandDto dto, AlbumEntity entity)
    {
        entity = mapper.FromCommandDto(entity, dto) ?? new AlbumEntity();
        if (dto.GenreId is not null)
        {
            var genre = await genreService.GetGenreEntityByIdAsync(dto.GenreId.GetValueOrDefault());
            if (genre is not null)
                entity.Genre = genre;
        }
        else if (dto.Genre is not null)
        {
            if (dto.Genre?.Name is not null)
            {
                var genre = await genreService.GetGenreEntityByNameAsync(dto.Genre.Name);
                if (genre is not null)
                    entity.Genre = genre;
                else
                {
                    var genreDto = await genreService.AddAsync(dto.Genre);
                    if (genreDto is not null)
                        genre = await genreService.GetGenreEntityByIdAsync(genreDto.Id);
                    if (genre is not null)
                        entity.Genre = genre;
                }
            }
        }

        if (dto.ArtistId is not null)
        {
            var artist = await artistService.GetArtistEntityByIdAsync(dto.ArtistId.GetValueOrDefault());
            if (artist is not null)
                entity.Artist = artist;
        }
        else if (dto.Artist?.Name is not null)
        {
            var artist = await artistService.GetArtistByDtoProperties(dto.Artist);
            if (artist is not null)
                entity.Artist = artist;
            else
            {
                artist = await artistService.CreateOrUpdateArtistEntity(dto.Artist, new ArtistEntity());
                if (artist is not null)
                    entity.Artist = artist;
            }
        }
        return entity;

    }
}
