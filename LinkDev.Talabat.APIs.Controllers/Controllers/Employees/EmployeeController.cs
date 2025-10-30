using LinkDev.Talabat.APIs.Controllers.Controllers.Base;
using LinkDev.Talabat.Application.Abstraction.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Employees
{
    public class EmployeeController(IServiceManager serviceManager) : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult> GetAllEmployeeAsync()
            => Ok(await serviceManager.EmployeeService.GetAllEmployeesAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult> GetEmployeeAsync(string id)
            => Ok(await serviceManager.EmployeeService.GetEmployeeAsync(id));
    }
}
