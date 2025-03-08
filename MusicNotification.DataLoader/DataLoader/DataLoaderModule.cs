using Microsoft.Extensions.DependencyInjection;
using MusicNotification.DataLoader.DataLoader.Import.Excel;
using MusicNotification.DataLoader.DataLoader.Service;
using System.Reflection;

namespace MusicNotification.DataLoader.DataLoader;

public static class DataLoaderModule
{
    public static IServiceCollection AddDataLoaderModule(this IServiceCollection services)
    {
        services.AddScoped<IDataLoaderService, DataLoaderService>();
        services.AddScoped<IImportMusicDataFromExcel, ImportMusicDataFromExcel>();


        services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()); });

        return services;
    }
}
