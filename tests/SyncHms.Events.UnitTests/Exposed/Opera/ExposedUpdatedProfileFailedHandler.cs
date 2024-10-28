namespace SyncHms.Events.UnitTests.Exposed.Opera;

internal class ExposedUpdatedProfileFailedHandler : UpdatedProfileFailedHandler
{
    public Task ExposedHandleAsync(UpdatedProfileFailed @in, IEventContext context)
    {
        return base.HandleAsync(@in, context);
    }
}
