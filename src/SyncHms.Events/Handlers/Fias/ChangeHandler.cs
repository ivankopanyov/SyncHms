namespace SyncHms.Events.Handlers.Fias;

public class ChangeHandler : Handler<FiasGuestChange>
{
    protected override Task HandleAsync(FiasGuestChange @in, IEventContext context)
    {
        DateTime? arrivalDate = null;
        if (@in.GuestArrivalDate is { } arrival)
            arrivalDate = arrival.ToDateTime(default);

        DateTime? departureDate = null;
        if (@in.GuestDepartureDate is { } departure)
        {
            departureDate = departure.ToDateTime(default);
            if (arrivalDate is { } date && date == departureDate)
                departureDate = date.AddDays(1).AddTicks(-1);
        }

        context.Send(new ReservationInfo
        {
            ReservationNumber = @in.ReservationNumber,
            Room = @in.RoomNumber,
            Status = "IN",
            ArrivalDate = arrivalDate,
            DepartureDate = departureDate
        });

        return Task.CompletedTask;
    }

    protected override string Message(FiasGuestChange @in)
    {
        var result = $"Reservation: {@in.ReservationNumber}";

        if (!string.IsNullOrWhiteSpace(@in.RoomNumber))
            result += $", Room: {@in.RoomNumber}";

        if (!string.IsNullOrWhiteSpace(@in.GuestName))
            result += $" {@in.GuestName}";

        return result;
    }
}
