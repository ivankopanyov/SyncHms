namespace SyncHms.Identity.Repositories.Abstract;

/// <summary>Интерфейс, определяющий методы репозитория для работы с подключениями пользователей.</summary>
internal interface IConnectionRepository
{
    /// <summary>Метод, сохраняющий подключение пользователя в репозитории.</summary>
    /// <param name="id">Идентификатор подключения.</param>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <param name="expiration">Время жизни подключения.</param>
    Task PushAsync(string id, long userId, TimeSpan expiration);

    /// <summary>Метод, удаляющий подключение пользователя из репозитория.</summary>
    /// <param name="id">Идентификатор подключения.</param>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <returns>Флаг, указывающий, содержал ли репозиторий подключение с указанными данными.</returns>
    Task<bool> RemoveAsync(string id, long userId);
}
