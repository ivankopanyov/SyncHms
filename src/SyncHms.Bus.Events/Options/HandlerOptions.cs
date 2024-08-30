namespace SyncHms.Bus.Events;

/// <summary>Абстрактный класс, описывающий базовую модель опций обработчика событий.</summary>
public abstract class HandlerOptions
{
    /// <summary>Имя задачи. Задается только для стартового обработчика в задаче.</summary>
    public string TaskName { get; set; }

    /// <summary>Имя обработчика.</summary>
    public string HandlerName { get; set; }

    /// <summary>Свойство определяет, нужно ли отправлять логи в обработчик логов.</summary>
    internal bool UseEventLogging { get; set; }
}

/// <summary>
/// Класс, описывающий модель опций обработчика событий.
/// Унаследован от класса <see cref="HandlerOptions"/>
/// </summary>
/// <typeparam name="THandler">Тип обработчика.</typeparam>
/// <typeparam name="TIn">Тип сообщения.</typeparam>
public class HandlerOptions<THandler, TIn>
    : HandlerOptions where THandler : HandlerBase<TIn>
{
}
