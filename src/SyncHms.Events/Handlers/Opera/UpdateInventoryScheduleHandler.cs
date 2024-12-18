namespace SyncHms.Events.Handlers.Opera;

/// <summary>
/// Класс, описывающий обработчик задачи на получений обновлений резервирования инвенторя из базы данных <c>OPERA</c>,
/// инициированных планировщиком событий.<br/>
/// Унаследован от класса <see cref="ScheduleHandler"/>
/// </summary>
/// <param name="operaService">Экземпляр сервиса взаимодействия с базой данных <c>OPERA</c></param>
[ScheduleDescription("Мониторинг обновлений резервирования инвенторя в базе данных OPERA.")]
internal class UpdateInventoryScheduleHandler(IOperaService operaService) : ScheduleHandler
{
    /// <summary>
    /// Метод, запрашивающий обновления резервирования инвенторя из базы данных <c>OPERA</c>.<br/>
    /// Переопределяет метод <see cref="ScheduleHandler.HandleAsync"/>
    /// </summary>
    /// <param name="context">Контекст обработки события.</param>
    protected override async Task HandleAsync(IScheduleEventContext context)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(operaService.Environment.ResortCode, nameof(operaService.Environment.ResortCode));

        var reservations = await operaService.GetUpdatedReservationInventoriesAsync(context.Previous, context.Current, OperaReservationStatus.CheckedIn);
        foreach (var reservationId in reservations)
        {
            context.Send(new ReservationInventoryRequest
            {
                ReservationId = reservationId
            });
        }
    }
}
