namespace SyncHms.Services;

public delegate void PostingRequestHandle(PostingRequest message);

public interface ISanatoriumService : IService<ServiceBusOptions, ApplicationEnvironment>
{
    ApplicationEnvironment Environment { get; }

    event PostingRequestHandle? PostingRequestEvent;

    internal void SendPostingRequest(PostingRequest message);

    void Exec(Action<IEndpointInstance?> action);

    T Exec<T>(Func<IEndpointInstance?, T> func);
}
