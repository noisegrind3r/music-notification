using MusicNotification.Catalogs.Countries.Application.Dtos;
using MusicNotification.Catalogs.Countries.Domain;
using MusicNotification.Common.Interfaces;

namespace MusicNotification.Catalogs.Countries.Application.Services;

public interface ICountryService: IBaseService<CountryEntity, CountryQueryDto, CountryCommandDto>
{
    Task<CountryEntity?> GetCountryEntityByIdAsync(int countryId);
    Task<CountryEntity?> GetCountryEntityByNameAsync(string countryName);
}
