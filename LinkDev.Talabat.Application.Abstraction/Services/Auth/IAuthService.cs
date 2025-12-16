using LinkDev.Talabat.Application.Abstraction.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Application.Abstraction.Services.Auth
{
    public interface IAuthService
    {
        Task<UserDto> LoginAsync(LoginDto model);

        Task<UserDto> RegisterAsync(RegisterDto model);

        Task<UserDto> GetCurrentLoginUser(ClaimsPrincipal claimsPrincipal);

        Task<AddressDto?> GetUserAddress(ClaimsPrincipal claimsPrincipal);

        Task<AddressDto> UpdateUserAddress(ClaimsPrincipal claimsPrincipal, AddressDto addressDto);

        Task<object> GetUserAddressReferenceLoopingIssue(ClaimsPrincipal claimsPrincipal);


        Task<bool> IsEmailExists(string email);
    }
}
