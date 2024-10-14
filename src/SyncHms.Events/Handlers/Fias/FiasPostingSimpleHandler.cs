namespace SyncHms.Events.Handlers.Fias;

/// <summary>
/// Класс, описывающий обработчик события <see cref="FiasPostSimple"/>,
/// оповещающего о совершении денежного платежа.<br/>
/// Унаследован от класса <see cref="PostingHandler{TIn}"/>
/// </summary>
internal class FiasPostingSimpleHandler(IFiasService fiasService)
    : PostingHandler<FiasPostSimple>(fiasService)
{
    /// <summary>
    /// Метод, обрабатывающий событие <see cref="FiasPostSimple"/>.
    /// Проводит попытку синхронизации платежа с бронированием через интерфейс <c>FIAS</c>.<br/>
    /// В случае неудачной попытки синхронизации или если параметр окружения
    /// <see cref="ApplicationEnvironment.SyncPostingMicros"/> равен <c>false</c>,
    /// отправляет в шину данных событие <see cref="PostTransactionsResponse"/>.<br/>
    /// В противном случае отправляет в шину данных событие <see cref="Check"/>.<br/>
    /// Переопределяет метод <see cref="Handler{TIn}.HandleAsync"/>
    /// </summary>
    /// <param name="in">Экземпляр обрабатываемого события.</param>
    /// <param name="context">Контекст обработки события.</param>
    protected override async Task HandleAsync(FiasPostSimple @in, IEventContext context)
    {
        var dateTime = DateTime.Now;
        var subtotals = @in.Checks.Select(c => c.Select(i => i.Total).Sum() * 100).ToArray();
        
        try
        {
            var answer = await fiasService.SendPostingAsync(new FiasPostingSimple
            {
                RoomNumber = string.Empty,
                PostingType = FiasPostingTypes.DirectCharge,
                SalesOutlet = 100,
                TotalPostingAmount = @in.Total,
                DateTime = dateTime,
                PmsPaymentMethod = @in.PmsPaymentMethod,
                CheckNumber = @in.CheckNumber,
                Subtotals = subtotals.Select(s => (decimal?)s).ToArray()
            });

            var success = answer.AnswerStatus == FiasAnswerStatuses.Successfully;

            if (!success || !fiasService.Environment.SyncPostingMicros)
                context.Send(new PostTransactionsResponse(@in.CorrelationId)
                {
                    Succeeded = success,
                    ErrorMessage = !success ? answer.ClearText : null
                });
            else
            {
                var checkNumber = GetCheckNumber(context);
                
                context.Send(new Check
                {
                    CorrelationId = @in.CorrelationId,
                    DateTime = dateTime,
                    Total = @in.Total.ToString(),
                    CheckNumber = checkNumber,
                    ExternalCheckNumber = @in.CheckNumber,
                    Details = @in.Checks.SelectMany(c => c)
                });
            }
        }
        catch (Exception ex)
        {
            context.Send(new PostTransactionsResponse(@in.CorrelationId)
            {
                Succeeded = false,
                ErrorMessage = ex.Message
            });
        }
    }

    /// <summary>
    /// Метод, возвращающий краткое описание события <see cref="FiasPostSimple"/><br/>
    /// Переопределяет метод <see cref="Handler{TIn}.Message"/>
    /// </summary>
    /// <param name="in">Экземпляр обрабатываемого события.</param>
    /// <returns>Краткое описание события.</returns>
    protected override string Message(FiasPostSimple @in) =>
        $"CheckNumber: {@in.CheckNumber}, Total: {@in.Total / 100:0.00}, PaymentMethod: {@in.PmsPaymentMethod}";
}
