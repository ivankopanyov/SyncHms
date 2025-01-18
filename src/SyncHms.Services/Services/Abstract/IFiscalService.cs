namespace SyncHms.Services;

/// <summary>
/// Интерфейс, определяющий методы сервиса интеграции с удаленным сервисом <c>MICROS</c><br/>
/// Расширяет методы интерфейса <see cref="IService{TOptions,TEnvironment}"/>
/// </summary>
public interface IFiscalService : IService<MicrosOptions, ApplicationEnvironment>
{
    /// <summary>
    /// Метод, отправляющий сообщение типа <see cref="FiscalCheck"/> 
    /// для сохранения чека в базе данных <c>MICROS</c>
    /// </summary>
    /// <param name="fiscalCheck">Объект чека.</param>
    /// <returns>Идентификатор записанного чека..</returns>
    Task<int> SetCheckAsync(FiscalCheck fiscalCheck);
}
