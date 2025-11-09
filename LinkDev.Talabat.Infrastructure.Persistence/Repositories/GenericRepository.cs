using LinkDev.Talabat.Domain.Common;
using LinkDev.Talabat.Domain.Contracts;
using LinkDev.Talabat.Domain.Contracts.Persistence;
using LinkDev.Talabat.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistence.Repositories
{
    internal class GenericRepository<TEntity, TKey>(StoreContext storeContext) : IGenericRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {

        private readonly DbSet<TEntity> _dbSet = storeContext.Set<TEntity>();

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool withTracking = false)
        {
            // Note: This is a temporary workaround to include navigation properties for Product entity.
            // The proper solution would be to implement specification pattern or expression-based includes.
            // we will implement it later.
            if (typeof(TEntity) == typeof(Product))
                return withTracking ?
                   (IEnumerable<TEntity>)await storeContext.Set<Product>().Include<Product, ProductCategory?>(P => P.Category).Include<Product, ProductBrand?>(P => P.Brand).ToListAsync()
                 : (IEnumerable<TEntity>)await storeContext.Set<Product>().Include<Product, ProductCategory?>(P => P.Category).Include<Product, ProductBrand?>(P => P.Brand).AsNoTracking().ToListAsync();

            return withTracking ?
                await _dbSet.ToListAsync() :
                await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<TEntity?> GetAsync(TKey id)
        {
            //if (typeof(TEntity) == typeof(Product))
            //    return await storeContext.Set<Product>().Include<Product, ProductCategory?>(P => P.Category).Include<Product, ProductBrand?>(P => P.Brand).FirstOrDefaultAsync() as TEntity;

            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(TEntity entity)
            => await _dbSet.AddAsync(entity);

        public void Delete(TEntity entity) => _dbSet.Remove(entity);

        public void Update(TEntity entity) => _dbSet.Update(entity);

        public async Task<int> DeleteAllAsync() => await _dbSet.ExecuteDeleteAsync();


        #region Specifications 

        public async Task<IEnumerable<TEntity>> GetAllWithSpecsAsync(ISpecifications<TEntity, TKey> specifications,
            bool withTracking = false)
            => await ApplySpecification(specifications).ToListAsync();

        public async Task<TEntity?> GetWithSpecsAsync(ISpecifications<TEntity, TKey> specifications)
           => await ApplySpecification(specifications).FirstOrDefaultAsync();

        public async Task<int> GetCountAsync(ISpecifications<TEntity, TKey> spec)
            => await ApplySpecification(spec).CountAsync();

        #endregion

        #region Helper

        public IQueryable<TEntity> ApplySpecification(ISpecifications<TEntity, TKey> spec)
            => SpecificationsEvaluator<TEntity, TKey>.GetQuery(_dbSet, spec);

        #endregion

    }
}
