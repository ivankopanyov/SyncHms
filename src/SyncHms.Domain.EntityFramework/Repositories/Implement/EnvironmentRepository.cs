namespace SyncHms.Domain.EntityFramework.Repositories.Implement;

/// <summary>
/// Класс, описывающий репозиторий для работы с окружением.<br/>
/// Реализует интерфейс <see cref="IEnvironmentRepository{TEnvironment}"/>
/// </summary>
/// <typeparam name="TEnvironment">
/// Тип окружения. Должен иметь открытый конструктор без параметров.
/// </typeparam>
/// <param name="domainContextFactory">
/// Экземпляр фабрики, создающей контекст подключения к базе данных домена.
/// </param>
/// <param name="domainOptions">Экземпляр опций домена.</param>
internal class EnvironmentRepository<TEnvironment>(IDomainContextFactory domainContextFactory, 
    EntityFrameworkDomainOptions domainOptions) : IEnvironmentRepository<TEnvironment>
    where TEnvironment : class, new()
{
    /// <summary>Ключ сущности окружения, хранящейся в базе данных.</summary>
    private const string Key = "environment";

    private static readonly SemaphoreSlim Semaphore = new(1);

    /// <summary>Метод, возвращающий экземпляр текущего окружения.</summary>
    /// <returns>Текущее окружение.</returns>
    public async Task<TEnvironment?> GetAsync()
    {
        await Semaphore.WaitAsync();

        try
        {
            await using var context = domainContextFactory.Create();
            if (await context.AppOptions.AsNoTracking().FirstOrDefaultAsync(o => o.Id == Key) is not
                { } result) 
                return null;

            if (JsonConvert.DeserializeObject<TEnvironment>(result.Value, domainOptions.JsonSerializerSettings)
                is { } env) 
                return env;
            
            context.AppOptions.Remove(result);
            await context.SaveChangesAsync();
            return null;

        }
        finally
        {
            Semaphore.Release();
        }
    }
    
    /// <summary>Метод, обновляющий текущее окружение.</summary>
    /// <param name="environment">Экземпляр обновленного окружения.</param>
    /// <returns>
    /// Флаг, указывающий, удалось ли применить новое окружение.
    /// Вернет значение <c>false</c>, если значения переменных текущего окружения идентичны обновленным.
    /// </returns>
    public async Task<bool> UpdateAsync(TEnvironment environment)
    {
        ArgumentNullException.ThrowIfNull(environment);

        await Semaphore.WaitAsync();

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
            return result;
        }
        finally
        {
            Semaphore.Release();
        }
    }
}
