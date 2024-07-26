namespace SyncHms.Bus.Events.DependencyInjection;

internal class EventsBusBuilder(IServiceCollection services) : IEventsBusBuilder
{
    public IServiceCollection Services => services;
    
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
        Services
            .AddTransient<THandler>()
            .AddSingleton<IEventPublisher<TIn>, EventPublisher<TIn>>()
            .AddSingleton(options)
            .AddHostedService<HandlerStarter<THandler, TIn>>();
    }
}