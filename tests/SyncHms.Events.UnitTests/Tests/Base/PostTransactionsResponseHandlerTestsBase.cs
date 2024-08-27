namespace SyncHms.Events.UnitTests.Tests.Base;

public abstract class PostTransactionsResponseHandlerTestsBase
{
    private protected static void CheckPostTransactionsResponse(bool succeeded, string correlationId,
        MockEventContext context, string? errorMessage = null)
    {
        Assert.Single(context.SendMessages);
        Assert.Empty(context.Breaks);
        Assert.Equivalent(context.SendMessages[0], new PostTransactionsResponse(correlationId)
        {
            Succeeded = succeeded,
            ErrorMessage = errorMessage
        });
    }
}