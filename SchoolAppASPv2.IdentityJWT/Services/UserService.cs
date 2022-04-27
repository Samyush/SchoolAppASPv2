using SchoolAppASPv2.IdentityJWT.Entities;
using SchoolAppASPv2.IdentityJWT.Model.Users;

namespace SchoolAppASPv2.IdentityJWT.Services
{
    public class UserService
    {
        public interface IUserService
        {
            AuthenticateResponse Authenticate(AuthenticateRequest model, string ipAddress);
            AuthenticateResponse RefreshToken(string token, string ipAddress);
            void RevokeToken(string token, string ipAddress);
            IEnumerable<User> GetAll();
            User GetById(int id);

        }
    }
}
