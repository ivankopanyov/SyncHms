namespace SyncHms.Events.UnitTests.Tests.Fias.Base;

public abstract class ReservationHandlerTestsBase
{
    private protected void CheckReservationInfo(ReservationInfo excepted, MockEventContext context)
    {
        Assert.Single(context.SendMessages);
        Assert.Empty(context.Breaks);
        Assert.Equivalent(context.SendMessages[0], excepted);
    }
}