namespace SyncHms.Bus.Services;

public interface IServicesBusBuilder : IBusBuilder
{
    IServicesBusBuilder AddTransient<TAbstract, TImplement, TOptions>(Action<ServiceOptions> options)
        where TAbstract : class, IService<TOptions>
        where TImplement : class, TAbstract
        where TOptions : class, new();

    IServicesBusBuilder AddScoped<TAbstract, TImplement, TOptions>(Action<ServiceOptions> options)
        where TAbstract : class, IService<TOptions>
        where TImplement : class, TAbstract
        where TOptions : class, new();

    IServicesBusBuilder AddSingleton<TAbstract, TImplement, TOptions>(Action<ServiceOptions> options)
        where TAbstract : class, IService<TOptions>
        where TImplement : class, TAbstract
        where TOptions : class, new();
}

public interface IServicesBusBuilder<TEnvironment> : IBusBuilder where TEnvironment : class, new()
{
    IServicesBusBuilder<TEnvironment> AddTransient<TAbstract, TImplement, TOptions>(Action<ServiceOptions> options)
        where TAbstract : class, IService<TOptions, TEnvironment>
        where TImplement : class, TAbstract
        where TOptions : class, new();

    IServicesBusBuilder<TEnvironment> AddScoped<TAbstract, TImplement, TOptions>(Action<ServiceOptions> options)
        where TAbstract : class, IService<TOptions, TEnvironment>
        where TImplement : class, TAbstract
        where TOptions : class, new();

    IServicesBusBuilder<TEnvironment> AddSingleton<TAbstract, TImplement, TOptions>(Action<ServiceOptions> options)
        where TAbstract : class, IService<TOptions, TEnvironment>
        where TImplement : class, TAbstract
        where TOptions : class, new();
}
