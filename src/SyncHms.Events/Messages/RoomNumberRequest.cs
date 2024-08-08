namespace SyncHms.Events.Messages;

internal class RoomNumberRequest
{
    public string CorrelationId { get; set; }
    
    public string? ReservationGuestId { get; set; }
    
    public string? FolioGenericNo { get; set; }
    
    public IEnumerable<IEnumerable<FiscalCheckItem>> Checks { get; set; }
}
