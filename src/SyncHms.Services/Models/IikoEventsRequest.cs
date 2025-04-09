namespace SyncHms.Services.Models;

[XmlRoot("eventsRequestData")]
public class IikoEventsRequest
{
    [XmlArray("events")]
    [XmlArrayItem("event")]
    public List<string> Events { get; set; }
}