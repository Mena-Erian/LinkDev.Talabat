using AutoMapper;
using LinkDev.Talabat.Application.Abstraction.Models.Products;
using LinkDev.Talabat.Application.Abstraction.Services.Products;
using LinkDev.Talabat.Domain.Contracts.Persistence;
using LinkDev.Talabat.Domain.Entities.Products;
using LinkDev.Talabat.Domain.Entities.Products.Products;
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

        public async Task<ProductToReturnDto?> GetProductAsync(string id)
            => mapper.Map<ProductToReturnDto>(await unitOfWork.GetRepository<Product, string>().GetAsync(id));

        public async Task<IEnumerable<BrandDto>> GetBrandsAsync()
            => mapper.Map<IEnumerable<BrandDto>>(await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync());
        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
            => mapper.Map<IEnumerable<CategoryDto>>(await unitOfWork.GetRepository<ProductCategory, int>().GetAllAsync());

        public async Task<int> DeleteAllProductAsync()
            => await unitOfWork.GetRepository<Product, string>().DeleteAllAsync();
    }
}
