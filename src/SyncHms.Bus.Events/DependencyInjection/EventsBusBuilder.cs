namespace SyncHms.Bus.Events;

public class EventsBusBuilder(IServiceCollection services) : ApplicationBuilder(services), IEventsBusBuilder
{
    private readonly IServiceCollection _services = services;

    public IEventsBusBuilder AddEvent<THandler, TIn>(Action<HandlerOptions> action) where THandler : Handler<TIn>
    {
        ArgumentNullException.ThrowIfNull(action);
        var options = new HandlerOptions<THandler, TIn>()
        {
            UseEventLogging = true
        };
        action.Invoke(options);

        AddHandler(options);
        return this;
    }

    public IEventsBusBuilder AddEventLog<THandler>() where THandler : LogHandler
    {
        AddHandler(new HandlerOptions<THandler, EventLog>());
        return this;
    }

    private void AddHandler<THandler, TIn>(HandlerOptions<THandler, TIn> options) where THandler : HandlerBase<TIn>
    {
        _services
            .AddTransient<THandler>()
            .AddSingleton<IEventPublisher<TIn>, EventPublisher<TIn>>()
            .AddSingleton(options)
            .AddHostedService<HandlerStarter<THandler, TIn>>();
    }
}
