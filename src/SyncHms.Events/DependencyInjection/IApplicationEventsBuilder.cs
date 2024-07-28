namespace SyncHms.Events;

public interface IApplicationEventsBuilder : IBusBuilder, ICacheBuilder
{
    IApplicationEventsBuilder AddApplicationEvents(Action<EventBusOptions>? options = null);
}