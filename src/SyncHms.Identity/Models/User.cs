namespace SyncHms.Identity.Models;

internal class User : IdentityUser<long>
{
    public bool CanModified { get; set; } = true;
}
