namespace SyncHms.Services;

/// <summary>
/// Метод, обрабатывающий сообщение типа <see cref="PostTransactionsRequest"/>
/// </summary>
public delegate void PostingRequestHandle(PostTransactionsRequest message);

/// <summary>
/// Интерфейс, определяющий методы сервиса интеграции с шиной данных <c>Sanatorium</c><br/>
/// Расширяет методы интерфейса <see cref="IService{TOptions,TEnvironment}"/>
/// </summary>
public interface ISanatoriumService : IService<SanatoriumOptions, ApplicationEnvironment>
{
    /// <summary>
    /// Событие, вызываемое при получении сервисом сообщения типа <see cref="PostingRequestEvent"/>,
    /// указывающее на совершение денежного платежа или начисления на комнату.
    /// </summary>
    event PostingRequestHandle? PostingRequestEvent;
    
    /// <summary>
    /// Метод, отправляющий сообщение типа <see cref="ReservationUpdatedMessage"/> 
    /// в шину данных <c>Sanatorium</c> для синхронизации данных бронирования.
    /// </summary>
    /// <param name="message">Тело сообщения.</param>
    Task SendReservationUpdatedMessageAsync(ReservationUpdatedMessage message);

    /// <summary>
    /// Метод, отправляющий сообщение типа <see cref="PostTransactionsResponse"/> 
    /// в шину данных <c>Sanatorium</c> в качестве ответ на сообщение события <see cref="PostingRequestEvent"/>
    /// </summary>
    /// <param name="message">Тело сообщения.</param>
    Task SendPostTransactionsResponseAsync(PostTransactionsResponse message);
    
    /// <summary>
    /// Метод, передающий сервису сообщение типа <see cref="PostTransactionsRequest"/>,
    /// полученное от обработчика <see cref="PostTransactionsRequestHandler"/>
    /// для вызова события <see cref="PostingRequestEvent"/>
    /// </summary>
    /// <param name="message">Тело сообщения.</param>
    /// <param name="context">Контекст обработки сообщения.</param>
    /// <param name="timeout">Таймаут в секундах на обработку сообщения и отправку ответа.</param>
    internal Task SendPostTransactionsRequestAsync(PostTransactionsRequest message,
        IMessageHandlerContext context, TimeSpan timeout);
}
