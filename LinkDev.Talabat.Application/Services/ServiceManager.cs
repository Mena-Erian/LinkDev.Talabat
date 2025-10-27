using LinkDev.Talabat.Application.Abstraction.Services;
using LinkDev.Talabat.Application.Abstraction.Services.Products;
using LinkDev.Talabat.Application.Services.Products;
using LinkDev.Talabat.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Application.Services
{

    internal class ServiceManager : IServiceManager
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly Lazy<IProductService> _productService;

        public ServiceManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            //_productService = new Lazy<IProductService>(() => new ProductService(_unitOfWork));
        }

        IProductService ProductService => _productService.Value;

        IProductService IServiceManager.ProductService => ProductService;

        //IProductService IServiceManager.ProductService => ProductService;
    }
}
