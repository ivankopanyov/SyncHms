namespace SyncHms.Services.Models;

public class DishCategory : RouteConfiguration
{
    public string? Name { get; set; }

    public override int GetHashCode() => (Name ?? string.Empty).GetHashCode();

    public override bool Equals(object? obj) => 
        obj is DishCategory other
        && (Name ?? string.Empty) == (other.Name ?? string.Empty);
}