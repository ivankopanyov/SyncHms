namespace SyncHms.Bus.Services;

/// <summary>
/// Класс, описывающий модель сообщения, отправленного сервисом
/// для уведомления контроллера сервисов об измении опций.
/// </summary>
public class UpdatedServiceInfo : ServiceInfo
{
    /// <summary>
    /// Флаг, сообщающий контроллеру сервисов, нужно ли перезаписать опции сервиса,
    /// хранящиеся в базе данных, опциями, переданными сервисом в этом сообщении.
    /// </summary>
    public bool UpdateOptions { get; set; } = true;
    
    /// <summary>
    /// Флаг, запрашивающий у контроллера сервисов обязательный ответ на это сообщение.
    /// </summary>
    public bool ResponseRequired { get; set; }
}
