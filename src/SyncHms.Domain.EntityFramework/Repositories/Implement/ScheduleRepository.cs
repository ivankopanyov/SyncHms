namespace SyncHms.Domain.EntityFramework.Repositories.Implement;

/// <summary>
/// Класс, описывающий репозиторий для работы с планируемыми событиями.<br/>
/// Реализует интерфейс <see cref="IScheduleRepository"/>
/// </summary>
/// <param name="domainContextFactory">
/// Экземпляр фабрики, создающей контекст подключения к базе данных домена.
/// </param>
internal class ScheduleRepository(IDomainContextFactory domainContextFactory) : IScheduleRepository
{
    private static readonly SemaphoreSlim Semaphore = new(1);

    /// <summary>Метод, возвращающий опции всех планируемых событий.</summary>
    /// <returns>Опции всех планируемых событий.</returns>
    public async Task<IEnumerable<Schedule>> GetAllAsync()
    {
        await using var context = domainContextFactory.Create();
        return await context.Schedules.AsNoTracking().ToListAsync();
    }

    /// <summary>Метод, возвращающий опции планируемого события.</summary>
    /// <param name="name">Имя планируемого события.</param>
    /// <returns>Экземпляр опций планируемого события.</returns>
    public async Task<Schedule?> GetAsync(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));

        await using var context = domainContextFactory.Create();
        return await context.Schedules.AsNoTracking().FirstOrDefaultAsync(s => s.Name.Equals(name));
    }

    /// <summary>Метод, обновляющий или добавляющий опции планируемого события.</summary>
    /// <param name="schedule">Экземпляр опций планируемого события.</param>
    public async Task UpdateAsync(Schedule schedule)
    {
        ArgumentNullException.ThrowIfNull(schedule?.Name, nameof(schedule.Name));

        await Semaphore.WaitAsync();

        try
        {
            await using var context = domainContextFactory.Create();

            if (await context.Schedules.FirstOrDefaultAsync(s => s.Name.Equals(schedule.Name)) is { } current)
            {
                current.IntervalSeconds = schedule.IntervalSeconds;
                current.First = schedule.First;
                current.Last = schedule.Last;
            }
            else
            {
                await context.Schedules.AddAsync(schedule);
            }

            await context.SaveChangesAsync();
        }
        finally
        {
            Semaphore.Release();
        }
    }

    /// <summary>Метод, удаляющий планируемое событие.</summary>
    /// <param name="scheduleName">Имя планируемого события.</param>
    public async Task RemoveAsync(string scheduleName)
    {
        ArgumentNullException.ThrowIfNull(scheduleName, nameof(scheduleName));

        await Semaphore.WaitAsync();

        try
        {
            await using var context = domainContextFactory.Create();

            if (await context.Schedules.FirstOrDefaultAsync(s => s.Name.Equals(scheduleName)) is { } current)
            {
                context.Schedules.Remove(current);
                await context.SaveChangesAsync();
            }
        }
        finally
        {
            Semaphore.Release();
        }
    }
}
