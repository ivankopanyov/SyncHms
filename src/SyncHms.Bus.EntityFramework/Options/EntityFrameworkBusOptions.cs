namespace SyncHms.Bus.EntityFramework;

public class EntityFrameworkBusOptions
{
    internal Action<DbContextOptionsBuilder>? OptionsBuilder { get; private set; }

    public JsonSerializerSettings JsonSerializerSettings { get; private init; } = new JsonSerializerSettings();

    public void OnConfiguring(Action<DbContextOptionsBuilder> optionsBuilder) => OptionsBuilder = optionsBuilder;
}
