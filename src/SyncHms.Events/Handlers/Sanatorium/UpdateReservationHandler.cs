namespace SyncHms.Events.Handlers.Sanatorium;

internal class UpdateReservationHandler(ISanatoriumService sanatoriumService) : Handler<ReservationUpdatedMessage>
{
    protected override async Task HandleAsync(ReservationUpdatedMessage @in, IEventContext context)
    {
        await sanatoriumService.SendReservationUpdatedMessageAsync(@in);
    }

    protected override string? Message(ReservationUpdatedMessage @in)
    {
        string? result = null;

        if (@in.ReservationGuests?.FirstOrDefault() is { } guest)
        {
            if (!string.IsNullOrEmpty(guest.Id))
                result = $"Id: {guest.Id}, {string.Join(' ', guest.LastName, guest.FirstName, guest.MiddleName)}";
        }

        if (@in.CurrentTimeline is { } timeline)
        {
            if (!string.IsNullOrEmpty(timeline.RoomCode))
            {
                if (result == null)
                    result = $"Room: {timeline.RoomCode}";
                else 
                    result += $", Room: {timeline.RoomCode}";
            }

            if (!string.IsNullOrEmpty(timeline.RateCode))
            {
                if (result == null)
                    result = $"Rate: {timeline.RateCode}";
                else
                    result += $", Rate: {timeline.RateCode}";
            }
        }

        return result;
    }
}
