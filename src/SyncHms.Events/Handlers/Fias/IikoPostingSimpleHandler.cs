namespace SyncHms.Events.Handlers.Fias;

/// <summary>
/// Класс, описывающий обработчик события <see cref="IikoPostings"/>,
/// оповещающего о совершении денежного платежа в системе IIKO.
/// </summary>
internal class IikoPostingSimpleHandler(IFiasService fiasService, IOperaService operaService) : Handler<IikoPostings>
{
    /// <summary>
    /// Метод, обрабатывающий событие <see cref="IikoPostings"/>.
    /// Проводит попытку синхронизации платежа интерфейс <c>FIAS</c>.<br/>
    /// Переопределяет метод <see cref="Handler{TIn}.HandleAsync"/>
    /// </summary>
    /// <param name="in">Экземпляр обрабатываемого события.</param>
    /// <param name="context">Контекст обработки события.</param>
    protected override async Task HandleAsync(IikoPostings @in, IEventContext context)
    {
        if (@in.Current != null)
        {
            try
            {
                var answer = await fiasService.SendPostingAsync(new FiasPostingSimple
                {
                    RoomNumber = string.Empty,
                    PostingType = FiasPostingTypes.DirectCharge,
                    SalesOutlet = @in.Current.SalesOutlet,
                    TotalPostingAmount = @in.Current.Subtotals.Sum(),
                    DateTime = @in.DateTime,
                    PmsPaymentMethod = @in.Current.PayType,
                    CheckNumber = @in.CheckNumber,
                    Subtotals = @in.Current.Subtotals.Select(s => (decimal?)s).ToArray()
                });

                if (answer.AnswerStatus != FiasAnswerStatuses.Successfully)
                {
                    context.MarkWithError($"Fias posting failed. {answer.ClearText}");
                }
                else
                {
                    try
                    {
                        await operaService.AddCheckDetails(new GuestCheckDetails
                        {
                            CheckNumber = $"{@in.CheckNumber}{@in.DateTime:yyyyMMddHHmmss}",
                            CheckDetail = @in.Check,
                            CheckDate = @in.DateTime
                        });
                    }
                    catch (Exception ex)
                    {
                        context.MarkWithError($"Check details save failed. {ex.Message}", ex.StackTrace);
                    }
                }
            }
            catch (Exception ex)
            {
                context.MarkWithError($"Fias posting failed. {ex.Message}", ex.StackTrace);
            }
        }
        else
        {
            context.Logiable = false;
        }
        
        if (@in.Queue.Count > 0)
        {
            context.Logiable = false;
            @in.Current = @in.Queue.First();
            @in.Queue.Remove(@in.Current);
            context.Send(@in);
            return;
        }
        
        if (@in.PostingErrors.Count > 0)
            context.Send(@in.PostingErrors);
    }

    /// <summary>
    /// Метод, возвращающий краткое описание события <see cref="IikoPostings"/><br/>
    /// Переопределяет метод <see cref="Handler{TIn}.Message"/>
    /// </summary>
    /// <param name="in">Экземпляр обрабатываемого события.</param>
    /// <returns>Краткое описание события.</returns>
    protected override string Message(IikoPostings @in)
        => $"Check: {@in.CheckNumber}, Date: {@in.DateTime:yyyy-MM-dd HH:mm:ss}, {@in.Current}";
}