namespace SyncHms.Bus.Services;

/// <summary>
/// Класс, описывающий построитель сервисов.
/// Реализует интерфейс <see cref="IServicesBusBuilder"/>
/// </summary>
/// <param name="services">Коллекция зависимостей приложения.</param>
public class ServicesBusBuilder(IServiceCollection services) : ApplicationBuilder(services), IServicesBusBuilder
{
    /// <summary>Имена зарегистрированных сервисов.</summary>
    private readonly HashSet<string> _serviceNames = [];
    
    /// <summary>Коллекция зависимостей приложения.</summary>
    private readonly IServiceCollection _services = services;

    /// <summary>
    /// Метод, регистрирующий в контейнере зависимостей сервис со временем жизни <see cref="ServiceLifetime.Transient"/>
    /// </summary>
    /// <typeparam name="TAbstract">Тип абстракции сервиса.</typeparam>
    /// <typeparam name="TImplement">Тип, имплементирующий сервис.</typeparam>
    /// <typeparam name="TOptions">Тип опций сервиса.</typeparam>
    /// <param name="options">Инициализация объекта опций сервиса.</param>
    public IServicesBusBuilder AddTransient<TAbstract, TImplement, TOptions>(Action<ServiceOptions> options)
        where TAbstract : class, IService<TOptions> where TImplement : class, TAbstract where TOptions : class, new()
    {
        _services.AddTransient<TAbstract, TImplement>();
        AddService<TAbstract, TOptions>(options);
        return this;
    }
    
    /// <summary>
    /// Метод, регистрирующий в контейнере зависимостей сервис со временем жизни <see cref="ServiceLifetime.Scoped"/>
    /// </summary>
    /// <typeparam name="TAbstract">Тип абстракции сервиса.</typeparam>
    /// <typeparam name="TImplement">Тип, имплементирующий сервис.</typeparam>
    /// <typeparam name="TOptions">Тип опций сервиса.</typeparam>
    /// <param name="options">Инициализация объекта опций сервиса.</param>
    public IServicesBusBuilder AddScoped<TAbstract, TImplement, TOptions>(Action<ServiceOptions> options)
        where TAbstract : class, IService<TOptions> where TImplement : class, TAbstract where TOptions : class, new()
    {
        _services.AddScoped<TAbstract, TImplement>();
        AddService<TAbstract, TOptions>(options);
        return this;
    }

    /// <summary>
    /// Метод, регистрирующий в контейнере зависимостей сервис со временем жизни <see cref="ServiceLifetime.Singleton"/>
    /// </summary>
    /// <typeparam name="TAbstract">Тип абстракции сервиса.</typeparam>
    /// <typeparam name="TImplement">Тип, имплементирующий сервис.</typeparam>
    /// <typeparam name="TOptions">Тип опций сервиса.</typeparam>
    /// <param name="options">Инициализация объекта опций сервиса.</param>
    public IServicesBusBuilder AddSingleton<TAbstract, TImplement, TOptions>(Action<ServiceOptions> options)
        where TAbstract : class, IService<TOptions> where TImplement : class, TAbstract where TOptions : class, new()
    {
        _services.AddSingleton<TAbstract, TImplement>();
        AddService<TAbstract, TOptions>(options);
        return this;
    }

    /// <summary>Метод, регистрирующий сервис в контейнере зависимостей.</summary>
    /// <typeparam name="TService">Тип сервиса.</typeparam>
    /// <typeparam name="TOptions">Тип опций сервиса.</typeparam>
    /// <param name="options">Инициализация объекта опций cервиса.</param>
    /// <exception cref="ArgumentException">
    /// Исключение, возбуждаемое, если параметр <b>options</b> равен <b>null</b>
    /// </exception>
    private void AddService<TService, TOptions>(Action<ServiceOptions> options)
        where TService : class, IService<TOptions> where TOptions : class, new()
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));

        var serviceOptions = new ServiceOptions();
        options.Invoke(serviceOptions);

        ArgumentException.ThrowIfNullOrWhiteSpace(serviceOptions.ServiceName, nameof(serviceOptions.ServiceName));

        if (!_serviceNames.Add(serviceOptions.ServiceName))
            throw new ArgumentException($"Service named {serviceOptions.ServiceName} has already been registered.", nameof(serviceOptions.ServiceName));

        _services
            .AddHostedService<ControlWorker<TService, TOptions>>()
            .AddSingleton<IControl<TOptions>, Control<TOptions>>()
            .AddSingleton(new ServiceOptions<TOptions>
            {
                ServiceName = serviceOptions.ServiceName
            });
    }
}

