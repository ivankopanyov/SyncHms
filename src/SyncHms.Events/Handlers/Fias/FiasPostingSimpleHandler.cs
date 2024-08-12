namespace SyncHms.Events.Handlers.Fias;

internal class FiasPostingSimpleHandler(IFiasService fiasService,
    ICheckNumberService checkNumberService) : Handler<FiasPostSimple>
{
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
                var checkNumber = await checkNumberService.GetCheckNumberAsync();
                
                context.Send(new Check
                {
                    CorrelationId = @in.CorrelationId,
                    DateTime = dateTime,
                    Total = @in.Total.ToString(),
                    CheckNumber = checkNumber,
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

    protected override string Message(FiasPostSimple @in) =>
        $"CheckNumber: {@in.CheckNumber}, Total: {@in.Total / 100:#.##}, PaymentMethod: {@in.PmsPaymentMethod}";
}
