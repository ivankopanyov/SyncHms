namespace SyncHms.Server.Dto;

/// <summary>Класс, описывающий модель объекта передачи данных аутентификации пользователя.</summary>
public class Login
{
    /// <summary>Имя пользователя.</summary>
    [Required]
    [RegularExpression(@"^[A-Za-z][A-Za-z0-9-_]{3,49}$")]
    public string Username { get; set; }

    /// <summary>Пароль пользователя.</summary>
    [Required]
    [StringLength(50, MinimumLength = 6)]
    public string Password { get; set; }
}
