namespace SyncHms.Events.UnitTests.Exposed.Opera;

internal class ExposedUpdatedProfileHandler(IOperaService operaService, IEmisService emisService)
    : UpdatedProfileHandler(operaService, emisService)
{
    public Task ExposedHandleAsync(UpdatedProfile @in, IEventContext context)
    {
        return base.HandleAsync(@in, context);
    }
}
