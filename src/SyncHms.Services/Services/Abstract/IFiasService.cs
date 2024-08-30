namespace SyncHms.Services;

/// <summary>
/// Метод, обрабатывающий сообщение типа <see cref="FiasGuestCheckIn"/>
/// </summary>
public delegate void FiasGuestCheckInHandle(FiasGuestCheckIn message);

/// <summary>
/// Метод, обрабатывающий сообщение типа <see cref="FiasGuestCheckOut"/>
/// </summary>
public delegate void FiasGuestCheckOutHandle(FiasGuestCheckOut message);

/// <summary>
/// Метод, обрабатывающий сообщение типа <see cref="FiasGuestChange"/>
/// </summary>
public delegate void FiasGuestChangeHandle(FiasGuestChange message);

/// <summary>
/// Интерфейс, определяющий методы сервиса интеграции с удаленным сервисом <c>FIAS</c><br/>
/// Расширяет методы интерфейса <see cref="IService{TOptions,TEnvironment}"/>
/// </summary>
public interface IFiasService : IService<FiasServiceOptions, ApplicationEnvironment>
{
    /// <summary>
    /// Событие, вызываемое при получении сервисом сообщения типа <see cref="FiasGuestCheckIn"/>,
    /// указывающее на изменение статуса бронирования на <c>CHECK_IN</c>
    /// </summary>
    event FiasGuestCheckInHandle? FiasGuestCheckInEvent;
    
    /// <summary>
    /// Событие, вызываемое при получении сервисом сообщения типа <see cref="FiasGuestCheckOut"/>,
    /// указывающее на изменение статуса бронирования на <c>CHECK_OUT</c>
    /// </summary>
    event FiasGuestCheckOutHandle? FiasGuestCheckOutEvent;
    
    /// <summary>
    /// Событие, вызываемое при получении сервисом сообщения типа <see cref="FiasGuestChange"/>,
    /// указывающее на изменение данных бронирования.
    /// </summary>
    event FiasGuestChangeHandle? FiasGuestChangeEvent;

    /// <summary>
    /// Метод, отправляющий сообщение типа <see cref="FiasPostingSimple"/> 
    /// для синхронизации платежа с системой <c>OPERA</c>
    /// </summary>
    /// <param name="message">Тело сообщения.</param>
    /// <param name="timeoutSeconds">Время ожидания ответа в секундах.</param>
    /// <returns>Ответ от удаленного сервиса.</returns>
    Task<FiasPostingAnswer> SendPostingAsync(FiasPostingSimple message, int timeoutSeconds = 60);

    /// <summary>
    /// Метод, отправляющий сообщение типа <see cref="FiasPostingRequest"/> 
    /// для начисления платежа на комнату в системе <c>OPERA</c>
    /// </summary>
    /// <param name="message">Тело сообщения.</param>
    /// <param name="timeoutSeconds">Время ожидания ответа в секундах.</param>
    /// <returns>Ответ от удаленного сервиса.</returns>
    Task<FiasPostingAnswer> SendPostingAsync(FiasPostingRequest message, int timeoutSeconds = 60);
}
