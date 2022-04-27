using Microsoft.Extensions.Options;
using SchoolAppASPv2.IdentityJWT.Helpers;
using static SchoolAppASPv2.IdentityJWT.Services.UserService;

namespace SchoolAppASPv2.IdentityJWT.Authorization
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context, IUserService userService, IJwtUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = jwtUtils.ValidateJwtToken(token);
        }
    }
}
