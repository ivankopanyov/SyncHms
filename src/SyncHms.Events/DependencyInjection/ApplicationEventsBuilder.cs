namespace SyncHms.Events.DependencyInjection;

internal class ApplicationEventsBuilder(IApplicationServicesBuilder builder) :
    EventsBusBuilder(builder), IApplicationEventsBuilder
{
    private const string ResvTaskName = "RESV";
    
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
            .AddEvent<PostingRequestHandler, PostTransactionsRequest>(options =>
            {
                options.TaskName = "POST";
                options.HandlerName = "POSTING";
            })
            .AddEvent<PostingHandler, PostRequestInfo>(options => options.HandlerName = "FIAS")
            .AddEvent<ReservationHandler, ReservationInfo>(options => options.HandlerName = "OPERA")
            .AddEvent<CheckHandler, Check>(options => options.HandlerName = "MICROS")
            .AddEvent<PostingResponseHandler, PostTransactionsResponse>(options => options.HandlerName = "SANATORIUM")
            .AddEvent<UpdateReservationHandler, ReservationUpdatedMessage>(options => options.HandlerName = "SANATORIUM")
            .AddEventLog<TelegramMessageHandler>()
            .AddHostedService<MessageProxyService>();

        return this;
    }
}