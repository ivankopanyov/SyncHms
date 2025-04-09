namespace SyncHms.Services.Models;

public abstract class RangeFilter : OlapReportFilter
{
    public override string FilterType { get; set; } = "DateRange";

    public virtual string PeriodType { get; } = "CUSTOM";
    
    protected RangeFilter() { }
    
    protected RangeFilter(string key) : base(key) { }
}