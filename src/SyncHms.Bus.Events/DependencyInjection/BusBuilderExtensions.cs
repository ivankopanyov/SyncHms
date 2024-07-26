namespace SyncHms.Bus.Events;

public static class BusBuilderExtensions
{
    private const string OutputConsoleTemplate = 
        "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {"
        + Extensions.LoggerExtensions.Queue + "} {" + Extensions.LoggerExtensions.Handler + "} {"
        + Extensions.LoggerExtensions.Task + "} {Message}{NewLine}";

    private const string OutputFileTemplate = OutputConsoleTemplate + "{Exception}{NewLine}";
    
    public static IEventsBusBuilder AddEvents(this IBusBuilder busBuilder, Action<EventBusOptions>? options = null)
    {
        if (options == null)
            return new EventsBusBuilder(busBuilder.Services);
        
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

        return new EventsBusBuilder(busBuilder.Services);
    }
}
