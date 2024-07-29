namespace SyncHms.Identity.Models;

public class User : IdentityUser<long>
{
    public bool CanModified { get; set; } = true;
}
