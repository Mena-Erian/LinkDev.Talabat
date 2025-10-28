using AutoMapper;
using LinkDev.Talabat.Application.Abstraction.Services;
using LinkDev.Talabat.Application.Abstraction.Services.Products;
using LinkDev.Talabat.Application.Services.Products;
using LinkDev.Talabat.Domain.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Application.Services
{

    internal class ServiceManager(IUnitOfWork unitOfWork, IMapper mapper) : IServiceManager
    {

        private readonly Lazy<IProductService> _productService =
            new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));


        public IProductService ProductService => _productService.Value;
    }
}
