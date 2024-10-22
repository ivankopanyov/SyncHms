namespace SyncHms.Events.Handlers.Opera;

/// <summary>
/// Класс, описывающий обработчик события <see cref="GuestNoShow"/>,
/// оповещающего об изменении статуса бронирования на <c>NO SHOW</c>.<br/>
/// Унаследован от класса <see cref="Handler{TIn}"/>
/// </summary>
internal class NoShowHandler : Handler<GuestNoShow>
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
        context.Send(new ReservationInfo
        {
            ReservationNumber = @in.ReservationNumber,
            Status = "OUT"
        });

        return Task.CompletedTask;
    }

    /// <summary>
    /// Метод, возвращающий краткое описание события <see cref="GuestNoShow"/><br/>
    /// Переопределяет метод <see cref="Handler{TIn}.Message"/>
    /// </summary>
    /// <param name="in">Экземпляр обрабатываемого события.</param>
    /// <returns>Краткое описание события.</returns>
    protected override string Message(GuestNoShow @in) => $"Reservation: {@in.ReservationNumber}";
}