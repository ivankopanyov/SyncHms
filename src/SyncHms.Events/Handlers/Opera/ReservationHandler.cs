namespace SyncHms.Events.Handlers.Opera;

/// <summary>
/// Класс, описывающий обработчик события <see cref="ReservationInfo"/>,
/// оповещающего об изменении данных бронирования.<br/>
/// Унаследован от класса <see cref="Handler{TIn}"/>
/// </summary>
internal class ReservationHandler(IOperaService operaService) : Handler<ReservationInfo>
{
    /// <summary>
    /// Метод, обрабатывающий событие <see cref="ReservationInfo"/>.
    /// Проводит попытку получения данных о бронировании из базы данных <c>OPERA</c>.<br/>
    /// В случае удачного запроса, отправляет в шину данных событие <see cref="ReservationUpdatedMessage"/>.<br/>
    /// В случае отсутсвия информации о бронировании в базе данных, завершает выполнение задачи.<br/>
    /// В случае неудачной попытки подключения к базе данных, возвращает событие в шину данных.<br/>
    /// Переопределяет метод <see cref="Handler{TIn}.HandleAsync"/>
    /// </summary>
    /// <param name="in">Экземпляр обрабатываемого события.</param>
    /// <param name="context">Контекст обработки события.</param>
    protected override async Task HandleAsync(ReservationInfo @in, IEventContext context)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(operaService.Environment.ResortCode, nameof(operaService.Environment.ResortCode));

        if (await operaService.GetReservationUpdatedMessageAsync(@in.ReservationNumber, @in.Status) is { } reservationResponse)
        {
            context.Send(reservationResponse);
        }
        else
        {
            context.Break("Reservation not found.");
        }
    }

    /// <summary>
    /// Метод, возвращающий краткое описание события <see cref="ReservationInfo"/><br/>
    /// Переопределяет метод <see cref="Handler{TIn}.Message"/>
    /// </summary>
    /// <param name="in">Экземпляр обрабатываемого события.</param>
    /// <returns>Краткое описание события.</returns>
    protected override string Message(ReservationInfo @in)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append($"Reservation: {@in.ReservationNumber:0}");

        if (!string.IsNullOrWhiteSpace(@in.Status))
            stringBuilder.Append($", Status: {@in.Status}");

        return stringBuilder.ToString();
    }
}
