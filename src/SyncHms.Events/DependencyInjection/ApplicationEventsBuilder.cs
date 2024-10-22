namespace SyncHms.Events.DependencyInjection;

/// <summary>
/// Класс, описывающий построитель, регистрирующий обработчики событий в контейнере зависимостей.
/// Унаследован от класса <see cref="EventsBusBuilder"/><br/>
/// Реализует интерфейс <see cref="IApplicationEventsBuilder"/>
/// </summary>
/// <param name="builder">Экземпляр построителя сервисов приложения.</param>
internal class ApplicationEventsBuilder(IApplicationServicesBuilder builder) :
    EventsBusBuilder(builder), IApplicationEventsBuilder
{
    /// <summary>Метод, регистрирующий обработчики событий в контейнере зависимостей.</summary>
    /// <param name="setupAction">Инициализация опций сервиса обработки событий.</param>
    public IApplicationEventsBuilder AddApplicationEvents(Action<EventBusOptions>? setupAction = null)
    {
        builder
            .AddEvents(setupAction)
            .AddEvent<CheckInHandler, FiasGuestCheckIn>(options =>
            {
                options.TaskName = "RESV";
                options.HandlerName = "CHECK_IN";
            })
            .AddEvent<CheckOutHandler, FiasGuestCheckOut>(options =>
            {
                options.TaskName = "RESV";
                options.HandlerName = "CHECK_OUT";
            })
            .AddEvent<ChangeHandler, FiasGuestChange>(options =>
            {
                options.TaskName = "RESV";
                options.HandlerName = "CHANGE";
            })
            .AddScheduleEvent<UpdateReservationScheduleHandler>(options =>
            {
                options.TaskName = "RESV";
                options.HandlerName = "Reservations Updates Monitoring";
            })
            .AddEvent<PostingRequestHandler, PostTransactionsRequest>(options =>
            {
                options.TaskName = "POST";
                options.HandlerName = "POSTING";
            })
            .AddEvent<FiasPostingSimpleHandler, FiasPostSimple>(options => options.HandlerName = "FIAS_SIMPLE")
            .AddEvent<FiasPostingRequestHandler, FiasPostRequest>(options => options.HandlerName = "FIAS_REQUEST")
            .AddEvent<ReservationHandler, ReservationInfo>(options => options.HandlerName = "OPERA")
            .AddEvent<RoomNumberHandler, RoomNumberRequest>(options => options.HandlerName = "OPERA")
            .AddEvent<CheckHandler, Check>(options => options.HandlerName = "MICROS")
            .AddEvent<PostingResponseHandler, PostTransactionsResponse>(options => options.HandlerName = "SANATORIUM")
            .AddEvent<UpdateReservationHandler, ReservationUpdatedMessage>(options => options.HandlerName = "SANATORIUM")
            .AddEvent<ReservedHandler, GuestReserved>(options => options.HandlerName = "RESERVED")
            .AddEvent<CancelledHandler, GuestCancelled>(options => options.HandlerName = "CANCELLED")
            .AddEvent<NoShowHandler, GuestNoShow>(options => options.HandlerName = "NO_SHOW")
            .AddEventLog<TelegramMessageHandler>()
            .AddUnloggedEvent<ChangedServiceStateHandler, ChangedServiceState>()
            .AddHostedService<MessageProxyWorker>();

        return this;
    }
}