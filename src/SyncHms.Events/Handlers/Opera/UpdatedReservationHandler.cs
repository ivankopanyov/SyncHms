namespace SyncHms.Events.Handlers.Opera;

/// <summary>
/// Класс, описывающий обработчик события <see cref="UpdatedReservation"/>,
/// оповещающего об изменении бронирования.<br/>
/// Унаследован от класса <see cref="Handler{TIn}"/>
/// </summary>
internal class UpdatedReservationHandler : Handler<UpdatedReservation>
{
    /// <summary>Соответствие статусов бронирования систем <c>OPERA</c> и <c>Sanatorium</c>.</summary>
    private static readonly Dictionary<string, string> Statuses = new()
    {
        {OperaReservationStatus.CheckedIn,  SanatoriumReservationStatus.CheckedIn},
        {OperaReservationStatus.CheckedOut, SanatoriumReservationStatus.CheckedOut},
        {OperaReservationStatus.Reserved,   SanatoriumReservationStatus.Reserved},
        {OperaReservationStatus.Cancelled,  SanatoriumReservationStatus.Cancelled},
        {OperaReservationStatus.WaitList,   SanatoriumReservationStatus.WaitList},
        {OperaReservationStatus.NoShow,     SanatoriumReservationStatus.NoShow}
    };

    /// <summary>
    /// Метод, обрабатывающий событие <see cref="UpdatedReservation"/>.
    /// Отправляет в шину данных событие <see cref="ReservationInfo"/>.<br/>
    /// Переопределяет метод <see cref="Handler{TIn}.HandleAsync"/>
    /// </summary>
    /// <param name="in">Экземпляр обрабатываемого события.</param>
    /// <param name="context">Контекст обработки события.</param>
    protected override Task HandleAsync(UpdatedReservation @in, IEventContext context)
    {
        if (!Statuses.TryGetValue(@in.Status, out var status))
        {
            context.Break($"No match found for status \"{@in.Status}\".");
            return Task.CompletedTask;
        }

        context.Send(new ReservationInfo
        {
            ReservationNumber = @in.ReservationNumber,
            Status = status
        });

        return Task.CompletedTask;
    }

    /// <summary>
    /// Метод, возвращающий краткое описание события <see cref="UpdatedReservation"/>.<br/>
    /// Переопределяет метод <see cref="Handler{TIn}.Message"/>
    /// </summary>
    /// <param name="in">Экземпляр обрабатываемого события.</param>
    /// <returns>Краткое описание события.</returns>
    protected override string Message(UpdatedReservation @in) => $"Reservation: {@in.ReservationNumber}";

    /// <summary>Метод переопределения стандартного имени обработчика.</summary>
    /// <param name="in">Объект обрабатываемого события.</param>
    /// <returns>
    /// Переопределленое имя обработчика.<br/>
    /// Присваивает обработчику имя из значения <see cref="UpdatedReservation.Status"/>
    /// </returns>
    protected override string HandlerName(UpdatedReservation @in) => @in.Status;
}
