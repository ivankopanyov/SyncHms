namespace SyncHms.Events.Handlers.Sanatorium;

/// <summary>
/// Класс, описывающий обработчик события <see cref="ReservationUpdatedMessage"/>,
/// оповещающего об изменении данных бронирования.<br/>
/// Унаследован от класса <see cref="Handler{TIn}"/>
/// </summary>
internal class UpdateReservationHandler(ISanatoriumService sanatoriumService) : Handler<ReservationUpdatedMessage>
{
    /// <summary>
    /// Метод, обрабатывающий событие <see cref="ReservationUpdatedMessage"/>.
    /// Отправляет в шину данных сервиса <c>Sanatorium</c> событие <see cref="ReservationUpdatedMessage"/>.<br/>
    /// В случае неудачной попытки отправки события, возвращает событие в шину данных приложения.<br/>
    /// Переопределяет метод <see cref="Handler{TIn}.HandleAsync"/>
    /// </summary>
    /// <param name="in">Экземпляр обрабатываемого события.</param>
    /// <param name="context">Контекст обработки события.</param>
    protected override async Task HandleAsync(ReservationUpdatedMessage @in, IEventContext context)
    {
        await sanatoriumService.SendReservationUpdatedMessageAsync(@in);
    }
    
    /// <summary>
    /// Метод, возвращающий краткое описание события <see cref="ReservationUpdatedMessage"/><br/>
    /// Переопределяет метод <see cref="Handler{TIn}.Message"/>
    /// </summary>
    /// <param name="in">Экземпляр обрабатываемого события.</param>
    /// <returns>Краткое описание события.</returns>
    protected override string? Message(ReservationUpdatedMessage @in)
    {
        var stringBuilder = new StringBuilder();
        if (@in.ReservationGuests?.FirstOrDefault() is { } guest)
        {
            if (!string.IsNullOrEmpty(guest.Id))
                stringBuilder.Append($"Id: {guest.Id}, {string.Join(' ', guest.LastName, guest.FirstName, guest.MiddleName)}");
        }

        if (@in.CurrentTimeline is { } timeline)
        {
            if (!string.IsNullOrEmpty(timeline.RoomCode))
            {
                if (stringBuilder.Length > 0)
                    stringBuilder.Append(", ");

                stringBuilder.Append($"Room: {timeline.RoomCode}");
            }

            if (!string.IsNullOrEmpty(timeline.RateCode))
            {
                if (stringBuilder.Length > 0)
                    stringBuilder.Append(", ");

                stringBuilder.Append($"Rate: {timeline.RateCode}");
            }
        }

        return stringBuilder.ToString();
    }
}
