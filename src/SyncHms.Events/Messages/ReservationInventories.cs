namespace SyncHms.Events.Messages;

internal class ReservationInventories
{
    public ReservationInventory Reservation { get; set; }

    public HashSet<ReservationInventory> Queue { get; set; }
}
