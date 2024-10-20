namespace SyncHms.Server.Controllers;

/// <summary>
/// Класс, описывающий контроллер для запроса планируемых событий.<br/>
/// Унаследован от класса <see cref="ControllerBase"/>
/// </summary>
/// <param name="eventScheduler">Экземпляр планировщика событий.</param>
[ApiController]
[Route("api/v1.0/scheduler")]
public class ScheduleController(IEventScheduler eventScheduler) : ControllerBase
{
    /// <summary>Конечная точка для запроса на изменение опций планируемого события.</summary>
    /// <param name="scheduleName">Имя планируемого события.</param>
    /// <param name="schedule">Экземпляр опций планируемого события.</param>
    /// <returns>Экземпляр обновленных опций планируемого события.</returns>
    [HttpPut("{scheduleName}")]
    [ProducesResponseType<ApplicationEnvironment>((int)HttpStatusCode.OK)]
    [ProducesResponseType<string>((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<Schedule>> UpdateAsync([Required][FromRoute] string scheduleName,
        [Required][FromBody] UpdateSchedule schedule)
    {
        try
        {
            var result = await eventScheduler.UpdateScheduleAsync(scheduleName,
                TimeSpan.FromSeconds(schedule.IntervalSeconds), schedule.Last);

            return Ok(new ScheduleInfo
            {
                Name = scheduleName,
                Description = result.Description,
                IntervalSeconds = (int)result.Interval.TotalSeconds,
                Last = result.Last,
                Message = result.Message,
                StackTrace = result.StackTrace
            });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}