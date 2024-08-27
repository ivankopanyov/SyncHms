namespace SyncHms.Events.UnitTests.Tests.Opera;

public class RoomNumberHandlerTests : PostTransactionsResponseHandlerTestsBase
{
    [Theory]
    [InlineData("ReservationGuestId/2147483647", null)]
    [InlineData(null, "2147483647/FolioGenericNo")]
    [InlineData("GuestId/2147483647", "2147483647")]
    internal async Task HandleAsyncSuccess(string? reservationGuestId, string? folioGenericNo)
    {
        var context = new MockEventContext();
        var operaService = GetOperaService();
        var handler = new ExposedRoomNumberHandler(operaService);
        var correlationId = default(int).ToString();
        await handler.ExposedHandleAsync(new RoomNumberRequest
        {
            CorrelationId = correlationId,
            ReservationGuestId = reservationGuestId,
            FolioGenericNo = folioGenericNo
        }, context);
        
        Assert.Single(context.SendMessages);
        Assert.Empty(context.Breaks);
        Assert.IsType<FiasPostRequest>(context.SendMessages[0]);
    }
    
    [Theory]
    [InlineData(null, null)]
    [InlineData("ReservationGuestId", null)]
    [InlineData("GuestId/ReservationId", null)]
    [InlineData(null, "FolioGenericNo")]
    [InlineData("GuestId/ReservationId", "FolioGenericNo")]
    [InlineData("ReservationGuestId/2147483648", null)]
    [InlineData(null, "2147483648/FolioGenericNo")]
    [InlineData("GuestId/2147483648", "2147483648")]
    internal async Task HandleAsyncReservationNumberIsNullFailed(string? reservationGuestId, string? folioGenericNo)
    {
        var context = new MockEventContext();
        var operaService = GetOperaService();
        var handler = new ExposedRoomNumberHandler(operaService);
        var correlationId = default(int).ToString();
        await handler.ExposedHandleAsync(new RoomNumberRequest
        {
            CorrelationId = correlationId,
            ReservationGuestId = reservationGuestId,
            FolioGenericNo = folioGenericNo
        }, context);
        
        Assert.Single(context.SendMessages);
        Assert.Empty(context.Breaks);
        CheckPostTransactionsResponse(false, correlationId, context, "Reservation ID is null.");
    }
    
    [Fact]
    internal async Task HandleAsyncRoomNotFoundFailed()
    {
        var context = new MockEventContext();
        var operaService = GetOperaService(true);
        var handler = new ExposedRoomNumberHandler(operaService);
        var correlationId = default(int).ToString();
        var reservationNumber = default(int).ToString();
        await handler.ExposedHandleAsync(new RoomNumberRequest
        {
            CorrelationId = correlationId,
            ReservationGuestId = $"GuestId/{reservationNumber}"
        }, context);
        
        Assert.Single(context.SendMessages);
        Assert.Empty(context.Breaks);
        CheckPostTransactionsResponse(false, correlationId, context, $"Reservation {reservationNumber} not found.");
    }
    
    [Fact]
    internal async Task HandleAsyncFailed()
    {
        var context = new MockEventContext();
        var operaService = GetOperaServiceWithThrow();
        var handler = new ExposedRoomNumberHandler(operaService);
        var correlationId = default(int).ToString();
        var reservationNumber = default(int).ToString();
        await handler.ExposedHandleAsync(new RoomNumberRequest
        {
            CorrelationId = correlationId,
            ReservationGuestId = $"GuestId/{reservationNumber}"
        }, context);
        
        Assert.Single(context.SendMessages);
        Assert.Empty(context.Breaks);
        CheckPostTransactionsResponse(false, correlationId, context, new Exception().Message);
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