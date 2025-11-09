using LinkDev.Talabat.Application.Abstraction.Models.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Application.Abstraction.Services.Employees
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeResultDto>> GetAllEmployeesAsync();
        Task<EmployeeResultDto> GetEmployeeAsync(string id);
    }
}
