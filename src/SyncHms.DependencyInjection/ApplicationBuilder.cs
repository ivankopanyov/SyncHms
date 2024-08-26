namespace SyncHms.DependencyInjection;

/// <summary>
/// Класс, описывающий построитель приложения.
/// Реализует интрефейс <see cref="IServiceCollection"/>
/// </summary>
/// <param name="services">Коллекция сервисов приложения.</param>
public abstract class ApplicationBuilder(IServiceCollection services) : IServiceCollection
{
    public int Count => services.Count;

    public bool IsReadOnly => services.IsReadOnly;

    public ServiceDescriptor this[int index]
    {
        get => services[index];
        set => services[index] = value;
    }
    
    public IEnumerator<ServiceDescriptor> GetEnumerator()
    {
        return services.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Add(ServiceDescriptor item)
    {
        services.Add(item);
    }

    public void Clear()
    {
        services.Clear();
    }

    public bool Contains(ServiceDescriptor item)
    {
        return services.Contains(item);
    }

    public void CopyTo(ServiceDescriptor[] array, int arrayIndex)
    {
        services.CopyTo(array, arrayIndex);
    }

    public bool Remove(ServiceDescriptor item)
    {
        return services.Remove(item);
    }
    
    public int IndexOf(ServiceDescriptor item)
    {
        return services.IndexOf(item);
    }

    public void Insert(int index, ServiceDescriptor item)
    {
        services.Insert(index, item);
    }

    public void RemoveAt(int index)
    {
        services.RemoveAt(index);
    }
}