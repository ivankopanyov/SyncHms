namespace SyncHms.Bus;

public class MessageContext
{
    public bool IsRequeue { get; private set; }

    public void Requeue() => IsRequeue = true;
}
