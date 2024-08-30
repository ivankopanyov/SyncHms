namespace SyncHms.Identity.Services.Abstract;

/// <summary>Интерфейс, определяющий методы сервиса, генерирующего токены доступа.</summary>
internal interface ITokenService
{
    /// <summary>Метод, генерирующий токены доступа.</summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <returns>Токен доступа.</returns>
    Token Generate(long userId);
}
