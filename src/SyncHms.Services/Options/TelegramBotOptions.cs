namespace SyncHms.Services.Options;

public class TelegramBotOptions
{
    public const string Section = "Telegram";

    public bool Enabled { get; set; }

    [Required(AllowEmptyStrings = true)]
    public string Token { get; set; } = string.Empty;

    public override int GetHashCode() => HashCode.Combine(Enabled, Token);

    public override bool Equals(object? obj) => 
        obj is TelegramBotOptions other
        && Enabled == other.Enabled
        && Token == other.Token;
}
