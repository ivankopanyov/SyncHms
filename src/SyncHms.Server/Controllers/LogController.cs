namespace SyncHms.Server.Controllers;

[ApiController]
[Route("api/v1.0/logs")]
public class LogController(ILogRepository logRepository) : ControllerBase
{
    [HttpPost("")]
    [ProducesResponseType<IEnumerable<Log>>((int)HttpStatusCode.OK)]
    [ProducesResponseType<string>((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<IEnumerable<Log>>> PostAsync([Required][FromBody] SearchFilter filter)
    {
        return Ok(await logRepository.FindAsync(filter));
    }

    [HttpGet("{id}")]
    [ProducesResponseType<IEnumerable<Log>>((int)HttpStatusCode.OK)]
    [ProducesResponseType<string>((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType<string>((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<IEnumerable<Log>>> GetAsync([Required][FromRoute] string id)
    {
        return await logRepository.GetAsync(id) is { } logs
            ? Ok(logs)
            : NotFound($"Task {id} not found.");
    }

    [HttpGet("{id}/data")]
    [ProducesResponseType<LogData>((int)HttpStatusCode.OK)]
    [ProducesResponseType<string>((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType<string>((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<LogData>> GetEventAsync([Required][FromRoute] string id)
    {
        return await logRepository.GetDataAsync(id) is { } logData
            ? Ok(logData)
            : NotFound($"Log {id} not found.");
    }
}
