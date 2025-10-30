using LinkDev.Talabat.Domain.Entities.Departments;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistence.Data.Configs.Employees
{
    internal class DepartmentConfigurations : BaseAuditableEntityConfigurations<Department, int>
    {
        public override void Configure(EntityTypeBuilder<Department> builder)
        {
            base.Configure(builder);

            builder.HasKey(d => d.Id);

            builder.Property(d => d.Name).IsRequired().HasMaxLength(128);

            builder.Property(d => d.CreationDate).IsRequired();
        }


    }
}
