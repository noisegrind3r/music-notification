# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MusicNotification.Backend/MusicNotification.Backend.csproj", "MusicNotification.Backend/"]
COPY ["MusicNotification.DataLoader/MusicNotification.DataLoader.csproj", "MusicNotification.DataLoader/"]
COPY ["MusicNotification.Common/MusicNotification.Common.csproj", "MusicNotification.Common/"]
COPY ["MusicNotification.Events/MusicNotification.Events.csproj", "MusicNotification.Events/"]
COPY ["MusicNotification.Catalogs/MusicNotification.Catalogs.csproj", "MusicNotification.Catalogs/"]
COPY ["MusicNotification.Feeder/MusicNotification.Feeder.csproj", "MusicNotification.Feeder/"]
COPY ["MusicNotification.Notification/MusicNotification.Notification.csproj", "MusicNotification.Notification/"]
COPY ["MusicNotification.Scheduler/MusicNotification.Scheduler.csproj", "MusicNotification.Scheduler/"]
RUN dotnet restore "./MusicNotification.Backend/MusicNotification.Backend.csproj"
COPY . .
WORKDIR "/src/MusicNotification.Backend"
RUN dotnet build "./MusicNotification.Backend.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MusicNotification.Backend.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MusicNotification.Backend.dll"]