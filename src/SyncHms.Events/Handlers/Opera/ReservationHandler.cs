namespace SyncHms.Events.Handlers.Opera;

internal class ReservationHandler(IOperaService operaService) : Handler<ReservationInfo>
{
    protected override async Task HandleAsync(ReservationInfo @in, IEventContext context)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(operaService.Environment.ResortCode, nameof(operaService.Environment.ResortCode));

        if (await operaService.GetReservationUpdatedMessageAsync(@in.ReservationNumber, @in.Status, @in.Room,
                @in.ArrivalDate ?? default, @in.DepartureDate ?? default, @in.NoPost) is { } reservationResponse)
        {
            context.Send(reservationResponse);
        }
        else
        {
            context.Break("Reservation not found.");
        }
    }

    protected override string Message(ReservationInfo @in)
    {
        var result = $"Reservation: {@in.ReservationNumber:0}";

        if (!string.IsNullOrWhiteSpace(@in.Room))
            result += $", Room: {@in.Room}";

        return result;
    }
}
