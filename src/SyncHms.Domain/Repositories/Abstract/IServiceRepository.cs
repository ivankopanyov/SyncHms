namespace SyncHms.Domain;

/// <summary>Интерфейс, определяющий методы репозитория для работы с состояниями сервисов.</summary>
public interface IServiceRepository
{
    /// <summary>Метод, возвращающий информацию о всех сервисах.</summary>
    /// <returns>Информация о всех сервисах.</returns>
    Task<IEnumerable<Service>> GetAllAsync();

    /// <summary>Метод, возвращающий информацию о сервисе с указанным именем.</summary>
    /// <param name="serviceName">Имя сервиса.</param>
    /// <returns>Информация о сервисе.</returns>
    Task<Service?> GetAsync(string serviceName);

    /// <summary>Метод, обновляющий информацию о сервисе.</summary>
    /// <param name="service">Информация о сервисе.</param>
    /// <param name="updateOptions">
    /// Флаг, указывающий, нужно ли обновить опции сервиса в базе данных.<br/>
    /// Если <c>true</c> - опции будут обновлены.<br/>
    /// Если <c>false</c> - опции будут обновлены только в случае, если опции отсутствуют в репозитории.<br/>
    /// Если в передаваемом объекте занчение опций <c>null</c>, они не будет применены к объекту в репозитории.
    /// </param>
    /// <returns>Обновленная информация о сервисе.</returns>
    Task<Service> UpdateAsync(Service service, bool updateOptions);

    /// <summary>Метод, удаляющий информацию о сервисе по его имени.</summary>
    /// <param name="serviceName">Имя сервиса.</param>
    /// <returns>Удаленный объект.</returns>
    Task<Service?> RemoveAsync(string serviceName);
}