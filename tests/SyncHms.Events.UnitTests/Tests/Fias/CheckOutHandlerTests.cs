namespace SyncHms.Events.UnitTests.Tests.Fias;

public class CheckOutHandlerTests : ReservationHandlerTestsBase
{
    [Theory]
    [ClassData(typeof(CheckOutHandlerCases))]
    internal async Task HandleAsyncSuccess(FiasGuestCheckOut data, ReservationInfo excepted)
    {
        var context = new MockEventContext();
        var handler = new ExposedCheckOutHandler();
        await handler.ExposedHandleAsync(data, context);
        CheckReservationInfo(excepted, context);
    }
}