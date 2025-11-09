using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Application.Abstraction.Models.Employees
{
    public class EmployeeResultDto
    {
        public required string Id { get; set; }


        public required string Name { get; set; }
        public int? Age { get; set; }
        public decimal Salary { get; set; }


        public int? DepartmentId { get; set; }
        public string? DepartmentName { get; set; }

        public required string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } /*= DateTime.UtcNow;*/
        public required string LastModifiedBy { get; set; }
        public DateTime LastModifiedOn { get; set; } /*= DateTime.UtcNow;*/
    }
}
