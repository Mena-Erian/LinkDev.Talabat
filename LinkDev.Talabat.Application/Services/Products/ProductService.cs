using AutoMapper;
using LinkDev.Talabat.Application.Abstraction.Models.Products;
using LinkDev.Talabat.Application.Abstraction.Services.Products;
using LinkDev.Talabat.Domain.Contracts;
using LinkDev.Talabat.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Application.Services.Products
{
    internal class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
    {
        public async Task<IEnumerable<ProductToReturnDto>> GetProductsAsync()
            => mapper.Map<IEnumerable<ProductToReturnDto>>(await unitOfWork.GetRepository<Product, string>().GetAllAsync());

        public Task<ProductToReturnDto> GetProductAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BrandDto>> GetBrandsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
