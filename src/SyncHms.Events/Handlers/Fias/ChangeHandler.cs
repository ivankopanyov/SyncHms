namespace SyncHms.Events.Handlers.Fias;

/// <summary>
/// Класс, описывающий обработчик события <see cref="FiasGuestChange"/>
/// оповещающего об изменении данных бронирования.<br/>
/// Унаследован от класса <see cref="Handler{TIn}"/>
/// </summary>
internal class ChangeHandler : Handler<FiasGuestChange>
{
    /// <summary>
    /// Метод, обрабатывающий событие <see cref="FiasGuestChange"/>.
    /// Отправляет в шину данных событие <see cref="ReservationInfo"/>.<br/>
    /// Переопределяет метод <see cref="Handler{TIn}.HandleAsync"/>
    /// </summary>
    /// <param name="in">Экземпляр обрабатываемого события.</param>
    /// <param name="context">Контекст обработки события.</param>
    protected override Task HandleAsync(FiasGuestChange @in, IEventContext context)
    {
        context.Send(new ReservationInfo
        {
            ReservationNumber = @in.ReservationNumber,
            Status = SanatoriumReservationStatus.CheckedIn
        });

        return Task.CompletedTask;
    }

    /// <summary>
    /// Метод, возвращающий краткое описание события <see cref="FiasGuestChange"/><br/>
    /// Переопределяет метод <see cref="Handler{TIn}.Message"/>
    /// </summary>
    /// <param name="in">Экземпляр обрабатываемого события.</param>
    /// <returns>Краткое описание события.</returns>
    protected override string Message(FiasGuestChange @in)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append($"Reservation: {@in.ReservationNumber}");

        if (!string.IsNullOrWhiteSpace(@in.RoomNumber))
            stringBuilder.Append($", Room: {@in.RoomNumber}");

        if (!string.IsNullOrWhiteSpace(@in.GuestName))
            stringBuilder.Append($" {@in.GuestName}");

        return stringBuilder.ToString();
    }
}
