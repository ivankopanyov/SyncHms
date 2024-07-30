namespace SyncHms.Domain.EntityFramework.Repositories.Implement;

internal class EnvironmentRepository<TEnvironment>(IDomainContextFactory domainContextFactory, ICache cache,
    EntityFrameworkDomainOptions domainOptions) : IEnvironmentRepository<TEnvironment>
    where TEnvironment : class, new()
{
    private const string Key = "environment";

    private static readonly SemaphoreSlim _semaphore = new(1);

    public async Task<TEnvironment?> GetAsync()
    {
        await _semaphore.WaitAsync();

        try
        {
            if (await cache.GetAsync<TEnvironment>(Key) is { } environment)
                return environment;

            await using var context = domainContextFactory.Create();
            if (await context.AppOptions.AsNoTracking().FirstOrDefaultAsync(o => o.Id == Key) is not
                { } result) 
                return null;
            
            if (JsonConvert.DeserializeObject<TEnvironment>(result.Value, domainOptions.JsonSerializerSettings)
                is not {} env)
            {
                context.AppOptions.Remove(result);
                await context.SaveChangesAsync();
                return null;
            }

            await cache.PushAsync(Key, env);
            return env;
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task<bool> UpdateAsync(TEnvironment environment)
    {
        ArgumentNullException.ThrowIfNull(environment);

        await _semaphore.WaitAsync();

        try
        {
            await using var context = domainContextFactory.Create();
            var options = await context.AppOptions.FirstOrDefaultAsync(o => o.Id == Key);
            TEnvironment? env = null;
            
            if (options != null)
            {
                env = JsonConvert.DeserializeObject<TEnvironment>(options.Value, domainOptions.JsonSerializerSettings);
                if (env == null)
                {
                    context.AppOptions.Remove(options);
                    await context.SaveChangesAsync();
                    options = null;
                }
            }

            var result = env == null || !environment.Equals(env);

            if (!result) 
                return result;

            if (options != null)
            {
                options.Value = JsonConvert.SerializeObject(environment, domainOptions.JsonSerializerSettings);
            }
            else
            {
                await context.AppOptions.AddAsync(new AppOptions
                {
                    Id = Key,
                    Value = JsonConvert.SerializeObject(environment, domainOptions.JsonSerializerSettings)
                });
            }

            await context.SaveChangesAsync();
            await cache.PushAsync(Key, environment);

            return result;
        }
        finally
        {
            _semaphore.Release();
        }
    }
}