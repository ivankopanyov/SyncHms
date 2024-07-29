namespace SyncHms.Identity.Infrastructure;

internal class JwtBearerPostConfigureOptions(ICache cache) : IPostConfigureOptions<JwtBearerOptions>
{
    private const string Key = "JWT";

    private SymmetricSecurityKey SecurityKey
    {
        get
        {
            if (cache.GetAsync<Jwt>(Key).Result?.Bytes is { } bytes)
                return new SymmetricSecurityKey(bytes);
            
            bytes = new byte[256];
            Random.Shared.NextBytes(bytes);
            cache.PushAsync(Key, new Jwt
            {
                Bytes = bytes
            });

            return new SymmetricSecurityKey(bytes);
        }
    }

    public void PostConfigure(string? name, JwtBearerOptions options)
    {
        options.TokenValidationParameters.ClockSkew = TimeSpan.Zero;
        options.TokenValidationParameters.RequireAudience = false;
        options.TokenValidationParameters.ValidateAudience = false;
        options.TokenValidationParameters.ValidateIssuer = false;

        if (name == "Refresh")
            options.TokenValidationParameters.ValidateLifetime = false;

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Cookies["Authorization"];
                if (!string.IsNullOrEmpty(accessToken))
                    context.Token = accessToken;

                return Task.CompletedTask;
            }
        };

        options.TokenValidationParameters.IssuerSigningKey = SecurityKey;
    }
}
