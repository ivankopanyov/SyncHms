namespace SyncHms.Services;

/// <summary>
/// Класс, описывающий модель данных, хранящуюю информацию о статусе инвенторя у бронирования.<br/>
/// Реализует интерфейс <see cref="IComparable{InventoryStatus}"/>
/// </summary>
public class InventoryStatus : IComparable<InventoryStatus>
{
    /// <summary>Статус инвенторя.</summary>
    public OzLocksStatus Status { get; set; }

    /// <summary>Код инвенторя.</summary>
    public string InventoryCode { get; set; }

    /// <summary>Инициализация объекта <see cref="InventoryStatus"/></summary>
    public InventoryStatus()
    {
    }

    /// <summary>Инициализация объекта <see cref="InventoryStatus"/></summary>
    /// <param name="status">Статус инвенторя.</param>
    /// <param name="inventoryCode">Код инвенторя.</param>
    public InventoryStatus(OzLocksStatus status, string inventoryCode)
    {
        Status = status;
        InventoryCode = inventoryCode;
    }

    /// <summary>Упорядочивает инвентарь по <see cref="InventoryStatus.Status"/></summary>
    public int CompareTo(InventoryStatus? other)
    {
        var compare = Status.CompareTo(other?.Status);
        if (compare == 0)
            compare = InventoryCode.CompareTo(other?.InventoryCode);

        return compare;
    }

    public override int GetHashCode() => HashCode.Combine(Status, InventoryCode);

    public override bool Equals(object? obj) => obj is InventoryStatus other
        && Status == other.Status && InventoryCode == other.InventoryCode;
}
