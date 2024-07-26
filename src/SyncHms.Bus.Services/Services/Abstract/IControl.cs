namespace SyncHms.Bus.Services;

internal delegate Task ActiveHandleAsync();

internal delegate Task UnactiveHandleAsync(string error, Exception? ex = null);

public interface IControl<TOptions> where TOptions : class, new()
{
    internal event ActiveHandleAsync? ActiveEvent;

    internal event UnactiveHandleAsync? UnactiveEvent;

    TOptions Options { get; internal set; }

    void Active();

    void Unactive(string error);

    void Unactive(Exception ex);
}

public interface IControl<TOptions, TEnvironment> : IControl<TOptions> where TOptions : class, new() where TEnvironment : class, new()
{
    TEnvironment Environment { get; internal set; }
}
