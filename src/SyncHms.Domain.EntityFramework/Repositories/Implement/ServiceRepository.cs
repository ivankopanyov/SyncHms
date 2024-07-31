namespace SyncHms.Domain.EntityFramework.Repositories.Implement;

internal class ServiceRepository(IDomainContextFactory domainContextFactory, ICache cache) : IServiceRepository
{
    private static readonly SemaphoreSlim _semaphore = new(1);
    
    public async Task<IEnumerable<Service>> GetAllAsync()
    {
        await using var context = domainContextFactory.Create();
        var services = await context.Services.AsNoTracking().ToListAsync();
        foreach (var service in services)
        {
            if (await cache.GetAsync<State>(service.Name) is { } state)
                service.State = state;
        }
        
        return services;
    }

    public async Task<Service?> GetAsync(string serviceName)
    {
        ArgumentNullException.ThrowIfNull(serviceName, nameof(serviceName));
        
        await using var context = domainContextFactory.Create();
        if (await context.Services.AsNoTracking()
                .FirstOrDefaultAsync(s => s.Name.Equals(serviceName)) is not { } service)
            return null;
        
        if (await cache.GetAsync<State>(serviceName) is { } state)
            service.State = state;
        
        return service;
    }

    public async Task<Service> UpdateAsync(Service service, bool updateOptions)
    {
        ArgumentNullException.ThrowIfNull(service?.Name, nameof(service.Name));

        await _semaphore.WaitAsync();

        try
        {
            if (service.State != null)
                await cache.PushAsync(service.Name, service.State);
            else
                service.State = await cache.GetAsync<State>(service.Name);

            await using var context = domainContextFactory.Create();
            if (await context.Services.FirstOrDefaultAsync(s => s.Name.Equals(service.Name)) is not { } s)
            {
                await context.Services.AddAsync(service);
                await context.SaveChangesAsync();
            }
            else if (updateOptions)
            {
                s.JsonOptions = service.JsonOptions;
                await context.SaveChangesAsync();
            }
            else
            {
                service.JsonOptions = s.JsonOptions;
            }

            return service;
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task<Service?> RemoveAsync(string serviceName)
    {
        await _semaphore.WaitAsync();
        
        try
        {
            var state = await cache.PopAsync<State>(serviceName);
            await using var context = domainContextFactory.Create();
            if (await context.Services
                    .FirstOrDefaultAsync(s => s.Name.Equals(serviceName)) is not { } service)
            {
                return null;
            }

            context.Services.Remove(service);
            await context.SaveChangesAsync();
            service.State = state;
            return service;
        }
        finally
        {
            _semaphore.Release();
        }
    }
}
