namespace SyncHms.Server.Controllers;

[ApiController]
[Route("")]
public class HomeController(ILogger<HomeController> logger) : ControllerBase
{
    private static readonly string _path =
        Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? @"/app", @"dist");

    [HttpGet("")]
    [ProducesResponseType<string>((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<string>> IndexAsync() =>
        await ContentAsync(Path.Combine(_path, "index.html"));

    [HttpGet("{resource}")]
    [ProducesResponseType<string>((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<string>> FileAsync([FromRoute] string resource) =>
        await ContentAsync(Path.Combine(_path, resource), Path.Combine(_path, "index.html"));

    [HttpGet("assets/{resource}")]
    [ProducesResponseType<string>((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<string>> AssetsAsync([FromRoute] string resource) =>
        await ContentAsync(Path.Combine(_path, "assets", resource));

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

    private static string ContentType(string fileName) => fileName.Split('.')[^1] switch
    {
        "html" => "text/html",
        "js" => "application/javascript",
        "css" => "text/css",
        "svg" => "image/svg+xml",
        _ => "text/html"
    };
}
