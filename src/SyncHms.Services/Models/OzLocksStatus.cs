namespace SyncHms.Services;

/// <summary>Статус инвенторя бронирования в системе <c>OzLocks</c></summary>
public enum OzLocksStatus
{
    /// <summary>Резеревирование инвенторя снято.</summary>
    CheckOut,

    /// <summary>Изменены сроки резервирования инвенторя.</summary>
    Change,

    /// <summary>Инвентарь присвоен бронированию.</summary>
    CheckIn,

    /// <summary>Изменены данные бронирования.</summary>
    Update
}
