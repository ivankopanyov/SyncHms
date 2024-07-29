namespace SyncHms.Services;

public delegate void PostingRequestHandle(PostingRequest message);

public interface ISanatoriumService : IService<SanatoriumOptions, ApplicationEnvironment>
{
    bool UsePosting { get; }
    
    ApplicationEnvironment Environment { get; }

    event PostingRequestHandle? PostingRequestEvent;

    void SendPostingRequest(PostingRequest message);

    void Exec(Action<IEndpointInstance?> action);

    T Exec<T>(Func<IEndpointInstance?, T> func);
}
