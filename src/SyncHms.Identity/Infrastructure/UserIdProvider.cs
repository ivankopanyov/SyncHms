namespace SyncHms.Identity.Infrastructure;

/// <summary>
/// Класс, описывающий провайдер, поставляющий идентификатор пользователя из токена доступа.<br/>
/// реализует интерфейс <see cref="IUserIdProvider"/>
/// </summary>
public class UserIdProvider: IUserIdProvider
{
    /// <summary>Метод, возвращающий идентификатор пользователя из токена доступа.</summary>
    /// <param name="connection">Экземпляр контекста подключения <c>SignalR</c></param>
    /// <returns>Идентификатор пользователя.</returns>
    public string? GetUserId(HubConnectionContext connection) =>
        connection.User.FindFirst(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
}
