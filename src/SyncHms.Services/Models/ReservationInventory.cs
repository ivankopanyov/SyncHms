namespace SyncHms.Services;

public class ReservationInventory
{
    public decimal ReservationId { get; init; }
    
    public string ConfirmationNo { get; set; }
    
    public string LastName { get; set; }

    public string FirstName { get; set; }

    public string MiddleName { get; set; }
    
    public string Room { get; set; }

    public HashSet<Inventory> Inventories { get; set; } = [];

    public override int GetHashCode() => ReservationId.GetHashCode();

    public override bool Equals(object? obj) =>
        obj is ReservationInventory other && other.ReservationId == ReservationId;
}