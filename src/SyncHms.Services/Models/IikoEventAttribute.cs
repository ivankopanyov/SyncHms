namespace SyncHms.Services.Models;

public class IikoEventAttribute
{
    [XmlElement("name")]
    public string Name { get; set; }
    
    [XmlElement("value")]
    public string Value { get; set; }
    
    [XmlElement("type")]
    public string Type { get; set; }
}