namespace SyncHms.Bus.Services;

public static class BusBuilderExtensions
{
    private const string OutputConsoleTemplate = 
        "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {"
        + LoggerExtensions.Service + "} {Message}{NewLine}";

    private const string OutputFileTemplate = OutputConsoleTemplate + "{Exception}{NewLine}";

    public static IServicesBusBuilder AddServices(this IBusBuilder busBuilder,
        Action<ServiceBusOptions>? optionsAction = null)
    {
        busBuilder
            .AddSingleton<IServiceController, ServiceController>()
            .AddHostedService<ServiceControllerStarter>();
        
        if (optionsAction != null)
            AddServices(optionsAction);
        
        return new ServicesBusBuilder(busBuilder);
    }

    public static IServicesBusBuilder<TEnvironment> AddServices<TEnvironment>(this IBusBuilder busBuilder,
        Action<ServiceBusOptions>? optionsAction = null) where TEnvironment : class, new()
    {
        busBuilder
            .AddSingleton<IServiceController<TEnvironment>, ServiceController<TEnvironment>>()
            .AddHostedService<ServiceControllerStarter<TEnvironment>>();
        
        if (optionsAction != null)
            AddServices(optionsAction);
        
        return new ServicesBusBuilder<TEnvironment>(busBuilder);
    }

    private static void AddServices(Action<ServiceBusOptions> optionsAction)
    {
        var serviceBusOptions = new ServiceBusOptions();
        optionsAction.Invoke(serviceBusOptions);

        serviceBusOptions.LoggerConfiguration?.WriteTo.Logger(config => config
            .Filter.ByIncludingOnly(e => e.Properties.Keys.Contains(LoggerExtensions.Service))
            .WriteTo.Console(outputTemplate: OutputConsoleTemplate)
            .WriteTo.File(serviceBusOptions.LogFileName ?? "logs/services_.log",
                outputTemplate: OutputFileTemplate, rollingInterval: RollingInterval.Day));
    }
}
