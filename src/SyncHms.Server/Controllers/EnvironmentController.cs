namespace SyncHms.Server.Controllers;

/// <summary>
/// Класс, описывающий контроллер для запроса окружения.<br/>
/// Унаследован от класса <see cref="ControllerBase"/>
/// </summary>
/// <param name="environmentRepository">Экземпляр репозитория для работы с окружением.</param>
/// <param name="serviceController">Экземпляр контроллера сервисов.</param>
/// <param name="hubContext">Экземпляр контекста концентратора окружения <c>SignalR</c></param>
[ApiController]
[Route("api/v1.0/environment")]
public class EnvironmentController(IEnvironmentRepository<ApplicationEnvironment> environmentRepository,
    IServiceController<ApplicationEnvironment> serviceController, IHubContext<EnvironmentHub> hubContext) : ControllerBase
{
    /// <summary>Конечная точка для запроса экземпляра текущего окружения.</summary>
    [HttpGet("")]
    [ProducesResponseType<ApplicationEnvironment>((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<ApplicationEnvironment>> GetAsync()
    {
        return Ok(await environmentRepository.GetAsync() ?? new ApplicationEnvironment());
    }

    /// <summary>Конечная точка для запроса на изменение значений переменных окружения.</summary>
    /// <param name="environment">Экземпляр объекта новых значений для переменных окружения.</param>
    /// <returns>Экземпляр обновленного окружения.</returns>
    [HttpPut("")]
    [ProducesResponseType<ApplicationEnvironment>((int)HttpStatusCode.OK)]
    [ProducesResponseType<string>((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<ApplicationEnvironment>> UpdateAsync([Required][FromBody] ApplicationEnvironment environment)
    {
        if (await environmentRepository.UpdateAsync(environment))
        {
            serviceController.SetEnvironment(environment);
            await hubContext.Clients.All.SendAsync("Environment", environment);
        }

        return Ok(environment);
    }
}
