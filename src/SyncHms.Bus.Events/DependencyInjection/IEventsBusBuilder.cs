namespace SyncHms.Bus.Events;

public interface IEventsBusBuilder : IBusBuilder
{
    IEventsBusBuilder AddEvent<THandler, TIn>(Action<HandlerOptions> action) where THandler : Handler<TIn>;

    IEventsBusBuilder AddEventLog<THandler>() where THandler : LogHandler;
}
