namespace SyncHms.Services.Models;

public class IikoConfiguration : RouteConfiguration
{
    public HashSet<RestaurantSection>? RestaurantSections { get; set; }
}
