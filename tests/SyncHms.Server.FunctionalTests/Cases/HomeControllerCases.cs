namespace SyncHms.Server.FunctionalTests.Cases;

public class HomeControllerCases : IEnumerable<object[]>
{
    private static readonly string Path =
        System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "/app", "dist");
    
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return ["/", "text/html"];

        foreach (var c in EnumerateCases(new DirectoryInfo(Path)))
            yield return c;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private static IEnumerable<object[]> EnumerateCases(DirectoryInfo directoryInfo)
    {
        foreach (var file in directoryInfo.EnumerateFiles())
            yield return [file.Name, ContentType(file.Extension)];

        foreach (var directory in directoryInfo.EnumerateDirectories())
            foreach (var c in EnumerateCases(directory))
                yield return c;
    }
    
    private static string ContentType(string extension) => extension switch
    {
        "html" => "text/html",
        "js" => "application/javascript",
        "css" => "text/css",
        "svg" => "image/svg+xml",
        _ => "text/html"
    };
}