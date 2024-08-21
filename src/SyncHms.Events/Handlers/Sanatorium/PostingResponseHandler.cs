namespace SyncHms.Events.Handlers.Sanatorium;

/// <summary>
/// Класс, описывающий обработчик события <see cref="PostTransactionsResponse"/>,
/// оповещающего о совершении денежного платежа или начислении платежа на номер.<br/>
/// Унаследован от класса <see cref="Handler{TIn}"/>
/// </summary>
internal class PostingResponseHandler(ISanatoriumService sanatoriumService) : Handler<PostTransactionsResponse>
{
    /// <summary>
    /// Метод, обрабатывающий событие <see cref="PostTransactionsResponse"/>.<br/>
    /// Отправляет в шину данных сервиса <c>Sanatorium</c> событие <see cref="PostTransactionsResponse"/>.<br/>
    /// В случае неудачной попытки синхронизации платежа завершает выполнение задачи с ошибкой.<br/>
    /// Переопределяет метод <see cref="Handler{TIn}.HandleAsync"/>
    /// </summary>
    /// <param name="in">Экземпляр обрабатываемого события.</param>
    /// <param name="context">Контекст обработки события.</param>
    protected override async Task HandleAsync(PostTransactionsResponse @in, IEventContext context)
    {
        try
        {
            await sanatoriumService.SendPostTransactionsResponseAsync(@in);

            if (!@in.Succeeded)
                context.Break(@in.ErrorMessage);
        }
        catch (Exception ex)
        {
            context.Break(ex.Message, ex);
        }
    }
    
    /// <summary>
    /// Метод, возвращающий краткое описание события <see cref="PostTransactionsResponse"/><br/>
    /// Переопределяет метод <see cref="Handler{TIn}.Message"/>
    /// </summary>
    /// <param name="in">Экземпляр обрабатываемого события.</param>
    /// <returns>Краткое описание события.</returns>
    protected override string? Message(PostTransactionsResponse @in)
    {
        var result = @in.Succeeded ? "Succeeded" : "Error";
        
        if (!string.IsNullOrWhiteSpace(@in.ErrorMessage))
            result += $": {@in.ErrorMessage}";

        return result;
    }
}
