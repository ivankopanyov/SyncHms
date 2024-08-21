namespace SyncHms.Events.Messages;

/// <summary>Класс, описывающий позицию в чеке.</summary>
internal class FiscalCheckItem
{
    /// <summary>Номер позиции.</summary>
    public long ObjectNumber { get; set; }
    
    /// <summary>Имя позиции.</summary>
    public string Name { get; set; }
    
    /// <summary>Колличество элементов в позиции.</summary>
    public double Quantity { get; set; }
    
    /// <summary>Итоговая сумма позиции.</summary>
    public decimal Total { get; set; }
    
    /// <summary>Налог.</summary>
    public byte Tax { get; set; }
    
    public int TaxPosting { get; set; }
}
