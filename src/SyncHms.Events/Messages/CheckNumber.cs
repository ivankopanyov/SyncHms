namespace SyncHms.Events.Messages;

/// <summary>Класс, описывающий модель, хранящую номер последнего чека в базе данных.</summary>
internal class CheckNumber
{
    /// <summary>Номер последнего чека.</summary>
    public int Value { get; set; }
}
