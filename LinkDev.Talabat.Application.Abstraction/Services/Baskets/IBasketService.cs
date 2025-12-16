using LinkDev.Talabat.Application.Abstraction.Models.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Application.Abstraction.Services.Baskets
{
    public interface IBasketService
    {
        Task<BasketDto?> GetCustomerBasketAsync(string id);
        Task<BasketDto?> CreateOrUpdateCustomerBasketAsync(BasketDto basketDto);
        Task<bool> DeleteCustomerBasketAsync(string id);
    }
}
