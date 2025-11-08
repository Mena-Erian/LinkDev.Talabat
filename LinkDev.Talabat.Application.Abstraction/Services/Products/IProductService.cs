using LinkDev.Talabat.Application.Abstraction.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Application.Abstraction.Services.Products
{
    public interface IProductService
    {
        Task<IEnumerable<ProductToReturnDto>> GetAllProductsAsync(string? sort, bool IsAscending = true);
        Task<ProductToReturnDto?> GetProductAsync(string id);
        Task<IEnumerable<BrandDto>> GetBrandsAsync();
        Task<IEnumerable<CategoryDto>> GetCategoriesAsync();

        Task<int> DeleteAllProductAsync();
    }
}
