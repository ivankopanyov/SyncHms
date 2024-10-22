namespace SyncHms.Events.Handlers.Opera;

/// <summary>
/// Класс, описывающий обработчик события <see cref="GuestReserved"/>,
/// оповещающего об изменении статуса бронирования на <c>RESERVED</c>.<br/>
/// Унаследован от класса <see cref="GuestHandlerBase{TIn}"/>
/// </summary>
internal class ReservedHandler : GuestHandlerBase<GuestReserved>
{
    /// <summary>
    /// Метод, обрабатывающий событие <see cref="GuestReserved"/>.
    /// Отправляет в шину данных событие <see cref="ReservationInfo"/>.<br/>
    /// Переопределяет метод <see cref="Handler{TIn}.HandleAsync"/>
    /// </summary>
    /// <param name="in">Экземпляр обрабатываемого события.</param>
    /// <param name="context">Контекст обработки события.</param>
    protected override Task HandleAsync(GuestReserved @in, IEventContext context)
    {
        SendUpdatedReservationMessage(@in, "IN", context);
        return Task.CompletedTask;
    }
}