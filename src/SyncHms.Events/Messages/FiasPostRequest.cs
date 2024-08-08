namespace SyncHms.Events.Messages;

internal class FiasPostRequest
{
    public string CorrelationId { get; set; }
    
    public long ReservationNumber { get; set; }
    
    public long? ProfileNumber { get; set; }
    
    public string RoomNumber { get; set; }
    
    public IEnumerable<IEnumerable<FiscalCheckItem>> Checks { get; set; }
}
