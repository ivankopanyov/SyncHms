namespace SyncHms.Bus.Services;

/// <summary>Интерфейс, содержащий методы для регистрации сервисов.</summary>
public interface IServicesBusBuilder : IBusBuilder
{
    /// <summary>
    /// Метод, регистрирующий в контейнере зависимостей сервис со временем жизни <see cref="ServiceLifetime.Transient"/>
    /// </summary>
    /// <typeparam name="TAbstract">Тип абстракции сервиса.</typeparam>
    /// <typeparam name="TImplement">Тип, имплементирующий сервис.</typeparam>
    /// <typeparam name="TOptions">Тип опций сервиса.</typeparam>
    /// <param name="options">Инициализация объекта опций сервиса.</param>
    IServicesBusBuilder AddTransient<TAbstract, TImplement, TOptions>(Action<ServiceOptions> options)
        where TAbstract : class, IService<TOptions>
        where TImplement : class, TAbstract
        where TOptions : class, new();

    /// <summary>
    /// Метод, регистрирующий в контейнере зависимостей сервис со временем жизни <see cref="ServiceLifetime.Scoped"/>
    /// </summary>
    /// <typeparam name="TAbstract">Тип абстракции сервиса.</typeparam>
    /// <typeparam name="TImplement">Тип, имплементирующий сервис.</typeparam>
    /// <typeparam name="TOptions">Тип опций сервиса.</typeparam>
    /// <param name="options">Инициализация объекта опций сервиса.</param>
    IServicesBusBuilder AddScoped<TAbstract, TImplement, TOptions>(Action<ServiceOptions> options)
        where TAbstract : class, IService<TOptions>
        where TImplement : class, TAbstract
        where TOptions : class, new();

    /// <summary>
    /// Метод, регистрирующий в контейнере зависимостей сервис со временем жизни <see cref="ServiceLifetime.Singleton"/>
    /// </summary>
    /// <typeparam name="TAbstract">Тип абстракции сервиса.</typeparam>
    /// <typeparam name="TImplement">Тип, имплементирующий сервис.</typeparam>
    /// <typeparam name="TOptions">Тип опций сервиса.</typeparam>
    /// <param name="options">Инициализация объекта опций сервиса.</param>
    IServicesBusBuilder AddSingleton<TAbstract, TImplement, TOptions>(Action<ServiceOptions> options)
        where TAbstract : class, IService<TOptions>
        where TImplement : class, TAbstract
        where TOptions : class, new();
}

/// <summary>Интерфейс, содержащий методы для регистрации сервисов.</summary>
/// <typeparam name="TEnvironment">Тип объекта, содержащего переменные окружения сервисов.</typeparam>
public interface IServicesBusBuilder<TEnvironment> : IBusBuilder where TEnvironment : class, new()
{
    /// <summary>
    /// Метод, регистрирующий в контейнере зависимостей сервис со временем жизни <see cref="ServiceLifetime.Transient"/>
    /// </summary>
    /// <typeparam name="TAbstract">Тип абстракции сервиса.</typeparam>
    /// <typeparam name="TImplement">Тип, имплементирующий сервис.</typeparam>
    /// <typeparam name="TOptions">Тип опций сервиса.</typeparam>
    /// <param name="options">Инициализация объекта опций сервиса.</param>
    IServicesBusBuilder<TEnvironment> AddTransient<TAbstract, TImplement, TOptions>(Action<ServiceOptions> options)
        where TAbstract : class, IService<TOptions, TEnvironment>
        where TImplement : class, TAbstract
        where TOptions : class, new();

    /// <summary>
    /// Метод, регистрирующий в контейнере зависимостей сервис со временем жизни <see cref="ServiceLifetime.Scoped"/>
    /// </summary>
    /// <typeparam name="TAbstract">Тип абстракции сервиса.</typeparam>
    /// <typeparam name="TImplement">Тип, имплементирующий сервис.</typeparam>
    /// <typeparam name="TOptions">Тип опций сервиса.</typeparam>
    /// <param name="options">Инициализация объекта опций сервиса.</param>
    IServicesBusBuilder<TEnvironment> AddScoped<TAbstract, TImplement, TOptions>(Action<ServiceOptions> options)
        where TAbstract : class, IService<TOptions, TEnvironment>
        where TImplement : class, TAbstract
        where TOptions : class, new();

    /// <summary>
    /// Метод, регистрирующий в контейнере зависимостей сервис со временем жизни <see cref="ServiceLifetime.Singleton"/>
    /// </summary>
    /// <typeparam name="TAbstract">Тип абстракции сервиса.</typeparam>
    /// <typeparam name="TImplement">Тип, имплементирующий сервис.</typeparam>
    /// <typeparam name="TOptions">Тип опций сервиса.</typeparam>
    /// <param name="options">Инициализация объекта опций сервиса.</param>
    IServicesBusBuilder<TEnvironment> AddSingleton<TAbstract, TImplement, TOptions>(Action<ServiceOptions> options)
        where TAbstract : class, IService<TOptions, TEnvironment>
        where TImplement : class, TAbstract
        where TOptions : class, new();
}
