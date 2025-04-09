namespace SyncHms.Services.Models;

public class ValuesFilter<T> : OlapReportFilter
{
    public override string FilterType { get; set; } = "IncludeValues";

    public T[] Values { get; set; }

    public ValuesFilter(params T[] values)
    {
        Values = values;
    }

    public ValuesFilter(string key, params T[] values) : base(key)
    {
        Values = values;
    }
}
