namespace SyncHms.Identity.Services.Abstract;

internal interface ITokenService
{
    Token Generate(long userId);
}
