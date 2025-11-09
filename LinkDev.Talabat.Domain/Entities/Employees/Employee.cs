using LinkDev.Talabat.Domain.Entities.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Domain.Entities.Employees
{
    public class Employee : BaseAuditableEntity<string>
    {

        public required string Name { get; set; }
        public int? Age { get; set; }
        public decimal Salary { get; set; }


        public int? DepartmentId { get; set; }
        public virtual Department? Department { get; set; }
    }
}
