namespace SyncHms.Domain;

public interface IEnvironmentRepository<TEnvironment> where TEnvironment : class, new()
{
    Task<TEnvironment?> GetAsync();

    Task<bool> UpdateAsync(TEnvironment environment);
}
