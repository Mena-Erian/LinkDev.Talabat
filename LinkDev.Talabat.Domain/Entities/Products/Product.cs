using LinkDev.Talabat.Domain.Entities.Products.Products;

namespace LinkDev.Talabat.Domain.Entities.Products
{
    public class Product : BaseAuditableEntity<string>
    {
        public required string Name { get; set; }
        public required string NormalizedName { get; set; }
        public string? Description { get; set; }
        public string? PictureUrl { get; set; }
        public decimal Price { get; set; }


        public int? CategoryId { get; set; }
        public virtual ProductCategory? Category { get; set; }
        public int? BrandId { get; set; }
        public virtual ProductBrand? Brand { get; set; }
    }

}
