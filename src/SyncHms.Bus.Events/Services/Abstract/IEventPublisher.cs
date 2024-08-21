namespace SyncHms.Bus.Events;

/// <summary>Делегат, описывающий метод, вызываемый при публикации события в шину данных.</summary>
/// <typeparam name="TIn">Тип сообщения.</typeparam>
internal delegate Task PublishHandleAsync<in TIn>(TIn @in);

/// <summary>Интерфейс, определяющий методы публикации событий в шину данных.</summary>
/// <typeparam name="TIn">Тип сообщения.</typeparam>
public interface IEventPublisher<TIn>
{
    /// <summary>Событие, вызываемое при публикации в шину данных.</summary>
    internal event PublishHandleAsync<TIn>? PublishEvent;

    /// <summary>Метод, публикующий событие в шину данныx.</summary>
    /// <param name="in">Экземпляр сообщения.</param>
    void Publish(TIn @in);
}
