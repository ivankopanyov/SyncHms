﻿namespace SyncHms.Events.Handlers.Fias;

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
        DateTime? arrivalDate = null;
        if (@in.GuestArrivalDate is { } arrival)
            arrivalDate = arrival.ToDateTime(default);

        DateTime? departureDate = null;
        if (@in.GuestDepartureDate is { } departure)
        {
            departureDate = departure.ToDateTime(default);
            if (arrivalDate is { } date && date == departureDate)
                departureDate = date.AddDays(1).AddTicks(-1);
        }

        context.Send(new ReservationInfo
        {
            ReservationNumber = @in.ReservationNumber,
            Room = @in.RoomNumber,
            Status = "IN",
            ArrivalDate = arrivalDate,
            DepartureDate = departureDate,
            NoPost = @in.NoPostStatus
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
