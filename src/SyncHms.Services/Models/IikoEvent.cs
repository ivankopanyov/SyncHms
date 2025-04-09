namespace SyncHms.Services.Models;

public class IikoEvent
{
    [XmlElement("id")]
    public string Id { get; set; }
    
    [XmlElement("date")]
    public DateTime Date { get; set; }
    
    [XmlElement("type")]
    public string Type { get; set; }
    
    [XmlElement("departmentId")]
    public string DepartmentId { get; set; }
    
    [XmlElement("attribute")]
    public List<IikoEventAttribute> Attributes { get; set; }
}