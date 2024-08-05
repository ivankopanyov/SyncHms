namespace SyncHms.Services.Infrastructure;

internal class PostTransactionsRequestHandler(ISanatoriumService sanatoriumService) : IHandleMessages<PostTransactionsRequest>
{
    public async Task Handle(PostTransactionsRequest message, IMessageHandlerContext context)
    {
        await sanatoriumService.SendPostTransactionsRequestAsync(message, context, TimeSpan.FromSeconds(90));
    }
}
