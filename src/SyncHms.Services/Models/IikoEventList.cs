namespace SyncHms.Services.Models;

[XmlRoot("eventsList")]
public class IikoEventList
{
    [XmlElement("revision")]
    public int Revision { get; set; }

    [XmlElement("event")]
    public List<IikoEvent> Events { get; set; }
}
