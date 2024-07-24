namespace SyncHms.Bus.EntityFramework.Models;

internal class Message
{
    public string Id { get; set; }

    public string ExchangeName { get; set; }
    
    public string QueueName { get; set; }

    public string Json { get; set; }

    public bool Requeue { get; set; }

    public bool Handled { get; set; }

    public virtual Queue Queue { get; set; }
}
