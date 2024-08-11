namespace SyncHms.Server.Extensions;

public static class ConfigureServices
{
    private const string _dateDirectory = "data";

    private static readonly object _lock = new();
    
    public static IBusBuilder AddBus(this IServiceCollection services,
        WebApplicationBuilder appBuilder)
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

        builder.AddEventLog<UpdateLogHandler>();
        return result;
    }
    
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

    public static WebApplication MapHubs(this WebApplication app)
    {
        app.MapHub<LogHub>("/hub/logs");
        app.MapHub<ServiceHub>("/hub/services");
        app.MapHub<EnvironmentHub>("/hub/environment");
        return app;
    }

    private static string GetConnectionString(string dbName)
    {
        lock (_lock)
        {
            if (!Directory.Exists(_dateDirectory))
                Directory.CreateDirectory(_dateDirectory);

            var dateDirectory = Path.Combine(_dateDirectory, dbName);
            if (!Directory.Exists(dateDirectory))
                Directory.CreateDirectory(dateDirectory);

            return $"Data Source={Path.Combine(dateDirectory, $"{dbName}.db")}";
        }
    }
}
