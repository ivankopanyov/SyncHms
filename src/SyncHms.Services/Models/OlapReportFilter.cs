namespace SyncHms.Services.Models;

public abstract class OlapReportFilter
{
    [Newtonsoft.Json.JsonIgnore]
    public string Key { get; set; }

    public abstract string FilterType { get; set; }
    
    protected OlapReportFilter() { }

    protected OlapReportFilter(string key)
    {
        Key = key;
    }

    public static implicit operator KeyValuePair<string, OlapReportFilter>(OlapReportFilter filter)
    {
        return new KeyValuePair<string, OlapReportFilter>(filter.Key, filter);
    }
}