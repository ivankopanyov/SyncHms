namespace SyncHms.Identity;

/// <summary>Интерфейс, определяющий методы сервиса идентификации пользователей.</summary>
public interface IIdentityService
{
    /// <summary>Метод, проверяющий наличие зарегистрированных пользователей.</summary>
    /// <returns>Флаг, указывающий, был ли зарегистрирован хотя бы один пользователь.</returns>
    Task<bool> AnyAsync();

    /// <summary>Метод регистрации пользователей.</summary>
    /// <param name="username">Имя пользователя.</param>
    /// <param name="password">Пароль пользователя.</param>
    /// <returns>Токен для авторизации пользователя.</returns>
    Task<Token> SignUpAsync(string username, string password);

    /// <summary>Метод аутентификации пользователей.</summary>
    /// <param name="username">Имя пользователя.</param>
    /// <param name="password">Пароль пользователя.</param>
    /// <returns>
    /// Токен для авторизации пользователя.<br/>
    /// Вернет <c>null</c> - если пользователь с указанными данными не зарегистрирован.
    /// </returns>
    Task<Token?> SignInAsync(string username, string password);

    /// <summary>Метод обновления токена доступа пользователя.</summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <param name="refreshToken">Токен обновления токена доступа.</param>
    /// <returns>
    /// Токен для авторизации пользователя.<br/>
    /// Вернет <c>null</c> - если пользователь с указанными данными не найден.
    /// </returns>
    Task<Token?> RefreshAsync(long userId, string refreshToken);
    
    /// <summary>Метод выхода пользователя.</summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <param name="refreshToken">Токен обновления токена доступа.</param>
    Task SignOutAsync(long userId, string refreshToken);
}