/// <summary>
/// Класс, описывающий построитель сервисов.
/// Реализует интерфейс <see cref="IServicesBusBuilder{TEnvironment}"/>
/// </summary>
/// <typeparam name="TEnvironment">Тип объекта, содержащего переменные окружения сервисов.</typeparam>
/// <param name="services">Коллекция зависимостей приложения.</param>
public class ServicesBusBuilder<TEnvironment>(IServiceCollection services) : ApplicationBuilder(services), IServicesBusBuilder<TEnvironment>
    where TEnvironment : class, new()
{
    /// <summary>Имена зарегистрированных сервисов.</summary>
    private readonly HashSet<string> _serviceNames = [];
    
    /// <summary>Коллекция зависимостей приложения.</summary>
    private readonly IServiceCollection _services = services;

    /// <summary>
    /// Метод, регистрирующий в контейнере зависимостей сервис со временем жизни <see cref="ServiceLifetime.Transient"/>
    /// </summary>
    /// <typeparam name="TAbstract">Тип абстракции сервиса.</typeparam>
    /// <typeparam name="TImplement">Тип, имплементирующий сервис.</typeparam>
    /// <typeparam name="TOptions">Тип опций сервиса.</typeparam>
    /// <param name="options">Инициализация объекта опций сервиса.</param>
    public IServicesBusBuilder<TEnvironment> AddTransient<TAbstract, TImplement, TOptions>(Action<ServiceOptions> options)
        where TAbstract : class, IService<TOptions, TEnvironment> where TImplement : class, TAbstract where TOptions : class, new()
    {
        _services.AddTransient<TAbstract, TImplement>();
        AddService<TAbstract, TOptions>(options);
        return this;
    }
    
    /// <summary>
    /// Метод, регистрирующий в контейнере зависимостей сервис со временем жизни <see cref="ServiceLifetime.Scoped"/>
    /// </summary>
    /// <typeparam name="TAbstract">Тип абстракции сервиса.</typeparam>
    /// <typeparam name="TImplement">Тип, имплементирующий сервис.</typeparam>
    /// <typeparam name="TOptions">Тип опций сервиса.</typeparam>
    /// <param name="options">Инициализация объекта опций сервиса.</param>
    public IServicesBusBuilder<TEnvironment> AddScoped<TAbstract, TImplement, TOptions>(Action<ServiceOptions> options)
        where TAbstract : class, IService<TOptions, TEnvironment> where TImplement : class, TAbstract where TOptions : class, new()
    {
        _services.AddScoped<TAbstract, TImplement>();
        AddService<TAbstract, TOptions>(options);
        return this;
    }
    
    /// <summary>
    /// Метод, регистрирующий в контейнере зависимостей сервис со временем жизни <see cref="ServiceLifetime.Singleton"/>
    /// </summary>
    /// <typeparam name="TAbstract">Тип абстракции сервиса.</typeparam>
    /// <typeparam name="TImplement">Тип, имплементирующий сервис.</typeparam>
    /// <typeparam name="TOptions">Тип опций сервиса.</typeparam>
    /// <param name="options">Инициализация объекта опций сервиса.</param>
    public IServicesBusBuilder<TEnvironment> AddSingleton<TAbstract, TImplement, TOptions>(Action<ServiceOptions> options)
        where TAbstract : class, IService<TOptions, TEnvironment> where TImplement : class, TAbstract where TOptions : class, new()
    {
        _services.AddSingleton<TAbstract, TImplement>();
        AddService<TAbstract, TOptions>(options);
        return this;
    }

    /// <summary>Метод, регистрирующий сервис в контейнере зависимостей.</summary>
    /// <typeparam name="TService">Тип сервиса.</typeparam>
    /// <typeparam name="TOptions">Тип опций сервиса.</typeparam>
    /// <param name="options">Инициализация объекта опций cервиса.</param>
    /// <exception cref="ArgumentException">
    /// Исключение, возбуждаемое, если параметр <b>options</b> равен <b>null</b>
    /// </exception>
    private void AddService<TService, TOptions>(Action<ServiceOptions> options)
        where TService : class, IService<TOptions, TEnvironment> where TOptions : class, new()
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));

        var serviceOptions = new ServiceOptions();
        options.Invoke(serviceOptions);

        ArgumentException.ThrowIfNullOrWhiteSpace(serviceOptions.ServiceName, nameof(serviceOptions.ServiceName));

        if (!_serviceNames.Add(serviceOptions.ServiceName))
            throw new ArgumentException($"Service named {serviceOptions.ServiceName} has already been registered.", nameof(serviceOptions.ServiceName));

        _services
            .AddHostedService<ControlWorker<TService, TOptions, TEnvironment>>()
            .AddSingleton<IControl<TOptions, TEnvironment>, Control<TOptions, TEnvironment>>()
            .AddSingleton(new ServiceOptions<TOptions>
            {
                ServiceName = serviceOptions.ServiceName
            });
    }
}
