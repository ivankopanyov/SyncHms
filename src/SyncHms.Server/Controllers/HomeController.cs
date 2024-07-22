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
        await ContentAsync(Path.Combine(_path, resource));

    [HttpGet("assets/{resource}")]
    [ProducesResponseType<string>((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<string>> AssetsAsync([FromRoute] string resource) =>
        await ContentAsync(Path.Combine(_path, "assets", resource));

    private async Task<ActionResult<string>> ContentAsync(string filePath)
    {
        if (System.IO.File.Exists(filePath))
        {
            try
            {
                return Content(await System.IO.File.ReadAllTextAsync(filePath), ContentType(filePath));
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
