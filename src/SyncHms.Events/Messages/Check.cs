namespace SyncHms.Events.Messages;

internal class Check
{
    public string CorrelationId { get; set; }
    
    public DateTime DateTime { get; set; }
    
    public string Total { get; set; }
    
    public int CheckNumber { get; set; }
    
    public IEnumerable<FiscalCheckItem> Details { get; set; }
}
