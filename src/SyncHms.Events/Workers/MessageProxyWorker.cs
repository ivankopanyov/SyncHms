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
    /// <param name="inventoryPublisher">Сервис публикации событий <see cref="ReservationInventoryRequest"/></param>
    public MessageProxyWorker(
        IFiasService fiasService,
        ISanatoriumService sanatoriumService,
        IEventPublisher<FiasGuestCheckIn> checkInPublisher,
        IEventPublisher<FiasGuestCheckOut> checkOutPublisher,
        IEventPublisher<FiasGuestChange> changePublisher,
        IEventPublisher<PostTransactionsRequest> postingRequestPublisher,
        IEventPublisher<ReservationInventoryRequest> inventoryPublisher)
    {
        fiasService.FiasGuestCheckInEvent += checkInPublisher.Publish;
        fiasService.FiasGuestCheckOutEvent += checkOutPublisher.Publish;
        fiasService.FiasGuestChangeEvent += changePublisher.Publish;
        sanatoriumService.PostingRequestEvent += postingRequestPublisher.Publish;

        fiasService.FiasGuestCheckInEvent += checkIn => inventoryPublisher.Publish(checkIn);
        fiasService.FiasGuestCheckOutEvent += checkOut => inventoryPublisher.Publish(checkOut);
        fiasService.FiasGuestChangeEvent += change => inventoryPublisher.Publish(change);
    }
    
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.CompletedTask;
    }
}