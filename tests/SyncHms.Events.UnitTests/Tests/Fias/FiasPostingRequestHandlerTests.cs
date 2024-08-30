namespace SyncHms.Events.UnitTests.Tests.Fias;

public class FiasPostingRequestHandlerTests : FiasPostingHandlerTestsBase
{
    [Fact]
    internal async Task HandleAsyncSuccess()
    {
        var context = new MockEventContext();
        var fiasService = GetFiasService(true, FiasAnswerStatuses.Successfully);
        var checkNumberService = new MockCheckNumberService();
        var handler = new ExposedFiasPostingRequestHandler(fiasService, checkNumberService);
        await handler.ExposedHandleAsync(new FiasPostRequest
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
        var checkNumberService = new MockCheckNumberService();
        var handler = new ExposedFiasPostingRequestHandler(fiasService, checkNumberService);
        var correlationId = default(int).ToString();
        await handler.ExposedHandleAsync(new FiasPostRequest
        {
            CorrelationId = correlationId,
            Checks = []
        }, context);

        CheckPostTransactionsResponse(true, correlationId, context);
    }
    
    [Fact]
    internal async Task HandleAsyncAnswerStatusIsNotSuccessfullyFailed()
    {
        var context = new MockEventContext();
        var answerStatus = (FiasAnswerStatuses)Random.Shared.Next((int)FiasAnswerStatuses.Successfully);
        var fiasService = GetFiasService(true, answerStatus);
        var checkNumberService = new MockCheckNumberService();
        var handler = new ExposedFiasPostingRequestHandler(fiasService, checkNumberService);
        var correlationId = default(int).ToString();
        await handler.ExposedHandleAsync(new FiasPostRequest
        {
            CorrelationId = correlationId,
            Checks = []
        }, context);

        CheckPostTransactionsResponse(false, correlationId, context, answerStatus.ToString());
    }
    
    [Fact]
    internal async Task HandleAsyncFailed()
    {
        var context = new MockEventContext();
        var fiasService = GetFiasServiceWithThrow(true);
        var checkNumberService = new MockCheckNumberService();
        var handler = new ExposedFiasPostingRequestHandler(fiasService, checkNumberService);
        var correlationId = default(int).ToString();
        await handler.ExposedHandleAsync(new FiasPostRequest
        {
            CorrelationId = correlationId,
            Checks = []
        }, context);
        
        CheckPostTransactionsResponse(false, correlationId, context, new Exception().Message);
    }
}