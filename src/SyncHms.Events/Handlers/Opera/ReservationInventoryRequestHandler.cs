namespace SyncHms.Events.Handlers.Opera;

internal class ReservationInventoryRequestHandler(IOperaService operaService) : Handler<ReservationInventoryRequest>
{
    protected override async Task HandleAsync(ReservationInventoryRequest @in, IEventContext context)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(operaService.Environment.ResortCode, nameof(operaService.Environment.ResortCode));

        var reservations = await operaService.GetReservationInventoriesAsync(@in.ReservationId, @in.Room,
            OperaReservationStatus.CheckedIn, OperaReservationStatus.CheckedOut);

        context.Logiable = context.HasError;

        if (reservations.Count == 0)
            return;

        var inventories = reservations
            .Where(r => r.Status == OperaReservationStatus.CheckedIn)
            .SelectMany(r => r.Inventories)
            .ToHashSet();

        foreach (var reservation in reservations)
            reservation.Inventories = reservation.Status == OperaReservationStatus.CheckedIn
                ? inventories : [];

        context.Send(new ReservationInventories
        {
            HasError = context.HasError,
            Queue = reservations
        });
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
