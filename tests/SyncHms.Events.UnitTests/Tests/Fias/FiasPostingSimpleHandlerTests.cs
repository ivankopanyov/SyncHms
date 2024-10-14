namespace SyncHms.Events.UnitTests.Tests.Fias;

public class FiasPostingSimpleHandlerTests : FiasPostingHandlerTestsBase
{
    [Fact]
    internal async Task HandleAsyncSuccess()
    {
        var context = new MockEventContext();
        var fiasService = GetFiasService(true, FiasAnswerStatuses.Successfully);
        var handler = new ExposedFiasPostingSimpleHandler(fiasService);
        await handler.ExposedHandleAsync(new FiasPostSimple
        {
            Checks = []
        }, context);

        CheckType(context);
    }
    
    [Fact]
    internal async Task HandleAsyncSyncPostingMicrosIsFalseSuccess()
    {
        var context = new MockEventContext();
        var fiasService = GetFiasService(false, FiasAnswerStatuses.Successfully);
        var handler = new ExposedFiasPostingSimpleHandler(fiasService);
        var correlationId = default(int).ToString();
        await handler.ExposedHandleAsync(new FiasPostSimple
        {
            CorrelationId = correlationId,
            Checks = []
        }, context);

        CheckPostTransactionsResponse(true, correlationId, context, 1);
    }
    
    [Fact]
    internal async Task HandleAsyncAnswerStatusIsNotSuccessfullyFailed()
    {
        var context = new MockEventContext();
        var answerStatus = (FiasAnswerStatuses)Random.Shared.Next((int)FiasAnswerStatuses.Successfully);
        var fiasService = GetFiasService(true, answerStatus);
        var handler = new ExposedFiasPostingSimpleHandler(fiasService);
        var correlationId = default(int).ToString();
        await handler.ExposedHandleAsync(new FiasPostSimple
        {
            CorrelationId = correlationId,
            Checks = []
        }, context);

        CheckPostTransactionsResponse(false, correlationId, context, 1, answerStatus.ToString());
    }
    
    [Fact]
    internal async Task HandleAsyncFailed()
    {
        var context = new MockEventContext();
        var fiasService = GetFiasServiceWithThrow(true);
        var handler = new ExposedFiasPostingSimpleHandler(fiasService);
        var correlationId = default(int).ToString();
        await handler.ExposedHandleAsync(new FiasPostSimple
        {
            CorrelationId = correlationId,
            Checks = []
        }, context);
        
        CheckPostTransactionsResponse(false, correlationId, context, 1, new Exception().Message);
    }
}
