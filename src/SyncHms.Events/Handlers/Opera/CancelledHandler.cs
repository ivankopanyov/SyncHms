namespace SyncHms.Events.Handlers.Opera;

/// <summary>
/// Класс, описывающий обработчик события <see cref="GuestCancelled"/>,
/// оповещающего об изменении статуса бронирования на <c>CANCELLED</c>.<br/>
/// Унаследован от класса <see cref="GuestHandlerBase{TIn}"/>
/// </summary>
internal class CancelledHandler : GuestHandlerBase<GuestCancelled>
{
    /// <summary>
    /// Метод, обрабатывающий событие <see cref="GuestCancelled"/>.
    /// Отправляет в шину данных событие <see cref="ReservationInfo"/>.<br/>
    /// Переопределяет метод <see cref="Handler{TIn}.HandleAsync"/>
    /// </summary>
    /// <param name="in">Экземпляр обрабатываемого события.</param>
    /// <param name="context">Контекст обработки события.</param>
    protected override Task HandleAsync(GuestCancelled @in, IEventContext context)
    {
        SendUpdatedReservationMessage(@in, "OUT", context);
        return Task.CompletedTask;
    }
}
