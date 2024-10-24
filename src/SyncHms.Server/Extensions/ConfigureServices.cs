namespace SyncHms.Server.Extensions;

/// <summary>
/// Статический класс, который содержит методы расширения для сервисов регистрации зависимостей.
/// </summary>
public static class ConfigureServices
{
    /// <summary>Имя директории, хранящей данные приложения в базе данных <c>SQLite</c></summary>
    private const string DateDirectory = "data";

    private static readonly object Lock = new();
    
    /// <summary>Метод, регистрирующий сервисы для работы с шиной данных в контейнере зависимостей.</summary>
    /// <param name="appBuilder">Экземпляр построителя приложения.</param>
    /// <returns>Экземпляр построителя шины данных.</returns>
    public static IBusBuilder AddBus(this IServiceCollection services, WebApplicationBuilder appBuilder)
    {
        var options = new BusProviderOptions();
        appBuilder.Configuration.Bind(BusProviderOptions.Section, options);
        
        return options.Provider switch
        {
            _ => services.AddSqliteBus(busOptions =>
            {
                busOptions.UseMigrations = true;

                if (options.Provider == BusProvider.Sqlite)
                    busOptions.ConnectionString = GetConnectionString("bus");
                else if (options.Sql?.ConnectionString != null)
                    busOptions.ConnectionString = options.Sql.ConnectionString;
                
                busOptions.ContractResolver = new DescriptionContractResolver();
            })
        };
    }

    /// <summary>Метод, регистрирующий сервисы приложения в контейнере зависимостей.</summary>
    /// <param name="appBuilder">Экземпляр построителя приложения.</param>
    /// <param name="loggerConfiguration">Экземпляр конфигураций логгера.</param>
    /// <param name="configurationManager">
    /// Экземпляр менеджера конфигураций приложения.<br/>
    /// Инициализируется для получения опций сервисов и окружения из файла <c>appsettings.json</c>
    /// </param>
    /// <returns>Экземпляр построителя сервисов приложения.</returns>
    public static IApplicationServicesBuilder AddServices(this IBusBuilder builder,
        WebApplicationBuilder appBuilder, LoggerConfiguration? loggerConfiguration = null,
        ConfigurationManager? configurationManager = null)
    {
        var options = new BusProviderOptions();
        appBuilder.Configuration.Bind(BusProviderOptions.Section, options);
        
        return builder
            .AddApplicationServices(servicesOptions =>
            {
                servicesOptions.ConfigurationManager = configurationManager;
                servicesOptions.LogFileName = "logs/services_.log";
                
                if (loggerConfiguration != null)
                    servicesOptions.LoggerConfiguration = loggerConfiguration;
            });
    }
    
    /// <summary>Метод, регистрирующий сервис кеширования данных в контейнере зависимостей.</summary>
    /// <param name="appBuilder">Экземпляр построителя приложения.</param>
    /// <returns>Экземпляр построителя обработчиков событий.</returns>
    public static IApplicationEventsBuilder AddCache(this IApplicationServicesBuilder builder,
        WebApplicationBuilder appBuilder)
    {
        var options = new CacheProviderOptions();
        appBuilder.Configuration.Bind(CacheProviderOptions.Section, options);
        
        return options.Provider switch
        {
            _ => builder.AddCache<MemoryCache>()
        };
    }

    /// <summary>Метод, регистрирующий обработчики событий в контейнере зависимостей.</summary>
    /// <param name="appBuilder">Экземпляр построителя приложения.</param>
    /// <param name="loggerConfiguration">Экземпляр конфигураций логгера.</param>
    /// <returns>Экземпляр построителя обработчиков событий.</returns>
    public static IApplicationEventsBuilder AddEvents(this IApplicationEventsBuilder builder,
        WebApplicationBuilder appBuilder, LoggerConfiguration? loggerConfiguration = null)
    {
        var options = new BusProviderOptions();
        appBuilder.Configuration.Bind(BusProviderOptions.Section, options);
        
        var result = builder
            .AddApplicationEvents(eventsOptions =>
            {
                eventsOptions.LogFileName = "logs/events_.log";
                
                if (loggerConfiguration != null)
                    eventsOptions.LoggerConfiguration = loggerConfiguration;
            });

        builder
            .AddEventLog<UpdateLogHandler>()
            .AddUnloggedEvent<UpdatedEnvironmentHandler, ApplicationEnvironment>();

        var schedulerOptions = new SchedulerOptions();
        appBuilder.Configuration.Bind(SchedulerOptions.Section, schedulerOptions);
        builder.AddSingleton(schedulerOptions);

        return result;
    }
    
