using LinkDev.Talabat.Application.Abstraction.Models.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Application.Abstraction.Services.Departments
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentResultDto>> GetAllDepartmentsAsync();
        Task<DepartmentResultDto> GetDepartmentAsync(int id);
    }
}
