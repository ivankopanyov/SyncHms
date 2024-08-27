namespace SyncHms.Server.FunctionalTests;

public class ServerApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public ServerApiTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        RecreateDatabase();
    }
    
    [Theory]
    [ClassData(typeof(HomeControllerCases))]
    public async Task EndpointsReturnSuccessAndCorrectContentType(string url, string exceptedContentType)
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        Assert.Equal(exceptedContentType, response.Content.Headers.ContentType?.ToString());
    }
    
    [Theory]
    [InlineData(null, null)]
    [InlineData("", "")]
    [InlineData("abc", "123456")]
    [InlineData("root", "12345")]
    [InlineData("root&", "12345")]
    public async Task AuthApiBadRequest(string username, string password)
    {
        var content = new StringContent(JsonConvert.SerializeObject(new Login
        {
            Username = username,
            Password = password
        }));
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        
        var client = _factory.CreateClient();
        
        var response = await client.PostAsync("/api/v1.0/auth/signup", content);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        
        response = await client.PostAsync("/api/v1.0/auth/signin", content);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task RefreshUnauthorized()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/api/v1.0/auth/refresh");
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task SignOutUnauthorized()
    {
        var client = _factory.CreateClient();
        var response = await client.DeleteAsync("/api/v1.0/auth/signout");
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
    
    [Theory]
    [InlineData("admin", "123456")]
    public async Task AuthApiSuccess(string username, string password)
    {
        var client = _factory.CreateClient();
        
        // Проверка отсутствия зарегистрированных пользователей.
        var response = await client.GetAsync("/api/v1.0/auth");
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        
        var content = new StringContent(JsonConvert.SerializeObject(new Login
        {
            Username = username,
            Password = password
        }));
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        
        // Аутентификация недоступна, т.к. не зарегистрировано ни одного пользователя.
        response = await client.PostAsync("/api/v1.0/auth/signin", content);
        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        
        // Регитрация пользователя.
        response = await client.PostAsync("/api/v1.0/auth/signup", content);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.True(response.Headers?.Any(h => h.Key.Equals("Set-Cookie")));
        
        // Проверка наличия зарегистрированных пользователей.
        response = await client.GetAsync("/api/v1.0/auth");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        
        // Регистрация недоступна, т.к. пользователь уже зарегистрирован.
        response = await client.PostAsync("/api/v1.0/auth/signup", content);
        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        
        // Аутентификация пользователя.
        response = await client.PostAsync("/api/v1.0/auth/signin", content);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var setCookieHeader = response.Headers.First(h => h.Key.Equals("Set-Cookie"));
        
        // Проверка ввода неправильного пароля.
        var other = new StringContent(JsonConvert.SerializeObject(new Login
        {
            Username = username,
            Password = $"{password}1",
        }));
        other.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        var otherResponse = await client.PostAsync("/api/v1.0/auth/signin", other);
        Assert.Equal(HttpStatusCode.NotFound, otherResponse.StatusCode);
        
        // Проверка ввода незарегистрированного имени пользователя.
        other = new StringContent(JsonConvert.SerializeObject(new Login
        {
            Username = $"{username}1",
            Password = password
        }));
        other.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        otherResponse = await client.PostAsync("/api/v1.0/auth/signin", other);
        Assert.Equal(HttpStatusCode.NotFound, otherResponse.StatusCode);
        
        // Обновление токена доступа.
        content.Headers.Add(setCookieHeader.Key, setCookieHeader.Value);
        response = await client.GetAsync("/api/v1.0/auth/refresh");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        setCookieHeader = response.Headers.First(h => h.Key.Equals("Set-Cookie"));

        // Выход
        content.Headers.Remove(setCookieHeader.Key);
        content.Headers.Add(setCookieHeader.Key, setCookieHeader.Value);
        response = await client.DeleteAsync("/api/v1.0/auth/signout");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.True(response.Headers?.Any(h => h.Key.Equals("Set-Cookie")));
    }
    
    private void RecreateDatabase()
    {
        using var scope = _factory.Services.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<SqliteIdentityContext>();
        context.Database.EnsureDeleted();
        context.Database.Migrate();
    }
}