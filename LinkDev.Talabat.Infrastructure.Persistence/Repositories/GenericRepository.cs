using LinkDev.Talabat.Domain.Contracts;
using LinkDev.Talabat.Infrastructure.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistence.Repositories
{
    internal class GenericRepository<TEntity, TKey>(StoreContext storeContext) : IGenericRepository<TEntity, TKey>
        where TEntity : Domain.Common.BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {

        private readonly DbSet<TEntity> _dbSet = storeContext.Set<TEntity>();

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool withTracking = false)
        => withTracking ? await _dbSet.ToListAsync() : await _dbSet.AsNoTracking().ToListAsync();

        public async Task<TEntity?> GetAsync(TKey id)
         => await _dbSet.FindAsync(id);

        public async Task AddAsync(TEntity entity)
            => await _dbSet.AddAsync(entity);

        public void Delete(TEntity entity) => _dbSet.Remove(entity);

        public void Update(TEntity entity) => _dbSet.Update(entity);
    }
}
