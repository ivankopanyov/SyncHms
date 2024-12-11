namespace SyncHms.Events.Handlers.Ozlocks;

internal class ReservationInventoriesHandler : Handler<ReservationInventories>
{
    protected override Task HandleAsync(ReservationInventories @in, IEventContext context)
    {
        if (@in.Queue.Count > 0)
        {
            var reservation = @in.Queue.First();
            @in.Queue.Remove(reservation);

            context.Send(new ReservationInventories
            {
                Reservation = reservation,
                Queue = @in.Queue
            });
        }

        return Task.CompletedTask;
    }

    protected override string? Message(ReservationInventories @in)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append($"{@in.Reservation.LastName} {@in.Reservation.FirstName} {@in.Reservation.MiddleName}, Reservation: {@in.Reservation.ReservationId:0}");

        if (!string.IsNullOrWhiteSpace(@in.Reservation.Room))
            stringBuilder.Append($", Room: {@in.Reservation.Room}");

        return stringBuilder.ToString();
    }
}
