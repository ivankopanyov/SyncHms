namespace SyncHms.Server.Hubs;

/// <summary>
/// Класс, описывающий концентратор окружения <c>SignalR</c><br/>
/// Унаследован от класса <see cref="Hub"/>
/// </summary>
/// <param name="eventScheduler">Экземпляр планировщика событий.</param>
public class ScheduleHub(IEventScheduler eventScheduler) : Hub
{
    public async Task Schedules()
    {
        await Clients
            .Client(Context.ConnectionId)
            .SendAsync("Schedules", eventScheduler.Schedules.Select(s => new Schedule
            {
                Name = s.Key,
                Description = s.Value.Description,
                IntervalSeconds = (int)s.Value.Interval.TotalSeconds,
                Last = s.Value.Last
            }).ToList());
    }
}
