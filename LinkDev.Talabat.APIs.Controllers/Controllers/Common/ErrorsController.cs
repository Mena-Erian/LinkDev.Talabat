using LinkDev.Talabat.APIs.Controllers.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Common
{

    [ApiController]
    [Route("Errors/{code}")]
    [ApiExplorerSettings(IgnoreApi = false)]
    public class ErrorsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Errors(int code)
        {
            if (code == (int)HttpStatusCode.NotFound)
            {
                var response = new ApiErrorResponse((int)HttpStatusCode.NotFound, $"The requested endpoint: {Request.Path} is not Found");

                return NotFound(response);
            }

            return StatusCode(code, new ApiErrorResponse(code, $"An error occurred with status code: {code}"));
        }
    }
}
