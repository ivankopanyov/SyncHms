namespace SyncHms.Bus.Services;

public class ServicesBusBuilder(IServiceCollection services) : ApplicationBuilder(services), IServicesBusBuilder
{
    private readonly HashSet<string> _serviceNames = [];
    
    private readonly IServiceCollection _services = services;

    public IServicesBusBuilder AddTransient<TAbstract, TImplement, TOptions>(Action<ServiceOptions> options)
        where TAbstract : class, IService<TOptions> where TImplement : class, TAbstract where TOptions : class, new()
    {
        _services.AddTransient<TAbstract, TImplement>();
        AddService<TAbstract, TOptions>(options);
        return this;
    }

    public IServicesBusBuilder AddScoped<TAbstract, TImplement, TOptions>(Action<ServiceOptions> options)
        where TAbstract : class, IService<TOptions> where TImplement : class, TAbstract where TOptions : class, new()
    {
        _services.AddScoped<TAbstract, TImplement>();
        AddService<TAbstract, TOptions>(options);
        return this;
    }

    public IServicesBusBuilder AddSingleton<TAbstract, TImplement, TOptions>(Action<ServiceOptions> options)
        where TAbstract : class, IService<TOptions> where TImplement : class, TAbstract where TOptions : class, new()
    {
        _services.AddSingleton<TAbstract, TImplement>();
        AddService<TAbstract, TOptions>(options);
        return this;
    }

    private void AddService<TService, TOptions>(Action<ServiceOptions> options)
        where TService : class, IService<TOptions> where TOptions : class, new()
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));

        var serviceOptions = new ServiceOptions();
        options.Invoke(serviceOptions);

        ArgumentException.ThrowIfNullOrWhiteSpace(serviceOptions.ServiceName, nameof(serviceOptions.ServiceName));

        if (!_serviceNames.Add(serviceOptions.ServiceName))
            throw new ArgumentException($"Service named {serviceOptions.ServiceName} has already been registered.", nameof(serviceOptions.ServiceName));

        _services
            .AddHostedService<ControlStarter<TService, TOptions>>()
            .AddSingleton<IControl<TOptions>, Control<TOptions>>()
            .AddSingleton(new ServiceOptions<TOptions>
            {
                ServiceName = serviceOptions.ServiceName
            });
    }
}

public class ServicesBusBuilder<TEnvironment>(IServiceCollection services) : ApplicationBuilder(services), IServicesBusBuilder<TEnvironment>
    where TEnvironment : class, new()
{
    private readonly HashSet<string> _serviceNames = [];
    
    private readonly IServiceCollection _services = services;

    public IServicesBusBuilder<TEnvironment> AddTransient<TAbstract, TImplement, TOptions>(Action<ServiceOptions> options)
        where TAbstract : class, IService<TOptions, TEnvironment> where TImplement : class, TAbstract where TOptions : class, new()
    {
        _services.AddTransient<TAbstract, TImplement>();
        AddService<TAbstract, TOptions>(options);
        return this;
    }

    public IServicesBusBuilder<TEnvironment> AddScoped<TAbstract, TImplement, TOptions>(Action<ServiceOptions> options)
        where TAbstract : class, IService<TOptions, TEnvironment> where TImplement : class, TAbstract where TOptions : class, new()
    {
        _services.AddScoped<TAbstract, TImplement>();
        AddService<TAbstract, TOptions>(options);
        return this;
    }

    public IServicesBusBuilder<TEnvironment> AddSingleton<TAbstract, TImplement, TOptions>(Action<ServiceOptions> options)
        where TAbstract : class, IService<TOptions, TEnvironment> where TImplement : class, TAbstract where TOptions : class, new()
    {
        _services.AddSingleton<TAbstract, TImplement>();
        AddService<TAbstract, TOptions>(options);
        return this;
    }

    private void AddService<TService, TOptions>(Action<ServiceOptions> options)
        where TService : class, IService<TOptions, TEnvironment> where TOptions : class, new()
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));

        var serviceOptions = new ServiceOptions();
        options.Invoke(serviceOptions);

        ArgumentException.ThrowIfNullOrWhiteSpace(serviceOptions.ServiceName, nameof(serviceOptions.ServiceName));

        if (!_serviceNames.Add(serviceOptions.ServiceName))
            throw new ArgumentException($"Service named {serviceOptions.ServiceName} has already been registered.", nameof(serviceOptions.ServiceName));

        _services
            .AddHostedService<ControlStarter<TService, TOptions, TEnvironment>>()
            .AddSingleton<IControl<TOptions, TEnvironment>, Control<TOptions, TEnvironment>>()
            .AddSingleton(new ServiceOptions<TOptions>
            {
                ServiceName = serviceOptions.ServiceName
            });
    }
}
