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
            .AddScheduleEvent<UpdateInventoryScheduleHandler>(options =>
            {
                options.TaskName = "LOCK";
                options.HandlerName = "Inventory Updates Monitoring";
            })
            .AddEvent<ReservationInventoryRequestHandler, ReservationInventoryRequest>(options =>
            {
                options.TaskName = "LOCK";
                options.HandlerName = "OPERA";
            })
            .AddEvent<FiasPostingSimpleHandler, FiasPostSimple>(options => options.HandlerName = "FIAS_SIMPLE")
            .AddEvent<FiasPostingRequestHandler, FiasPostRequest>(options => options.HandlerName = "FIAS_REQUEST")
            .AddEvent<ReservationHandler, ReservationInfo>(options => options.HandlerName = "OPERA")
            .AddEvent<RoomNumberHandler, RoomNumberRequest>(options => options.HandlerName = "ROOM")
            .AddEvent<CheckDetailsHandler, CheckDetails>(options => options.HandlerName = "CHECK_DETAILS")
            .AddEvent<MicrosPostingRequestHandler, MicrosPostingRequest>(options => options.HandlerName = "MICROS")
            .AddEvent<PostingResponseHandler, PostTransactionsResponse>(options => options.HandlerName = "SANATORIUM")
            .AddEvent<UpdateReservationHandler, ReservationUpdatedMessage>(options => options.HandlerName = "SANATORIUM")
            .AddEvent<UpdatedReservationHandler, UpdatedReservation>(options => options.HandlerName = "UPDATED")
            .AddEvent<ReservationInventoriesHandler, ReservationInventories>(options => options.HandlerName = "OZLOCKS")
            .AddEventLog<TelegramMessageHandler>()
            .AddUnloggedEvent<ChangedServiceStateHandler, ChangedServiceState>()
            .AddHostedService<MessageProxyWorker>();

        return this;
    }
}