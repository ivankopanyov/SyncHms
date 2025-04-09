namespace SyncHms.Services.Models;

public class DateTimeRangeFilter : RangeFilter
{
    [Newtonsoft.Json.JsonConverter(typeof(IikoDateTimeFormatConverter))]
    public DateTime From { get; set; }
        
    [Newtonsoft.Json.JsonConverter(typeof(IikoDateTimeFormatConverter))]
    public DateTime To { get; set; }
    
    public DateTimeRangeFilter() { }
    
    public DateTimeRangeFilter(string key) : base(key) { }

    public DateTimeRangeFilter(DateTime from, DateTime to)
    {
        From = from;
        To = to;
    }

    public DateTimeRangeFilter(string key, DateTime from, DateTime to) : base(key)
    {
        From = from;
        To = to;
    }
}
