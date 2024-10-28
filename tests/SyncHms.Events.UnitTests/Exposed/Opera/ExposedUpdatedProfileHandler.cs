namespace SyncHms.Events.UnitTests.Exposed.Opera;

internal class ExposedUpdatedProfileHandler(IOperaService operaService) : UpdatedProfileHandler(operaService)
{
    public Task ExposedHandleAsync(UpdatedProfile @in, IEventContext context)
    {
        return base.HandleAsync(@in, context);
    }
}
