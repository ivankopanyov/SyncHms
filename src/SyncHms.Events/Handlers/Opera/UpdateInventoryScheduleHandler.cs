namespace SyncHms.Events.Handlers.Opera;

internal class UpdateInventoryScheduleHandler(IOperaService operaService) : ScheduleHandler
{
    protected override async Task HandleAsync(IScheduleEventContext context)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(operaService.Environment.ResortCode, nameof(operaService.Environment.ResortCode));

        var reservations = await operaService.GetUpdatedReservationInventoriesAsync(context.Previous, context.Current, OperaReservationStatus.CheckedIn);
        foreach (var reservationId in reservations)
        {
            context.Send(new ReservationInventoryRequest
            {
                ReservationId = reservationId
            });
        }
    }
}
