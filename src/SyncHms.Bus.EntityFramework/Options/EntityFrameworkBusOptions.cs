namespace SyncHms.Bus;

public class EntityFrameworkBusOptions
{
    internal Action<DbContextOptionsBuilder>? OptionsBuilder { get; private set; }

    public InitDatabase InitDatabase { get; set; }
    
    public JsonSerializerSettings JsonSerializerSettings { get; private init; } = new JsonSerializerSettings();

    public void OnConfiguring(Action<DbContextOptionsBuilder> optionsBuilder) => OptionsBuilder = optionsBuilder;
}
