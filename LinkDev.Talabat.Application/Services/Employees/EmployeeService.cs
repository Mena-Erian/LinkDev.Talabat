using AutoMapper;
using LinkDev.Talabat.Application.Abstraction.Models.Employees;
using LinkDev.Talabat.Application.Abstraction.Services.Employees;
using LinkDev.Talabat.Application.Specifications;
using LinkDev.Talabat.Domain.Contracts.Persistence;
using LinkDev.Talabat.Domain.Entities.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Application.Services.Employees
{
    public class EmployeeService(IUnitOfWork unitOfWork, IMapper mapper) : IEmployeeService
    {
        public async Task<IEnumerable<EmployeeResultDto>> GetAllEmployeesAsync() => mapper.Map<IEnumerable<EmployeeResultDto>>(await unitOfWork.GetRepository<Employee, string>().GetAllWithSpecsAsync(new EmployeeWithDepartmentSpecifications()));

        public async Task<EmployeeResultDto> GetEmployeeAsync(string id)
         => mapper.Map<EmployeeResultDto>(await unitOfWork.GetRepository<Employee, string>().GetWithSpecsAsync(new EmployeeWithDepartmentSpecifications(id)));
    }
}
