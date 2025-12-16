using LinkDev.Talabat.APIs.Controllers.Controllers.Base;
using LinkDev.Talabat.Application.Abstraction.Models.Auth;
using LinkDev.Talabat.Application.Abstraction.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Account
{
    public class AccountController(IServiceManager serviceManager) : BaseApiController
    {
        [HttpPost("login")] // POST: /api/account/login
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
            => Ok(await serviceManager.AuthService.LoginAsync(model));

        [HttpPost("register")] // POST: /api/account/register
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
            => Ok(await serviceManager.AuthService.RegisterAsync(model));

        [Authorize]
        [HttpGet] // GET: /api/account
        public async Task<ActionResult<UserDto>> GetCurrentUser()
            => Ok(await serviceManager.AuthService.GetCurrentLoginUser(User));


        [Authorize]
        [HttpGet("address")] // GET: /api/account/address
        public async Task<ActionResult<AddressDto>> GetUserAddress()
            => Ok(await serviceManager.AuthService.GetUserAddress(User));

        [Authorize]
        [HttpPut("address")] // PUT: /api/account/address
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto addressDto)
            => Ok(await serviceManager.AuthService.UpdateUserAddress(User, addressDto));

    }
}
