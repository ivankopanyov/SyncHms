namespace SyncHms.Domain.EntityFramework.Repositories.Implement;

internal class ServiceRepository(IDomainContextFactory domainContextFactory, ICache cache) : IServiceRepository
{
    private const string Key = "services";

    private static readonly SemaphoreSlim _semaphore = new(1);

    public async Task<IEnumerable<Service>> GetAllAsync()
    {
        await _semaphore.WaitAsync();

        try
        {
            if (await GetAllUsedAsync())
                return await cache.GetAllAsync<Service>();

            await using var context = domainContextFactory.Create();
            var services = await context.Services.AsNoTracking().ToListAsync();

            foreach (var service in services)
            {
                if (!await cache.ExistsAsync<Service>(service.Name))
                    await cache.PushAsync(service.Name, service);
            }

            await UseGetAllAsync();
            return services;
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task<Service?> GetAsync(string serviceName)
    {
        ArgumentNullException.ThrowIfNull(serviceName, nameof(serviceName));
        await _semaphore.WaitAsync();

        try
        {
            if (await cache.GetAsync<Service>(serviceName) is { } s)
                return s;

            await using var context = domainContextFactory.Create();
            if (await context.Services
                    .AsNoTracking()
                    .FirstOrDefaultAsync(s => s.Name == serviceName) is not { } service)
                return null;
            
            service = new Service
            {
                Name = serviceName,
                JsonOptions = service.JsonOptions
            };

            await cache.PushAsync(serviceName, service);
            return service;

        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task<bool> AddAsync(Service service)
    {
        ArgumentNullException.ThrowIfNull(service?.Name, nameof(service.Name));
        await _semaphore.WaitAsync();

        try
        {
            if (await cache.ExistsAsync<Service>(service.Name))
                return false;
            
            if (!await GetAllUsedAsync())
            {
                await using var context = domainContextFactory.Create();
                if (await context.Services
                        .AsNoTracking()
                        .FirstOrDefaultAsync(s => s.Name == service.Name) is { } s)
                {
                    await cache.PushAsync(s.Name, s);
                    return false;
                }
                    
                await context.Services.AddAsync(service);
                await context.SaveChangesAsync();
            }

            await cache.PushAsync(service.Name, service);
            return true;
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task<Service> UpdateAsync(Service service, bool updateOptions)
    {
        ArgumentNullException.ThrowIfNull(service?.Name, nameof(service.Name));
        var serviceClone = service.Clone();

        await _semaphore.WaitAsync();

        try
        {
            await using var context = domainContextFactory.Create();
            if (await context.Services.FirstOrDefaultAsync(s => s.Name == service.Name) is not {} s)
            {
                s = new Service()
                {
                    Name = service.Name,
                    JsonOptions = service.JsonOptions
                };
                
                await context.Services.AddAsync(s);
                await context.SaveChangesAsync();
            }
            else if (updateOptions)
            {
                s.JsonOptions = service.JsonOptions;
                await context.SaveChangesAsync();
            }

            if (await cache.PopAsync<Service>(serviceClone.Name) is { } cachedService)
            {
                serviceClone.State ??= cachedService.State;

                if (!updateOptions)
                    serviceClone.JsonOptions = cachedService.JsonOptions;
            }

            await cache.PushAsync(serviceClone.Name, serviceClone);
            return serviceClone;
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task<Service?> RemoveAsync(string serviceName)
    {
        ArgumentNullException.ThrowIfNull(serviceName, nameof(serviceName));
        await _semaphore.WaitAsync();

        try
        {
            var result = await cache.PopAsync<Service>(serviceName);

            await using var context = domainContextFactory.Create();
            if (await context.Services
                    .AsNoTracking()
                    .FirstOrDefaultAsync(s => s.Name == serviceName) is not { } service)
                return result;
            
            context.Services.Remove(service);
            await context.SaveChangesAsync();

            return result ?? new Service
            {
                Name = service.Name,
                JsonOptions = service.JsonOptions
            };
        }
        finally
        {
            _semaphore.Release();
        }
    }

    private async Task<bool> GetAllUsedAsync() => await cache.ExistsAsync<object>(Key);

    private async Task UseGetAllAsync() => await cache.PushAsync(Key, new object());
}
