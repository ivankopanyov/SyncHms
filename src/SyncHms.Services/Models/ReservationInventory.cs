namespace SyncHms.Services;

/// <summary>
/// Класс, описывающий модель данных, хранящую информацию о бронировании и зарезервированном для него инвенторе.<br/>
/// Реализует интерфейс <see cref="IComparable{ReservationInventory}"/>
/// </summary>
public class ReservationInventory : IComparable<ReservationInventory>
{
    /// <summary>Идентификатор бронирования.</summary>
    public decimal ReservationId { get; init; }
    
    /// <summary>Номер подтверждения бронирования.</summary>
    public string ConfirmationNo { get; set; }

    /// <summary>Фамилия.</summary>
    public string LastName { get; set; }

    /// <summary>Имя.</summary>
    public string FirstName { get; set; }

    /// <summary>Отчество.</summary>
    public string MiddleName { get; set; }

    /// <summary>Пол.</summary>
    public string Sex { get; set; }

    /// <summary>Номер комнаты.</summary>
    public string Room { get; set; }

    /// <summary>Статус бронирования.</summary>
    public string Status { get; set; }

    /// <summary>Статусы инвенторя бронирования.</summary>
    public HashSet<InventoryStatus>? InventoryQueue { get; set; }

    /// <summary>Инвентарь бронирования.</summary>
    public HashSet<Inventory> Inventories { get; set; } = [];

    /// <summary>Упорядочивает бронирования по <see cref="ReservationInventory.ReservationId"/></summary>
    public int CompareTo(ReservationInventory? other) => ReservationId.CompareTo(other?.ReservationId);

    public override int GetHashCode() => ReservationId.GetHashCode();

    public override bool Equals(object? obj) =>
        obj is ReservationInventory other && other.ReservationId == ReservationId;
}