using LinkDev.Talabat.Domain.Entities.Products;
using LinkDev.Talabat.Domain.Entities.Products.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Domain.Contracts
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        public IGenericRepository<Product, string> ProductRepository { get;}
        public IGenericRepository<ProductBrand, int> BrandRepository { get; }
        public IGenericRepository<ProductCategory, int> CategoryRepository { get; }





        Task<int> SaveChangesAsync();
    }
}
