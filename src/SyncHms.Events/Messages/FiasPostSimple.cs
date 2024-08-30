namespace SyncHms.Events.Messages;

/// <summary>Класс, описывающий событие проведения денежного платежа.</summary>
internal class FiasPostSimple
{
    /// <summary>Идентификатор события из шины данных удаленного сервиса <c>Sanatorium</c></summary>
    public string CorrelationId { get; set; }
    
    /// <summary>Итоговая сумма платежа.</summary>
    public decimal Total { get; set; }
    
    /// <summary>Метод оплаты.</summary>
    public string PmsPaymentMethod { get; set; }
    
    /// <summary>Номер чека.</summary>
    public string CheckNumber { get; set; }
    
    /// <summary>Позиции в чеке платежа.</summary>
    public IEnumerable<IEnumerable<FiscalCheckItem>> Checks { get; set; }
}
