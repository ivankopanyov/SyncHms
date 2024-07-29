namespace SyncHms.Identity;

public class Token
{
    public string AccessToken { get; set; }
    
    public string RefreshToken { get; set; }
    
    public TimeSpan Expiration { get; set; }
}