using LinkDev.Talabat.Domain.Entities.Employees;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistence.Data.Configs.Employees
{
    internal class EmployeeConfigurations : BaseAuditableEntityConfigurations<Employee, string>
    {
        public override void Configure(EntityTypeBuilder<Employee> builder)
        {
            base.Configure(builder);

            builder.HasKey(e => e.Id);

            //builder.Property(e => e.Id).HasDefaultValue(new Guid());

            builder.Property(e => e.Name).IsRequired().HasMaxLength(128);


            // Employee M      1 Department 
            builder.HasOne(e => e.Department).
                    WithMany(d => d.Employees)
                   .HasForeignKey(e => e.DepartmentId)
                   .OnDelete(DeleteBehavior.SetNull)
                   ;
        }
    }
}
