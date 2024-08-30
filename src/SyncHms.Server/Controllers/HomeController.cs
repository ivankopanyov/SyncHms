namespace SyncHms.Server.Controllers;

/// <summary>
/// Класс, описывающий контроллер для запроса файлов клиентского приложения.<br/>
/// Унаследован от класса <see cref="ControllerBase"/>
/// </summary>
/// <param name="logger">Экземпляр логгера.</param>
[ApiController]
[Route("")]
public class HomeController(ILogger<HomeController> logger) : ControllerBase
{
    /// <summary>Путь к директории, хранящей файлы клиентского приложения.</summary>
    private static readonly string Path =
        System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "/app", "dist");

    /// <summary>Конечная точка для запроса файла <c>index.html</c> клиентского приложения.</summary>
    [HttpGet("")]
    [ProducesResponseType<string>((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<string>> IndexAsync() =>
        await ContentAsync(System.IO.Path.Combine(Path, "index.html"));

    /// <summary>Конечная точка для запроса ресурсных файлов клиентского приложения.</summary>
    [HttpGet("{resource}")]
    [ProducesResponseType<string>((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<string>> FileAsync([FromRoute] string resource) =>
        await ContentAsync(System.IO.Path.Combine(Path, resource), System.IO.Path.Combine(Path, "index.html"));

    /// <summary>Конечная точка для запроса ассетов клиентского приложения.</summary>
    [HttpGet("assets/{resource}")]
    [ProducesResponseType<string>((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<string>> AssetsAsync([FromRoute] string resource) =>
        await ContentAsync(System.IO.Path.Combine(Path, "assets", resource));

    /// <summary>Метод, возвращающий файл клиентского приложения.</summary>
    /// <param name="filePath">Путь к файлу.</param>
    /// <param name="defaultFilePath">Путь к файлу по умолчанию.</param>
    /// <returns>Файл клиентского приложения.</returns>
    private async Task<ActionResult<string>> ContentAsync(string filePath, string? defaultFilePath = null)
    {
        var path = System.IO.File.Exists(filePath) ? filePath
            : defaultFilePath != null && System.IO.File.Exists(defaultFilePath) ? defaultFilePath : null;
        
        if (path != null)
        {
            try
            {
                return Content(await System.IO.File.ReadAllTextAsync(path), ContentType(path));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
            }
        }

        return NotFound();
    }

    /// <summary>Метод, определяющий значение заголовка типа контента по расширению файла.</summary>
    /// <param name="fileName">Имя файла.</param>
    /// <returns>Тип контента.</returns>
    private static string ContentType(string fileName) => fileName.Split('.')[^1] switch
    {
        "html" => "text/html",
        "js" => "application/javascript",
        "css" => "text/css",
        "svg" => "image/svg+xml",
        _ => "text/html"
    };
}
