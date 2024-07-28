namespace SyncHms.Events.Infrastructure;

internal class PostTransactionsRequestHandler(ISanatoriumService sanatoriumService) : IHandleMessages<PostTransactionsRequest>
{
    public Task Handle(PostTransactionsRequest message, IMessageHandlerContext context)
    {
        if (sanatoriumService.UsePosting)
        {
            sanatoriumService.SendPostingRequest(new PostingRequest
            {
                Headers = context.MessageHeaders.ToDictionary(),
                PostTransactionsRequest = message
            });
        }

        return Task.CompletedTask;
    }
}

