using LinkDev.Talabat.APIs.Controllers.Controllers.Base;
using LinkDev.Talabat.Application.Abstraction.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Departments
{
    public class DepartmentController(IServiceManager serviceManager) : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult> GetAllDepartmentsAsync()
            => Ok(await serviceManager.DepartmentService.GetAllDepartmentsAsync());

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetDepartmentAsync(int id)
            => Ok(await serviceManager.DepartmentService.GetDepartmentAsync(id));

    }
}
