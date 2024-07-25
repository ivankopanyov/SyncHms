namespace SyncHms.Bus.EntityFramework.Implement;

internal class MessageContext : IMessageContext
{
    public bool IsRequeue { get; private set; }
    
    public bool Update { get; private set; }
    
    public void Requeue(bool update = false)
    {
        IsRequeue = true;
        Update = update;
    }
}
