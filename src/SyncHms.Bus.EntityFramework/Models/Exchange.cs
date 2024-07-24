namespace SyncHms.Bus.EntityFramework.Models;

internal class Exchange
{
    public string Name { get; set; }

    public virtual ICollection<Queue> Queues { get; set; } = [];
}
