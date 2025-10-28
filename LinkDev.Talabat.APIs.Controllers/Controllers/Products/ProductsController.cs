using LinkDev.Talabat.APIs.Controllers.Controllers.Base;
using LinkDev.Talabat.Application.Abstraction.Models.Products;
using LinkDev.Talabat.Application.Abstraction.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Products
{
    public class ProductsController(IServiceManager serviceManager) : BaseApiController
    {
        [HttpGet]  // GET: /api/Products
        public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProductsAsync()
            => Ok(await serviceManager.ProductService.GetProductsAsync());

        //[HttpGet("{id}")]  // GET: /api/Products/{id}] 
        //public async Task<ActionResult<ProductToReturnDto>> GetProductByIdAsync(Guid id)

        [HttpDelete("DeleteAllProducts")]
        public async Task<ActionResult> DeleteAllAsync()
            => Ok(await serviceManager.ProductService.DeleteAllProductAsync());
    }
}
