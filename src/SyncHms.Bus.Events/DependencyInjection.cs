namespace SyncHms.Bus.Events;

public static class DependencyInjection
{
    private const string OutputConsoleTemplate = 
        "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {"
        + Extensions.LoggerExtensions.Queue + "} {" + Extensions.LoggerExtensions.Handler + "} {"
        + Extensions.LoggerExtensions.Task + "} {Message}{NewLine}";

    private const string OutputFileTemplate = OutputConsoleTemplate + "{Exception}{NewLine}";
    
    public static BusBuilder AddEvent<THandler, TIn>(this BusBuilder busBuilder, Action<HandlerOptions> action)
        where THandler : Handler<TIn>
    {
        ArgumentNullException.ThrowIfNull(action);
        var options = new HandlerOptions<THandler, TIn>()
        {
            UseEventLogging = true
        };
        action.Invoke(options);

        return busBuilder.AddHandler(options);
    }

    public static BusBuilder AddEventLog<THandler>(this BusBuilder busBuilder) where THandler : LogHandler
    {
        return busBuilder.AddHandler(new HandlerOptions<THandler, EventLog>());
    }

    public static BusBuilder ConfigureEvents(this BusBuilder busBuilder, Action<EventBusOptions>? options = null)
    {
        if (options == null)
            return busBuilder;
        
        var eventBusOptions = new EventBusOptions();
        options.Invoke(eventBusOptions);

        eventBusOptions.LoggerConfiguration?.WriteTo.Logger(config => config
            .Filter.ByIncludingOnly(e =>
                e.Properties.Keys.Contains(Extensions.LoggerExtensions.Queue) &&
                e.Properties.Keys.Contains(Extensions.LoggerExtensions.Handler) &&
                e.Properties.Keys.Contains(Extensions.LoggerExtensions.Task))
            .WriteTo.Console(outputTemplate: OutputConsoleTemplate)
            .WriteTo.File(eventBusOptions.LogFileName ?? "logs/events_.log",
                outputTemplate: OutputFileTemplate, rollingInterval: RollingInterval.Day));

        return busBuilder;
    }

    private static BusBuilder AddHandler<THandler, TIn>(this BusBuilder busBuilder,
        HandlerOptions<THandler, TIn> options) where THandler : HandlerBase<TIn>
    {
        busBuilder.Services
            .AddTransient<THandler>()
            .AddSingleton<IEventPublisher<TIn>, EventPublisher<TIn>>()
            .AddSingleton(options)
            .AddHostedService<HandlerStarter<THandler, TIn>>();

        return busBuilder;
    }
}
