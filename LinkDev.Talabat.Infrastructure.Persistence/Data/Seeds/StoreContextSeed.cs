using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistence.Data.Seeds
{
    public static class StoreContextSeed
    {
        public static async Task SeedAsync(this StoreContext dbContext)
        {

            if (!dbContext.Brands.Any())
            {
                //var currentDirectory = Directory.GetCurrentDirectory();
                var brandsData = await File.ReadAllTextAsync($"../LinkDev.Talabat.Infrastructure.Persistence/Data/Seeds/Products/brands.json");

                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData) ?? [];

                if (brands?.Count() > 0)
                {
                    await dbContext.Brands.AddRangeAsync(brands);
                    await dbContext.SaveChangesAsync();
                }
            }

            if (!dbContext.Categories.Any())
            {
                var categoriesData = await File.ReadAllTextAsync($"../LinkDev.Talabat.Infrastructure.Persistence/Data/Seeds/Products/categories.json");
                var categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoriesData) ?? [];
                if (categories?.Count() > 0)
                {
                    await dbContext.Categories.AddRangeAsync(categories);
                    await dbContext.SaveChangesAsync();
                }
            }

            if (!dbContext.Products.Any())
            {
                var productsData = await File.ReadAllTextAsync($"../LinkDev.Talabat.Infrastructure.Persistence/Data/Seeds/Products/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData,new JsonSerializerOptions()
                {
                    Converters = { new CustomProductJsonConverter() }
                }) ?? [];
                if (products?.Count() > 0)
                {
                    await dbContext.Products.AddRangeAsync(products);
                    await dbContext.SaveChangesAsync();
                }

            }
        }
    }
}
