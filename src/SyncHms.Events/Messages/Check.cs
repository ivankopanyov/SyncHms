namespace SyncHms.Events.Messages;

/// <summary>Класс, описывающий событие сохранения чека в базе данных <c>MICROS</c></summary>
internal class Check
{
    /// <summary>Идентификатор события из шины данных удаленного сервиса <c>Sanatorium</c></summary>
    public string CorrelationId { get; set; }
    
    /// <summary>Дата и время совершения платежа.</summary>
    public DateTime DateTime { get; set; }
    
    /// <summary>Итоговая сумма платежа.</summary>
    public string Total { get; set; }
    
    /// <summary>Номер чека.</summary>
    public int CheckNumber { get; set; }
    
    /// <summary>Позиции в чеке.</summary>
    public IEnumerable<FiscalCheckItem> Details { get; set; }
}
