namespace SyncHms.Events.Handlers.Opera;

/// <summary>
/// Класс, описывающий обработчик события <see cref="RoomNumberRequest"/>,
/// оповещающего о начисления платежа на номер.<br/>
/// Унаследован от класса <see cref="Handler{TIn}"/>
/// </summary>
internal class RoomNumberHandler(IOperaService operaService) : Handler<RoomNumberRequest>
{
    /// <summary>
    /// Метод, обрабатывающий событие <see cref="RoomNumberRequest"/>.
    /// Проводит попытку получения номера комнаты бронирования из базы данных <c>OPERA</c>.<br/>
    /// В случае неудачной попытки подключения к базе данных или отсутствия данных о бронировании в базе данных,
    /// отправляет в шину данных событие <see cref="PostTransactionsResponse"/>.<br/>
    /// В противном случае отправляет в шину данных событие <see cref="FiasPostRequest"/>.<br/>
    /// Переопределяет метод <see cref="Handler{TIn}.HandleAsync"/>
    /// </summary>
    /// <param name="in">Экземпляр обрабатываемого события.</param>
    /// <param name="context">Контекст обработки события.</param>
    protected override async Task HandleAsync(RoomNumberRequest @in, IEventContext context)
    {
        try
        {
            var split = @in.ReservationGuestId.Split('/');
            var profileNumber = long.Parse(split[0]);
            var reservationNumber = decimal.Parse(split[1]);

            if (await operaService.GetRoomNumberAsync(reservationNumber) is not { } room)
            {
                context.Send(new PostTransactionsResponse(@in.CorrelationId)
                {
                    Succeeded = false,
                    ErrorMessage = $"Reservation {reservationNumber} not found."
                });
                return;
            }

            context.SetMessage($"{@in}, Room: {room}, Reservation: {(long)reservationNumber}");
            @in.PaymentMethod = $"Room {room}";
            context.Send(@in.ToFiasPostRequest((long)reservationNumber, room, profileNumber));
        }
        catch (Exception ex)
        {
            context.Send(new PostTransactionsResponse(@in.CorrelationId)
            {
                Succeeded = false,
                ErrorMessage = ex.Message
            });
        }
    }

    /// <summary>
    /// Метод, возвращающий краткое описание события <see cref="RoomNumberRequest"/><br/>
    /// Переопределяет метод <see cref="Handler{TIn}.Message"/>
    /// </summary>
    /// <param name="in">Экземпляр обрабатываемого события.</param>
    /// <returns>Краткое описание события.</returns>
    protected override string Message(RoomNumberRequest @in) => @in.ToString();
}
