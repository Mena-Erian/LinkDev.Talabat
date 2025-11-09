using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Application.Abstraction.Models.Departments
{
    public class DepartmentResultDto
    {
        public int Id { get; set; }

        public required string Name { get; set; }
        public DateOnly CreationDate { get; set; }

        public IEnumerable<string>? EmployeesNames { get; set; }
    }
}
