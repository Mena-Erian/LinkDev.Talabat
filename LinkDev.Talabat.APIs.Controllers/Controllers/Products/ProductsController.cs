using LinkDev.Talabat.APIs.Controllers.Controllers.Base;
using LinkDev.Talabat.Application.Abstraction.Common;
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
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProductsAsync([FromQuery] ProductSpecParams specParams)
            => Ok(await serviceManager.ProductService.GetAllProductsAsync(specParams));

        [HttpGet("{id}")]  // GET: /api/Products/{id}] 
        public async Task<ActionResult<ProductToReturnDto>> GetProductByIdAsync(string id)
            //=> Ok(await serviceManager.ProductService.GetProductAsync(id));
            => Ok(await serviceManager.ProductService.GetProductAsync(id));


        [HttpGet("brands")] // GET: /api/Products/brands
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetBrandsAsync()
            => Ok(await serviceManager.ProductService.GetBrandsAsync());

        [HttpGet("categories")] // GET: /api/Products/categories
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategoriesAsync()
            => Ok(await serviceManager.ProductService.GetCategoriesAsync());

        [HttpDelete("DeleteAllProducts")]
        public async Task<ActionResult> DeleteAllAsync()
            => Ok(await serviceManager.ProductService.DeleteAllProductAsync());
    }
}
