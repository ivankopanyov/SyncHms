namespace SyncHms.Services.Infrastructure;

internal class ReservationUpdatedMessageHandler : IHandleMessages<ReservationUpdatedMessage>
{
    public Task Handle(ReservationUpdatedMessage message, IMessageHandlerContext context)
    {
        return Task.CompletedTask;
    }
}
