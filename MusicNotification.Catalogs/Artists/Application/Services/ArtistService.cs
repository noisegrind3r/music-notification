using MusicNotification.Catalogs.Artists.Application.Dtos;
using MusicNotification.Catalogs.Artists.Domain;
using MusicNotification.Catalogs.Artists.Repositories;
using MusicNotification.Catalogs.Countries.Application.Dtos;
using MusicNotification.Catalogs.Countries.Application.Services;
using MusicNotification.Common.Exceptions;
using MusicNotification.Common.Interfaces;
using MusicNotification.Common.Services;

namespace MusicNotification.Catalogs.Artists.Application.Services;

public class ArtistService(
    IArtistRepository repository, 
    IArtistDtoMapper mapper,
    ICountryService countryService
    ): BaseService<ArtistEntity, ArtistQueryDto, ArtistCommandDto>(repository, mapper), IArtistService
{
    public async new Task<IEnumerable<ArtistQueryDto?>?> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var query = GetArtistQuery();
        var data = await repository.ToListAsync(query);

        return data?.Select(x => mapper.ToQueryDto(x));
    }

    public async new Task<ArtistQueryDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await GetArtistEntityByIdAsync(id);
        return mapper.ToQueryDto(entity);
    }

    public async new Task<ArtistQueryDto?> AddAsync(ArtistCommandDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            var entity = new ArtistEntity();
            entity = await CreateOrUpdateArtistEntity(dto, entity);
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

    public async new Task<ArtistQueryDto?> UpdateAsync(int id, ArtistCommandDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            var entity = await GetArtistEntityByIdAsync(id);
            entity = await CreateOrUpdateArtistEntity(dto, entity);
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

    private IQueryable<ArtistEntity> GetArtistQuery()
    {
        return repository.Get(new ArtistQueryOptions
        {
            IncludeCountry = true,
            IncludeAlbums = true,
        });
    }

    public async Task<ArtistEntity> GetArtistEntityByIdAsync(int id)
    {
        var query = GetArtistQuery();
        return await repository.FirstOrDefaultAsync(query.Where(x => x.Id == id)) ??
                 throw new EntityNotFoundException($"Не найдена сущность с id {id}");
    }

    public async Task<ArtistEntity?> GetArtistByDtoProperties(ArtistCommandDto dto)
    {
        var query = repository.Get(new ArtistQueryOptions
        {
            IncludeCountry = true,
        });

        if (dto.CountryId is not null)
            return await repository.FirstOrDefaultAsync(query.Where(x => x.Name != null && x.Name.Equals(dto.Name) && x.Country.Id.Equals(dto.CountryId)));
        else if (dto.Country is not null)
            return await repository.FirstOrDefaultAsync(query.Where(
                x => x.Name != null && x.Name.Equals(dto.Name) && x.Country.Name != null && x.Country.Name.Equals(dto.Country.Name))
            );

        return default;
    }

    public async Task<ArtistEntity> CreateOrUpdateArtistEntity(ArtistCommandDto dto, ArtistEntity entity)
    {
        var existed = await GetArtistByDtoProperties(dto);
        if (existed is not null)
            return existed;

        entity = mapper.FromCommandDto(entity, dto) ?? new ArtistEntity();
        if (dto.CountryId is not null)
        {
            var country = await countryService.GetCountryEntityByIdAsync(dto.CountryId.GetValueOrDefault());
            if (country is not null)
                entity.Country = country;
        }
        else if (dto.Country is not null)
        {
            if (dto.Country?.Name is not null) 
            {
                var country = await countryService.GetCountryEntityByNameAsync(dto.Country.Name);
                if (country is not null)
                    entity.Country = country;
                else 
                {
                    var countryDto = await countryService.AddAsync(dto.Country);
                    if (countryDto is not null)
                        country = await countryService.GetCountryEntityByIdAsync(countryDto.Id);
                    if (country is not null)
                        entity.Country = country;
                }
            }
        }
        return entity;

    }
}
