namespace SyncHms.Events.Handlers.Opera;

/// <summary>
/// Класс, описывающий обработчик события <see cref="CheckDetails"/>,
/// записывающего детали чека в базу данных <c>OPERA</c>.<br/>
/// Унаследован от класса <see cref="Handler{TIn}"/>
/// </summary>
internal class CheckDetailsHandler(IOperaService operaService) : Handler<CheckDetails>
{
    /// <summary>
    /// Метод, обрабатывающий событие <see cref="CheckDetails"/>.
    /// Проводит попытку записи деталей чека в базу данных <c>OPERA</c><br/>
    /// Переопределяет метод <see cref="Handler{TIn}.HandleAsync"/>
    /// </summary>
    /// <param name="in">Экземпляр обрабатываемого события.</param>
    /// <param name="context">Контекст обработки события.</param>
    protected override async Task HandleAsync(CheckDetails @in, IEventContext context)
    {
        try
        {
            await operaService.AddCheckDetails(new GuestCheckDetails
            {
                CheckNumber = $"{@in.CheckNumber}{@in.DateTime:yyyyMMddHHmmss}",
                CheckDetail = @in.ToCheck(operaService.Environment.CheckLocalizationCode, operaService.Environment.CheckHeader),
                CheckDate = @in.DateTime
            });

            Send(@in, context);
        }
        catch (Exception ex)
        {
            context.MarkWithError(ex.Message, ex.StackTrace);
            Send(@in, context);
        }
    }

    /// <summary>Метод отправления события в шину данных.</summary>
    /// <param name="in">Экземпляр обрабатываемого события.</param>
    /// <param name="context">Контекст обработки события.</param>
    private void Send(CheckDetails @in, IEventContext context)
    {
        if (operaService.Environment.SyncPostingMicros)
        {
            context.Send(@in.ToMicrosPostingRequest());
        }
        else
        {
            context.Send(new PostTransactionsResponse(@in.CorrelationId)
            {
                Succeeded = true,
                ErrorMessage = @in.ToString()
            });
        }
    }

    /// <summary>
    /// Метод, возвращающий краткое описание события <see cref="CheckDetails"/><br/>
    /// Переопределяет метод <see cref="Handler{TIn}.Message"/>
    /// </summary>
    /// <param name="in">Экземпляр обрабатываемого события.</param>
    /// <returns>Краткое описание события.</returns>
    protected override string Message(CheckDetails @in) => @in.ToString();
}
