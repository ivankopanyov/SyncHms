namespace SyncHms.Domain;

public class Service
{
    public string Name { get; set; }

    public string? JsonOptions { get; set; }
    
    public State? State { get; set; }

    public Service Clone()
    {
        var service = (Service)MemberwiseClone();
        if (State != null)
            service.State = State.Clone();

        return service;
    }
}
