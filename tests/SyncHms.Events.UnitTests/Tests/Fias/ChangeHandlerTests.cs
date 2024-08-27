namespace SyncHms.Events.UnitTests.Tests.Fias;

public class ChangeHandlerTests : ReservationHandlerTestsBase
{
    [Theory]
    [ClassData(typeof(ChangeHandlerCases))]
    internal async Task HandleAsyncSuccess(FiasGuestChange data, ReservationInfo excepted)
    {
        var context = new MockEventContext();
        var handler = new ExposedChangeHandler();
        await handler.ExposedHandleAsync(data, context);
        CheckReservationInfo(excepted, context);
    }
}