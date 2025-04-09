namespace SyncHms.Events.Messages;

public class IikoPosting
{
    public int SalesOutlet { get; set; }

    public string PayType { get; set; }

    public decimal[] Subtotals { get; set; }

    public override string ToString() => $"PayType: {PayType}, SalesOutlet: {SalesOutlet}, Sum: {Subtotals.Sum() / 100:0.00}";
}