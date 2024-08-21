namespace SyncHms.Bus.Services;

/// <summary>
/// Класс, описывающий модель сообщения, переющего текущее
/// состояние подключения сервиса к удаленному ресурсу.
/// </summary>
public class ServiceInfo : Options
{
    /// <summary>Имя сервиса.</summary>
    public string Name { get; set; }

    /// <summary>Текущее состояние подключения сервиса к удаленному ресурсу.</summary>
    public ServiceState? State { get; set; }
}
