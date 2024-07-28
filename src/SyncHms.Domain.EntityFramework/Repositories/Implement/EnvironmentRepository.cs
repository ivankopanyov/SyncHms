namespace SyncHms.Domain.EntityFramework.Repositories.Implement;

internal class EnvironmentRepository<TEnvironment>(DomainContext context, ICache cache)
    : IEnvironmentRepository<TEnvironment> where TEnvironment : class, new()
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

            if (await context.AppOptions.AsNoTracking().FirstOrDefaultAsync(o => o.Id == Key) is not
                { } result) 
                return null;
            
            if (JsonConvert.DeserializeObject<TEnvironment>(result.Value) is not {} env)
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
            var options = await context.AppOptions.FirstOrDefaultAsync(o => o.Id == Key);
            TEnvironment? env = null;
            
            if (options != null)
            {
                env = JsonConvert.DeserializeObject<TEnvironment>(options.Value);
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
                options.Value = JsonConvert.SerializeObject(environment);
            }
            else
            {
                await context.AppOptions.AddAsync(new AppOptions
                {
                    Id = Key,
                    Value = JsonConvert.SerializeObject(environment)
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
