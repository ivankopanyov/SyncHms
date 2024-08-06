namespace SyncHms.Services;

public static class BusBuilderExtensions
{
    public static IApplicationServicesBuilder AddApplicationServices(this IBusBuilder servicesBusBuilder,
        Action<ServicesOptions>? setupAction = null)
    {
        var servicesOptions = new ServicesOptions();
        setupAction?.Invoke(servicesOptions);

        return servicesBusBuilder
            .AddServices<ApplicationEnvironment>(options =>
            {
                options.LoggerConfiguration = servicesOptions.LoggerConfiguration;
                options.LogFileName = servicesOptions.LogFileName;
            })
            .AddApplicationServices(options => options.ConfigurationManager = servicesOptions.ConfigurationManager);
    }
    
    public static IApplicationServicesBuilder AddApplicationServices(this IServicesBusBuilder<ApplicationEnvironment> servicesBusBuilder,
         Action<ApplicationServicesOptions>? setupAction = null)
    {
        var servicesOptions = new ApplicationServicesOptions();
        setupAction?.Invoke(servicesOptions);

        if (servicesOptions.ConfigurationManager != null)
        {
            var configurationManager = servicesOptions.ConfigurationManager;
            servicesBusBuilder.AddOptions<FiasServiceOptions>().Bind(configurationManager.GetSection(FiasServiceOptions.Section));
            servicesBusBuilder.AddOptions<OperaOptions>().Bind(configurationManager.GetSection(OperaOptions.Section));
            servicesBusBuilder.AddOptions<MicrosOptions>().Bind(configurationManager.GetSection(MicrosOptions.Section));
            servicesBusBuilder.AddOptions<SanatoriumOptions>().Bind(configurationManager.GetSection(SanatoriumOptions.Section));
            servicesBusBuilder.AddOptions<TelegramBotOptions>().Bind(configurationManager.GetSection(TelegramBotOptions.Section));
            servicesBusBuilder.AddOptions<ApplicationEnvironment>().Bind(configurationManager.GetSection(ApplicationEnvironment.Section));
        }

        servicesBusBuilder
            .AddSingleton<IFiasService, FiasService, FiasServiceOptions>(options => options.ServiceName = "Fias")
            .AddSingleton<ISanatoriumService, SanatoriumService, SanatoriumOptions>(options => options.ServiceName = "NServiceBus")
            .AddScoped<IOperaService, OperaService, OperaOptions>(options => options.ServiceName = "Oracle")
            .AddScoped<IFiscalService, FiscalService, MicrosOptions>(options => options.ServiceName = "CheckDB")
            .AddScoped<ITelegramBotService, TelegramBotService, TelegramBotOptions>(options => options.ServiceName = "TelegramBot")
            .AddCacheMemory()
            .AddSingleton<ISocketConnectionFactory, SocketConnectionFactory>();

        return new ApplicationServicesBuilder(servicesBusBuilder);
    }
}
