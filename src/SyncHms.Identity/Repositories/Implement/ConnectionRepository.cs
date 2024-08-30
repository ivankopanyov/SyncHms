namespace SyncHms.Identity.Repositories.Implement;

/// <summary>Класс, описывающий репозиторий для работы с подключениями пользователей.</summary>
internal class ConnectionRepository(ICache cache) : IConnectionRepository
{
    /// <summary>Метод, сохраняющий подключение пользователя в репозитории.</summary>
    /// <param name="id">Идентификатор подключения.</param>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <param name="expiration">Время жизни подключения.</param>
    public async Task PushAsync(string id, long userId, TimeSpan expiration)
    {
        ArgumentNullException.ThrowIfNull(id, nameof(id));
        await cache.PushAsync(GetKey(id, userId), new Connection());
    }

    /// <summary>Метод, удаляющий подключение пользователя из репозитория.</summary>
    /// <param name="id">Идентификатор подключения.</param>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <returns>Флаг, указывающий, содержал ли репозиторий подключение с указанными данными.</returns>
    public async Task<bool> RemoveAsync(string id, long userId)
    {
        ArgumentNullException.ThrowIfNull(id, nameof(id));
        return await cache.PopAsync<Connection>(GetKey(id, userId)) != null;
    }

    /// <summary>Метод, формирующий ключ подключения для хранения в базе данных.</summary>
    /// <param name="id">Идентификатор подключения.</param>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <returns>Ключ подключения.</returns>
    private static string GetKey(string id, long userId) => $"{userId}_{id}";
}
