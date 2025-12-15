using LinkDev.Talabat.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistence.Identity.Config
{
    internal class AddressConfigurations : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.Property(nameof(Address.Id)).ValueGeneratedOnAdd();
            builder.Property(nameof(Address.FirstName)).HasColumnType("nvarchar").HasMaxLength(50);
            builder.Property(nameof(Address.LastName)).HasColumnType("nvarchar").HasMaxLength(50);

            builder.Property(nameof(Address.Street)).HasColumnType("nvarchar").HasMaxLength(100);
            builder.Property(nameof(Address.City)).HasColumnType("nvarchar").HasMaxLength(100);
            builder.Property(nameof(Address.Country)).HasColumnType("nvarchar").HasMaxLength(100);

            builder.ToTable("Address");
        }
    }
}
