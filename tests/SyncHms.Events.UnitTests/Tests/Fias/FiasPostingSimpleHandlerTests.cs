namespace SyncHms.Events.UnitTests.Tests.Fias;

public class FiasPostingSimpleHandlerTests : FiasPostingHandlerTestsBase
{
    [Fact]
    internal async Task HandleAsyncSuccess()
    {
        var context = new MockEventContext();
        var fiasService = GetFiasService(FiasAnswerStatuses.Successfully);
        var handler = new ExposedFiasPostingSimpleHandler(fiasService);
        await handler.ExposedHandleAsync(new FiasPostSimple
        {
            Transactions = []
        }, context);
        
        Assert.Single(context.SendMessages);
        Assert.Empty(context.Breaks);
        Assert.IsType<CheckDetails>(context.SendMessages[0]);
    }

    [Theory]
    [ClassData(typeof(FiasPostingHandlerCases))]
    internal async Task HandleAsyncUnsucceededStatusSuccess(FiasAnswerStatuses status)
    {
        var context = new MockEventContext();
        var fiasService = GetFiasService(status);
        var handler = new ExposedFiasPostingSimpleHandler(fiasService);
        var correlationId = default(int).ToString();
        await handler.ExposedHandleAsync(new FiasPostSimple
        {
            CorrelationId = correlationId,
            Transactions = []
        }, context);
        
        Assert.Single(context.SendMessages);
        Assert.Empty(context.Breaks);
        Assert.Equivalent(context.SendMessages[0], new PostTransactionsResponse(correlationId)
        {
            Succeeded = false,
            ErrorMessage = status.ToString()
        });
    }

    [Fact]
    internal async Task HandleAsyncFailed()
    {
        var context = new MockEventContext();
        var fiasService = GetFiasServiceWithThrow();
        var handler = new ExposedFiasPostingSimpleHandler(fiasService);
        var correlationId = default(int).ToString();
        await handler.ExposedHandleAsync(new FiasPostSimple
        {
            CorrelationId = correlationId,
            Transactions = []
        }, context);
        
        Assert.Single(context.SendMessages);
        Assert.Empty(context.Breaks);
        Assert.Equivalent(context.SendMessages[0], new PostTransactionsResponse(correlationId)
        {
            Succeeded = false,
            ErrorMessage = new Exception().Message
        });
    }
}
