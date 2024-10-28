namespace SyncHms.Events.UnitTests.Tests.Opera;

public class UpdatedProfileFailedHandlerTests
{
    [Fact]
    internal async Task HandleAsyncSuccess()
    {
        var context = new MockEventContext();
        var handler = new ExposedUpdatedProfileFailedHandler();
        await handler.ExposedHandleAsync(new UpdatedProfileFailed(), context);
        Assert.Empty(context.SendMessages);
        Assert.Single(context.Breaks);
    }
}
