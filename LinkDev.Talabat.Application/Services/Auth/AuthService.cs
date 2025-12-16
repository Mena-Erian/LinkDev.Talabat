using LinkDev.Talabat.Application.Abstraction.Models.Auth;
using LinkDev.Talabat.Application.Abstraction.Services.Auth;
using LinkDev.Talabat.Application.Exceptions;
using LinkDev.Talabat.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LinkDev.Talabat.Application.Services.Auth
{
    public class AuthService(IOptions<JwtSettings> jwtSettings, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) : IAuthService
    {
        private readonly JwtSettings jwtSettings = jwtSettings.Value;

        public async Task<UserDto> GetCurrentLoginUser(ClaimsPrincipal claimsPrincipal)
        {
            var email = claimsPrincipal.FindFirstValue(ClaimTypes.Email);

            var user = await userManager.FindByEmailAsync(email!);

            return new UserDto()
            {
                Id = user!.Id,
                DisplayName = user.DisplayName,
                Email = user.Email ?? "",
                Token = await GenerateTokenAsync(user) // generate new access token
            };
        }

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
                Token = await GenerateTokenAsync(user)
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
                Token = await GenerateTokenAsync(user)
            };

            return response;
        }


        private async Task<string> GenerateTokenAsync(ApplicationUser user)
        {
            var userClaims = await userManager.GetClaimsAsync(user);
            var roles = await userManager.GetRolesAsync(user);

            var userRolesClaims = roles.Select(r => new Claim(ClaimTypes.Role, r));

            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.PrimarySid, user.Id),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.GivenName, user.DisplayName),

                new Claim("Secret","That is Private Info or key"),
            }
            .Union(userRolesClaims)
            .Union(userClaims);

            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey));

            var signingCredentials = new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                expires: DateTime.UtcNow.AddMinutes(jwtSettings.DurationInMinutes),
                claims: authClaims,
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }
    }
}
