namespace SyncHms.Events.UnitTests.Tests.Opera;

public class UpdatedReservationHandlerTests : ReservationHandlerTestsBase
{
    [Theory]
    [InlineData(OperaReservationStatus.CheckedIn,  SanatoriumReservationStatus.CheckedIn)]
    [InlineData(OperaReservationStatus.CheckedOut, SanatoriumReservationStatus.CheckedOut)]
    [InlineData(OperaReservationStatus.Reserved,   SanatoriumReservationStatus.Reserved)]
    [InlineData(OperaReservationStatus.Cancelled,  SanatoriumReservationStatus.Cancelled)]
    [InlineData(OperaReservationStatus.WaitList,   SanatoriumReservationStatus.WaitList)]
    [InlineData(OperaReservationStatus.NoShow,     SanatoriumReservationStatus.NoShow)]
    internal async Task HandleAsyncSuccess(string status, string expectedStatus)
    {
        var context = new MockEventContext();
        var handler = new ExposedUpdatedReservationHandler();
        decimal reservationNumber = Random.Shared.Next();
        await handler.ExposedHandleAsync(new UpdatedReservation
        {
            ReservationNumber = reservationNumber,
            Status = status
        }, context);

        CheckReservationInfo(new ReservationInfo
        {
            ReservationNumber = reservationNumber,
            Status = expectedStatus
        }, context);
    }

    [Theory]
    [InlineData(OperaReservationStatus.Prospect)]
    [InlineData(SanatoriumReservationStatus.Reserved)]
    [InlineData(SanatoriumReservationStatus.CheckedIn)]
    [InlineData(SanatoriumReservationStatus.CheckedOut)]
    [InlineData(SanatoriumReservationStatus.Cancelled)]
    [InlineData(SanatoriumReservationStatus.NoShow)]
    [InlineData(SanatoriumReservationStatus.WaitList)]
    [InlineData(SanatoriumReservationStatus.TemporaryCheckedOut)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("STATUS")]
    internal async Task HandleAsyncNoMatchFoundForStatusFailed(string status)
    {
        var context = new MockEventContext();
        var handler = new ExposedUpdatedReservationHandler();
        await handler.ExposedHandleAsync(new UpdatedReservation
        {
            Status = status
        }, context);

        Assert.Empty(context.SendMessages);
        Assert.Single(context.Breaks);
        (string? Message, Exception? Exception) expected = ($"No match found for status \"{status}\".", null);
        Assert.Equivalent(expected, context.Breaks[0]);
    }
}
