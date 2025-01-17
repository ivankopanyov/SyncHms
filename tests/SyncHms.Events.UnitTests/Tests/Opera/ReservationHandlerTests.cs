namespace SyncHms.Events.UnitTests.Tests.Opera;

public class ReservationHandlerTests : OperaHandlerTestsBase
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
}
