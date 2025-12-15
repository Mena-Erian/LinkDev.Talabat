using LinkDev.Talabat.Application.Abstraction.Models.Auth;
using LinkDev.Talabat.Application.Abstraction.Services.Auth;
using LinkDev.Talabat.Application.Exceptions;
using LinkDev.Talabat.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Application.Services.Auth
{
    internal class AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) : IAuthService
    {
        public async Task<UserDto> LoginAsync(LoginDto model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user is null)
            {
                throw new UnAuthorizedException("Invalid login.");
            }

            var result = await signInManager.CheckPasswordSignInAsync(user, model.Password, true);

            if (result.IsNotAllowed) throw new UnAuthorizedException("Account not confirmed yet");

            if (result.IsLockedOut) throw new UnAuthorizedException("Account is locked out.");

            //if(result.RequiresTwoFactor) throw new UnAuthorizedException("Account requires two factor authentication."); // should handled by frontend

            if (!result.Succeeded) throw new UnAuthorizedException("Invalid login."); // should to be here not before IsNotAllowed and IsLockedOut

            var response = new UserDto()
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
                Email = user.Email ?? "",
                Token = "" //TODO: Generate JWT Token
            };

            return response;
        }

        public async Task<UserDto> RegisterAsync(RegisterDto model)
        {
            var user = new ApplicationUser()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                UserName = model.Email,
            };
            var result = await userManager.CreateAsync(user, model.Password);


            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                throw new ValidationException { Errors = errors };
            }

            var response = new UserDto()
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
                Email = user.Email ?? "",
                Token = "" //TODO: Generate JWT Token
            };

            return response;
        }
    }
}
