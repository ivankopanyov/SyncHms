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
        var emisService = GetEmisService();
        var handler = new ExposedUpdatedProfileHandler(operaService, emisService);
        await handler.ExposedHandleAsync(new UpdatedProfile(), context);
        Assert.Empty(context.Breaks);
        Assert.Equal(length, context.SendMessages.Count);
        Assert.Equivalent(reservation, context.SendMessages
            .Select(s => ((UpdatedReservation)s!).ReservationNumber).ToArray());
    }

    [Fact]
    internal async Task HandleAsyncReservationListIsEmptySuccess()
    {
        var context = new MockEventContext();
        var operaService = GetOperaService("ResortCode");
        var emisService = GetEmisService();
        var handler = new ExposedUpdatedProfileHandler(operaService, emisService);
        var profileNumber = Random.Shared.Next();
        await handler.ExposedHandleAsync(new UpdatedProfile
        {
            ProfileNumber = profileNumber
        }, context);
        Assert.Empty(context.SendMessages);
        Assert.Empty(context.Breaks);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    internal async Task HandleAsyncResortCodeIsEmptyOrWhiteSpaceFailed(string resortCode)
    {
        var context = new MockEventContext();
        var operaService = GetOperaService(resortCode);
        var emisService = GetEmisService();
        var handler = new ExposedUpdatedProfileHandler(operaService, emisService);
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
        var emisService = GetEmisService();
        var handler = new ExposedUpdatedProfileHandler(operaService, emisService);
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
