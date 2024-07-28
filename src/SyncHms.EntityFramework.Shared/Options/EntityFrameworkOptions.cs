namespace SyncHms.EntityFramework.Shared;

public class EntityFrameworkOptions
{
    public Action<DbContextOptionsBuilder>? OptionsBuilder { get; private set; }

    public InitDatabase InitDatabase { get; set; }
    
    public void OnConfiguring(Action<DbContextOptionsBuilder> optionsBuilder) => OptionsBuilder = optionsBuilder;

}