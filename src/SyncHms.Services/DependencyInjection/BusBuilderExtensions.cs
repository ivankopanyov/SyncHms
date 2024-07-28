namespace SyncHms.Services;

public static class BusBuilderExtensions
{
    public static IApplicationServicesBuilder AddApplicationServices(this IBusBuilder servicesBusBuilder,
        Action<SyncHms.Bus.Services.ServiceBusOptions>? optionsAction = null)
    {
        servicesBusBuilder
            .AddServices<ApplicationEnvironment>(optionsAction)
            .AddSingleton<IFiasService, FiasService, FiasServiceOptions>(options =>
                options.ServiceName = "Fias")
            .AddSingleton<ISanatoriumService, SanatoriumService, ServiceBusOptions>(options =>
                options.ServiceName = "NServiceBus")
            .AddScoped<IOperaService, OperaService, OperaOptions>(options =>
                options.ServiceName = "Oracle")
            .AddScoped<IFiscalService, FiscalService, CheckDbOptions>(options =>
                options.ServiceName = "CheckDB")
            .AddScoped<ITelegramBotService, TelegramBotService, TelegramBotOptions>(options =>
                options.ServiceName = "TelegramBot")
            .Services.AddCacheMemory();

        return new ApplicationServicesBuilder(servicesBusBuilder.Services);
    }
}