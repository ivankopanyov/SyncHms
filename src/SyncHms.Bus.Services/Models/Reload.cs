namespace SyncHms.Bus.Services;

/// <summary>
/// Класс, описывающий модель сообщения, инициирующего переподключение сервиса к удаленному ресурсу.
/// </summary>
public class Reload
{
    /// <summary>Имя сервиса.</summary>
    public string ServiceName { get; set; }
}
