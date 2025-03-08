using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MusicNotification.Catalogs.Albums.Application.Dtos;
using MusicNotification.Catalogs.Albums.Application.EventHandlers;
using MusicNotification.Catalogs.Albums.Application.Services;
using MusicNotification.Catalogs.Albums.Repositories;
using MusicNotification.Catalogs.Artists.Application.Dtos;
using MusicNotification.Catalogs.Artists.Application.Services;
using MusicNotification.Catalogs.Artists.Repositories;
using MusicNotification.Catalogs.Countries.Application.Dtos;
using MusicNotification.Catalogs.Countries.Application.Services;
using MusicNotification.Catalogs.Countries.Repositories;
using MusicNotification.Catalogs.Genries.Application.Dtos;
using MusicNotification.Catalogs.Genries.Application.Services;
using MusicNotification.Catalogs.Genries.Repositories;
using MusicNotification.Common.EventPublisher;
using System.Reflection;

namespace MusicNotification.Catalogs;

public static class CatalogsModule
{
    public static IServiceCollection AddCatalogsModule(this IServiceCollection services, Action<CatalogsModuleOptions> configureOptions)
    {
        var settings = new CatalogsModuleOptions();
        configureOptions(settings);

        services.Configure(configureOptions);

        services.AddDbContext<CatalogsDbContext>(options => options.UseNpgsql(settings.ConnectionStrings?.Default,
            sql =>
            {
                object value = sql.UseNodaTime();
            }));

        services.AddScoped<IGenreRepository, GenreRepository>();
        services.AddScoped<IGenreDtoMapper, GenreDtoMapper>();
        services.AddScoped<IGenreService, GenreService>();

        services.AddScoped<ICountryRepository, CountryRepository>();
        services.AddScoped<ICountryDtoMapper, CountryDtoMapper>();
        services.AddScoped<ICountryService, CountryService>();

        services.AddScoped<IArtistRepository, ArtistRepository>();
        services.AddScoped<IArtistDtoMapper, ArtistDtoMapper>();
        services.AddScoped<IArtistService, ArtistService>();

        services.AddScoped<IAlbumRepository, AlbumRepository>();
        services.AddScoped<IAlbumDtoMapper, AlbumDtoMapper>();
        services.AddScoped<IAlbumService, AlbumService>();

        services.AddScoped<IMusicDataAddedEventHandler, MusicDataAddedEventHandler>();

        services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()); });

        return services;
    }

    public static void MigrateCatalogsDb(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope();
        serviceScope?.ServiceProvider.GetRequiredService<CatalogsDbContext>().Database.Migrate();
    }
}
