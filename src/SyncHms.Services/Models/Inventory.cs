namespace SyncHms.Services;

/// <summary>Класс, описывающий модель данных, хранящую информацию об инвентаре.</summary>
public class Inventory
{
    /// <summary>Артикль инвенторя.</summary>
    public string ArticleNumber { get; init; }
    
    /// <summary>Дата и время присвоения инвенторя бронированию.</summary>
    public DateTime BeginDate { get; set; }

    /// <summary>Дата и время окончания резервирования инвенторя бронированием.</summary>
    public DateTime EndDate { get; set; }

    public override int GetHashCode() => ArticleNumber.GetHashCode();

    public override bool Equals(object? obj) => obj is Inventory other && other.ArticleNumber == ArticleNumber;
}