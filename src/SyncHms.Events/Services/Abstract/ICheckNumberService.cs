namespace SyncHms.Events.Services.Abstract;

/// <summary>
/// Интефейс, определяющий методы сервиса, предоставляющего номера чеков для сохранения в базу данных <c>MICROS</c>
/// </summary>
internal interface ICheckNumberService
{
    /// <summary>Метод, возвращающий следующий номер чека.</summary>
    /// <returns>Номер чека.</returns>
    Task<int> GetCheckNumberAsync();
}
