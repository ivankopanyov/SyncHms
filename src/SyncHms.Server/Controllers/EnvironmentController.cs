namespace SyncHms.Server.Controllers;

[ApiController]
[Route("api/v1.0/environment")]
public class EnvironmentController(IEnvironmentRepository<ApplicationEnvironment> environmentRepository,
    IServiceController<ApplicationEnvironment> serviceController, IHubContext<EnvironmentHub> hubContext) : ControllerBase
{
    [HttpGet("")]
    [ProducesResponseType<ApplicationEnvironment>((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<ApplicationEnvironment>> GetAsync()
    {
        return Ok(await environmentRepository.GetAsync() ?? new ApplicationEnvironment());
    }

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
