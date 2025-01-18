namespace SyncHms.Events.UnitTests.Exposed.Fiscal;

internal class ExposedMicrosPostingRequestHandler(IFiscalService fiscalService) : MicrosPostingRequestHandler(fiscalService)
{
    public Task ExposedHandleAsync(MicrosPostingRequest @in, IEventContext context)
    {
        return base.HandleAsync(@in, context);
    }

    public string? ExposedMessage(MicrosPostingRequest @in)
    {
        return base.Message(@in);
    }
}
