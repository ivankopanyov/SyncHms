namespace SyncHms.Events.Handlers.Fiscal;

public class CheckHandler(IFiscalService fiscalService, ICache cache) : Handler<Check>
{
    private const string CheckNumberKey = "CheckNumber";

    private const int CheckNumberMin = 1;

    private const int CheckNumberMax = 9999;

    private static readonly SemaphoreSlim _semaphore = new(1);

    protected override async Task HandleAsync(Check @in, IEventContext context)
    {
        try
        {
            await _semaphore.WaitAsync();

            if (await cache.PopAsync<CheckNumber>(CheckNumberKey) is not { } checkNumber)
                checkNumber = new CheckNumber
                {
                    Value = 1
                };

            var checkNumberValue = Math.Min(Math.Max(CheckNumberMin, checkNumber.Value), CheckNumberMax);
            checkNumber.Value = checkNumberValue == CheckNumberMax ? CheckNumberMin : (checkNumberValue + 1);
            await cache.PushAsync(CheckNumberKey, checkNumber);

            _semaphore.Release();

            var fiscalCheck = new FiscalCheck
            {
                uws = 1,
                rvc = (int)fiscalService.Environment.Rvc,
                cknum = checkNumberValue,
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

            var response = await fiscalService.Exec<Task<SetCheckResponse>>(async checkDb => await checkDb.SetCheckAsync(fiscalCheck));

            context.Send(new PostResponseInfo
            {
                Headers = @in.Headers,
                CorrelationId = @in.CorrelationId,
                Succeeded = response.SetCheckResult.success,
                ErrorMessage = response.SetCheckResult.errtext
            });
        }
        catch (Exception ex)
        {
            context.Send(new PostResponseInfo
            {
                Headers = @in.Headers,
                CorrelationId = @in.CorrelationId,
                Succeeded = false,
                ErrorMessage = ex.Message
            });
        }
    }

    protected override string Message(Check @in) =>
        $"Correlation ID: {@in.CorrelationId}, Date: {@in.DateTime:dd.MM.yyyy HH:mm:ss}, Total: {@in.Total}";
}
