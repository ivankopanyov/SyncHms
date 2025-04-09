namespace SyncHms.Events.Messages;

public class IikoPostings
{
    public string CheckNumber { get; set; }

    public DateTime DateTime { get; set; }

    public IikoPosting? Current { get; set; }

    public List<IikoPosting> Queue { get; set; } = [];

    public List<IikoPostingError> PostingErrors { get; set; } = [];
    
    public string Check { get; set; }
}
