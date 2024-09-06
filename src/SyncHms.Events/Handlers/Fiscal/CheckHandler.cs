namespace SyncHms.Events.Handlers.Fiscal;

/// <summary>
/// Класс, описывающий обработчик события <see cref="Check"/>,
/// оповещающего о совершении денежного платежа или начислении платежа на номер.<br/>
/// Унаследован от класса <see cref="Handler{TIn}"/>
/// </summary>
internal class CheckHandler(IFiscalService fiscalService) : Handler<Check>
{
    /// <summary>
    /// Метод, обрабатывающий событие <see cref="Check"/>.
    /// Проводит попытку сохранения чека платежа в базе данных <c>MICROS</c>.<br/>
    /// Отправляет в шину данных событие <see cref="PostTransactionsResponse"/>.<br/>
    /// Переопределяет метод <see cref="Handler{TIn}.HandleAsync"/>
    /// </summary>
    /// <param name="in">Экземпляр обрабатываемого события.</param>
    /// <param name="context">Контекст обработки события.</param>
    protected override async Task HandleAsync(Check @in, IEventContext context)
    {
        try
        {
            var fiscalCheck = new FiscalCheck
            {
                uws = 1,
                rvc = (int)fiscalService.Environment.Rvc,
                cknum = @in.CheckNumber,
                open_time = @in.DateTime,
                close_time = @in.DateTime,
                total = @in.Total,
                tremp = 99,
                tremp_fname = "IFC",
                tremp_lname = "Sanatorium",
                cashier = 99,
                dtl = @in.Details.Select(item => new CheckItem
                {
                    type = 'M',
                    objnum = item.ObjectNumber,
                    name = item.Name,
                    qty = (int)item.Quantity,
                    qtf = item.Quantity,
                    ttl = item.Total.ToString(),
                    tax = item.Tax,
                    taxps = item.TaxPosting
                }).ToArray()
            };

            var response = await fiscalService.SetCheckAsync(fiscalCheck);
            context.Send(new PostTransactionsResponse(@in.CorrelationId)
            {
                Succeeded = response.SetCheckResult.success,
                ErrorMessage = response.SetCheckResult.success
                    ? Message(@in)
                    : response.SetCheckResult.errtext
            });
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
    /// Метод, возвращающий краткое описание события <see cref="Check"/><br/>
    /// Переопределяет метод <see cref="Handler{TIn}.Message"/>
    /// </summary>
    /// <param name="in">Экземпляр обрабатываемого события.</param>
    /// <returns>Краткое описание события.</returns>
    protected override string Message(Check @in) =>
        $"CheckNumber: {@in.ExternalCheckNumber} ({@in.CheckNumber}), Date: {@in.DateTime:dd.MM.yyyy HH:mm:ss}, Total: {@in.Details?.Select(i => i.Total).Sum():0.00}";
}
