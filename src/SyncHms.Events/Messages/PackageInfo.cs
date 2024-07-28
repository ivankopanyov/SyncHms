namespace SyncHms.Events.Messages;

internal class PackageInfo
{
    public string? Code { get; set; }

    public decimal Amount { get; set; }

    public int? Count { get; set; }

    public string? CurrencyCode { get; set; }
}
