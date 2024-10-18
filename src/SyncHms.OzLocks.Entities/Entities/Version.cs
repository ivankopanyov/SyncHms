namespace SyncHms.OzLocks.Entities;

public partial class Version
{
    public int? Major { get; set; }

    public int? Minor { get; set; }

    public DateTime? Date { get; set; }

    public string? Block { get; set; }
}
