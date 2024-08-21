namespace SyncHms.Services.Infrastructure;

/// <summary>
/// Класс, описывающий обработчик сообщений типа <see cref="PostTransactionsRequest"/>,
/// получинных из шины данных удаленного сервиса <c>Sanatorium</c><br/>
/// Реализует интерфейс <see cref="IHandleMessages{T}"/>
/// </summary>
/// <param name="sanatoriumService">
/// Экземпляр сервиса интеграции с шиной данных удаленного сервиса <c>Sanatorium</c>
/// </param>
internal class PostTransactionsRequestHandler(ISanatoriumService sanatoriumService)
    : IHandleMessages<PostTransactionsRequest>
{
    /// <summary>
    /// Метод, обрабатывающий сообшения типа <see cref="PostTransactionsRequest"/><br/>
    /// Перенаправляет полученное сообщение в сервис <see cref="ISanatoriumService"/>
    /// </summary>
    /// <param name="message">Экземпляр обрабатываемого сообщения.</param>
    /// <param name="context">Контекст обработки сообщения.</param>
    public async Task Handle(PostTransactionsRequest message, IMessageHandlerContext context)
    {
        await sanatoriumService.SendPostTransactionsRequestAsync(message, context, TimeSpan.FromSeconds(90));
    }
}
