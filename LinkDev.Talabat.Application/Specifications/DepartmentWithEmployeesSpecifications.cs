using LinkDev.Talabat.Domain.Entities.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Application.Specifications
{
    internal class DepartmentWithEmployeesSpecifications : BaseSpecifications<Department, int>
    {
        public DepartmentWithEmployeesSpecifications() : base()
        {
            AddIncludes(d => d.Employees!);
        }
        public DepartmentWithEmployeesSpecifications(int id): base(id) 
        {
            AddIncludes(d => d.Employees!);
        }
    }
}
