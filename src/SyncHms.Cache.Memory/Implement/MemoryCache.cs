namespace SyncHms.Cache;

/// <summary>Класс, описывающий сервис для работы с кешем в памяти приложения.</summary>
public class MemoryCache : IMemoryCache
{
    private readonly Dictionary<Type, Dictionary<string, (CancellationTokenSource, object)>> _objects = [];

    private readonly object _lock = new();

    /// <summary>Метод возвращает из кеша все объекты указанного типа.</summary>
    /// <typeparam name="T">Тип возвращаемых объектов.</typeparam>
    /// <returns>Все сохранненые объекты указанного типа.</returns>
    public Task<IEnumerable<T>> GetAllAsync<T>() where T : class
    {
        lock (_lock)
        {
            return Task.FromResult(
                !_objects.TryGetValue(typeof(T), out var result) ? [] : result.Values
                    .Select(value => value.Item2 is T obj ? obj : null)
                    .Where(value => value != null))!;
        }
    }

    /// <summary>Метод возвращает из кеша объект указанного типа с указанным ключем.</summary>
    /// <typeparam name="T">Тип возвращаемого объекта.</typeparam>
    /// <param name="key">Ключ возвращаемого объекта.</param>
    /// <returns>
    /// Сохраненный объект указанного типа с указанным ключем.<br/>
    /// Если объект не найден, возвращает <c>null</c>.
    /// </returns>
    public Task<T?> GetAsync<T>(string key) where T : class
    {
        ArgumentNullException.ThrowIfNull(key, nameof(key));

        lock (_lock)
        {
            return Task.FromResult(
                !_objects.TryGetValue(typeof(T), out var objects)
                || !objects.TryGetValue(key, out var obj) || obj.Item2 is not T result
                    ? null : result);
        }
    }

    /// <summary>Метод добавляет или заменяет объект указанного типа с указанным ключем.</summary>
    /// <typeparam name="T">Тип добавляемого объекта.</typeparam>
    /// <param name="key">Ключ добавляемого объекта.</param>
    /// <param name="value">Добавляемый объект.</param>
    /// <param name="expiry">Время хранения объекта в кэше.</param>
    public Task PushAsync<T>(string key, T value, TimeSpan? expiry = null) where T : class
    {
        ArgumentNullException.ThrowIfNull(key, nameof(key));
        ArgumentNullException.ThrowIfNull(value, nameof(value));

        lock (_lock)
        {
            if (!_objects.TryGetValue(typeof(T), out var objects))
            {
                objects = [];
                _objects.Add(typeof(T), objects);
            }

            var cancellationTokenSource = new CancellationTokenSource();

            if (!objects.TryAdd(key, (cancellationTokenSource, value)))
                objects[key] = (cancellationTokenSource, value);

            if (expiry is not { } timeSpan)
                return Task.CompletedTask;

            async void Start()
            {
                try
                {
                    await Task.Delay(timeSpan, cancellationTokenSource.Token);

                    lock (_lock)
                    {
                        if (!_objects.TryGetValue(typeof(T), out objects)) return;

                        objects?.Remove(key);
                        if (objects == null || objects.Count == 0)
                            _objects.Remove(typeof(T));
                    }
                }
                catch (TaskCanceledException)
                {
                    // ignored
                }
            }

            new Thread(Start).Start();
        }

        return Task.CompletedTask;
    }
    
    /// <summary>Метод возвращает и удаляет из кеша объект указанного типа с указанным ключем.</summary>
    /// <typeparam name="T">Тип возвращаемого объекта.</typeparam>
    /// <param name="key">Ключ возвращаемого объекта.</param>
    /// <returns>
    /// Сохраненный объект указанного типа с указанным ключем.<br/>
    /// Если объект не найден, возвращает <c>null</c>.
    /// </returns>
    public Task<T?> PopAsync<T>(string key) where T : class
    {
        ArgumentNullException.ThrowIfNull(key, nameof(key));

        lock (_lock)
        {
            if (!_objects.TryGetValue(typeof(T), out var objects))
                return Task.FromResult<T?>(null);
            
            if (objects.Count == 0)
            {
                _objects.Remove(typeof(T));
                return Task.FromResult<T?>(null);
            }

            if (!objects.TryGetValue(key, out var obj))
                return Task.FromResult<T?>(null);
                
            obj.Item1.Cancel();
            objects.Remove(key);

            if (objects.Count == 0)
                _objects.Remove(typeof(T));

            return Task.FromResult(obj.Item2 as T);
        }
    }
    
    /// <summary>Метод проверяет наличие в кеше объекта указанного типа с указанным ключем.</summary>
    /// <typeparam name="T">Тип проверяемого объекта.</typeparam>
    /// <param name="key">Ключ проверяемого объекта.</param>
    /// <returns>
    /// <c>true</c> - если объект найден в кеше.<br/>
    /// <c>false</c> - если объект не найден в кеше.
    /// </returns>
    public Task<bool> ExistsAsync<T>(string key) where T : class
    {
        ArgumentNullException.ThrowIfNull(key, nameof(key));

        lock (_lock)
        {
            return Task.FromResult(_objects.TryGetValue(typeof(T), out var objects) && objects.ContainsKey(key));
        }
    }
}