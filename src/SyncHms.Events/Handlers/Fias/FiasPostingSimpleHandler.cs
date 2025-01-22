namespace SyncHms.Events.Handlers.Fias;

/// <summary>
/// Класс, описывающий обработчик события <see cref="FiasPostSimple"/>,
/// оповещающего о совершении денежного платежа.
/// </summary>
internal class FiasPostingSimpleHandler(IFiasService fiasService) : Handler<FiasPostSimple>
{
    /// <summary>
    /// Метод, обрабатывающий событие <see cref="FiasPostSimple"/>.
    /// Проводит попытку синхронизации платежа с бронированием через интерфейс <c>FIAS</c>.<br/>
    /// Переопределяет метод <see cref="Handler{TIn}.HandleAsync"/>
    /// </summary>
    /// <param name="in">Экземпляр обрабатываемого события.</param>
    /// <param name="context">Контекст обработки события.</param>
    protected override async Task HandleAsync(FiasPostSimple @in, IEventContext context)
    {
        var subtotals = fiasService.Environment.TaxCodes.Keys.Subtotals(@in.Transactions);
        var total = @in.Total * 100;
        
        try
        {
            var answer = await fiasService.SendPostingAsync(new FiasPostingSimple
            {
                RoomNumber = string.Empty,
                PostingType = FiasPostingTypes.DirectCharge,
                SalesOutlet = 100,
                TotalPostingAmount = total,
                DateTime = @in.DateTime,
                PmsPaymentMethod = @in.TransactionCode,
                CheckNumber = @in.CheckNumber,
                Subtotals = subtotals.Select(s => (decimal?)s).ToArray()
            });
            
            if (answer.AnswerStatus != FiasAnswerStatuses.Successfully)
            {
                context.Send(new PostTransactionsResponse(@in.CorrelationId)
                {
                    Succeeded = false,
                    ErrorMessage = answer.ClearText
                });
            }
            else
            {
                context.Send(@in.ToCheckDetails());
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
    protected override string Message(FiasPostSimple @in) => @in.ToString();
}
