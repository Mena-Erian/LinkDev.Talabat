using LinkDev.Talabat.Domain.Contracts;
using LinkDev.Talabat.Infrastructure.Persistence.Data;
using LinkDev.Talabat.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistence.UnitOfWork
{
    internal class UnitOfWork(StoreContext storeContext) : IUnitOfWork
    {

        #region Lazy Loading Implementation
        // The Problem here is that every time we send a request to get a repository it will create a new instance of each repository
        // To solve this problem we can use Lazy<T> to create the repository only when it is needed 
        // Another solution is to use private fields to hold the repository instances => maybe better for performance from Lazy<T>

        private Lazy<IGenericRepository<Product, string>> _productRepository = new Lazy<IGenericRepository<Product, string>>(() => new GenericRepository<Product, string>(storeContext));
        private Lazy<IGenericRepository<ProductBrand, int>> _brandRepository = new Lazy<IGenericRepository<ProductBrand, int>>(() => new GenericRepository<ProductBrand, int>(storeContext));
        private Lazy<IGenericRepository<ProductCategory, int>> _categoryRepository = new Lazy<IGenericRepository<ProductCategory, int>>(() => new GenericRepository<ProductCategory, int>(storeContext));

        public IGenericRepository<Product, string> ProductRepository => _productRepository.Value;
        public IGenericRepository<ProductBrand, int> BrandRepository => _brandRepository.Value;
        public IGenericRepository<ProductCategory, int> CategoryRepository => _categoryRepository.Value;
        #endregion


        public async ValueTask DisposeAsync()
            => await storeContext.DisposeAsync();

        public async Task<int> SaveChangesAsync()
            => await storeContext.SaveChangesAsync();
    }
}
