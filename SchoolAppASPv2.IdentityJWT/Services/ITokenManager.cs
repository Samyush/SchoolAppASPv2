namespace SchoolAppASPv2.IdentityJWT.Services
{
    public interface ITokenManager
    {
        Task<bool> IsCurrentActiveToken();
        Task DeactivateCurrentAsync();
        Task<bool> IsActiveAsync(string T);
        Task DeactivateAsync(string T);
    }
}
