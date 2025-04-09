namespace SyncHms.Services.Models;

public class RouteConfiguration
{
    [MaxLength(2)]
    public int?[]? TotalRoute { get; set; }
    
    [MaxLength(2)]
    public int?[]? DiscountRoute { get; set; }
        
    [MaxLength(2)]
    public int?[]? IncreaseRoute { get; set; }
}