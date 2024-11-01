namespace SyncHms.Events.UnitTests.Exposed.Opera;

internal class ExposedReservationHandler(IOperaService operaService, IEmisService emisService)
    : ReservationHandler(operaService, emisService)
{
    public Task ExposedHandleAsync(ReservationInfo @in, IEventContext context)
    {
        return base.HandleAsync(@in, context);
    }
}
