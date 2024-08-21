namespace SyncHms.Identity.Infrastructure;

/// <summary>
/// Класс, описывающий опции конфигурации валидации токена доступа.<br/>
/// Реализует интерфейс <see cref="IPostConfigureOptions{TOptions}"/>
/// </summary>
/// <param name="cache">Экземпляр сервиса кеширования.</param>
internal class JwtBearerPostConfigureOptions(ICache cache) : IPostConfigureOptions<JwtBearerOptions>
{
    /// <summary>Ключ секретного ключа шифрования в базе данных.</summary>
    private const string Key = "JWT";

    /// <summary>Свойство, возвращающее секретный ключ шифрования токена доступа.</summary>
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

    /// <summary>Метод, конфигурирующий валидацию токена доступа</summary>
    /// <param name="name">Имя экземпляра опций конфигурации валидации токена доступа.</param>
    /// <param name="options">Экземпляр опций коонфигурации валидации токена доступа.</param>
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
