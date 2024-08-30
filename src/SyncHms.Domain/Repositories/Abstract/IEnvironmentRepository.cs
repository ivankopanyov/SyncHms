namespace SyncHms.Domain;

/// <summary>Интерфейс, определяющий методы репозитория для работы с окружением.</summary>
/// <typeparam name="TEnvironment">
/// Тип окружения. Должен иметь открытый конструктор без параметров.
/// </typeparam>
public interface IEnvironmentRepository<TEnvironment> where TEnvironment : class, new()
{
    /// <summary>Метод, возвращающий экземпляр текущего окружения.</summary>
    /// <returns>Текущее окружение.</returns>
    Task<TEnvironment?> GetAsync();

    /// <summary>Метод, обновляющий текущее окружение.</summary>
    /// <param name="environment">Экземпляр обновленного окружения.</param>
    /// <returns>
    /// Флаг, указывающий, удалось ли применить новое окружение.
    /// Вернет значение <c>false</c>, если значения переменных текущего окружения идентичны обновленным.
    /// </returns>
    Task<bool> UpdateAsync(TEnvironment environment);
}
