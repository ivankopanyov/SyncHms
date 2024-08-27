namespace SyncHms.Events.UnitTests.Exposed.Fias;

internal class ExposedCheckInHandler : CheckInHandler
{
    public Task ExposedHandleAsync(FiasGuestCheckIn @in, IEventContext context)
    {
        return base.HandleAsync(@in, context);
    }
}