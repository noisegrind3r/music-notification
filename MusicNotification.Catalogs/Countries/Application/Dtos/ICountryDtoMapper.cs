using MusicNotification.Catalogs.Countries.Domain;
using MusicNotification.Common.Interfaces;

namespace MusicNotification.Catalogs.Countries.Application.Dtos;

public interface ICountryDtoMapper: IDtoMapper<CountryEntity, CountryQueryDto, CountryCommandDto>
{
}
