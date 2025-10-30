using AutoMapper;
using LinkDev.Talabat.Application.Abstraction.Models.Departments;
using LinkDev.Talabat.Application.Abstraction.Services.Departments;
using LinkDev.Talabat.Application.Specifications;
using LinkDev.Talabat.Domain.Contracts.Persistence;
using LinkDev.Talabat.Domain.Entities.Departments;

namespace LinkDev.Talabat.Application.Services.Departments
{
    public class DepartmentService(IUnitOfWork unitOfWork, IMapper mapper) : IDepartmentService
    {
        public async Task<IEnumerable<DepartmentResultDto>> GetAllDepartmentsAsync()
            => mapper.Map<IEnumerable<DepartmentResultDto>>(await unitOfWork.GetRepository<Department, int>().GetAllWithSpecsAsync(new DepartmentWithEmployeesSpecifications()));
        public async Task<DepartmentResultDto> GetDepartmentAsync(int id)
            => mapper.Map<DepartmentResultDto>(await unitOfWork.GetRepository<Department, int>().GetWithSpecsAsync(new DepartmentWithEmployeesSpecifications(id)));
    }
}
