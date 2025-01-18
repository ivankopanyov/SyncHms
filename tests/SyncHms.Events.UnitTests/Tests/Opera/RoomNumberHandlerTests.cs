namespace SyncHms.Events.UnitTests.Tests.Opera;

public class RoomNumberHandlerTests
{
    [Fact]
    internal async Task HandleAsyncSuccess()
    {
        var context = new MockEventContext();
        var operaService = GetOperaService();
        var handler = new ExposedRoomNumberHandler(operaService);
        var correlationId = default(int).ToString();
        await handler.ExposedHandleAsync(new RoomNumberRequest
        {
            CorrelationId = correlationId,
            ReservationGuestId = $"{Random.Shared.Next(int.MaxValue)}/{Random.Shared.Next(int.MaxValue)}"
        }, context);
        
        Assert.Single(context.SendMessages);
        Assert.Empty(context.Breaks);
        Assert.IsType<FiasPostRequest>(context.SendMessages[0]);
    }

    [Fact]
    internal async Task HandleAsyncRoomNotFoundFailed()
    {
        var context = new MockEventContext();
        var operaService = GetOperaService(true);
        var handler = new ExposedRoomNumberHandler(operaService);
        var correlationId = default(int).ToString();
        var reservationNumber = Random.Shared.Next(int.MaxValue);
        await handler.ExposedHandleAsync(new RoomNumberRequest
        {
            CorrelationId = correlationId,
            ReservationGuestId = $"{Random.Shared.Next(int.MaxValue)}/{reservationNumber}"
        }, context);
        
        Assert.Single(context.SendMessages);
        Assert.Empty(context.Breaks);
        Assert.Equivalent(context.SendMessages[0], new PostTransactionsResponse(correlationId)
        {
            Succeeded = false,
            ErrorMessage = $"Reservation {reservationNumber} not found."
        });
    }
    
    [Fact]
    internal async Task HandleAsyncFailed()
    {
        var context = new MockEventContext();
        var operaService = GetOperaServiceWithThrow();
        var handler = new ExposedRoomNumberHandler(operaService);
        var correlationId = default(int).ToString();
        await handler.ExposedHandleAsync(new RoomNumberRequest
        {
            CorrelationId = correlationId,
            ReservationGuestId = $"{Random.Shared.Next(int.MaxValue)}/{Random.Shared.Next(int.MaxValue)}"
        }, context);
        
        Assert.Single(context.SendMessages);
        Assert.Empty(context.Breaks);
        Assert.Equivalent(context.SendMessages[0], new PostTransactionsResponse(correlationId)
        {
            Succeeded = false,
            ErrorMessage = new Exception().Message
        });
    }

    private static IOperaService GetOperaService(bool returnNull = false)
    {
        var operaService = new Mock<IOperaService>();
        
        operaService
            .Setup(fs => fs.GetRoomNumberAsync(It.IsAny<decimal>()))
            .ReturnsAsync(returnNull ? null : default(int).ToString());

        return operaService.Object;
    }
    
    private static IOperaService GetOperaServiceWithThrow()
    {
        var operaService = new Mock<IOperaService>();
        
        operaService
            .Setup(fs => fs.GetRoomNumberAsync(It.IsAny<decimal>()))
            .Throws<Exception>();

        return operaService.Object;
    }
}
