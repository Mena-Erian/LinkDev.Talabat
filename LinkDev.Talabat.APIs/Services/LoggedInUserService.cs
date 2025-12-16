using LinkDev.Talabat.Application.Abstraction.Services.Auth;
using System.Security.Claims;

namespace LinkDev.Talabat.APIs.Services
{
    public class LoggedInUserService(IHttpContextAccessor httpContextAccessor) : ILoggedInUserService
    {
        public string? UserId => httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.PrimarySid);


    }
}
