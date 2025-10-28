using LinkDev.Talabat.Application.Abstraction.Services.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Application.Abstraction.Services
{
    public interface IServiceManager
    {
        public IProductService ProductService { get; }
        /// ICategoryService CategoryService { get; }
        /// IOrderService OrderService { get; }
        /// IAuthenticationService AuthenticationService { get; }
        /// IUserService UserService { get; }
    }
}
