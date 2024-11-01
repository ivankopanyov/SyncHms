namespace SyncHms.Server.Controllers;

/// <summary>
/// Класс, описывающий контроллер получения обновлений от сервиса <c>EMIS</c><br/>
/// Унаследован от класса <see cref="ControllerBase"/>
/// </summary>
/// <param name="emisService">Сервис интеграции с удаленным сервисом <c>EMIS</c></param>
[ApiController]
[Route("emis")]
public class EmisController(IEmisService emisService) : ControllerBase
{
    /// <summary>Конечная точка для получения обновлений от сервиса <c>EMIS</c>.</summary>
    /// <param name="profileNumber">Идентификатор обновленного профиля.</param>
    [HttpPost("name_doc_update")]
    public async Task UpdateAsync([FromQuery][Bind(Prefix = "hp_id")] decimal profileNumber)
    {
        await emisService.PublishAsync(profileNumber);
    }
}
