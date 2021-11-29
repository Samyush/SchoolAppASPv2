using Microsoft.AspNetCore.Authentication;

namespace SchoolAppASPv2.Identity.Services
{
    public interface ILoginService<T>
    {
        Task<bool> ValidateCredentials(T user, string password);

        Task<T> FindByUserName(string user);

        Task SignIn(T user);

        Task SignInAsync(T user, AuthenticationProperties properties, string? authenticationMethod = null);
    }
}
