namespace SyncHms.Events.UnitTests.Exposed.Fias;

internal class ExposedFiasPostingSimpleHandler(IFiasService fiasService, ICheckNumberService checkNumberService)
    : FiasPostingSimpleHandler(fiasService, checkNumberService)
{
    public Task ExposedHandleAsync(FiasPostSimple @in, IEventContext context)
    {
        return base.HandleAsync(@in, context);
    }
}