    /// <summary>
    /// Метод, регистрирующий репозитории для работы с данными приложения в контейнере зависимостей.
    /// </summary>
    /// <param name="appBuilder">Экземпляр построителя приложения.</param>
    /// <returns>Экземпляр построителя системы кеширования данных.</returns>
    public static ICacheBuilder AddDomain(this ICacheBuilder builder,
        WebApplicationBuilder appBuilder)
    {
        var options = new DomainProviderOptions();
        appBuilder.Configuration.Bind(DomainProviderOptions.Section, options);
        
        return options.Provider switch
        {
            _ => builder.AddSqliteDomain<ApplicationEnvironment>(domainOptions =>
            {
                domainOptions.UseMigrations = true;
                domainOptions.ContractResolver = new DescriptionContractResolver();
                
                if (options.Provider == DomainProvider.Sqlite)
                    domainOptions.ConnectionString = GetConnectionString("domain");
                else if (options.Sql?.ConnectionString != null)
                    domainOptions.ConnectionString = options.Sql.ConnectionString;
            })
        };
    }
    
    /// <summary>Метод, регистрирующий сервисы идентификации пользователей в контейнере зависимостей.</summary>
    /// <param name="appBuilder">Экземпляр построителя приложения.</param>
    /// <returns>Экземпляр построителя системы кеширования данных.</returns>
    public static ICacheBuilder AddIdentity(this ICacheBuilder builder,
        WebApplicationBuilder appBuilder)
    {
        var options = new IdentityProviderOptions();
        appBuilder.Configuration.Bind(IdentityProviderOptions.Section, options);
        
        return options.Provider switch
        {
            _ => builder.AddSqliteIdentity(identityOptions =>
            {
                identityOptions.UseMigrations = true;
                identityOptions.AccessTokenExpirationMinutes = 1;
                identityOptions.RefreshTokenExpirationDays = 7;
                identityOptions.SetupAction = setupOptions =>
                {
                    setupOptions.Password.RequiredUniqueChars = 0;
                    setupOptions.Password.RequireNonAlphanumeric = false;
                    setupOptions.Password.RequireLowercase = false;
                    setupOptions.Password.RequireUppercase = false;
                    setupOptions.Password.RequireDigit = false;
                };
                
                if (options.Provider == IdentityProvider.Sqlite)
                    identityOptions.ConnectionString = GetConnectionString("identity");
                else if (options.Sql?.ConnectionString != null)
                    identityOptions.ConnectionString = options.Sql.ConnectionString;
            })
        };
    }

    /// <summary>Метод, регистрирующий маршруты к концентраторам <c>SignalR</c></summary>
    public static WebApplication MapHubs(this WebApplication app)
    {
        app.MapHub<LogHub>("/hub/logs");
        app.MapHub<ServiceHub>("/hub/services");
        app.MapHub<EnvironmentHub>("/hub/environment");
        app.MapHub<ScheduleHub>("/hub/scheduler");
        return app;
    }

    /// <summary>Метод, формирующий строку подключения к базе данных <c>SQLite</c></summary>
    /// <param name="dbName">Имя базы данных.</param>
    /// <returns>Строка подключения к базе данных <c>SQLite</c></returns>
    private static string GetConnectionString(string dbName)
    {
        lock (Lock)
        {
            if (!Directory.Exists(DateDirectory))
                Directory.CreateDirectory(DateDirectory);

            var dateDirectory = Path.Combine(DateDirectory, dbName);
            if (!Directory.Exists(dateDirectory))
                Directory.CreateDirectory(dateDirectory);

            return $"Data Source={Path.Combine(dateDirectory, $"{dbName}.db")}";
        }
    }
}
