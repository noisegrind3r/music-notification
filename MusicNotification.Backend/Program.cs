using MusicNotification.Catalogs;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using MusicNotification.Common.EventPublisher;
using MusicNotification.Common.Interfaces;
using MusicNotification.DataLoader.DataLoader;
using MusicNotification.Notification;
using MusicNotification.Feeder;
using MusicNotification.Scheduler;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddOpenApi();

var services = builder.Services;

services.AddCors(policyBuilder =>
    policyBuilder.AddDefaultPolicy(policy =>
        policy.WithOrigins("*").AllowAnyHeader().AllowAnyMethod())
);

services.AddEndpointsApiExplorer();
services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();

    options.OrderActionsBy(apiDesc =>
        $"{apiDesc.ActionDescriptor.RouteValues["controller"]}_{apiDesc.RelativePath}_{apiDesc.HttpMethod}");
});

services.AddCatalogsModule(opt => configuration.GetSection("Modules:Catalogs").Bind(opt));
services.AddDataLoaderModule();
services.AddNotificationModule(opt => configuration.GetSection("Modules:Notification").Bind(opt));
services.AddFeederModule(opt => configuration.GetSection("Modules:Feeder").Bind(opt));
services.AddSchedulerModule();

services.AddScoped<IEventPublisher, EventPublisher>();

services.AddJobsSchedulerModule();

builder.Services.AddControllers();

// Serialize Enums as strings
services.Configure<JsonOptions>(opt =>
{
    opt.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
    opt.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});


var app = builder.Build();

app.MigrateCatalogsDb();
app.MigrateFeederDb();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors();
// app.UseHttpsRedirection();

app.UseExceptionHandler("/error");

app.UseAuthorization();

app.MapControllers();

app.Run();
