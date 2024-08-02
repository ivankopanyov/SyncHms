namespace SyncHms.Services.Infrastructure;

internal class PostTransactionsRequestHandler(ISanatoriumService sanatoriumService) : IHandleMessages<PostTransactionsRequest>
{
    public async Task Handle(PostTransactionsRequest message, IMessageHandlerContext context)
    {
        var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;
        var timeout = TimeSpan.FromSeconds(90);
        await sanatoriumService.SendPostTransactionsRequestAsync(message, context, timeout, cancellationTokenSource);

        try
        {
            await Task.Delay(timeout, cancellationToken);
        }
        catch (OperationCanceledException)
        {
            // ignored
        }
    }
}
