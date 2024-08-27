namespace SyncHms.Events.UnitTests.Exposed.Fias;

internal class ExposedChangeHandler : ChangeHandler
{
    public Task ExposedHandleAsync(FiasGuestChange @in, IEventContext context)
    {
        return base.HandleAsync(@in, context);
    }
}