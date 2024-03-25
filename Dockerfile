FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["NutriTrackerAPI/NutriTrackerAPI.csproj", "NutriTrackerAPI/"]
COPY ["Entities/Entities.csproj", "Entities/"]
COPY ["Repository/Repository.csproj", "Repository/"]
COPY ["Contracts/Contracts.csproj", "Contracts/"]
COPY ["Shared/Shared.csproj", "Shared/"]
COPY ["Services/Services.csproj", "Services/"]
COPY ["Controllers/Controllers.csproj", "Controllers/"]
RUN dotnet restore "NutriTrackerAPI/NutriTrackerAPI.csproj"
COPY . .
WORKDIR "/src/NutriTrackerAPI"
RUN dotnet build "NutriTrackerAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "NutriTrackerAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NutriTrackerAPI.dll"]
