namespace SyncHms.Bus;

public interface IMessageContext
{
    void Requeue(bool update = false);
}