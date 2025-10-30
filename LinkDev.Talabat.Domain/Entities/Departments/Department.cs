using LinkDev.Talabat.Domain.Entities.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Domain.Entities.Departments
{
    public class Department : BaseAuditableEntity<int>
    {
        public required string Name { get; set; }
        public DateOnly CreationDate { get; set; }

        public virtual IEnumerable<Employee>? Employees { get; set; }
    }


}
