namespace SyncHms.Events.Handlers.Fiscal;

internal class CheckHandler(IFiscalService fiscalService) : Handler<Check>
{
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
                ErrorMessage = response.SetCheckResult.errtext
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

    protected override string Message(Check @in) =>
        $"CheckNumber: {@in.CheckNumber}, Date: {@in.DateTime:dd.MM.yyyy HH:mm:ss}, Total: {@in.Total}";
}
