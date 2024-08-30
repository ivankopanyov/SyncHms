namespace SyncHms.Events.UnitTests.Tests.Fiscal;

public class CheckHandlerTests : PostTransactionsResponseHandlerTestsBase
{
    [Fact]
    internal async Task HandleAsyncSuccess()
    {
        var context = new MockEventContext();
        var fiscalService = GetFiscalService(true);
        var handler = new ExposedCheckHandler(fiscalService);
        var correlationId = default(int).ToString();
        await handler.ExposedHandleAsync(new Check
        {
            CorrelationId = correlationId,
            Details = []
        }, context);
        
        CheckPostTransactionsResponse(true, correlationId, context);
    }
    
    [Fact]
    internal async Task HandleAsyncSetCheckAsyncAnswerIsNotSuccessFailed()
    {
        var context = new MockEventContext();
        var error = "Error";
        var fiscalService = GetFiscalService(false, error);
        var handler = new ExposedCheckHandler(fiscalService);
        var correlationId = default(int).ToString();
        await handler.ExposedHandleAsync(new Check
        {
            CorrelationId = correlationId,
            Details = []
        }, context);

        CheckPostTransactionsResponse(false, correlationId, context, error);
    }
    
    [Fact]
    internal async Task HandleAsyncFailed()
    {
        var context = new MockEventContext();
        var fiscalService = GetFiscalServiceWithThrow();
        var handler = new ExposedCheckHandler(fiscalService);
        var correlationId = default(int).ToString();
        await handler.ExposedHandleAsync(new Check
        {
            CorrelationId = correlationId,
            Details = []
        }, context);
        
        CheckPostTransactionsResponse(false, correlationId, context, new Exception().Message);
    }

    private static IFiscalService GetFiscalService(bool success, string? errorMessage = null)
    {
        var fiscalService = new Mock<IFiscalService>();

        fiscalService.SetupGet(fs => fs.Environment).Returns(new ApplicationEnvironment
        {
            Rvc = 99
        });
        
        fiscalService
            .Setup(fs => fs.SetCheckAsync(It.IsAny<FiscalCheck>()))
            .ReturnsAsync(new SetCheckResponse
            {
                SetCheckResult = new Response
                {
                    success = success,
                    errtext = errorMessage
                }
            });

        return fiscalService.Object;
    }

    private static IFiscalService GetFiscalServiceWithThrow()
    {
        var fiscalService = new Mock<IFiscalService>();

        fiscalService.SetupGet(fs => fs.Environment).Returns(new ApplicationEnvironment
        {
            Rvc = 99
        });

        fiscalService
            .Setup(fs => fs.SetCheckAsync(It.IsAny<FiscalCheck>()))
            .Throws<Exception>();

        return fiscalService.Object;
    }
}