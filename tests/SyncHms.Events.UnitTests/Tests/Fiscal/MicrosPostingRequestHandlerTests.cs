namespace SyncHms.Events.UnitTests.Tests.Fiscal;

public class MicrosPostingRequestHandlerTests
{
    [Fact]
    internal async Task HandleAsyncSuccess()
    {
        var context = new MockEventContext();
        var fiscalService = GetFiscalService();
        var handler = new ExposedMicrosPostingRequestHandler(fiscalService);
        var correlationId = default(int).ToString();

        var microsPostingRequest = new MicrosPostingRequest
        {
            CorrelationId = correlationId,
            Transactions = []
        };
        
        await handler.ExposedHandleAsync(microsPostingRequest, context);
        
        Assert.Single(context.SendMessages);
        Assert.Empty(context.Breaks);
        Assert.Equivalent(context.SendMessages[0], new PostTransactionsResponse(correlationId)
        {
            Succeeded = true,
            ErrorMessage = microsPostingRequest.ToString()
        });
    }

    [Fact]
    internal async Task HandleAsyncSyncPostingOperaIsTrueFailed()
    {
        var context = new MockEventContext();
        var fiscalService = GetFiscalServiceWithThrow(true);
        var handler = new ExposedMicrosPostingRequestHandler(fiscalService);
        var correlationId = default(int).ToString();

        var microsPostingRequest = new MicrosPostingRequest
        {
            CorrelationId = correlationId,
            Transactions = []
        };
        
        await handler.ExposedHandleAsync(microsPostingRequest, context);
        
        Assert.Single(context.SendMessages);
        Assert.Empty(context.Breaks);
        Assert.Equivalent(context.SendMessages[0], new PostTransactionsResponse(correlationId)
        {
            Succeeded = true,
            ErrorMessage = microsPostingRequest.ToString()
        });
    }
    
    [Fact]
    internal async Task HandleAsyncSyncPostingOperaIsFalseFailed()
    {
        var context = new MockEventContext();
        var fiscalService = GetFiscalServiceWithThrow(false);
        var handler = new ExposedMicrosPostingRequestHandler(fiscalService);
        var correlationId = default(int).ToString();
        await handler.ExposedHandleAsync(new MicrosPostingRequest
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

    private static IFiscalService GetFiscalService()
    {
        var fiscalService = new Mock<IFiscalService>();

        fiscalService.SetupGet(fs => fs.Environment).Returns(new ApplicationEnvironment
        {
            Rvc = 99
        });
        
        fiscalService
            .Setup(fs => fs.SetCheckAsync(It.IsAny<FiscalCheck>()))
            .ReturnsAsync(0);

        return fiscalService.Object;
    }

    private static IFiscalService GetFiscalServiceWithThrow(bool syncPostingOpera)
    {
        var fiscalService = new Mock<IFiscalService>();

        fiscalService.SetupGet(fs => fs.Environment).Returns(new ApplicationEnvironment
        {
            SyncPostingOpera = syncPostingOpera,
            Rvc = 99
        });

        fiscalService
            .Setup(fs => fs.SetCheckAsync(It.IsAny<FiscalCheck>()))
            .Throws<Exception>();

        return fiscalService.Object;
    }
}
