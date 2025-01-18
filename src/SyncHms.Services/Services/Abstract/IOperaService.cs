namespace SyncHms.Services;

/// <summary>
/// Интерфейс, определяющий методы сервиса интеграции с базой данных <c>OPERA</c><br/>
/// Расширяет методы интерфейса <see cref="IService{TOptions,TEnvironment}"/>
/// </summary>
public interface IOperaService : IService<OperaOptions, ApplicationEnvironment>
{
    /// <summary>Метод, запрашивающий номер комнаты бронирования в базе данных <c>OPERA</c></summary>
    /// <param name="reservationId">Идентификатор бронирования.</param>
    /// <returns>Номер комнаты бронирования.</returns>
    Task<string?> GetRoomNumberAsync(decimal reservationId);
    
    /// <summary>Метод, запрашивающий данные бронирования в базе данных <c>OPERA</c></summary>
    /// <param name="reservationId">Идентификатор бронирования.</param>
    /// <param name="status">Статус бронирования.</param>
    /// <returns>Данные бронирования.</returns>
    Task<ReservationUpdatedMessage?> GetReservationUpdatedMessageAsync(decimal reservationId, string status);

    /// <summary>Метод, возвращающий коллекцию бронирований, которые были обновлены ы указанный период.</summary>
    /// <param name="fromDate">Минимальная дата обновления бронирования.</param>
    /// <param name="toDate">Максимальная дата обноаления бронирования.</param>
    /// <returns>Коллекция обновленных бронирований.</returns>
    Task<List<UpdatedReservation>> GetUpdatedReservationsAsync(DateTime fromDate, DateTime toDate);

    /// <summary>
    /// Метод возвращает коллекцию бронирований с зарезервированнм инвентарем, которые зарегестрированы в одном номере
    /// с бронированием, идентификатор которого передан в параметрах метода.
    /// </summary>
    /// <param name="reservationId">Идентификатор бронирования.</param>
    /// <param name="room">Номер комнаты бронирования.</param>
    /// <returns>Коллекция бронирований с зарезервированным инвентарем.</returns>
    Task<HashSet<ReservationInventory>> GetReservationInventoriesAsync(decimal reservationId, string? room);

    /// <summary>Метод, возвращающий коллекцию бронирований, у которых был обновлен инвентарь в указанный период.</summary>
    /// <param name="fromDate">Минимальная дата начала и окончания резервирования инвенторя.</param>
    /// <param name="toDate">Максимальная дата начала и окончания резервирования инвенторя.</param>
    /// <param name="statuses">Статусы бронирований, которые будут включены в результат.</param>
    /// <returns>Коллекция идентификаторов бронирований, у которых был обновлен инвентарь.</returns>
    Task<HashSet<decimal>> GetUpdatedReservationInventoriesAsync(DateTime fromDate, DateTime toDate, params string[] statuses);

    /// <summary>Метод, записывающий детали чека в базу данных <c>OPERA</c></summary>
    /// <param name="details">Объект деталей чека.</param>
    Task AddCheckDetails(GuestCheckDetails details);
}
