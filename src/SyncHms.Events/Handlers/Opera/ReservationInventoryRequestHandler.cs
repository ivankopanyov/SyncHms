namespace SyncHms.Events.Handlers.Opera;

/// <summary>
/// Класс, описывающий обработчик события <see cref="ReservationInventoryRequest"/>,
/// оповещающего о проверке изменения резервирования инвентаря.<br/>
/// Унаследован от класса <see cref="Handler{TIn}"/>
/// </summary>
internal class ReservationInventoryRequestHandler(IOperaService operaService) : Handler<ReservationInventoryRequest>
{
    /// <summary>
    /// Метод, обрабатывающий событие <see cref="ReservationInventoryRequest"/>.
    /// Проводит попытку получения данных о резервировании инвентаря из базы данных <c>OPERA</c>.<br/>
    /// В случае удачного запроса, отправляет в шину данных событие <see cref="ReservationInventories"/>.<br/>
    /// В случае отсутсвия информации о бронировании в базе данных, завершает выполнение задачи.<br/>
    /// В случае неудачной попытки подключения к базе данных, возвращает событие в шину данных.<br/>
    /// Переопределяет метод <see cref="Handler{TIn}.HandleAsync"/>
    /// </summary>
    /// <param name="in">Экземпляр обрабатываемого события.</param>
    /// <param name="context">Контекст обработки события.</param>
    protected override async Task HandleAsync(ReservationInventoryRequest @in, IEventContext context)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(operaService.Environment.ResortCode, nameof(operaService.Environment.ResortCode));

        var reservations = await operaService.GetReservationInventoriesAsync(@in.ReservationId, @in.Room,
            OperaReservationStatus.CheckedIn, OperaReservationStatus.CheckedOut, OperaReservationStatus.Cancelled, OperaReservationStatus.NoShow);

        context.Logiable = context.HasError;

        if (reservations.Count == 0)
            return;

        var inventories = reservations
            .Where(r => r.Status == OperaReservationStatus.CheckedIn)
            .SelectMany(r => r.Inventories)
            .ToHashSet();

        foreach (var reservation in reservations)
        {
            reservation.Inventories = reservation.Status == OperaReservationStatus.CheckedIn ? inventories : [];
            if (reservation.Status != OperaReservationStatus.CheckedIn)
                reservation.Status = OperaReservationStatus.CheckedOut;
        }

        context.Send(new ReservationInventories
        {
            HasError = context.HasError,
            Queue = reservations
        });
    }

    /// <summary>
    /// Метод, возвращающий краткое описание события <see cref="ReservationInventoryRequest"/><br/>
    /// Переопределяет метод <see cref="Handler{TIn}.Message"/>
    /// </summary>
    /// <param name="in">Экземпляр обрабатываемого события.</param>
    /// <returns>Краткое описание события.</returns>
    protected override string Message(ReservationInventoryRequest @in)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append($"Reservation: {@in.ReservationId:0}");

        if (!string.IsNullOrWhiteSpace(@in.Room))
            stringBuilder.Append($", Room: {@in.Room}");

        return stringBuilder.ToString();
    }
}
