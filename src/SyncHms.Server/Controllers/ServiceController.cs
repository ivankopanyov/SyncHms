namespace SyncHms.Server.Controllers;

[ApiController]
[Route("api/v1.0/services")]
public class ServiceController(IServiceRepository serviceRepository, IServiceController<ApplicationEnvironment> serviceController, IHubContext<ServiceHub> hubContext) : ControllerBase
{
    [HttpGet("")]
    [ProducesResponseType<IEnumerable<Service>>((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<IEnumerable<Service>>> GetAllAsync() => Ok(await serviceRepository.GetAllAsync());

    [HttpGet("{serviceName}")]
    [ProducesResponseType<Service>((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType<string>((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<Service>> GetAsync([Required][FromRoute] string serviceName)
    {
        return await serviceRepository.GetAsync(serviceName) is Service serviceInfo
            ? Ok(serviceInfo)
            : NotFound();
    }

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

    [HttpDelete("{serviceName}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType<string>((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> DeleteAsync([Required][FromRoute] string serviceName)
    {
        if (await serviceRepository.RemoveAsync(serviceName) is Service serviceInfo)
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
