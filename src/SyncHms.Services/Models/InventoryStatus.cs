namespace SyncHms.Services;

public class InventoryStatus : IComparable<InventoryStatus>
{
    public OzLocksStatus Status { get; set; }

    public string InventoryCode { get; set; }

    public InventoryStatus()
    {
    }

    public InventoryStatus(OzLocksStatus status, string inventoryCode)
    {
        Status = status;
        InventoryCode = inventoryCode;
    }

    public int CompareTo(InventoryStatus? other)
    {
        var compare = Status.CompareTo(other?.Status);
        if (compare == 0)
            compare = InventoryCode.CompareTo(other?.InventoryCode);

        return compare;
    }

    public override int GetHashCode() => HashCode.Combine(Status, InventoryCode);

    public override bool Equals(object? obj) => obj is InventoryStatus other
        && Status == other.Status && InventoryCode == other.InventoryCode;
}
