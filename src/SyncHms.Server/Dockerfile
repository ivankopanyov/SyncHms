FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /synchms
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/SyncHms.Server/SyncHms.Server.csproj", "src/SyncHms.Server/"]
COPY ["src/SyncHms.Identity/SyncHms.Identity.csproj", "src/SyncHms.Identity/"]
COPY ["src/SyncHms.Cache/SyncHms.Cache.csproj", "src/SyncHms.Cache/"]
COPY ["src/SyncHms.DependencyInjection/SyncHms.DependencyInjection.csproj", "src/SyncHms.DependencyInjection/"]
COPY ["src/SyncHms.Fias.Entities/SyncHms.Fias.Entities.csproj", "src/SyncHms.Fias.Entities/"]
COPY ["src/SyncHms.OzLocks.Entities/SyncHms.OzLocks.Entities.csproj", "src/SyncHms.OzLocks.Entities/"]
COPY ["src/SyncHms.Domain.EntityFramework/SyncHms.Domain.EntityFramework.csproj", "src/SyncHms.Domain.EntityFramework/"]
COPY ["src/SyncHms.Domain/SyncHms.Domain.csproj", "src/SyncHms.Domain/"]
COPY ["src/SyncHms.Opera.Entities.Tables/SyncHms.Opera.Entities.Tables.csproj", "src/SyncHms.Opera.Entities.Tables/"]
COPY ["src/SyncHms.CheckDB/SyncHms.CheckDB.csproj", "src/SyncHms.CheckDB/"]
COPY ["src/SyncHms.Cache.Memory/SyncHms.Cache.Memory.csproj", "src/SyncHms.Cache.Memory/"]
COPY ["src/SyncHms.Bus.Services/SyncHms.Bus.Services.csproj", "src/SyncHms.Bus.Services/"]
COPY ["src/SyncHms.Bus/SyncHms.Bus.csproj", "src/SyncHms.Bus/"]
COPY ["src/SyncHms.Bus.Events/SyncHms.Bus.Events.csproj", "src/SyncHms.Bus.Events/"]
COPY ["src/SyncHms.Bus.EntityFramework/SyncHms.Bus.EntityFramework.csproj", "src/SyncHms.Bus.EntityFramework/"]
COPY ["src/SyncHms.Services/SyncHms.Services.csproj", "src/SyncHms.Services/"]
COPY ["src/SyncHms.Identity.Sqlite/SyncHms.Identity.Sqlite.csproj", "src/SyncHms.Identity.Sqlite/"]
COPY ["src/SyncHms.Events/SyncHms.Events.csproj", "src/SyncHms.Events/"]
COPY ["src/SyncHms.Domain.EntityFramework.Sqlite/SyncHms.Domain.EntityFramework.Sqlite.csproj", "src/SyncHms.Domain.EntityFramework.Sqlite/"]
COPY ["src/SyncHms.Bus.EntityFramework.Sqlite/SyncHms.Bus.EntityFramework.Sqlite.csproj", "src/SyncHms.Bus.EntityFramework.Sqlite/"]
RUN dotnet restore "src/SyncHms.Server/SyncHms.Server.csproj"
COPY . .
WORKDIR "/src/src/SyncHms.Server"
RUN dotnet build "SyncHms.Server.csproj" -c $BUILD_CONFIGURATION -o /synchms/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "SyncHms.Server.csproj" -c $BUILD_CONFIGURATION -o /synchms/publish /p:UseAppHost=false

FROM base AS final
ENV TZ=Europe/Moscow
ENV LANG=ru_RU.UTF-8
ENV LANGUAGE=ru_RU.UTF-8
ENV LC_ALL=ru_RU.UTF-8
WORKDIR /synchms
COPY --from=publish /synchms/publish .
ENTRYPOINT ["dotnet", "SyncHms.Server.dll"]
