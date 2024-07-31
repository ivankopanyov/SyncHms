namespace SyncHms.Services;

public static class BusBuilderExtensions
{
    public static IApplicationServicesBuilder AddApplicationServices(this IBusBuilder servicesBusBuilder,
        Action<ServiceBusOptions>? optionsAction = null)
    {
        return servicesBusBuilder
            .AddServices<ApplicationEnvironment>(optionsAction)
            .AddApplicationServices();
    }
    
    public static IApplicationServicesBuilder AddApplicationServices(this IServicesBusBuilder<ApplicationEnvironment> servicesBusBuilder)
    {
        servicesBusBuilder
            .AddSingleton<IFiasService, FiasService, FiasServiceOptions>(options =>
                options.ServiceName = "Fias")
            .AddSingleton<ISanatoriumService, SanatoriumService, SanatoriumOptions>(options =>
                options.ServiceName = "NServiceBus")
            .AddScoped<IOperaService, OperaService, OperaOptions>(options =>
                options.ServiceName = "Oracle")
            .AddScoped<IFiscalService, FiscalService, CheckDbOptions>(options =>
                options.ServiceName = "CheckDB")
            .AddScoped<ITelegramBotService, TelegramBotService, TelegramBotOptions>(options =>
                options.ServiceName = "TelegramBot")
            .AddCacheMemory()
            .AddSingleton<ISocketConnectionFactory, SocketConnectionFactory>();

        return new ApplicationServicesBuilder(servicesBusBuilder);
    }
}
