namespace SyncHms.Services;

public class Inventory
{
    public string ArticleNumber { get; init; }
    
    public DateTime BeginDate { get; set; }

    public DateTime EndDate { get; set; }

    public override int GetHashCode() => ArticleNumber.GetHashCode();

    public override bool Equals(object? obj) => obj is Inventory other && other.ArticleNumber == ArticleNumber;
}