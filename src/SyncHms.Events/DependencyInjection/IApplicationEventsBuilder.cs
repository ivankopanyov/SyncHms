namespace SyncHms.Events;

public interface IApplicationEventsBuilder : IEventsBusBuilder, ICacheBuilder
{
    IApplicationEventsBuilder AddApplicationEvents(Action<EventBusOptions>? setupAction = null);
}