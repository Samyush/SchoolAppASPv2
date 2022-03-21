using System.Net;

namespace SchoolAppASPv2.IdentityJWT.Services
{
    public class TokenManagerMiddleware : IMiddleware
    {
        public readonly ITokenManager _tokenManager;

        public TokenManagerMiddleware(ITokenManager tokenManager)
        {
            _tokenManager = tokenManager;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (await _tokenManager.IsCurrentActiveToken())
            {
                await next(context);
                return;
            }
            context.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
        }
    }
}
