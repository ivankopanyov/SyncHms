namespace SyncHms.Events.Workers;

/// <summary>
/// Класс, описывающий фоновый сервис, пересылающий события, полученные сервисами приложения в шину данных.
/// </summary>
internal class MessageProxyWorker : BackgroundService
{
    /// <summary>Инициализация сервиса.</summary>
    /// <param name="fiasService">Сервис интеграции с удаленным сервисом <c>FIAS</c></param>
    /// <param name="sanatoriumService">Сервис интеграции с шиной данных удаленного сервиса <c>Sanatorium</c></param>
    /// <param name="checkInPublisher">Сервис публикации событий <see cref="FiasGuestCheckIn"/></param>
    /// <param name="checkOutPublisher">Сервис публикации событий <see cref="FiasGuestCheckOut"/></param>
    /// <param name="changePublisher">Сервис публикации событий <see cref="FiasGuestChange"/></param>
    /// <param name="postingRequestPublisher">Сервис публикации событий <see cref="FiasPostingRequest"/></param>
    public MessageProxyWorker(
        IFiasService fiasService,
        ISanatoriumService sanatoriumService,
        IEventPublisher<FiasGuestCheckIn> checkInPublisher,
        IEventPublisher<FiasGuestCheckOut> checkOutPublisher,
        IEventPublisher<FiasGuestChange> changePublisher,
        IEventPublisher<FiasConnection> fiasConnectionPublisher,
        IEventPublisher<PostTransactionsRequest> postingRequestPublisher)
    {
        fiasService.FiasGuestCheckInEvent += checkInPublisher.Publish;
        fiasService.FiasGuestCheckOutEvent += checkOutPublisher.Publish;
        fiasService.FiasGuestChangeEvent += changePublisher.Publish;
        fiasService.ChangeServiceStateEvent += (connected, message) => fiasConnectionPublisher.Publish(new FiasConnection
        {
            Connected = connected,
            Message = message
        });
        sanatoriumService.PostingRequestEvent += postingRequestPublisher.Publish;
    }
    
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.CompletedTask;
    }
}