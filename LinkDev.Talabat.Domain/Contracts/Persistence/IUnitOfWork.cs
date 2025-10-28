using LinkDev.Talabat.Domain.Entities.Products;
using LinkDev.Talabat.Domain.Entities.Products.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Domain.Contracts.Persistence
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IGenericRepository<TEntity,TKey> GetRepository<TEntity, TKey>() 
            where TEntity : BaseEntity<TKey>
            where TKey : IEquatable<TKey>;


        Task<int> SaveChangesAsync();
    }
}
