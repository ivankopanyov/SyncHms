namespace SyncHms.Bus.Services;

/// <summary>Интерфейс, определяющий методы сервиса.</summary>
/// <typeparam name="TOptions">
/// Тип опций сервиса.
/// Должен содержать открытый конструктор без параметров.
/// </typeparam>
public interface IService<TOptions> where TOptions : class, new()
{
    Task ChangedOptionsHandleAsync(TOptions options);
}

/// <summary>
/// Интерфейс, определяющий методы сервиса.<br/>
/// Расширяет методы интерфейса <see cref="IService{TOptions}"/>
/// </summary>
/// <typeparam name="TOptions">
/// Тип опций сервиса.
/// Должен содержать открытый конструктор без параметров.
/// </typeparam>
/// <typeparam name="TEnvironment">
/// Тип окружения.
/// Должен содержать открытый конструктор без параметров.
/// </typeparam>
public interface IService<TOptions, TEnvironment> : IService<TOptions>
    where TOptions : class, new() where TEnvironment : class, new()
{
    /// <summary>Экземпляр окружения.</summary>
    TEnvironment Environment { get; }

    /// <summary>Метод, обрабатывающий изменение значений переменных окружения.</summary>
    /// <param name="current">Текущий экземпляр окружения.</param>
    /// <param name="previous">Измененный экземпляр окружения.</param>
    Task ChangedEnvironmentHandleAsync(TEnvironment current, TEnvironment previous);
}
