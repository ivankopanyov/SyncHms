namespace SyncHms.Events.UnitTests.Tests.Fias;

public class CheckInHandlerTests : ReservationHandlerTestsBase
{
    [Theory]
    [ClassData(typeof(CheckInHandlerCases))]
    internal async Task HandleAsyncSuccess(FiasGuestCheckIn data, ReservationInfo excepted)
    {
        var context = new MockEventContext();
        var handler = new ExposedCheckInHandler();
        await handler.ExposedHandleAsync(data, context);
        CheckReservationInfo(excepted, context);
    }
}