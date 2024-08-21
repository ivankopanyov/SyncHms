namespace SyncHms.Server.Controllers;

/// <summary>
/// Класс, описывающий контроллер для запроса логов.<br/>
/// Унаследован от класса <see cref="ControllerBase"/>
/// </summary>
/// <param name="logRepository">Экземпляр репозитория для работы с логами.</param>
[ApiController]
[Route("api/v1.0/logs")]
public class LogController(ILogRepository logRepository) : ControllerBase
{
    /// <summary>Конечная точка для запроса поиска по логам.</summary>
    /// <param name="filter">Экземпляр фильтра для поиска.</param>
    /// <returns>Результат поиска.</returns>
    [HttpPost("")]
    [ProducesResponseType<IEnumerable<Log>>((int)HttpStatusCode.OK)]
    [ProducesResponseType<string>((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<IEnumerable<Log>>> PostAsync([Required][FromBody] SearchFilter filter)
    {
        return Ok(await logRepository.FindAsync(filter));
    }

    /// <summary>Конечная точка для запроса экземпляра лога по идентификатору.</summary>
    /// <param name="id">Идентификатор лога.</param>
    /// <returns>Результат поиска лога.</returns>
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
    
    /// <summary>Конечная точка для запроса экземпляра данных лога по идентификатору лога.</summary>
    /// <param name="id">Идентификатор лога.</param>
    /// <returns>Результат поиска данных лога.</returns>
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
