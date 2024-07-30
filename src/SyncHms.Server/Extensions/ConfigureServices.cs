namespace SyncHms.Server.Extensions;

public static class ConfigureServices
{
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
                
                if (options.Sql?.ConnectionString != null)
                    busOptions.ConnectionString = options.Sql.ConnectionString;
                
                busOptions.ContractResolver = new DescriptionContractResolver();
            })
        };
    }

    public static IApplicationServicesBuilder AddServices(this IBusBuilder builder,
        WebApplicationBuilder appBuilder, LoggerConfiguration? loggerConfiguration = null)
    {
        var options = new BusProviderOptions();
        appBuilder.Configuration.Bind(BusProviderOptions.Section, options);
        
        return builder
            .AddApplicationServices(servicesOptions =>
            {
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
                
                if (options.Sql?.ConnectionString != null)
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
                identityOptions.Options.Password.RequiredUniqueChars = 0;
                identityOptions.Options.Password.RequireNonAlphanumeric = false;
                identityOptions.Options.Password.RequireLowercase = false;
                identityOptions.Options.Password.RequireUppercase = false;
                identityOptions.Options.Password.RequireDigit = false;
                
                if (options.Sql?.ConnectionString != null)
                    identityOptions.ConnectionString = options.Sql.ConnectionString;
            })
        };
    }

    public static WebApplication MapHubs(this WebApplication app)
    {
        app.MapHub<LogHub>("/logs");
        app.MapHub<ServiceHub>("/services");
        app.MapHub<EnvironmentHub>("/environment");
        return app;
    }
}
