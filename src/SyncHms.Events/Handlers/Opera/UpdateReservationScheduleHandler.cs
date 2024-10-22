namespace SyncHms.Events.Handlers.Opera;

/// <summary>
/// Класс, описывающий обработчик задачи на получений обновлений бронирований из базы данных <c>OPERA</c>,
/// инициированных планировщиком событий.<br/>
/// Унаследован от класса <see cref="ScheduleHandler"/>
/// </summary>
/// <param name="operaService">Экземпляр сервиса взаимодействия с базой данных <c>OPERA</c></param>
[ScheduleDescription("Мониторинг обновлений бронирований в базе данных OPERA.\nСтатусы: RESERVED, CANCELLED, NO SHOW")]
internal class UpdateReservationScheduleHandler(IOperaService operaService) : ScheduleHandler
{
    /// <summary>
    /// Метод, запрашивающий обновления бронирований из базы данных <c>OPERA</c>.<br/>
    /// Переопределяет метод <see cref="ScheduleHandler.HandleAsync"/>
    /// </summary>
    /// <param name="context">Контекст обработки события.</param>
    protected override async Task HandleAsync(IScheduleEventContext context)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(operaService.Environment.ResortCode, nameof(operaService.Environment.ResortCode));

        var reservations = await operaService.GetUpdatedReservationsAsync(context.Previous, context.Current);
        foreach (var reservation in reservations)
        {
            switch (reservation.Status)
            {
                case ReservationStatus.Reserved:
                    SendUpdatedReservationMessage<GuestReserved>(reservation, context);
                    return;
                case ReservationStatus.Cancelled:
                    SendUpdatedReservationMessage<GuestCancelled>(reservation, context);
                    return;
                case ReservationStatus.NoShow:
                    SendUpdatedReservationMessage<GuestNoShow>(reservation, context);
                    return;
            }
        }
    }

    /// <summary>Метод отправляет объект с информацие о бронировании в шину данных.</summary>
    /// <typeparam name="T">Тип объекта бронирования.</typeparam>
    /// <param name="reservation">Исходный объект.</param>
    private static void SendUpdatedReservationMessage<T>(UpdatedReservation reservation, IEventContext context)
        where T : GuestBase, new()
    {
        context.Send(new T
        {
            ReservationNumber = (long)reservation.ReservationNumber,
            Arrival = reservation.Arrival,
            Departure = reservation.Departure
        });
    }
}
