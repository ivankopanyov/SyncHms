namespace SyncHms.Events.UnitTests.Tests.Opera;

public class UpdateReservationScheduleHandlerTests : OperaHandlerTestsBase
{
    [Fact]
    internal async Task HandleAsyncSuccess()
    {
        string[] expected =
        [
            OperaReservationStatus.Reserved,
            OperaReservationStatus.Cancelled,
            OperaReservationStatus.WaitList,
            OperaReservationStatus.NoShow
        ];

        string[] valid = [.. expected, .. expected];
        string[] cases =
        [
            ..valid,
            OperaReservationStatus.CheckedIn,
            OperaReservationStatus.CheckedOut,
            OperaReservationStatus.Prospect,
            SanatoriumReservationStatus.Reserved,
            SanatoriumReservationStatus.CheckedIn,
            SanatoriumReservationStatus.CheckedOut,
            SanatoriumReservationStatus.Cancelled,
            SanatoriumReservationStatus.NoShow,
            SanatoriumReservationStatus.WaitList,
            SanatoriumReservationStatus.TemporaryCheckedOut
        ];

        var context = new MockEventContext();
        var operaService = GetOperaService("ResortCode", false, cases);
        var handler = new ExposedUpdateReservationScheduleHandler(operaService);
        await handler.ExposedHandleAsync(context);

        Assert.Empty(context.Breaks);
        Assert.Equal(valid.Length, context.SendMessages.Count);
        foreach (var send in context.SendMessages)
        {
            Assert.IsType<UpdatedReservation>(send);
            Assert.Contains(((UpdatedReservation)send!).Status, expected);
        }
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    internal async Task HandleAsyncResortCodeIsEmptyOrWhiteSpaceFailed(string resortCode)
    {
        var context = new MockEventContext();
        var operaService = GetOperaService(resortCode);
        var handler = new ExposedUpdateReservationScheduleHandler(operaService);
        await Assert.ThrowsAsync<ArgumentException>(async () =>
            await handler.ExposedHandleAsync(context));
    }

    [Fact]
    internal async Task HandleAsyncFailed()
    {
        var context = new MockEventContext();
        var operaService = GetOperaServiceWithThrow();
        var handler = new ExposedUpdateReservationScheduleHandler(operaService);
        await Assert.ThrowsAsync<Exception>(async () =>
            await handler.ExposedHandleAsync(context));
    }
}
