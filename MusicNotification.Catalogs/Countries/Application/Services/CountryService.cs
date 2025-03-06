using MusicNotification.Catalogs.Countries.Application.Dtos;
using MusicNotification.Catalogs.Countries.Domain;
using MusicNotification.Catalogs.Countries.Repositories;
using MusicNotification.Common.Services;

namespace MusicNotification.Catalogs.Countries.Application.Services;

public class CountryService(ICountryRepository repository, ICountryDtoMapper mapper): BaseService<CountryEntity, CountryQueryDto, CountryCommandDto>(repository, mapper), ICountryService
{
    public async Task<CountryEntity?> GetCountryEntityByIdAsync(int countryId)
    {
        return await repository.GetByIdAsync(countryId);
    }
    public async Task<CountryEntity?> GetCountryEntityByNameAsync(string countryName)
    {
        return await repository.FirstOrDefaultAsync(repository.GetAll().Where(x => x.Name != null && x.Name.Equals(countryName)));
    }
}
