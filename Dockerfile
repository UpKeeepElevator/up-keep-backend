FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["UpKeep.Api/UpKeep.Api.csproj", "UpKeep.Api/"]
COPY ["UpKeep.Entities/UpKeep.Entities.csproj", "UpKeep.Entities/"]
COPY ["UpKepp.Services/UpKepp.Services.csproj", "UpKepp.Services/"]
COPY ["UpKeep.Data/UpKeep.Data.csproj", "UpKeep.Data/"]
RUN dotnet restore "UpKeep.Api/UpKeep.Api.csproj"
COPY . .
WORKDIR "/src/UpKeep.Api"
RUN dotnet build "UpKeep.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "UpKeep.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UpKeep.Api.dll"]
