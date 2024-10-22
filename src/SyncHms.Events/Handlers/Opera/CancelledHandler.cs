namespace SyncHms.Events.Handlers.Opera;

/// <summary>
/// Класс, описывающий обработчик события <see cref="GuestCancelled"/>,
/// оповещающего об изменении статуса бронирования на <c>CANCELLED</c>.<br/>
/// Унаследован от класса <see cref="Handler{TIn}"/>
/// </summary>
internal class CancelledHandler : Handler<GuestCancelled>
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
        context.Send(new ReservationInfo
        {
            ReservationNumber = @in.ReservationNumber,
            Status = "OUT"
        });

        return Task.CompletedTask;
    }

    /// <summary>
    /// Метод, возвращающий краткое описание события <see cref="GuestCancelled"/><br/>
    /// Переопределяет метод <see cref="Handler{TIn}.Message"/>
    /// </summary>
    /// <param name="in">Экземпляр обрабатываемого события.</param>
    /// <returns>Краткое описание события.</returns>
    protected override string Message(GuestCancelled @in) => $"Reservation: {@in.ReservationNumber}";
}
