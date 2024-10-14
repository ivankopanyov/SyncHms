namespace SyncHms.Events.UnitTests.Exposed.Fias;

internal class ExposedFiasPostingSimpleHandler(IFiasService fiasService)
    : FiasPostingSimpleHandler(fiasService)
{
    public Task ExposedHandleAsync(FiasPostSimple @in, IEventContext context)
    {
        return base.HandleAsync(@in, context);
    }
}