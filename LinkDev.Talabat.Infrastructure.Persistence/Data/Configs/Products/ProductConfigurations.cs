using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistence.Data.Configs.Products
{
    internal class ProductConfigurations
        : BaseAuditableEntityConfigurations<string, Product>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Description).IsRequired();

            builder.Property(p => p.Price)
                .HasColumnType("decimal(9,2)");

            // ProductSeedDto M to 1 Category
            // ProductSeedDto M to 1 Brand

            builder.HasOne(p => p.Category).WithMany(p => p.Products)
                .HasForeignKey(p => p.CategoryId).OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(p => p.Brand).WithMany(p => p.Products)
                 .HasForeignKey(p => p.BrandId).OnDelete(DeleteBehavior.SetNull);

        }
    }
}
