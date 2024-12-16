namespace SyncHms.Events.Messages;

/// <summary>
/// Класс, описывающий модель данных, хранящую информацию о бронированиях и зарезервированном ими инвенторе.<br/>
/// <seealso cref="ReservationInventory"/>
/// <seealso cref="Inventory"/>
/// </summary>
internal class ReservationInventories
{
    /// <summary>Флаг, указывающий, было ли изменение в статусе инвентаря.</summary>
    public bool HasInventories { get; set; }

    /// <summary>Флаг, указывающий. была ли ошибка при получении коллекции бронирований из базы данных.</summary>
    public bool HasError { get; set; }

    /// <summary>Коллекция бронирований с зарезервированным для них инвентарем.</summary>
    public HashSet<ReservationInventory> Queue { get; set; } = [];
}
