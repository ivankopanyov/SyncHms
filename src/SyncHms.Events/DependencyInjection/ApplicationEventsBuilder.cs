namespace SyncHms.Events.DependencyInjection;

internal class ApplicationEventsBuilder(IApplicationServicesBuilder builder) :
    EventsBusBuilder(builder), IApplicationEventsBuilder
{
    private const string ResvTaskName = "RESV";

    private const string PostTaskName = "POST";

    private const string NServiceBusHandlerName = "N_SERVICE_BUS";
    
    public IApplicationEventsBuilder AddApplicationEvents(Action<EventBusOptions>? options = null)
    {
        builder
            .AddEvents(options)
            .AddEvent<CheckInHandler, FiasGuestCheckIn>(handlerOptions =>
            {
                handlerOptions.TaskName = ResvTaskName;
                handlerOptions.HandlerName = "CHECK_IN";
            })
            .AddEvent<CheckOutHandler, FiasGuestCheckOut>(handlerOptions =>
            {
                handlerOptions.TaskName = ResvTaskName;
                handlerOptions.HandlerName = "CHECK_OUT";
            })
            .AddEvent<ChangeHandler, FiasGuestChange>(handlerOptions =>
            {
                handlerOptions.TaskName = ResvTaskName;
                handlerOptions.HandlerName = "CHANGE";
            })
            .AddEvent<PostingHandler, PostRequestInfo>(handlerOptions =>
            {
                handlerOptions.HandlerName = "FIAS";
            })
            .AddEvent<ReservationHandler, ReservationInfo>(handlerOptions =>
            {
                handlerOptions.HandlerName = "OPERA_DB";
            })
            .AddEvent<CheckHandler, Check>(handlerOptions =>
            {
                handlerOptions.HandlerName = "CHECK_DB";
            })
            .AddEvent<PostingRequestHandler, PostTransactionsRequest>(handlerOptions =>
            {
                handlerOptions.TaskName = PostTaskName;
                handlerOptions.HandlerName = NServiceBusHandlerName;
            })
            .AddEvent<PostingResponseHandler, PostTransactionsResponse>(handlerOptions =>
            {
                handlerOptions.TaskName = PostTaskName;
                handlerOptions.HandlerName = NServiceBusHandlerName;
            })
            .AddEvent<UpdateReservationHandler, ReservationUpdatedMessage>(handlerOptions =>
            {
                handlerOptions.HandlerName = NServiceBusHandlerName;
            })
            .AddEventLog<TelegramMessageHandler>()
            .AddHostedService<MessageProxyService>();

        return this;
    }
}