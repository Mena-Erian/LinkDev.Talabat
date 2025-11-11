using AutoMapper;
using LinkDev.Talabat.Application.Abstraction.Common;
using LinkDev.Talabat.Application.Abstraction.Models.Products;
using LinkDev.Talabat.Application.Abstraction.Services.Products;
using LinkDev.Talabat.Application.Exceptions;
using LinkDev.Talabat.Application.Specifications;
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
        public async Task<Pagination<ProductToReturnDto>> GetAllProductsAsync(ProductSpecParams specParams)
        {
            var specs = new ProductWithBrandAndCategorySpecifications(specParams.Sort, specParams.IsDescending, specParams.BrandId, specParams.CategoryId, specParams.PageIndex, specParams.PageSize, specParams.Search);

            var result = mapper.Map<IEnumerable<ProductToReturnDto>>(
                await unitOfWork.GetRepository<Product, string>().GetAllWithSpecsAsync(specs));

            int count = await unitOfWork.GetRepository<Product, string>().GetCountAsync(new ProductWithBrandAndCategorySpecifications(specParams.BrandId, specParams.CategoryId, specParams.Search));

            return new Pagination<ProductToReturnDto>(specParams.PageIndex, specParams.PageSize, count)
            { Data = result };
        }
        //public async Task<IEnumerable<ProductToReturnDto>> GetProductsWithSpecsAsync()
        //   => mapper.Map<IEnumerable<ProductToReturnDto>>(await unitOfWork.GetRepository<Product, string>()
        //       .GetAllWithSpecsAsync(specifications: new ProductWithBrandAndCategorySpecifications()));

        public async Task<ProductToReturnDto?> GetProductAsync(string id)
        {
            var product = await unitOfWork.GetRepository<Product, string>().GetWithSpecsAsync(new ProductWithBrandAndCategorySpecifications(id));

            if (product is null)
                throw new NotFoundException(nameof(Product), id);


            return mapper.Map<ProductToReturnDto>(product);
        }
        //public async Task<ProductToReturnDto?> GetProductWithSpecAsync(string id)
        //    => mapper.Map<ProductToReturnDto>(await unitOfWork.GetRepository<Product, string>().GetWithSpecsAsync(new ProductWithBrandAndCategorySpecifications(id)));

        public async Task<IEnumerable<BrandDto>> GetBrandsAsync()
            => mapper.Map<IEnumerable<BrandDto>>(await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync());
        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
            => mapper.Map<IEnumerable<CategoryDto>>(await unitOfWork.GetRepository<ProductCategory, int>().GetAllAsync());

        public async Task<int> DeleteAllProductAsync()
            => await unitOfWork.GetRepository<Product, string>().DeleteAllAsync();
    }
}
