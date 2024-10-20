namespace SyncHms.Domain;

/// <summary>Интерфейс, определяющий методы репозитория для работы с планируемыми событиями.</summary>
public interface IScheduleRepository
{
    /// <summary>Метод, возвращающий опции всех планируемых событий.</summary>
    /// <returns>Опции всех планируемых событий.</returns>
    Task<IEnumerable<Schedule>> GetAllAsync();

    /// <summary>Метод, возвращающий опции планируемого события.</summary>
    /// <param name="name">Имя планируемого события.</param>
    /// <returns>Экземпляр опций планируемого события.</returns>
    Task<Schedule?> GetAsync(string name);

    /// <summary>Метод, обновляющий или добавляющий опции планируемого события.</summary>
    /// <param name="schedule">Экземпляр опций планируемого события.</param>
    Task UpdateAsync(Schedule schedule);

    /// <summary>Метод, удаляющий планируемое событие.</summary>
    /// <param name="scheduleName">Имя планируемого события.</param>
    Task RemoveAsync(string scheduleName);
}
