namespace SyncHms.Services.Models;

public class RestaurantSection : RouteConfiguration
{
    [Required]
    public string Name { get; set; } = string.Empty;
    
    public HashSet<DishCategory>? DishCategories { get; set; }

    public override int GetHashCode() => Name.GetHashCode();

    public override bool Equals(object? obj) => obj is RestaurantSection other && Name == other.Name;
}
