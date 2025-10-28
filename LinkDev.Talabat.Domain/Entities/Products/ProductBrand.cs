namespace LinkDev.Talabat.Domain.Entities.Products.Products
{
    public class ProductBrand : BaseAuditableEntity<int>
    {
        public required string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
