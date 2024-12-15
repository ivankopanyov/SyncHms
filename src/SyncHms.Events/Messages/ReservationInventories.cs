namespace SyncHms.Events.Messages;

internal class ReservationInventories
{
    public bool HasInventories { get; set; }

    public bool HasError { get; set; }

    public HashSet<ReservationInventory> Queue { get; set; } = [];
}
