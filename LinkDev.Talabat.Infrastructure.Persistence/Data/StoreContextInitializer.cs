using LinkDev.Talabat.Domain.Contracts.Persistence.DbInitializers;
using LinkDev.Talabat.Domain.Entities.Departments;
using LinkDev.Talabat.Domain.Entities.Employees;
using LinkDev.Talabat.Infrastructure.Persistence.Common;
using LinkDev.Talabat.Infrastructure.Persistence.Data.Seeds;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistence.Data
{
    internal class StoreContextInitializer(StoreContext dbContext) : BaseDbInitializer(dbContext), IStoreContextInitializer
    {

        public override async Task SeedDataAsync()
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
                var products = JsonSerializer.Deserialize<List<Product>>(productsData, new JsonSerializerOptions()
                {
                    Converters = { new CustomProductJsonConverter() }
                }) ?? [];
                if (products?.Count() > 0)
                {
                    await dbContext.Products.AddRangeAsync(products);
                    await dbContext.SaveChangesAsync();
                }

            }

            if (!dbContext.Departments.Any())
            {
                var departmentData = await File.ReadAllTextAsync($"../LinkDev.Talabat.Infrastructure.Persistence/Data/Seeds/Employees/departments.json");
                var departments = JsonSerializer.Deserialize<List<Department>>(departmentData);

                if (departments?.Count() > 0)
                {
                    await dbContext.Departments.AddRangeAsync(departments);
                    await dbContext.SaveChangesAsync();
                }
            }

            if (!dbContext.Employees.Any())
            {
                var employeeData = await File.ReadAllTextAsync($"../LinkDev.Talabat.Infrastructure.Persistence/Data/Seeds/Employees/employees.json");
                var employees = JsonSerializer.Deserialize<List<Employee>>(employeeData);

                if (employees?.Count() > 0)
                {
                    await dbContext.Employees.AddRangeAsync(employees);
                    await dbContext.SaveChangesAsync();
                }
            }
        }


    }
}
