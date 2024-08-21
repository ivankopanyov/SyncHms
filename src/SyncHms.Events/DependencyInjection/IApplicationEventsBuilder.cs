namespace SyncHms.Events;

/// <summary>
/// Интерфейс, определяющий методы построителя, регистрирующего обработчики событий в контейнере зависимостей.
/// </summary>
public interface IApplicationEventsBuilder : IEventsBusBuilder, ICacheBuilder
{
    /// <summary>Метод, регистрирующий обработчики событий в контейнере зависимостей.</summary>
    /// <param name="setupAction">Инициализация опций сервиса обработки событий.</param>
    IApplicationEventsBuilder AddApplicationEvents(Action<EventBusOptions>? setupAction = null);
}