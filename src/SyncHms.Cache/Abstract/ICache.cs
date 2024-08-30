namespace SyncHms.Cache;

/// <summary>Интерфейс, определяющий методы для работы с кешем.</summary>
public interface ICache
{
    /// <summary>Метод возвращает из кеша все объекты указанного типа.</summary>
    /// <typeparam name="T">Тип возвращаемых объектов.</typeparam>
    /// <returns>Все сохранненые объекты указанного типа.</returns>
    Task<IEnumerable<T>> GetAllAsync<T>() where T : class;

    /// <summary>Метод возвращает из кеша объект указанного типа с указанным ключем.</summary>
    /// <typeparam name="T">Тип возвращаемого объекта.</typeparam>
    /// <param name="key">Ключ возвращаемого объекта.</param>
    /// <returns>
    /// Сохраненный объект указанного типа с указанным ключем.<br/>
    /// Если объект не найден, возвращает <c>null</c>.
    /// </returns>
    Task<T?> GetAsync<T>(string key) where T : class;

    /// <summary>Метод добавляет или заменяет объект указанного типа с указанным ключем.</summary>
    /// <typeparam name="T">Тип добавляемого объекта.</typeparam>
    /// <param name="key">Ключ добавляемого объекта.</param>
    /// <param name="value">Добавляемый объект.</param>
    /// <param name="expiry">Время хранения объекта в кэше.</param>
    Task PushAsync<T>(string key, T value, TimeSpan? expiry = null) where T : class;

    /// <summary>Метод возвращает и удаляет из кеша объект указанного типа с указанным ключем.</summary>
    /// <typeparam name="T">Тип возвращаемого объекта.</typeparam>
    /// <param name="key">Ключ возвращаемого объекта.</param>
    /// <returns>
    /// Сохраненный объект указанного типа с указанным ключем.<br/>
    /// Если объект не найден, возвращает <c>null</c>.
    /// </returns>
    Task<T?> PopAsync<T>(string key) where T : class;

    /// <summary>Метод проверяет наличие в кеше объекта указанного типа с указанным ключем.</summary>
    /// <typeparam name="T">Тип проверяемого объекта.</typeparam>
    /// <param name="key">Ключ проверяемого объекта.</param>
    /// <returns>
    /// <c>true</c> - если объект найден в кеше.<br/>
    /// <c>false</c> - если объект не найден в кеше.
    /// </returns>
    Task<bool> ExistsAsync<T>(string key) where T : class;
}
