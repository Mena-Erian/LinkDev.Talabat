using LinkDev.Talabat.Domain.Common;
using LinkDev.Talabat.Domain.Contracts.Persistence;
using LinkDev.Talabat.Infrastructure.Persistence.Data;
using LinkDev.Talabat.Infrastructure.Persistence.Repositories;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistence.UnitOfWork
{
    internal class UnitOfWork(StoreContext storeContext) : IUnitOfWork
    {

        #region Lazy Loading Implementation
        /// // The Problem here is that every time we send a request to get a repository it will create a new instance of each repository
        /// // To solve this problem we can use Lazy<T> to create the repository only when it is needed 
        /// // Another solution is to use private fields to hold the repository instances => maybe better for performance from Lazy<T>
        /// 
        /// private Lazy<IGenericRepository<Product, string>> _productRepository = new Lazy<IGenericRepository<Product, string>>(() => new GenericRepository<Product, string>(storeContext));
        /// private Lazy<IGenericRepository<ProductBrand, int>> _brandRepository = new Lazy<IGenericRepository<ProductBrand, int>>(() => new GenericRepository<ProductBrand, int>(storeContext));
        /// private Lazy<IGenericRepository<ProductCategory, int>> _categoryRepository = new Lazy<IGenericRepository<ProductCategory, int>>(() => new GenericRepository<ProductCategory, int>(storeContext));
        /// 
        /// public IGenericRepository<Product, string> ProductRepository => _productRepository.Value;
        /// public IGenericRepository<ProductBrand, int> BrandRepository => _brandRepository.Value;
        /// public IGenericRepository<ProductCategory, int> CategoryRepository => _categoryRepository.Value;
        #endregion


        private readonly ConcurrentDictionary<string, object> _repositories = new();

        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
            where TEntity : BaseEntity<TKey>
            where TKey : IEquatable<TKey>
        {

            #region Using Normal Dictionary
            // This implementation may cause issues in multi-threaded scenarios with Normal Dictionary
            /// var typeName = typeof(TEntity).Name;
            /// if (_repositories.ContainsKey(typeName))
            ///     return (IGenericRepository<TEntity, TKey>)_repositories[typeName];
            /// 
            /// var repository = new GenericRepository<TEntity, TKey>(storeContext);
            /// 
            /// _repositories.Add(typeName, repository);
            /// 
            /// return repository; 
            #endregion

            #region Using ConcurrentDictionary

            return (IGenericRepository<TEntity, TKey>)_repositories.GetOrAdd(typeof(TEntity).Name, new GenericRepository<TEntity, TKey>(storeContext));
            //(IGenericRepository<TEntity, TKey>)_repositories.GetOrAdd(typeof(TEntity).Name, () => new GenericRepository<TEntity, TKey>(storeContext));
            //(IGenericRepository<TEntity, TKey>)_repositories.GetOrAdd(typeof(TEntity).Name, (key) => new GenericRepository<TEntity, TKey>(storeContext));
            
            #endregion

        }

        public async Task<int> SaveChangesAsync()
            => await storeContext.SaveChangesAsync();
        public async ValueTask DisposeAsync()
               => await storeContext.DisposeAsync();
    }
}
