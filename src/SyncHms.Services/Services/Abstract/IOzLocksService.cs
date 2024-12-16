namespace SyncHms.Services;

/// <summary>
/// Интерфейс, определяющий методы сервиса интеграции с базой данных <c>OzLocks</c><br/>
/// Расширяет методы интерфейса <see cref="IService{TOptions,TEnvironment}"/>
/// </summary>
public interface IOzLocksService : IService<OzLocksOptions, ApplicationEnvironment>
{
    /// <summary>Метод обновляет статусы резервирования инвенторя для указанного бронирования.</summary>
    /// <param name="reservationInventory">Информация о бронирования.</param>
    /// <returns>Коллекция идентификаторов инвенторя и статусов, на которые резервирование инвентаря было изменено.</returns>
    Task<HashSet<InventoryStatus>> UpdateInventoriesAsync(ReservationInventory reservationInventory);
}
