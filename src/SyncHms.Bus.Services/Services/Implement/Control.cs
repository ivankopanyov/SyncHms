namespace SyncHms.Bus.Services.Services.Implement;

internal class Control<TOptions>(IOptions<TOptions>? options) : IControl<TOptions> where TOptions : class, new()
{
    private TOptions _options = options?.Value ?? new();

    public TOptions Options
    {
        get => _options;
        set => _options = value ?? new();
    }

    public event ActiveHandleAsync? ActiveEvent;

    public event UnactiveHandleAsync? UnactiveEvent;

    public void Active() => ActiveEvent?.Invoke();

    public void Unactive(string error) => UnactiveEvent?.Invoke(error);

    public void Unactive(Exception ex) => UnactiveEvent?.Invoke(ex.Message, ex);
}

internal class Control<TOptions, TEnvironment>(IOptions<TOptions>? options) : Control<TOptions>(options), IControl<TOptions, TEnvironment>
    where TOptions : class, new() where TEnvironment : class, new()
{
    private TEnvironment _environment = new();

    public TEnvironment Environment
    {
        get => _environment;
        set => _environment = value ?? new();
    }
}
