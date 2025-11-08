using LinkDev.Talabat.Domain.Entities.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Application.Specifications
{
    internal class EmployeeWithDepartmentSpecifications : BaseSpecifications<Employee, string>
    {
        public EmployeeWithDepartmentSpecifications() : base()
        {
            AddIncludes(e => e.Department!);
        }
        public EmployeeWithDepartmentSpecifications(string id) : base(e => e.Id == id)
        {
            AddIncludes(e => e.Department!);
        }
    }
}
