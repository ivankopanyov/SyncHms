namespace SyncHms.Server.Controllers;

/// <summary>
/// Класс, описывающий контроллер для запроса состояния сервисов.<br/>
/// Унаследован от класса <see cref="ControllerBase"/>
/// </summary>
/// <param name="serviceRepository">Экземпляр репозитория для работы с состоянием сервисов.</param>
/// <param name="serviceController">Экземпляр контроллера сервисов.</param>
/// <param name="hubContext">Экземпляр контекста концентратора сервисов <c>SignalR</c></param>
[ApiController]
[Route("api/v1.0/services")]
public class ServiceController(IServiceRepository serviceRepository, IServiceController<ApplicationEnvironment> serviceController, IHubContext<ServiceHub> hubContext) : ControllerBase
{
    /// <summary>Конечная точка для запроса состояния все сервисов приложения.</summary>
    /// <returns>Результат запроса.</returns>
    [HttpGet("")]
    [ProducesResponseType<IEnumerable<Service>>((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<IEnumerable<Service>>> GetAllAsync() => Ok(await serviceRepository.GetAllAsync());

    /// <summary>Конечная точка для запроса состояния сервиса по имени.</summary>
    /// <param name="serviceName">Имя сервиса.</param>
    /// <returns>Результат запроса.</returns>
    [HttpGet("{serviceName}")]
    [ProducesResponseType<Service>((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType<string>((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<Service>> GetAsync([Required][FromRoute] string serviceName)
    {
        return await serviceRepository.GetAsync(serviceName) is { } serviceInfo
            ? Ok(serviceInfo)
            : NotFound();
    }

    /// <summary>Конечная точка для запроса переподключения сервиса к удаленному ресурсу по имени.</summary>
    /// <param name="serviceName">Имя сервиса.</param>
    [HttpGet("reload/{serviceName}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType<string>((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public IActionResult Reload([Required][FromRoute] string serviceName)
    {
        serviceController.Reload(serviceName);
        return Ok();
    }

    /// <summary>Конечная точка для запроса обновления опций сервиса по имени.</summary>
    /// <param name="serviceName">Имя сервиса.</param>
    /// <param name="settings">Опции сервиса.</param>
    [HttpPut("{serviceName}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType<string>((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public IActionResult Update([Required][FromRoute] string serviceName, [Required][FromBody] ServiceSettings settings)
    {
        serviceController.SetOptions(new Bus.Services.Options
        {
            ServiceName = serviceName,
            JsonOptions = settings.JsonOptions
        });

        return Ok();
    }

    /// <summary>Конечная точка для запроса удаления сервиса по имени.</summary>
    /// <param name="serviceName">Имя сервиса.</param>
    [HttpDelete("{serviceName}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType<string>((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> DeleteAsync([Required][FromRoute] string serviceName)
    {
        if (await serviceRepository.RemoveAsync(serviceName) is { } serviceInfo)
        {
            await hubContext.Clients.All.SendAsync("RemoveService", new RemoveService
            {
                ServiceName = serviceName
            });

            serviceController.SetOptions(new Bus.Services.Options
            {
                ServiceName = serviceName,
                JsonOptions = serviceInfo.JsonOptions
            });

            return Ok();
        }

        return NotFound();
    }
}
