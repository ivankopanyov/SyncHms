namespace SyncHms.Cache;

/// <summary>
/// Интерфейс, определяющий методы для работы с кешем, хранящемся в памяти приложения.<br/>
/// Расширяет методы интерфейса <see cref="ICache"/>
/// </summary>
public interface IMemoryCache : ICache
{
    /// <summary>Метод добавляет или заменяет объект указанного типа с указанным ключем.</summary>
    /// <typeparam name="T">Тип добавляемого объекта.</typeparam>
    /// <param name="key">Ключ добавляемого объекта.</param>
    /// <param name="value">Добавляемый объект.</param>
    /// <param name="expiry">Время хранения объекта в кэше.</param>
    /// <param name="callback">Функция, срабатывающая во время удаления объекта по истечению срока жизни.</param>
    Task PushAsync<T>(string key, T value, TimeSpan expiry, Func<string, T, Task>? callback = null) where T : class;
}