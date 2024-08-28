namespace SyncHms.Bus.Events;

/// <summary>Статический класс, который содержит методы расширения для интерфейса <see cref="IBusBuilder"/>.</summary>
public static class BusBuilderExtensions
{
    /// <summary>Шаблон логов, выводимых в консоль.</summary>
    private const string OutputConsoleTemplate = 
        "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {"
        + Extensions.LoggerExtensions.Queue + "} {" + Extensions.LoggerExtensions.Handler + "} {"
        + Extensions.LoggerExtensions.Task + "} {Message}{NewLine}";

    /// <summary>Шаблон логов, записываемых в файл.</summary>
    private const string OutputFileTemplate = OutputConsoleTemplate + "{Exception}{NewLine}";
    
    /// <summary>Метод, регистрирующий сервисы обработки событий из шины данных.</summary>
    /// <param name="options">Инициализация опций обработчиков событий.</param>
    /// <returns>Экземпляр построителя обработки событий для регистрации обработчиков в контейнере зависимостей.</returns>
    public static IEventsBusBuilder AddEvents(this IBusBuilder busBuilder, Action<EventBusOptions>? options = null)
    {
        if (options == null)
            return new EventsBusBuilder(busBuilder);
        
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

        return new EventsBusBuilder(busBuilder);
    }
}
