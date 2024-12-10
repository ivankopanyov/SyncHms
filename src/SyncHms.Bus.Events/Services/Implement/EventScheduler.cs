namespace SyncHms.Bus.Events.Services.Implement;

/// <summary>
/// Класс, описывающий планировщик событий.<br/>
/// Реализует интерфейс <see cref="IEventScheduler"/>
/// </summary>
/// <param name="schedulePublisher">Объект, публикующий запланированное событие в шину данных.</param>
/// <param name="schedulerFactory">Объект фабрики, создающей объекты типа <see cref="IScheduler"/></param>
/// <param name="options">Экземпляр опций планировщика событий.</param>
internal class EventScheduler(IEventPublisher<ScheduleEvent> schedulePublisher,
    ISchedulerFactory schedulerFactory, EventSchedulerOptions options) : IEventScheduler
{
    /// <summary>Экземпляр планировщика событий.</summary>
    private readonly IScheduler _scheduler = schedulerFactory.GetScheduler().Result;

    /// <summary>Коллекция опций планируемых событий с именем обработчика в качестве ключа.</summary>
    private readonly Dictionary<string, ScheduleOptions> _events = options.Events;

    /// <summary>Коллекция опций планируемых событий с именем обработчика в качестве ключа.</summary>
    public IReadOnlyDictionary<string, ScheduleOptions> Schedules => _events;

    /// <summary>Событие, вызываемое при обновлении опций планируемого события.</summary>
    public event UpdateScheduleHandleAsync UpdateScheduleEvent;

    /// <summary>Метод, инициализирующий отправку сообщения об обработке планируемого события в шину данных.</summary>
    /// <param name="context">Контекст обработки события.</param>
    public async Task Execute(IJobExecutionContext context)
    {
        var key = context.JobDetail.Key;
        await _scheduler.DeleteJob(key);

        var now = DateTime.Now;

        if (!_events.TryGetValue(key.Name, out var options))
            return;

        schedulePublisher.Publish(new ScheduleEvent
        {
            Destination = key.Name,
            EventScheduler = this,
            Previous = options.Last,
            Current = now
        });
    }

    /// <summary>Метод обновления опций планируемого события.</summary>
    /// <param name="scheduleName">Уникальное имя планируемого события.</param>
    /// <param name="interval">
    /// Интервал обработки планируемого события.<br/>
    /// Если передан <c>0</c> - событие будет остановлено.
    /// </param>
    /// <param name="last">Дата и время последней удачной обработки события.</param>
    /// <param name="notify">
    /// Флаг, указывающий, нужно ли вызывать событие <see cref="IEventScheduler.UpdateScheduleEvent"/>
    /// </param>
    /// <returns>Экземпляр обновленных опций планируемого события.</returns>
    /// <exception cref="ArgumentException">
    /// Исключение возбуждается, если имя переданного обработчика не инициализировано.
    /// </exception>
    /// <exception cref="KeyNotFoundException">
    /// Исключение возбуждается, если обработчик с указанным именем не зарегистрирован.
    /// </exception>
    public async Task<ScheduleOptions> UpdateScheduleAsync(string scheduleName, TimeSpan interval, DateTime last, bool notify = false)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(scheduleName, nameof(scheduleName));

        if (!_events.TryGetValue(scheduleName, out var options))
            throw new KeyNotFoundException($"Handler named {scheduleName} was not found.");

        if (options.Interval == interval && options.Last == last)
        {
            if (!await _scheduler.CheckExists(options.Key))
                await RunScheduleAsync(options, notify);
        }
        else
        {
            options.Interval = interval;
            options.First = last;
            options.Last = last;
            await _scheduler.DeleteJob(options.Key);
            await RunScheduleAsync(options, notify);
        }

        return options;
    }

    /// <summary>Метод обновления опций планируемого события.</summary>
    /// <param name="scheduleName">Уникальное имя планируемого события.</param>
    /// <param name="previous">Дата и время текущей удачной обработки события.</param>
    /// <param name="current">Дата и время предыдущей удачной обработки события.</param>
    /// <param name="ex">Исключение, возбужденное в процессе обработки события.</param>
    public async Task UpdateScheduleAsync(string scheduleName, DateTime previous, DateTime current, Exception? ex = null)
    {
        if (!_events.TryGetValue(scheduleName, out var options))
            return;

        var currentMessage = options.Message;
        options.Message = ex?.Message;
        options.StackTrace = ex?.StackTrace;

        if (await _scheduler.CheckExists(options.Key) || options.Last != previous)
        {
            if (currentMessage != options.Message)
                UpdateScheduleEvent?.Invoke(options.Key.Name, options);

            return;
        }

        var last = options.Last;
        options.Last = current;
        await RunScheduleAsync(options, currentMessage != options.Message || last != current);
    }

    /// <summary>Метод, инициализирующий обработку планируемого события.</summary>
    /// <param name="options">Экземпляр опций планируемого события.</param>
    /// <param name="notify">
    /// Флаг, указывающий, нужно ли вызывать событие <see cref="IEventScheduler.UpdateScheduleEvent"/>
    /// </param>
    private async Task RunScheduleAsync(ScheduleOptions options, bool notify)
    {
        if (notify)
            UpdateScheduleEvent?.Invoke(options.Key.Name, options);

        if (options.Interval == TimeSpan.Zero)
            return;

        var job = JobBuilder
            .Create<IEventScheduler>()
            .WithIdentity(options.Key)
            .Build();

        var triggerBuilder = TriggerBuilder
            .Create()
            .WithIdentity(options.Key.Name);

        var now = DateTime.Now;
        var next = options.Last;

        if (options.First == options.Last)
        {
            next += options.Interval;
        }
        else
        {
            var runTime = (options.Last - options.First).TotalSeconds;
            var interval = options.Interval.TotalSeconds;
            next += TimeSpan.FromSeconds(interval - (runTime - (int)runTime / interval * interval));
        }

        triggerBuilder = next <= now
            ? triggerBuilder.StartNow()
            : triggerBuilder.StartAt(next);

        await _scheduler.ScheduleJob(job, triggerBuilder.Build());
    }
}
