namespace SyncHms.Services.Models;

internal class OlapReportRequest
{
    public string ReportType { get; set; }

    public string[] GroupByRowFields { get; set; } = [];
    
    public string[] GroupByColFields  { get; set; } = [];
    
    public string[] AggregateFields  { get; set; } = [];

    public Dictionary<string, OlapReportFilter> Filters { get; set; } = [];
}
