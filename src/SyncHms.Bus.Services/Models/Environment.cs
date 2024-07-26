namespace SyncHms.Bus.Services;

public class Environment<TEnvironment>
{
    public string ServiceName { get; set; }
    
    public TEnvironment Value { get; set; }
}