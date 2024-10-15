namespace SyncHms.Services.Models;

/// <summary>Класс, описывающий объект, ожидающий подключения к удаленному сервису.</summary>
internal class WaitConnection()
{
    /// <summary>
    /// Объект, у которого будет вызван метод <see cref="CancellationTokenSource.Cancel"/>
    /// после удачного подключения к сервису.
    /// </summary>
    private readonly CancellationTokenSource _cancellationTokenSource = new();

    /// <summary>Возвращает токен, указывающий на удачное подключение к сервису.</summary>
    public CancellationToken CancellationToken => _cancellationTokenSource.Token;

    /// <summary>Метод, который будет вызван при удачном подключении к удаленному сервису.</summary>
    public void Cancel() => _cancellationTokenSource.Cancel();
}
