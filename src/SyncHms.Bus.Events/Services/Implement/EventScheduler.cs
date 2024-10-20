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
    public Task Execute(IJobExecutionContext context)
    {
        var key = context.JobDetail.Key.Name;
        var now = DateTime.Now;

        if (!_events.TryGetValue(key, out var options))
            return Task.CompletedTask;

        schedulePublisher.Publish(new ScheduleEvent
        {
            Destination = key,
            EventScheduler = this,
            Previous = options.Last,
            Current = now
        });

        return Task.CompletedTask;
    }

    /// <summary>Метод обновления опций планируемого события.</summary>
    /// <param name="scheduleName">Уникальное имя планируемого события.</param>
    /// <param name="interval">
    /// Интервал обработки планируемого события.<br/>
    /// Если передан <c>0</c> - событие будет остановлено.<br/>
    /// Если передан <c>null</c> - интервал не будет обновлен.<br/>
    /// </param>
    /// <param name="last">
    /// Дата и время последней удачной обработки события.<br/>
    /// Если передан <c>null</c> - дата не будет обновлена.<br/>
    /// </param>
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
    public async Task<ScheduleOptions> UpdateScheduleAsync(string scheduleName, TimeSpan? interval = null, DateTime? last = null, bool notify = false)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(scheduleName, nameof(scheduleName));

        if (!_events.TryGetValue(scheduleName, out var options))
            throw new KeyNotFoundException($"Handler named {scheduleName} was not found.");

        if (!await _scheduler.CheckExists(options.Key))
        {
            if (interval is { } timeSpan)
                options.Interval = timeSpan;

            if (last is { } dateTime)
                options.Last = dateTime;
        }
        else
        {
            if ((interval == null || (interval is { } timeSpan && options.Interval == timeSpan))
                && (last == null || (last is { } dateTime && options.Last == dateTime)))
                return options;

            await _scheduler.DeleteJob(options.Key);

            options.Interval = interval ?? options.Interval;
            options.Last = last ?? options.Last;
        }

        await RunScheduleAsync(options, notify);
        return options;
    }

    /// <summary>Метод, инициализирующий обработку планируемого события.</summary>
    /// <param name="options">Экземпляр опций планируемого события.</param>
    /// <param name="lastDateNotify">
    /// Флаг, указывающий, нужно ли вызывать событие обновления даты и времени
    /// <see cref="IEventScheduler.UpdateScheduleEvent"/> последней удачной обработки планируемого события.
    /// </param>
    private async Task RunScheduleAsync(ScheduleOptions options, bool lastDateNotify)
    {
        if (lastDateNotify)
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
        var next = options.Last + options.Interval;

        triggerBuilder = next <= now
            ? triggerBuilder.StartNow()
            : triggerBuilder.StartAt(next);

        await _scheduler.ScheduleJob(job, triggerBuilder.Build());
    }
}
