namespace SyncHms.Events.UnitTests.Exposed.Opera;

internal class ExposedUpdatedReservationHandler : UpdatedReservationHandler
{
    public Task ExposedHandleAsync(UpdatedReservation @in, IEventContext context)
    {
        return base.HandleAsync(@in, context);
    }
}
