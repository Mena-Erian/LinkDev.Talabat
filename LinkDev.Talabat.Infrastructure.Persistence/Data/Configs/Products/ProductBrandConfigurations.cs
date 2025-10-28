using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkDev.Talabat.Infrastructure.Persistence.Data.Configs.Products
{
    internal class ProductBrandConfigurations : BaseAuditableEntityConfigurations<ProductBrand, int>
    {
        public override void Configure(EntityTypeBuilder<ProductBrand> builder)
        {
            base.Configure(builder);

            builder.Property(b => b.Name)
                   .IsRequired()
                   .HasMaxLength(100);
        }
    }
}
