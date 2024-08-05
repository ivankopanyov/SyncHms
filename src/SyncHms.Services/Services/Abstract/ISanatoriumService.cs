namespace SyncHms.Services;

public delegate void PostingRequestHandle(PostTransactionsRequest message);

public interface ISanatoriumService : IService<SanatoriumOptions, ApplicationEnvironment>
{
    event PostingRequestHandle? PostingRequestEvent;
    
    Task SendReservationUpdatedMessageAsync(ReservationUpdatedMessage message);

    Task SendPostTransactionsResponseAsync(PostTransactionsResponse message);

    internal Task SendPostTransactionsRequestAsync(PostTransactionsRequest message,
        IMessageHandlerContext context, TimeSpan timeout);
}
