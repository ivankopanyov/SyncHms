namespace SyncHms.Events.Workers;

/// <summary>
/// Класс, описывающий фоновый сервис, пересылающий события, полученные сервисами приложения в шину данных.
/// </summary>
internal class MessageProxyWorker : BackgroundService
{
    /// <summary>Инициализация сервиса.</summary>
    /// <param name="fiasService">Сервис интеграции с удаленным сервисом <c>FIAS</c></param>
    /// <param name="sanatoriumService">Сервис интеграции с шиной данных удаленного сервиса <c>Sanatorium</c></param>
    /// <param name="emisService">Сервис интеграции с удаленным сервисом <c>EMIS</c></param>
    /// <param name="checkInPublisher">Сервис публикации событий <see cref="FiasGuestCheckIn"/></param>
    /// <param name="checkOutPublisher">Сервис публикации событий <see cref="FiasGuestCheckOut"/></param>
    /// <param name="changePublisher">Сервис публикации событий <see cref="FiasGuestChange"/></param>
    /// <param name="postingRequestPublisher">Сервис публикации событий <see cref="FiasPostingRequest"/></param>
    /// <param name="updatedProfilePublisher">Сервис публикации событий <see cref="UpdatedProfile"/></param>
    public MessageProxyWorker(
        IFiasService fiasService,
        ISanatoriumService sanatoriumService,
        IEmisService emisService,
        IEventPublisher<FiasGuestCheckIn> checkInPublisher,
        IEventPublisher<FiasGuestCheckOut> checkOutPublisher,
        IEventPublisher<FiasGuestChange> changePublisher,
        IEventPublisher<PostTransactionsRequest> postingRequestPublisher,
        IEventPublisher<UpdatedProfile> updatedProfilePublisher)
    {
        fiasService.FiasGuestCheckInEvent += checkInPublisher.Publish;
        fiasService.FiasGuestCheckOutEvent += checkOutPublisher.Publish;
        fiasService.FiasGuestChangeEvent += changePublisher.Publish;
        sanatoriumService.PostingRequestEvent += postingRequestPublisher.Publish;
        emisService.UpdateProfileEvent += profile => updatedProfilePublisher.Publish(new UpdatedProfile
        {
            ProfileNumber = profile
        });
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.CompletedTask;
    }
}