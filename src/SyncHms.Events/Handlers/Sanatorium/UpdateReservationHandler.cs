namespace SyncHms.Events.Handlers.Sanatorium;

internal class UpdateReservationHandler(ISanatoriumService sanatoriumService) : Handler<ReservationUpdatedMessage>
{
    protected override async Task HandleAsync(ReservationUpdatedMessage @in, IEventContext context)
    {
        await sanatoriumService.SendReservationUpdatedMessageAsync(@in);
    }

    protected override string? Message(ReservationUpdatedMessage @in)
    {
        var result = $"GenericNo: {@in.GenericNo}, Id: {@in.Id}";

        var name = @in.ReservationGuests?.FirstOrDefault() is { } guest
            ? string.Join(' ', guest.LastName, guest.FirstName, guest.MiddleName)
            : null;

        if (!string.IsNullOrWhiteSpace(name))
            result += $", {name}";

        return result;
    }
}
