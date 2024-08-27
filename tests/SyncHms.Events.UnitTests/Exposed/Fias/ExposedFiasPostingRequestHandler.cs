namespace SyncHms.Events.UnitTests.Exposed.Fias;

internal class ExposedFiasPostingRequestHandler(IFiasService fiasService, ICheckNumberService checkNumberService)
    : FiasPostingRequestHandler(fiasService, checkNumberService)
{
    public Task ExposedHandleAsync(FiasPostRequest @in, IEventContext context)
    {
        return base.HandleAsync(@in, context);
    }
}