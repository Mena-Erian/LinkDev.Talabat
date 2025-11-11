using LinkDev.Talabat.APIs.Controllers.Controllers.Base;
using LinkDev.Talabat.APIs.Controllers.Errors;
using LinkDev.Talabat.Application.Abstraction.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Buggy
{
    public class BuggyController(IServiceManager serviceManager) : BaseApiController
    {
        [HttpGet("not-fount")]
        public async Task<ActionResult> GetNotFoundRequest()
        {
            return NotFound(new ApiErrorResponse(404)); // 404
        }

        [HttpGet("server-error")]
        public async Task<ActionResult> GetServerErrorRequest()
        {
            throw new Exception(); // 500
        }

        [HttpGet("bad-request")]
        public async Task<ActionResult> GetBadRequest()
        {
            return BadRequest(new ApiErrorResponse(400)); // 400
        }

        [HttpGet("bad-request/{id}")]
        public IActionResult GetValidationErrorRequest(int id)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Where(p => p.Value.Errors.Count > 0)
                                       .Select(p => new ApiValidationErrorResponse.ValidationError()
                                       {
                                           Field = p.Key,
                                           Errors = p.Value.Errors.Select(p => p.ErrorMessage)
                                       });

                return BadRequest(new ApiValidationErrorResponse()
                {
                    Errors = errors
                });
            }


            return Ok(ModelState);
        }

        //[Authorize]
        [HttpGet("unauthorized")]
        public async Task<ActionResult> GetUnauthorizedErrorRequest()
        {
            return Unauthorized(new { Message = "Unauthorized!", StatusCode = 401 });
        }


        [HttpGet("forbidden")]
        public async Task<ActionResult> GetForbiddenErrorRequest()
        {
            return Forbid(); // 403 // if someone want to access recourse and they are not in the role to access this recourse
        }

    }
}
