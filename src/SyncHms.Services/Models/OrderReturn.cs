namespace SyncHms.Services.Models;

public class OrderReturn
{
    public long CheckNumber { get; set; }

    public DateTime DateTime { get; set; }

    public override int GetHashCode() => CheckNumber.GetHashCode();

    public override bool Equals(object? obj)
        => obj is OrderReturn other && CheckNumber == other.CheckNumber;
}