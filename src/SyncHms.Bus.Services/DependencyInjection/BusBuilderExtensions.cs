namespace SyncHms.Bus.Services;

/// <summary>Статический класс, который содержит методы расширения для интерфейса <see cref="IBusBuilder"/>.</summary>
public static class BusBuilderExtensions
{
    /// <summary>Шаблон логов, выводимых в консоль.</summary>
    private const string OutputConsoleTemplate = 
        "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {"
        + LoggerExtensions.Service + "} {Message}{NewLine}";

    /// <summary>Шаблон логов, записываемых в файл.</summary>
    private const string OutputFileTemplate = OutputConsoleTemplate + "{Exception}{NewLine}";

    /// <summary>Метод, регистрирующий контроллер сервисов в контейнере зависимостей.</summary>
    /// <param name="setupAction">Инициализация опций сервисов.</param>
    /// <returns>Экземпляр построителя, регистрирующего сервисы в контейнере зависимостей.</returns>
    public static IServicesBusBuilder AddServices(this IBusBuilder busBuilder,
        Action<ServiceBusOptions>? setupAction = null)
    {
        busBuilder
            .AddSingleton<IServiceController, ServiceController>()
            .AddHostedService<ServiceControllerWorker>();
        
        if (setupAction != null)
            AddServices(setupAction);
        
        return new ServicesBusBuilder(busBuilder);
    }

    /// <summary>Метод, регистрирующий контроллер сервисов в контейнере зависимостей.</summary>
    /// <typeparam name="TEnvironment">Тип объекта, содержащего пересенные окружения сервисов.</typeparam>
    /// <param name="setupAction">Инициализация опций сервисов.</param>
    /// <returns>Экземпляр построителя, регистрирующего сервисы в контейнере зависимостей.</returns>
    public static IServicesBusBuilder<TEnvironment> AddServices<TEnvironment>(this IBusBuilder busBuilder,
        Action<ServiceBusOptions>? setupAction = null) where TEnvironment : class, new()
    {
        busBuilder
            .AddSingleton<IServiceController<TEnvironment>, ServiceController<TEnvironment>>()
            .AddHostedService<ServiceControllerWorker<TEnvironment>>();
        
        if (setupAction != null)
            AddServices(setupAction);
        
        return new ServicesBusBuilder<TEnvironment>(busBuilder);
    }

    /// <summary>Метод конфигурирует логгер.</summary>
    /// <param name="setupAction">Инициализация опций сервисов.</param>
    private static void AddServices(Action<ServiceBusOptions> setupAction)
    {
        var serviceBusOptions = new ServiceBusOptions();
        setupAction.Invoke(serviceBusOptions);

        serviceBusOptions.LoggerConfiguration?.WriteTo.Logger(config => config
            .Filter.ByIncludingOnly(e => e.Properties.Keys.Contains(LoggerExtensions.Service))
            .WriteTo.Console(outputTemplate: OutputConsoleTemplate)
            .WriteTo.File(serviceBusOptions.LogFileName ?? "logs/services_.log",
                outputTemplate: OutputFileTemplate, rollingInterval: RollingInterval.Day));
    }
}
