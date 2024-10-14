namespace SyncHms.Events.UnitTests.Exposed.Fias;

internal class ExposedFiasPostingRequestHandler(IFiasService fiasService)
    : FiasPostingRequestHandler(fiasService)
{
    public Task ExposedHandleAsync(FiasPostRequest @in, IEventContext context)
    {
        return base.HandleAsync(@in, context);
    }
}