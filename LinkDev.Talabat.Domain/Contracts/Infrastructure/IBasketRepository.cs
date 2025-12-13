using LinkDev.Talabat.Domain.Entities.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Domain.Contracts.Infrastructure
{
    public interface IBasketRepository
    {
        Task<Basket?> GetAsync(string id);
        Task<Basket?> CreateOrUpdateAsync(Basket basket, TimeSpan expiration);
        Task<bool> DeleteAsync(string id);
    }
}
