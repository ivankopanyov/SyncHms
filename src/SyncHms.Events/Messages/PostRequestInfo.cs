namespace SyncHms.Events.Messages;

internal class PostRequestInfo
{
    public string CorrelationId { get; set; }

    public string? ReservationGuestId { get; set; }

    public string InvoiceGenericNo { get; set; }

    public string? FolioGenericNo { get; set; }

    public ISet<TransactionInfo> Transactions { get; set; }
}
