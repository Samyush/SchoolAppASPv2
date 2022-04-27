using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SchoolAppASPv2.IdentityJWT.Data;
using SchoolAppASPv2.IdentityJWT.Entities;
using SchoolAppASPv2.IdentityJWT.Helpers;
using SchoolAppASPv2.IdentityJWT.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SchoolAppASPv2.IdentityJWT.Authorization
{
    public interface IJwtUtils
    {
        public string GenerateJwtToken(ApplicationUser user);
        public int? ValidateJwtToken(string token);
        public RefreshToken GenerateRefreshToken(string ipAddress);
    }
    public class JwtUtils : IJwtUtils
    {
        private ApplicationDbContext _db;
        private readonly AppSettings _appSetting;

        public JwtUtils(ApplicationDbContext db, IOptions<AppSettings> appSetting)
        {
            _db = db;
            _appSetting = appSetting.Value;
        }
        public string GenerateJwtToken(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSetting.Secret);
            var tokenDersctriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString())}),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDersctriptor);
            return tokenHandler.WriteToken(token);
        }

        public RefreshToken GenerateRefreshToken(string ipAddress)
        {
            var refreshToken = new RefreshToken();
            throw new NotImplementedException();
                //here I am right now in JwtUtils
        }

        public int? ValidateJwtToken(string token)
        {
            if (token == null)
                return null;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSetting.Secret);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero,

                }, out  SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

                // return user id from JWT token if validation successful
                return userId;
            }catch
            {
                //return null if validation fails
                return null;
            }
        }
    }
}
