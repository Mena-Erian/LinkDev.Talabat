using LinkDev.Talabat.APIs.Controllers.Controllers.Base;
using LinkDev.Talabat.Application.Abstraction.Models.Baskets;
using LinkDev.Talabat.Application.Abstraction.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Baskets
{
    public class BasketController(IServiceManager serviceManager) : BaseApiController
    {
        [HttpGet] // Get: base/api/v1/Basket?id=bd30cfe9-c1e9-40eb-857b-b2385bab04dc
        public async Task<ActionResult<BasketDto>> GetBasketAsync(string id)
            => Ok(await serviceManager.BasketService.GetCustomerBasketAsync(id));

        [HttpPost] // Post: base/api/v1/Basket
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasketAsync(BasketDto basketDto)
            => Ok(await serviceManager.BasketService.CreateOrUpdateCustomerBasketAsync(basketDto));

        [HttpDelete] // Delete: base/api/v1/Basket?id=bd30cfe9-c1e9-40eb-857b-b2385bab04dc
        public async Task<ActionResult<bool>> DeleteBasketAsync(string id)
            => Ok(await serviceManager.BasketService.DeleteCustomerBasketAsync(id));
    }
}
