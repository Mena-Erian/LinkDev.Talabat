using System.Text.Json;
using System.Text.Json.Serialization;

namespace LinkDev.Talabat.Infrastructure.Persistence.Data.Seeds
{

    internal class ProductSeedDto
    {

        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? PictureUrl { get; set; }
        public decimal Price { get; set; }


        public int? CategoryId { get; set; }
        public virtual ProductCategory? Category { get; set; }
        public int? BrandId { get; set; }
        public virtual ProductBrand? Brand { get; set; }



        public required string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } /*= DateTime.UtcNow;*/
        public required string LastModifiedBy { get; set; }
        public DateTime LastModifiedOn { get; set; } /*= DateTime.UtcNow;*/

    }

    internal class CustomProductJsonConverter : JsonConverter<Product>
    {

        public override Product? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var productDto = JsonSerializer.Deserialize<ProductSeedDto>(ref reader, options);


            if (productDto != null)
            {
                var product = new Product()
                {
                    Id = Guid.NewGuid().ToString(),
                    CreatedBy = productDto.CreatedBy,
                    LastModifiedBy = productDto.LastModifiedBy,
                    Name = productDto.Name,
                    NormalizedName = productDto.Name.ToLower(),
                    Description = productDto.Description,
                    Brand = productDto.Brand,
                    BrandId = productDto.BrandId,
                    Category = productDto.Category,
                    CategoryId = productDto.CategoryId,
                    PictureUrl = productDto.PictureUrl,
                    CreatedOn = productDto.CreatedOn,
                    LastModifiedOn = productDto.LastModifiedOn,
                    Price = productDto.Price
                };

                return product;
            }
            return null;
        }

        public override void Write(Utf8JsonWriter writer, Product value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, options);
        }
    }
}