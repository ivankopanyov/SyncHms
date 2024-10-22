namespace SyncHms.Events.Handlers.Opera.Base;

/// <summary>
/// Класс, описывающий базовый обработчик событий,
/// оповещающих об изменении статуса бронирования.<br/>
/// Унаследован от класса <see cref="Handler{TIn}"/>
/// </summary>
/// <typeparam name="TIn">Тип обрабатываемого события.</typeparam>
internal abstract class GuestHandlerBase<TIn> : Handler<TIn> where TIn : GuestBase, new()
{
    /// <summary>Метод отправляет объект с информацие о бронировании в шину данных.</summary>
    /// <param name="in">Обрабатываемое событие.</param>
    /// <param name="status">Статус бронирования.</param>
    /// <param name="context">Контекст обработки события.</param>
    protected static void SendUpdatedReservationMessage(TIn @in, string status, IEventContext context)
    {
        context.Send(new ReservationInfo
        {
            ReservationNumber = @in.ReservationNumber,
            Status = status,
            ArrivalDate = @in.Arrival,
            DepartureDate = @in.Departure
        });
    }

    /// <summary>
    /// Метод, возвращающий краткое описание события.<br/>
    /// Переопределяет метод <see cref="Handler{TIn}.Message"/>
    /// </summary>
    /// <param name="in">Экземпляр обрабатываемого события.</param>
    /// <returns>Краткое описание события.</returns>
    protected override string Message(TIn @in)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append($"Reservation: {@in.ReservationNumber}");

        if (@in.Arrival != null)
            stringBuilder.Append($", Arrival: {@in.Arrival}");

        if (@in.Departure != null)
            stringBuilder.Append($", Departure: {@in.Departure}");

        return stringBuilder.ToString();
    }
}
