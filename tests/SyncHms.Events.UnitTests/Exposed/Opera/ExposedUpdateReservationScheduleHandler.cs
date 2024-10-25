namespace SyncHms.Events.UnitTests.Exposed.Opera;

internal class ExposedUpdateReservationScheduleHandler(IOperaService operaService)
    : UpdateReservationScheduleHandler(operaService)
{
    public Task ExposedHandleAsync(IScheduleEventContext context)
    {
        return base.HandleAsync(context);
    }
}
