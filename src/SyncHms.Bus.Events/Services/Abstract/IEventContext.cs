namespace SyncHms.Bus.Events;

public interface IEventContext
{
    void Send<TIn>(TIn @in);

    void Break(string? message = null, Exception? innerException = null);
}
