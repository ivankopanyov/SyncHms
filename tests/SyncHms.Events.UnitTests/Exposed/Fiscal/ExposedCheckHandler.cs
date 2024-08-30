namespace SyncHms.Events.UnitTests.Exposed.Fiscal;

internal class ExposedCheckHandler(IFiscalService fiscalService) : CheckHandler(fiscalService)
{
    public Task ExposedHandleAsync(Check @in, IEventContext context)
    {
        return base.HandleAsync(@in, context);
    }
}