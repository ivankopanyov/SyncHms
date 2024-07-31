namespace SyncHms.Events.Infrastructure;

internal class MessageProxyService : BackgroundService
{
    public MessageProxyService(
        IFiasService fiasService,
        ISanatoriumService sanatoriumService,
        IEventPublisher<FiasGuestCheckIn> checkInPublisher,
        IEventPublisher<FiasGuestCheckOut> checkOutPublisher,
        IEventPublisher<FiasGuestChange> changePublisher,
        IEventPublisher<PostingRequest> postingRequestPublisher)
    {
        fiasService.FiasGuestCheckInEvent += checkInPublisher.Publish;
        fiasService.FiasGuestCheckOutEvent += checkOutPublisher.Publish;
        fiasService.FiasGuestChangeEvent += changePublisher.Publish;
        sanatoriumService.PostingRequestEvent += postingRequestPublisher.Publish;
    }
    
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.CompletedTask;
    }
}