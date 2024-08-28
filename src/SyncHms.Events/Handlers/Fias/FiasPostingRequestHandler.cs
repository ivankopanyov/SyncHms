using Microsoft.Extensions.Primitives;

namespace SyncHms.Events.Handlers.Fias;

/// <summary>
/// Класс, описывающий обработчик события <see cref="FiasPostRequest"/>,
/// оповещающего о начисления платежа на номер.<br/>
/// Унаследован от класса <see cref="Handler{TIn}"/>
/// </summary>
internal class FiasPostingRequestHandler(IFiasService fiasService,
    ICheckNumberService checkNumberService) : Handler<FiasPostRequest>
{
    /// <summary>
    /// Метод, обрабатывающий событие <see cref="FiasPostRequest"/>.
    /// Проводит попытку начисления платежа на номер через интерфейс <c>FIAS</c>.<br/>
    /// В случае неудачного начисления или если параметр окружения
    /// <see cref="ApplicationEnvironment.SyncPostingMicros"/> равен <c>false</c>,
    /// отправляет в шину данных событие <see cref="PostTransactionsResponse"/>.<br/>
    /// В противном случае отправляет в шину данных событие <see cref="Check"/>.<br/>
    /// Переопределяет метод <see cref="Handler{TIn}.HandleAsync"/>
    /// </summary>
    /// <param name="in">Экземпляр обрабатываемого события.</param>
    /// <param name="context">Контекст обработки события.</param>
    protected override async Task HandleAsync(FiasPostRequest @in, IEventContext context)
    {
        try
        {
            var subtotals = @in.Checks.Select(c => c.Select(i => i.Total).Sum() * 100).ToArray();
            var checkNumber = await checkNumberService.GetCheckNumberAsync();
            var dateTime = DateTime.Now;
            var total = subtotals.Sum();

            var answer = await fiasService.SendPostingAsync(new FiasPostingRequest
            {
                DateTime = dateTime,
                ReservationNumber = @in.ReservationNumber,
                TotalPostingAmount = total,
                ProfileNumber = @in.ProfileNumber,
                CheckNumber = $"{fiasService.Environment.Rvc:000.##}{checkNumber}{DateTime.Now:yyyyMMddHHmmss}",
                RoomNumber = @in.RoomNumber,
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
                context.Send(new Check
                {
                    CorrelationId = @in.CorrelationId,
                    DateTime = dateTime,
                    Total = total.ToString(),
                    CheckNumber = checkNumber,
                    Details = @in.Checks.SelectMany(c => c)
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
    /// Метод, возвращающий краткое описание события <see cref="FiasPostRequest"/><br/>
    /// Переопределяет метод <see cref="Handler{TIn}.Message"/>
    /// </summary>
    /// <param name="in">Экземпляр обрабатываемого события.</param>
    /// <returns>Краткое описание события.</returns>
    protected override string? Message(FiasPostRequest @in)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append($"Reservation: {@in.ReservationNumber}");

        if (!string.IsNullOrEmpty(@in.RoomNumber))
            stringBuilder.Append($", Room: {@in.RoomNumber}");

        if (@in.ProfileNumber != null)
            stringBuilder.Append($", Profile: {@in.ProfileNumber}");

        return stringBuilder.ToString();
    }
}
