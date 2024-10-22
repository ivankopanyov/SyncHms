namespace SyncHms.Events.Handlers.Opera;

/// <summary>
/// Класс, описывающий обработчик события <see cref="GuestNoShow"/>,
/// оповещающего об изменении статуса бронирования на <c>NO SHOW</c>.<br/>
/// Унаследован от класса <see cref="GuestHandlerBase{TIn}"/>
/// </summary>
internal class NoShowHandler : GuestHandlerBase<GuestNoShow>
{
    /// <summary>
    /// Метод, обрабатывающий событие <see cref="GuestNoShow"/>.
    /// Отправляет в шину данных событие <see cref="ReservationInfo"/>.<br/>
    /// Переопределяет метод <see cref="Handler{TIn}.HandleAsync"/>
    /// </summary>
    /// <param name="in">Экземпляр обрабатываемого события.</param>
    /// <param name="context">Контекст обработки события.</param>
    protected override Task HandleAsync(GuestNoShow @in, IEventContext context)
    {
        SendUpdatedReservationMessage(@in, "OUT", context);
        return Task.CompletedTask;
    }
}