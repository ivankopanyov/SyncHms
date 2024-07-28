namespace SyncHms.Events.Infrastructure;

internal class PostingRequestPublisher : BackgroundService
{
    public PostingRequestPublisher(ISanatoriumService sanatoriumService,
        IEventPublisher<PostingRequest> publisher)
    {
        sanatoriumService.PostingRequestEvent += publisher.Publish;
    }
    
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.CompletedTask;
    }
}