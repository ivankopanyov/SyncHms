namespace SyncHms.Identity.Models;

/// <summary>
/// Класс, описывающий модель пользоваетля.<br/>
/// Унаследован от класса <see cref="IdentityUser{TKey}"/>
/// </summary>
public class User : IdentityUser<long>
{
    /// <summary>Флаг, указывающий, разрешено ли изменение данных пользователя.</summary>
    public bool CanModified { get; set; } = true;
}
