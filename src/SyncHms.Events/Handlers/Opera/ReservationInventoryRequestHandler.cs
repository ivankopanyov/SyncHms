namespace SyncHms.Events.Handlers.Opera;

internal class ReservationInventoryRequestHandler(IOperaService operaService) : Handler<ReservationInventoryRequest>
{
    protected override async Task HandleAsync(ReservationInventoryRequest @in, IEventContext context)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(operaService.Environment.ResortCode, nameof(operaService.Environment.ResortCode));

        var reservations = await operaService.GetReservationInventoriesAsync(@in.ReservationId, @in.Room, OperaReservationStatus.CheckedIn);
        if (reservations.Count > 0)
        {
            var reservation = reservations.First();
            reservations.Remove(reservation);

            context.Send(new ReservationInventories
            {
                Reservation = reservation,
                Queue = reservations
            });
        }
    }

    protected override string Message(ReservationInventoryRequest @in)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append($"Reservation: {@in.ReservationId:0}");

        if (!string.IsNullOrWhiteSpace(@in.Room))
            stringBuilder.Append($", Room: {@in.Room}");

        return stringBuilder.ToString();
    }
}
