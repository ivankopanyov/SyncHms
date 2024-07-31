namespace SyncHms.Domain;

public interface IServiceRepository
{
    Task<IEnumerable<Service>> GetAllAsync();

    Task<Service?> GetAsync(string serviceName);

    Task<Service> UpdateAsync(Service service, bool updateOptions);

    Task<Service?> RemoveAsync(string serviceName);
}