namespace SyncHms.Events.UnitTests.Exposed.Opera;

internal class ExposedCheckDetailsHandler(IOperaService operaService) : CheckDetailsHandler(operaService)
{
    public Task ExposedHandleAsync(CheckDetails @in, IEventContext context)
    {
        return base.HandleAsync(@in, context);
    }
}