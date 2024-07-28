namespace SyncHms.Bus;

public class EntityFrameworkBusOptions : EntityFrameworkOptions
{
    public JsonSerializerSettings JsonSerializerSettings { get; private init; } = new();
}
