namespace SyncHms.Services;

public class PostingRequest
{
    public Dictionary<string, string> Headers { get; set; }

    public PostTransactionsRequest PostTransactionsRequest { get; set; }
}
