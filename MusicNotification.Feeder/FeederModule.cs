using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MusicNotification.Common.Interfaces;
using MusicNotification.Feeder.FeedParser;
using MusicNotification.Feeder.FeedParser.FeedContentParser;
using MusicNotification.Feeder.Feeds.Application.Dtos;
using MusicNotification.Feeder.Feeds.Application.Services;
using MusicNotification.Feeder.Feeds.Repositories;
using MusicNotification.Feeder.ReleaseNotify;
using MusicNotification.Feeder.ReleaseNotify.EventHandlers;
using System.Reflection;

namespace MusicNotification.Feeder;

public static class FeederModule
{
    public static IServiceCollection AddFeederModule(this IServiceCollection services, Action<FeederModuleOptions> configureOptions)
    {
        var settings = new FeederModuleOptions();
        configureOptions(settings);

        services.Configure(configureOptions);

        services.AddDbContext<FeederDbContext>(options => options.UseNpgsql(settings.ConnectionStrings?.Default,
            sql =>
            {
                object value = sql.UseNodaTime();
            }));

        services.AddScoped<IFeedRepository, FeedRepository>();
        services.AddScoped<IFeedDtoMapper, FeedDtoMapper>();
        services.AddScoped<IFeedService, FeedService>();

        services.AddScoped<IFeedContentParserFabric, FeedContentParserFabric>();
        services.AddScoped<IFeedProcessor, FeedProcessor>();
        services.AddScoped<IReleaseNotification>(x => new ReleaseNotification(
            settings.TelegramRecepient,
            settings.FavoriteTags,
            x.GetService<IEventPublisher>()!,
            x.GetService<IFeedService>()!,
            x.GetService<IFeedProcessor>()!
        ));

        services.AddScoped<IReleaseNotifyEventHandler, ReleaseNotifyEventHandler>();

        services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()); });

        return services;
    }

    public static void MigrateFeederDb(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope();
        serviceScope?.ServiceProvider.GetRequiredService<FeederDbContext>().Database.Migrate();
    }
}
