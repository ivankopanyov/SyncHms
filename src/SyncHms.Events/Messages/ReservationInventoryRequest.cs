namespace SyncHms.Events.Messages;

public class ReservationInventoryRequest
{
    public decimal ReservationId { get; set; }

    public string? Room { get; set; }

    public static implicit operator ReservationInventoryRequest(FiasGuestCheckIn checkIn)
    {
        return new ReservationInventoryRequest
        {
            ReservationId = checkIn.ReservationNumber,
            Room = checkIn.RoomNumber
        };
    }

    public static implicit operator ReservationInventoryRequest(FiasGuestChange change)
    {
        return new ReservationInventoryRequest
        {
            ReservationId = change.ReservationNumber,
            Room = change.RoomNumber
        };
    }
}
