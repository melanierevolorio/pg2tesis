using IdentityModel;
using Microsoft.AspNetCore.Identity;
using SuperGasSecurity.Models;
using System.Security.Claims;

namespace SuperGasSecurity.Middleware
{
    public class ClaimsMiddleware
    {
        private readonly RequestDelegate _next;

        public ClaimsMiddleware(RequestDelegate next)
        {
            _next = next;
        }



        public async Task InvokeAsync(HttpContext httpContext, UserManager<ApplicationUser> userManager)
        {
            if (httpContext.User != null && httpContext.User.Identity.IsAuthenticated)
            {
                var sub = httpContext.User.Claims.SingleOrDefault(c => c.Type == JwtClaimTypes.Subject);
                if (sub != null)
                {
                    var user = await userManager.FindByIdAsync(sub.Value);

                    if (user != null)
                    {
                        var claims = new List<Claim> {
                            new Claim(JwtClaimTypes.Subject,user.Id),
                            new Claim(JwtClaimTypes.PreferredUserName,user.UserName),
                        };

                        var role = await userManager.GetRolesAsync(user);
                        role.ToList().ForEach(f =>
                        {
                            claims.Add(new Claim(JwtClaimTypes.Role, f));
                        });

                        var appIdentity = new ClaimsIdentity(claims);
                        httpContext.User.AddIdentity(appIdentity);
                    }
                }

                await _next(httpContext);
            }
            if (!httpContext.Response.HasStarted)
                await _next(httpContext);

        }
    }
}
