namespace SyncHms.Identity.Models;

/// <summary>Класс, описывающий модель JSON Web Token.</summary>
internal class Jwt
{
    /// <summary>Байтовое представление секретного ключа шифрования токена.</summary>
    public byte[] Bytes { get; set; }
}
