namespace SyncHms.Events.Handlers.Fias;

/// <summary>
/// Класс, описывающий обработчик события <see cref="FiasPostRequest"/>,
/// оповещающего о начисления платежа на номер.
/// </summary>
internal class FiasPostingRequestHandler(IFiasService fiasService) : Handler<FiasPostRequest>
{
    /// <summary>
    /// Метод, обрабатывающий событие <see cref="FiasPostRequest"/>.
    /// Проводит попытку начисления платежа на номер через интерфейс <c>FIAS</c>.<br/>
    /// Переопределяет метод <see cref="Handler{TIn}.HandleAsync"/>
    /// </summary>
    /// <param name="in">Экземпляр обрабатываемого события.</param>
    /// <param name="context">Контекст обработки события.</param>
    protected override async Task HandleAsync(FiasPostRequest @in, IEventContext context)
    {
        try
        {
            var subtotals = fiasService.Environment.TaxCodes.Keys.Subtotals(@in.Transactions);
            var total = @in.Total * 100;

            var answer = await fiasService.SendPostingAsync(new FiasPostingRequest
            {
                DateTime = @in.DateTime,
                ReservationNumber = @in.ReservationNumber,
                PmsPaymentMethod = @in.TransactionCode,
                TotalPostingAmount = total,
                ProfileNumber = @in.ProfileNumber,
                RoomNumber = @in.Room,
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
    /// Метод, возвращающий краткое описание события <see cref="FiasPostRequest"/><br/>
    /// Переопределяет метод <see cref="Handler{TIn}.Message"/>
    /// </summary>
    /// <param name="in">Экземпляр обрабатываемого события.</param>
    /// <returns>Краткое описание события.</returns>
    protected override string Message(FiasPostRequest @in) => @in.ToString();
}
