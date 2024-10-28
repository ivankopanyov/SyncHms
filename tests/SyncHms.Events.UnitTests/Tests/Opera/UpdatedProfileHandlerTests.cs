namespace SyncHms.Events.UnitTests.Tests.Opera;

public class UpdatedProfileHandlerTests : OperaHandlerTestsBase
{
    [Fact]
    internal async Task HandleAsyncSuccess()
    {
        var length = Random.Shared.Next(10, 100);
        var reservation = new decimal[length];
        for (var i = 0; i < length; i++)
            reservation[i] = Random.Shared.Next(100000, 99999999);

        var context = new MockEventContext();
        var operaService = GetOperaService("ResortCode", reservationNumbers: reservation);
        var handler = new ExposedUpdatedProfileHandler(operaService);
        await handler.ExposedHandleAsync(new UpdatedProfile(), context);
        Assert.Empty(context.Breaks);
        Assert.Equal(length, context.SendMessages.Count);
        Assert.Equivalent(reservation, context.SendMessages
            .Select(s => ((UpdatedReservation)s!).ReservationNumber).ToArray());
    }

    [Fact]
    internal async Task HandleAsyncReservationListIsEmptyFailed()
    {
        var context = new MockEventContext();
        var operaService = GetOperaService("ResortCode");
        var handler = new ExposedUpdatedProfileHandler(operaService);
        var profileNumber = Random.Shared.Next();
        await handler.ExposedHandleAsync(new UpdatedProfile
        {
            ProfileNumber = profileNumber
        }, context);
        Assert.Single(context.SendMessages);
        Assert.Empty(context.Breaks);
        Assert.IsType<UpdatedProfileFailed>(context.SendMessages[0]);
        Assert.Equal(profileNumber, ((UpdatedProfileFailed)context.SendMessages[0]!).ProfileNumber);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    internal async Task HandleAsyncResortCodeIsEmptyOrWhiteSpaceFailed(string resortCode)
    {
        var context = new MockEventContext();
        var operaService = GetOperaService(resortCode);
        var handler = new ExposedUpdatedProfileHandler(operaService);
        var profileNumber = Random.Shared.Next();
        await handler.ExposedHandleAsync(new UpdatedProfile
        {
            ProfileNumber = profileNumber
        }, context);
        Assert.Single(context.SendMessages);
        Assert.Empty(context.Breaks);
        Assert.IsType<UpdatedProfileFailed>(context.SendMessages[0]);
        Assert.Equal(profileNumber, ((UpdatedProfileFailed)context.SendMessages[0]!).ProfileNumber);
    }

    [Fact]
    internal async Task HandleAsyncFailed()
    {
        var context = new MockEventContext();
        var operaService = GetOperaServiceWithThrow();
        var handler = new ExposedUpdatedProfileHandler(operaService);
        var profileNumber = Random.Shared.Next();
        await handler.ExposedHandleAsync(new UpdatedProfile
        {
            ProfileNumber = profileNumber
        }, context);
        Assert.Single(context.SendMessages);
        Assert.Empty(context.Breaks);
        Assert.IsType<UpdatedProfileFailed>(context.SendMessages[0]);
        Assert.Equal(profileNumber, ((UpdatedProfileFailed)context.SendMessages[0]!).ProfileNumber);
    }
}
