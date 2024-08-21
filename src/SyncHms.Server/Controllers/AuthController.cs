namespace SyncHms.Server.Controllers;

/// <summary>
/// Класс, описывающий контроллер идентификации пользователей.<br/>
/// Унаследован от класса <see cref="ControllerBase"/>
/// </summary>
/// <param name="identityService">Экземпляр сервиса идентификации пользователей.</param>
[ApiController]
[Route("api/v1.0/auth")]
public class AuthController(IIdentityService identityService) : ControllerBase
{
    /// <summary>Свойство, возвращающее идентификатор пользователя из токена доступа.</summary>
    protected long? UserId => long.TryParse(HttpContext.User.FindFirst(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value, out long userId)
        ? userId : null;

    /// <summary>Конечная точка для запроса наличия зарегистрированных пользователей.</summary>
    /// <returns>
    /// Если есть есть хотя бы один зарегистрированный пользователь -
    /// будет возвращен резульатат <see cref="HttpStatusCode.OK"/><br/>
    /// В противном случае будет возвращен результат <see cref="HttpStatusCode.NotFound"/>
    /// </returns>
    [HttpGet("")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetStateAsync()
    {
        return await identityService.AnyAsync() ? Ok() : NotFound();
    }

    /// <summary>Конечная точка регистрации пользователя.</summary>
    /// <param name="login">Данные для регистрации пользователя.</param>
    [HttpPost("signup")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.Forbidden)]
    [ProducesResponseType<string>((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> SignUpAsync([Required][FromBody] Login login)
    {
        try
        {
            var token = await identityService.SignUpAsync(login.Username, login.Password);
            return SetCookie(token);
        }
        catch (MethodAccessException)
        {
            return StatusCode(StatusCodes.Status403Forbidden);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>Конечная точка аутентификации пользователя.</summary>
    /// <param name="login">Данные для аутентификации пользователя.</param>
    [HttpPost("signin")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType<string>((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType<string>((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Forbidden)]
    public async Task<IActionResult> SignInAsync([Required][FromBody] Login login)
    {
        try
        {
            return await identityService.SignInAsync(login.Username, login.Password) is { } token
                ? SetCookie(token)
                : NotFound("The username or password is incorrect");
        }
        catch (MethodAccessException)
        {
            return StatusCode(StatusCodes.Status403Forbidden);
        }
    }

    /// <summary>Конечная точка обновления токена доступа.</summary>
    [HttpGet("refresh")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [Authorize(AuthenticationSchemes = "Refresh")]
    public async Task<IActionResult> RefreshAsync()
    {
        var refreshToken = HttpContext.Request.Cookies["Refresh"];
        if (string.IsNullOrEmpty(refreshToken) || UserId is not { } userId ||
            await identityService.RefreshAsync(userId, refreshToken) is not { } token)
        {
            RemoveCookie();
            return Unauthorized();
        }

        return SetCookie(token);
    }
    
    /// <summary>Конечная точка выхода из приложения.</summary>
    [HttpDelete("signout")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> SignOutAsync()
    {
        RemoveCookie();

        var refreshToken = Request.Cookies["Refresh"];
        if (string.IsNullOrEmpty(refreshToken) || UserId is not { } userId)
            return Unauthorized();

        await identityService.SignOutAsync(userId, refreshToken);
        return Ok();
    }

    /// <summary>Метод, формирующий заголовки с токена доступа.</summary>
    /// <param name="token">Экземпляр токена доступа.</param>
    private OkResult SetCookie(Token token)
    {
        var cookieExpiration = token.Expiration.TotalSeconds;

        Response.Headers.Append("Set-Cookie", $"Authorization={token.AccessToken}; Max-Age={cookieExpiration}; Path=/; HttpOnly");
        Response.Headers.Append("Set-Cookie", $"Refresh={token.RefreshToken}; Max-Age={cookieExpiration}; Path=/api/v1.0/auth/refresh; HttpOnly");

        return Ok();
    }

    /// <summary>Метод, формирующий заголовки для удаления токена доступа в клиентском приложении.</summary>
    private void RemoveCookie()
    {
        Response.Headers.Append("Set-Cookie", "Authorization=; Max-Age=-1; Path=/; HttpOnly");
        Response.Headers.Append("Set-Cookie", "Refresh=; Max-Age=-1; Path=/api/v1.0/auth/refresh; HttpOnly");
    }
}
