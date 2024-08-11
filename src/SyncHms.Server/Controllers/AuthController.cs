﻿namespace SyncHms.Server.Controllers;

[ApiController]
[Route("api/v1.0/auth")]
public class AuthController(IIdentityService identityService) : ControllerBase
{
    protected long? UserId => long.TryParse(HttpContext.User.FindFirst(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value, out long userId)
        ? userId : null;

    [HttpGet("")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetStateAsync()
    {
        return await identityService.AnyAsync() ? Ok() : NotFound();
    }

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

    private OkResult SetCookie(Token token)
    {
        var cookieExpiration = token.Expiration.TotalSeconds;

        Response.Headers.Append("Set-Cookie", $"Authorization={token.AccessToken}; Max-Age={cookieExpiration}; Path=/; HttpOnly");
        Response.Headers.Append("Set-Cookie", $"Refresh={token.RefreshToken}; Max-Age={cookieExpiration}; Path=/api/v1.0/auth/refresh; HttpOnly");

        return Ok();
    }

    private void RemoveCookie()
    {
        Response.Headers.Append("Set-Cookie", "Authorization=; Max-Age=-1; Path=/; HttpOnly");
        Response.Headers.Append("Set-Cookie", "Refresh=; Max-Age=-1; Path=/api/v1.0/auth/refresh; HttpOnly");
    }
}