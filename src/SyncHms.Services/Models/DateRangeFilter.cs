namespace SyncHms.Services.Models;

public class DateRangeFilter : RangeFilter
{
    public DateOnly From { get; set; }

    public DateOnly To { get; set; }
    
    public DateRangeFilter() { }
    
    public DateRangeFilter(string key) : base(key) { }

    public DateRangeFilter(DateOnly from, DateOnly to)
    {
        From = from;
        To = to;
    }

    public DateRangeFilter(string key, DateOnly from, DateOnly to) : base(key)
    {
        From = from;
        To = to;
    }
}