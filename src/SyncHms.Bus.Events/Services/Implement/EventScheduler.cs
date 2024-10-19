namespace SyncHms.Bus.Events.Services.Implement;

/// <summary>
/// Класс, описывающий планировщик событий.<br/>
/// Реализует интерфейс <see cref="IEventScheduler"/>
/// </summary>
/// <param name="schedulePublisher">Объект, публикующий запланированное событие в шину данных.</param>
/// <param name="schedulerFactory">Объект фабрики, создающей объекты типа <see cref="IScheduler"/></param>
/// <param name="options">Экземпляр опций планировщика событий.</param>
internal class EventScheduler(IEventPublisher<ScheduleEvent> schedulePublisher,
    ISchedulerFactory schedulerFactory, EventShedulerOptions options) : IEventScheduler
{
    /// <summary>Экземпляр планировщика событий.</summary>
    private readonly IScheduler _sheduler = schedulerFactory.GetScheduler().Result;

    /// <summary>Коллекция опций планируемых событий с именем обработчика в качестве ключа.</summary>
    private readonly Dictionary<string, ScheduleOptions> _events = options.Events;

    /// <summary>
    /// Событие, вызываемое при изменении даты и времени последней удачной обработки планируемого события.
    /// </summary>
    public event UpdateLastDateHandleAsync UpdateLastDateEvent;

    public Task Execute(IJobExecutionContext context)
    {
        var key = context.JobDetail.Key.Name;
        var now = DateTime.Now;

        if (!_events.TryGetValue(key, out var options))
            return Task.CompletedTask;

        schedulePublisher.Publish(new()
        {
            Destination = key,
            EventScheduler = this,
            Previous = options.Last,
            Current = now
        });

        return Task.CompletedTask;
    }

    /// <summary>Метод обновления опций планируемого события.</summary>
    /// <param name="handlerName">Уникальное имя обработчика планируемого события.</param>
    /// <param name="interval">
    /// Интервал обработки планируемого события.<br/>
    /// Если передан <c>0</c> - событие будет остановлено.<br/>
    /// Если передан <c>null</c> - интервал не будет обновлен.<br/>
    /// </param>
    /// <param name="last">
    /// Дата и время последней удачной обработки события.<br/>
    /// Если передан <c>null</c> - дата не будет обновлена.<br/>
    /// </param>
    /// <param name="lastDateNotify">
    /// Флаг, указывающий, нужно ли вызывать событие обновления даты и времени
    /// <see cref="IEventScheduler.UpdateLastDateEvent"/> последней удачной обработки планируемого события.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Исключение возбуждается, если имя переданного обработчика не
    /// инициализировано или обработчик с указанным именем не зарегистрирован.
    /// </exception>
    public async Task UpdateSheduleEventAsync(string handlerName, TimeSpan? interval = null, DateTime? last = null, bool lastDateNotify = false)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(handlerName, nameof(handlerName));

        if (!_events.TryGetValue(handlerName, out var options))
            throw new ArgumentException($"Handler named {handlerName} was not found.");

        if (!await _sheduler.CheckExists(options.Key))
        {
            if (interval is TimeSpan timeSpan)
                options.Interval = timeSpan;

            if (last is DateTime dateTime)
                options.Last = dateTime;

            await RunSheduleAsync(options, lastDateNotify);
        }
        else
        {
            if ((interval == null || (interval is TimeSpan timeSpan && options.Interval == timeSpan))
                && (last == null || (last is DateTime dateTime && options.Last == dateTime)))
                return;

            await _sheduler.DeleteJob(options.Key);

            options.Interval = interval ?? options.Interval;
            options.Last = last ?? options.Last;

            await RunSheduleAsync(options, lastDateNotify);
        }
    }

    /// <summary>Метод, инициализирующий обработку планируемого события.</summary>
    /// <param name="options">Экземпляр опций планируемого события.</param>
    /// <param name="lastDateNotify">
    /// Флаг, указывающий, нужно ли вызывать событие обновления даты и времени
    /// <see cref="IEventScheduler.UpdateLastDateEvent"/> последней удачной обработки планируемого события.
    /// </param>
    private async Task RunSheduleAsync(ScheduleOptions options, bool lastDateNotify)
    {
        if (lastDateNotify)
            UpdateLastDateEvent?.Invoke(options.Key.Name, options.Last, options.Description);

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

        await _sheduler.ScheduleJob(job, triggerBuilder.Build());
    }
}
