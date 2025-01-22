namespace SyncHms.Events.UnitTests.Tests.Opera;

public class CheckDetailsHandlerTests
{
    [Fact]
    internal async Task HandleAsyncSyncPostingMicrosIsTrueSuccess()
    {
        var context = new MockEventContext();
        var operaService = GetOperaService();
        var handler = new ExposedCheckDetailsHandler(operaService);
        await handler.ExposedHandleAsync(new CheckDetails(), context);
        
        Assert.Single(context.SendMessages);
        Assert.Empty(context.Breaks);
        Assert.IsType<MicrosPostingRequest>(context.SendMessages[0]);
    }
    
    [Fact]
    internal async Task HandleAsyncSyncPostingMicrosIsFalseSuccess()
    {
        var context = new MockEventContext();
        var operaService = GetOperaService(false);
        var handler = new ExposedCheckDetailsHandler(operaService);
        var correlationId = default(int).ToString();
        var checkDetails = new CheckDetails
        {
            CorrelationId = correlationId
        };
        await handler.ExposedHandleAsync(checkDetails, context);
        
        Assert.Single(context.SendMessages);
        Assert.Empty(context.Breaks);
        Assert.Equivalent(context.SendMessages[0], new PostTransactionsResponse(correlationId)
        {
            Succeeded = true,
            ErrorMessage = checkDetails.ToString()
        });
    }
    
    [Fact]
    internal async Task HandleAsyncSyncPostingMicrosIsTrueFailed()
    {
        var context = new MockEventContext();
        var operaService = GetOperaServiceWithThrow();
        var handler = new ExposedCheckDetailsHandler(operaService);
        await handler.ExposedHandleAsync(new CheckDetails(), context);
        
        Assert.Single(context.SendMessages);
        Assert.Empty(context.Breaks);
        Assert.IsType<MicrosPostingRequest>(context.SendMessages[0]);
    }
    
    [Fact]
    internal async Task HandleAsyncSyncPostingMicrosIsFalseFailed()
    {
        var context = new MockEventContext();
        var operaService = GetOperaServiceWithThrow(false);
        var handler = new ExposedCheckDetailsHandler(operaService);
        var correlationId = default(int).ToString();
        var checkDetails = new CheckDetails
        {
            CorrelationId = correlationId
        };
        await handler.ExposedHandleAsync(checkDetails, context);
        
        Assert.Single(context.SendMessages);
        Assert.Empty(context.Breaks);
        Assert.Equivalent(context.SendMessages[0], new PostTransactionsResponse(correlationId)
        {
            Succeeded = true,
            ErrorMessage = checkDetails.ToString()
        });
    }
    
    private static IOperaService GetOperaService(bool syncPostingMicros = true)
    {
        var operaService = new Mock<IOperaService>();

        operaService.SetupGet(fs => fs.Environment).Returns(new ApplicationEnvironment
        {
            SyncPostingMicros = syncPostingMicros
        });

        operaService
            .Setup(fs => fs.AddCheckDetails(It.IsAny<GuestCheckDetails>()));

        return operaService.Object;
    }
    
    private static IOperaService GetOperaServiceWithThrow(bool syncPostingMicros = true)
    {
        var operaService = new Mock<IOperaService>();

        operaService.SetupGet(fs => fs.Environment).Returns(new ApplicationEnvironment
        {
            SyncPostingMicros = syncPostingMicros
        });
        
        operaService
            .Setup(fs => fs.AddCheckDetails(It.IsAny<GuestCheckDetails>()))
            .Throws<Exception>();

        return operaService.Object;
    }
}