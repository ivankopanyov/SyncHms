namespace SyncHms.Bus.Events.Services.Hosted;

internal class HandlerStarter<THandler, TIn> : BackgroundService where THandler : HandlerBase<TIn>
{
    private static readonly string QueueName = typeof(THandler).FullName ?? typeof(THandler).Name;

    private readonly IServiceScopeFactory _serviceScopeFactory;

    private readonly ILogger? _logger;

    private readonly IBusProvider _provider;

    private readonly string _taskName;

    private readonly string _handlerName;

    private readonly bool _useEventLogging;

    private readonly bool _useLogging;

    private readonly object _lock = new();

    private DateTime _dateTime = DateTime.Now.Trim(TimeSpan.TicksPerSecond);

    private int _counter;

    public HandlerStarter(IServiceScopeFactory serviceScopeFactory, IEventPublisher<TIn> eventPublisher,
        IBusProvider provider, HandlerOptions<THandler, TIn> options, ILogger<THandler> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;

        eventPublisher.PublishEvent += async @in => await HandleAsync(new Event<TIn>
        {   
            Id = GenerateId(),
            TaskId = GenerateId(),
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
                    Id = @event.Id,
                    TaskId = @event.TaskId,
                    TaskName = @event.TaskName,
                    HandlerName = _handlerName,
                    IsError = true,
                    IsEnd = true,
                    Data = new EventData
                    {
                        LogId = @event.Id,
                        TaskId = @event.TaskId,
                        Error = "Input object is null"
                    }
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
                    Id = @event.Id,
                    TaskId = @event.TaskId,
                    TaskName = @event.TaskName,
                    HandlerName = _handlerName,
                    Message = handler.Message(@event.Message),
                    IsEnd = !context.Events.Any(),
                    Data = new EventData
                    {
                        LogId = @event.Id,
                        TaskId = @event.TaskId,
                        InputObjectJson = inputObjectJson,
                    }
                });
            }

            foreach (var @out in context.Events)
            {
                @out.Id = GenerateId();
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
                    Id = @event.Id,
                    TaskId = @event.TaskId,
                    TaskName = @event.TaskName,
                    HandlerName = _handlerName,
                    Message = handler.Message(@event.Message),
                    IsError = true,
                    IsEnd = true,
                    Data = new EventData
                    {
                        LogId = @event.Id,
                        TaskId = @event.TaskId,
                        Error = ex.Message,
                        StackTrace = ex.StackTrace,
                        InputObjectJson = inputObjectJson,
                    }
                }, ex);
            }
        }
        catch (Exception ex)
        {
            if (@event.Error == null || @event.Error != ex.Message)
            {
                @event.Id = GenerateId();
                @event.Error = ex.Message;
                @event.StackTrace = ex.StackTrace;

                if (_useLogging)
                {
                    await LogAsync(new EventLog
                    {
                        Id = @event.Id,
                        TaskName = @event.TaskName,
                        HandlerName = _handlerName,
                        TaskId = @event.TaskId,
                        Message = handler.Message(@event.Message),
                        IsError = true,
                        Data = new EventData
                        {
                            LogId = @event.Id,
                            TaskId = @event.TaskId,
                            Error = @event.Error,
                            StackTrace = @event.StackTrace,
                            InputObjectJson = inputObjectJson,
                        }
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
        _logger?.LogEvent(eventLog, ex);

        if (!_useEventLogging)
            return;
        
        await _provider.PublishAsync(new Event<EventLog>
        {
            Id = GenerateId(),
            Message = eventLog
        });
    }

    protected sealed override Task ExecuteAsync(CancellationToken stoppingToken) => Task.CompletedTask;

    private string GenerateId()
    {
        lock(_lock)
        {
            var now = DateTime.Now.Trim(TimeSpan.TicksPerSecond);
            if (_dateTime != now)
            {
                _dateTime = now;
                _counter = 0;
            }

            return $"{_dateTime:yyMMddHHmmss}{(++_counter).ToString().PadLeft(2, '0')}";
        }
    }
}
