namespace SyncHms.Events.Handlers.Opera;

/// <summary>
/// Класс, описывающий обработчик задачи на получений обновлений бронирований из базы данных <c>OPERA</c>,
/// инициированных планировщиком событий.<br/>
/// Унаследован от класса <see cref="ScheduleHandler"/>
/// </summary>
/// <param name="operaService">Экземпляр сервиса взаимодействия с базой данных <c>OPERA</c></param>
[ScheduleDescription("Мониторинг обновлений бронирований в базе данных OPERA.")]
internal class UpdateReservationScheduleHandler(IOperaService operaService) : ScheduleHandler
{
    /// <summary>Статусы бронирований, запрашиваемые в базе данных <c>OPERA</c></summary>
    private static readonly IReadOnlySet<string> Statuses = new HashSet<string>
    {
        OperaReservationStatus.Reserved,
        OperaReservationStatus.Cancelled,
        OperaReservationStatus.WaitList,
        OperaReservationStatus.NoShow
    };

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
            if (Statuses.Contains(reservation.Status))
                context.Send(reservation);
    }
}
