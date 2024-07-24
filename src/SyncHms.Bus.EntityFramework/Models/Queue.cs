namespace SyncHms.Bus.EntityFramework.Models;

internal class Queue
{
    public string Name { get; set; }

    public string ExchangeName {  get; set; }

    public virtual Exchange Exchange { get; set; }

    public virtual ICollection<Message> Messages { get; set; } = [];
}
