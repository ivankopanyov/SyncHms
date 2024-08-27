namespace SyncHms.Events.UnitTests.Exposed.Fias;

internal class ExposedCheckOutHandler : CheckOutHandler
{
    public Task ExposedHandleAsync(FiasGuestCheckOut @in, IEventContext context)
    {
        return base.HandleAsync(@in, context);
    }
}