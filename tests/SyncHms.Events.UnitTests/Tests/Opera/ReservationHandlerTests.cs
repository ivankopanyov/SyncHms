namespace SyncHms.Events.UnitTests.Tests.Opera;

public class ReservationHandlerTests
{
    [Fact]
    internal async Task HandleAsyncSuccess()
    {
        var context = new MockEventContext();
        var operaService = GetOperaService("ResortCode");
        var handler = new ExposedReservationHandler(operaService);
        await handler.ExposedHandleAsync(new ReservationInfo(), context);

        Assert.Single(context.SendMessages);
        Assert.Empty(context.Breaks);
        Assert.IsType<ReservationUpdatedMessage>(context.SendMessages[0]);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    internal async Task HandleAsyncResortCodeIsEmptyOrWhiteSpaceFailed(string resortCode)
    {
        var context = new MockEventContext();
        var operaService = GetOperaService(resortCode);
        var handler = new ExposedReservationHandler(operaService);
        await Assert.ThrowsAsync<ArgumentException>(async () =>
            await handler.ExposedHandleAsync(new ReservationInfo(), context));
    }
    
    [Fact]
    internal async Task HandleAsyncReservationNotFoundFailed()
    {
        var context = new MockEventContext();
        var operaService = GetOperaService("ResortCode", true);
        var handler = new ExposedReservationHandler(operaService);
        await handler.ExposedHandleAsync(new ReservationInfo(), context);

        Assert.Single(context.Breaks);
        Assert.Empty(context.SendMessages);
        Assert.Equal(context.Breaks[0], ("Reservation not found.", null));
    }
    
    [Fact]
    internal async Task HandleAsyncFailed()
    {
        var context = new MockEventContext();
        var operaService = GetOperaServiceWithThrow();
        var handler = new ExposedReservationHandler(operaService);
        await Assert.ThrowsAsync<Exception>(async () =>
            await handler.ExposedHandleAsync(new ReservationInfo(), context));
    }

    private static IOperaService GetOperaService(string resortCode, bool returnNull = false)
    {
        var operaService = new Mock<IOperaService>();

        operaService.SetupGet(fs => fs.Environment).Returns(new ApplicationEnvironment
        {
            ResortCode = resortCode
        });
        
        operaService
            .Setup(fs => fs.GetReservationUpdatedMessageAsync(It.IsAny<decimal>(), It.IsAny<string>()))
            .ReturnsAsync(returnNull ? null : new ReservationUpdatedMessage());

        return operaService.Object;
    }
    
    private static IOperaService GetOperaServiceWithThrow()
    {
        var operaService = new Mock<IOperaService>();

        operaService.SetupGet(fs => fs.Environment).Returns(new ApplicationEnvironment
        {
            ResortCode = "ResortCode"
        });
        
        operaService
            .Setup(fs => fs.GetReservationUpdatedMessageAsync(It.IsAny<decimal>(), It.IsAny<string>()))
            .Throws<Exception>();

        return operaService.Object;
    }
}
