namespace SyncHms.Services.Infrastructure;

/// <summary>
/// Класс, описывающий обработчик сообщений типа <see cref="ReservationUpdatedMessage"/>,
/// получинных из шины данных удаленного сервиса <c>Sanatorium</c><br/>
/// Реализует интерфейс <see cref="IHandleMessages{T}"/>
/// </summary>
internal class ReservationUpdatedMessageHandler : IHandleMessages<ReservationUpdatedMessage>
{
    /// <summary>
    /// Метод, обрабатывающий сообщения типа <see cref="ReservationUpdatedMessage"/><br/>
    /// Не производит никаких действий с сообщением. Предназначен для скрытия сообщении
    /// об отсутсвии обработчика сообщений типа <see cref="ReservationUpdatedMessage"/>
    /// </summary>
    /// <param name="message">Экземпляр обрабатываемого сообщения.</param>
    /// <param name="context">Контекст обработки сообщения.</param>
    public Task Handle(ReservationUpdatedMessage message, IMessageHandlerContext context)
    {
        return Task.CompletedTask;
    }
}
