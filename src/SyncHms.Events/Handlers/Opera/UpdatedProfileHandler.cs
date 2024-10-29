namespace SyncHms.Events.Handlers.Opera;

/// <summary>
/// Класс, описывающий обработчик события <see cref="UpdatedProfile"/>,
/// оповещающего об изменении профиля.<br/>
/// Унаследован от класса <see cref="Handler{TIn}"/>
/// </summary>
internal class UpdatedProfileHandler(IOperaService operaService, IEmisService emisService) : Handler<UpdatedProfile>
{
    /// <summary>
    /// Метод, обрабатывающий событие <see cref="UpdatedProfile"/>.<br/>
    /// Отправляет в шину данных события <see cref="UpdatedReservation"/>.<br/>
    /// Переопределяет метод <see cref="Handler{TIn}.HandleAsync"/>
    /// </summary>
    /// <param name="in">Экземпляр обрабатываемого события.</param>
    /// <param name="context">Контекст обработки события.</param>
    protected override async Task HandleAsync(UpdatedProfile @in, IEventContext context)
    {
        try
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(operaService.Environment.ResortCode, nameof(operaService.Environment.ResortCode));

            var reservations = await operaService
                .GetReservationsByProfileAsync(@in.ProfileNumber, emisService.Statuses);

            foreach (var reservation in reservations)
            {
                context.Send(new UpdatedReservation
                {
                    ReservationNumber = reservation,
                    Status = EmisReservationStatus.Emis
                });
            }
        }
        catch (Exception ex)
        {
            context.Send(new UpdatedProfileFailed
            {
                ProfileNumber = @in.ProfileNumber,
                ErrorMessage = ex.Message
            });
        }
    }

    /// <summary>
    /// Метод, возвращающий краткое описание события <see cref="UpdatedProfile"/>.<br/>
    /// Переопределяет метод <see cref="Handler{TIn}.Message"/>
    /// </summary>
    /// <param name="in">Экземпляр обрабатываемого события.</param>
    /// <returns>Краткое описание события.</returns>
    protected override string Message(UpdatedProfile @in) => $"Profile: {@in.ProfileNumber:0}";
}
