namespace SyncHms.Events.Messages;

internal class FiasPostSimple
{
    public string CorrelationId { get; set; }
    
    public decimal Total { get; set; }
    
    public string PmsPaymentMethod { get; set; }
    
    public string CheckNumber { get; set; }
    
    public IEnumerable<IEnumerable<FiscalCheckItem>> Checks { get; set; }
}
