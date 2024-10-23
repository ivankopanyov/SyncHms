namespace SyncHms.Bus.Events.Workers;

/// <summary>Класс, описывающий фоновый сервис, инициализирующий обработчик указанного типа.</summary>
/// <typeparam name="THandler">Тип обработчика.</typeparam>
/// <typeparam name="TIn">Тип сообщения.</typeparam>
internal class HandlerWorker<THandler, TIn> : BackgroundService where THandler : HandlerBase<TIn>
{
    /// <summary>Экземпляр фабрики, инициализирующей объекты, реализующие интерфейс <see cref="IServiceScope"/></summary>
    private readonly IServiceScopeFactory _serviceScopeFactory;

    /// <summary>Экземпляр логгера.</summary>
    private readonly ILogger? _logger;

    /// <summary>Экземпляр провайдера шины данных.</summary>
    private readonly IBusProvider _provider;

    /// <summary>Имя задачи.</summary>
    private readonly string _taskName;

    /// <summary>Имя обработчика.</summary>
    private readonly string _handlerName;
    
    /// <summary>Поле определяет, нужно ли отправлять логи в обработчик логов.</summary>
    private readonly bool _useEventLogging;

    /// <summary>Поле определяет, будет ли логироваться процесс обработки события.</summary>
    private readonly bool _useLogging;

    /// <summary>Инициализация сервиса.</summary>
    /// <param name="serviceScopeFactory">
    /// Экземпляр фабрики, инициализирующей объекты, реализующие интерфейс <see cref="IServiceScope"/>
    /// </param>
    /// <param name="eventPublisher">Экземпляр сервиса, публикующего события в шине данных.</param>
    /// <param name="provider">Экземпляр провайдера шины данных.</param>
    /// <param name="options">Экземпляр опций обработчика.</param>
    /// <param name="logger">Экземпляр логгера.</param>
    public HandlerWorker(IServiceScopeFactory serviceScopeFactory, IEventPublisher<TIn> eventPublisher,
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

    /// <summary>Метод, обрабатывающий событие.</summary>
    /// <param name="event">Обрабатываемое событие.</param>
    /// <param name="messageContext">Контекст обработки сообщения.</param>
    private async Task HandleAsync(Event<TIn> @event, IMessageContext? messageContext = null)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var handler = scope.ServiceProvider.GetRequiredService<THandler>();


        var handlerName = _handlerName;
        string? message = null;

        try
        {
            handlerName = handler.HandlerName(@event.Message) ?? handlerName;
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, ex.Message);
        }

        try
        {
            message = handler.Message(@event.Message);
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, ex.Message);
        }

        if (@event.Message == null)
        {
            if (_useLogging)
            {
                await LogAsync(new EventLog
                {
                    TaskId = @event.TaskId,
                    TaskName = @event.TaskName,
                    HandlerName = handlerName,
                    Message = message,
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
        
        var context = new EventContext(handlerName);

        try
        {
            await handler.ProcessHandleAsync(@event.Message, context);

            if (_useLogging)
            {
                await LogAsync(new EventLog
                {
                    TaskId = @event.TaskId,
                    TaskName = @event.TaskName,
                    HandlerName = handlerName,
                    Message = message,
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
                    HandlerName = handlerName,
                    Message = message,
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
                        HandlerName = handlerName,
                        TaskId = @event.TaskId,
                        Message = message,
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

    /// <summary>Метод, публикующий логи обработки события в шину данных.</summary>
    /// <param name="eventLog">Экземпляр лога обработки события.</param>
    /// <param name="ex">Исключение, возбужденное во время обработки события.</param>
    private async Task LogAsync(EventLog eventLog, Exception? ex = null)
    {
        if (!_useEventLogging)
            return;

        _logger?.LogEvent(eventLog, ex);

        try
        {
            await _provider.PublishAsync(new Event<EventLog>
            {
                Message = eventLog
            });
        }
        catch (Exception e)
        {
            _logger?.LogError(e, e.Message);
        }
    }

    protected sealed override Task ExecuteAsync(CancellationToken stoppingToken) => Task.CompletedTask;
}
