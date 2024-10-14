namespace SyncHms.Events.UnitTests.Tests.Base;

public abstract class PostTransactionsResponseHandlerTestsBase
{
    private protected static void CheckPostTransactionsResponse(bool succeeded, string correlationId,
        MockEventContext context, int sendCount, string? errorMessage = null)
    {
        Assert.Equal(sendCount, context.SendMessages.Count);
        Assert.Empty(context.Breaks);

        int index = 0;
        if (sendCount == 2)
        {
            Assert.IsType<ApplicationEnvironment>(context.SendMessages[0]);
            index = 1;
        }

        Assert.Equivalent(context.SendMessages[index], new PostTransactionsResponse(correlationId)
        {
            Succeeded = succeeded,
            ErrorMessage = errorMessage
        });
    }
}