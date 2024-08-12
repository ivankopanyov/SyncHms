namespace SyncHms.Events.Handlers.Opera;

internal class RoomNumberHandler(IOperaService operaService) : Handler<RoomNumberRequest>
{
    protected override async Task HandleAsync(RoomNumberRequest @in, IEventContext context)
    {
        try
        {
            decimal? reservationNumber = null;
            long? profileNumber = null;
            
            if (@in.ReservationGuestId?.Split('/') is { Length: 2 } reservationGuestIdSplit)
            {
                if (long.TryParse(reservationGuestIdSplit[0], out var profileId))
                    profileNumber = profileId;

                if (int.TryParse(reservationGuestIdSplit[1], out var reservationId))
                    reservationNumber = reservationId;
            }

            if (@in.FolioGenericNo?.Split('/') is { Length: 2 } folioGenericNoSplit)
            {
                if (int.TryParse(folioGenericNoSplit[0], out var reservationId))
                    reservationNumber = reservationId;
            }
            
            if (reservationNumber is not { } reservation)
            {
                context.Send(new PostTransactionsResponse(@in.CorrelationId)
                {
                    Succeeded = false,
                    ErrorMessage = "Reservation ID is null."
                });
                return;
            }

            if (await operaService.GetRoomNumberAsync(reservation) is not { } room)
            {
                context.Send(new PostTransactionsResponse(@in.CorrelationId)
                {
                    Succeeded = false,
                    ErrorMessage = $"Reservation {reservationNumber} not found."
                });
                return;
            }
            
            context.Send(new FiasPostRequest
            {
                CorrelationId = @in.CorrelationId,
                ReservationNumber = (long)reservation,
                ProfileNumber = profileNumber,
                RoomNumber = room,
                Checks = @in.Checks
            });
        }
        catch (Exception ex)
        {
            context.Send(new PostTransactionsResponse(@in.CorrelationId)
            {
                Succeeded = false,
                ErrorMessage = ex.Message
            });
        }
    }

    protected override string? Message(RoomNumberRequest @in) => $"Reservation: {@in.ReservationGuestId}";
}
