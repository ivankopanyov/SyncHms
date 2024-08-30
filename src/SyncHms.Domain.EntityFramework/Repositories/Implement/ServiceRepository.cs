namespace SyncHms.Domain.EntityFramework.Repositories.Implement;

/// <summary>
/// Класс, описывающий репозиторий для работы с состояниями сервисов.<br/>
/// Реализует интерфейс <see cref="IServiceRepository"/>
/// </summary>
/// <param name="domainContextFactory">
/// Экземпляр фабрики, создающей контекст подключения к базе данных домена.
/// </param>
/// <param name="cache">Экземпляр сервиса кеширования.</param>
internal class ServiceRepository(IDomainContextFactory domainContextFactory, ICache cache) : IServiceRepository
{
    private static readonly SemaphoreSlim Semaphore = new(1);
    
    /// <summary>Метод, возвращающий информацию о всех сервисах.</summary>
    /// <returns>Информация о всех сервисах.</returns>
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

    /// <summary>Метод, возвращающий информацию о сервисе с указанным именем.</summary>
    /// <param name="serviceName">Имя сервиса.</param>
    /// <returns>Информация о сервисе.</returns>
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

    /// <summary>Метод, обновляющий информацию о сервисе.</summary>
    /// <param name="service">Информация о сервисе.</param>
    /// <param name="updateOptions">
    /// Флаг, указывающий, нужно ли обновить опции сервиса в базе данных.<br/>
    /// Если <c>true</c> - опции будут обновлены.<br/>
    /// Если <c>false</c> - опции будут обновлены только в случае, если опции отсутствуют в репозитории.<br/>
    /// Если в передаваемом объекте занчение опций <c>null</c>, они не будет применены к объекту в репозитории.
    /// </param>
    /// <returns>Обновленная информация о сервисе.</returns>
    public async Task<Service> UpdateAsync(Service service, bool updateOptions)
    {
        ArgumentNullException.ThrowIfNull(service?.Name, nameof(service.Name));

        await Semaphore.WaitAsync();

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
            Semaphore.Release();
        }
    }

    /// <summary>Метод, удаляющий информацию о сервисе по его имени.</summary>
    /// <param name="serviceName">Имя сервиса.</param>
    /// <returns>Удаленный объект.</returns>
    public async Task<Service?> RemoveAsync(string serviceName)
    {
        await Semaphore.WaitAsync();
        
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
            Semaphore.Release();
        }
    }
}
