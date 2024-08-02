namespace SyncHms.Events.Messages;

internal class FiscalCheckItem
{
    public long ObjectNumber { get; set; }
    
    public string Name { get; set; }
    
    public double Quantity { get; set; }
    
    public decimal Total { get; set; }
    
    public byte Tax { get; set; }
    
    public int TaxPosting { get; set; }
}
