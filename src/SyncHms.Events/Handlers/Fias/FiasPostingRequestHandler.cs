namespace SyncHms.Events.Handlers.Fias;

internal class FiasPostingRequestHandler(IFiasService fiasService,
    ICheckNumberService checkNumberService) : Handler<FiasPostRequest>
{
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

    protected override string? Message(FiasPostRequest @in)
    {
        var result = $"Reservation: {@in.ReservationNumber}, Room: {@in.RoomNumber}";

        if (@in.ProfileNumber != null)
            result = $", Profile: {@in.ProfileNumber}";

        return result;
    }
}
