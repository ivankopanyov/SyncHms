namespace SyncHms.Bus.Events.Services.Hosted;

internal class HandlerStarter<THandler, TIn> : BackgroundService where THandler : HandlerBase<TIn>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    private readonly ILogger? _logger;

    private readonly IBusProvider _provider;

    private readonly string _taskName;

    private readonly string _handlerName;

    private readonly bool _useEventLogging;

    private readonly bool _useLogging;

    public HandlerStarter(IServiceScopeFactory serviceScopeFactory, IEventPublisher<TIn> eventPublisher,
        IBusProvider provider, HandlerOptions<THandler, TIn> options, ILogger<THandler> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;

        eventPublisher.PublishEvent += async @in => await HandleAsync(new Event<TIn>
        {   
            TaskId = Guid.NewGuid().ToString(),
            TaskName = _taskName,
            Message = @in
        });

        _handlerName = options.HandlerName ?? GetType().Name;
        _taskName = options.TaskName;
        _logger = logger;
        _useEventLogging = options.UseEventLogging;
        _useLogging = _useEventLogging || _logger != null;

        _provider = provider;
        _provider.Subscribe<Event<TIn>, THandler>(HandleAsync);
    }

    private async Task HandleAsync(Event<TIn> @event, IMessageContext? messageContext = null)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var handler = scope.ServiceProvider.GetRequiredService<THandler>();

        if (@event.Message == null)
        {
            if (_useLogging)
            {
                await LogAsync(new EventLog
                {
                    TaskId = @event.TaskId,
                    TaskName = @event.TaskName,
                    HandlerName = _handlerName,
                    IsEnd = true,
                    Error = "Input object is null"
                });
            }

            return;
        }

        string? inputObjectJson = null;

        if (_useLogging)
        {
            try
            {
                inputObjectJson = JsonConvert.SerializeObject(@event.Message);
            }
            catch (Exception ex)
            {
                inputObjectJson = ex.Message;
            }
        }

        try
        {
            var context = new EventContext();
            await handler.ProcessHandleAsync(@event.Message, context);

            if (_useLogging)
            {
                await LogAsync(new EventLog
                {
                    TaskId = @event.TaskId,
                    TaskName = @event.TaskName,
                    HandlerName = _handlerName,
                    Message = handler.Message(@event.Message),
                    IsEnd = !context.Events.Any(),
                    InputObjectJson = inputObjectJson
                });
            }

            foreach (var @out in context.Events)
            {
                @out.TaskId = @event.TaskId;
                @out.TaskName = @event.TaskName;
                await @out.PublishAsync(_provider);
            }
        }
        catch (TaskCriticalException ex)
        {
            if (_useLogging)
            {
                await LogAsync(new EventLog
                {
                    TaskId = @event.TaskId,
                    TaskName = @event.TaskName,
                    HandlerName = _handlerName,
                    Message = handler.Message(@event.Message),
                    IsEnd = true,
                    Error = ex.Message,
                    StackTrace = ex.StackTrace,
                    InputObjectJson = inputObjectJson
                }, ex);
            }
        }
        catch (Exception ex)
        {
            if (@event.Error == null || @event.Error != ex.Message)
            {
                @event.Error = ex.Message;
                @event.StackTrace = ex.StackTrace;

                if (_useLogging)
                {
                    await LogAsync(new EventLog
                    {
                        TaskName = @event.TaskName,
                        HandlerName = _handlerName,
                        TaskId = @event.TaskId,
                        Message = handler.Message(@event.Message),
                        Error = @event.Error,
                        StackTrace = @event.StackTrace,
                        InputObjectJson = inputObjectJson
                    }, ex);
                }
                
                messageContext?.Requeue(true);
            }
            else
            {
                messageContext?.Requeue();
            }
        }
    }

    private async Task LogAsync(EventLog eventLog, Exception? ex = null)
    {
        if (!_useEventLogging)
            return;

        _logger?.LogEvent(eventLog, ex);

        await _provider.PublishAsync(new Event<EventLog>
        {
            Message = eventLog
        });
    }

    protected sealed override Task ExecuteAsync(CancellationToken stoppingToken) => Task.CompletedTask;
}
