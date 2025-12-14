using LinkDev.Talabat.Application.Abstraction.Services.Baskets;
using LinkDev.Talabat.Application.Abstraction.Services.Departments;
using LinkDev.Talabat.Application.Abstraction.Services.Employees;
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
        public IBasketService BasketService { get; }
        public IProductService ProductService { get; }
        public IEmployeeService EmployeeService { get; }
        public IDepartmentService DepartmentService { get; }
        /// ICategoryService CategoryService { get; }
        /// IOrderService OrderService { get; }
        /// IAuthenticationService AuthenticationService { get; }
        /// IUserService UserService { get; }
    }
}
