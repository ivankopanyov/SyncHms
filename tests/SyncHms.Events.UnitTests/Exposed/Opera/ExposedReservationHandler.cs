namespace SyncHms.Events.UnitTests.Exposed.Opera;

internal class ExposedReservationHandler(IOperaService operaService) : ReservationHandler(operaService)
{
    public Task ExposedHandleAsync(ReservationInfo @in, IEventContext context)
    {
        return base.HandleAsync(@in, context);
    }
}
