﻿using AutoMapper;
using LinkDev.Talabat.Application.Abstraction.Services;
using LinkDev.Talabat.Application.Abstraction.Services.Departments;
using LinkDev.Talabat.Application.Abstraction.Services.Employees;
using LinkDev.Talabat.Application.Abstraction.Services.Products;
using LinkDev.Talabat.Application.Services.Departments;
using LinkDev.Talabat.Application.Services.Employees;
using LinkDev.Talabat.Application.Services.Products;
using LinkDev.Talabat.Domain.Contracts.Persistence;

namespace LinkDev.Talabat.Application.Services
{

    internal class ServiceManager(IUnitOfWork unitOfWork, IMapper mapper) : IServiceManager
    {

        private readonly Lazy<IProductService> _productService =
            new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));

        private readonly Lazy<IEmployeeService> _employeeService = new Lazy<IEmployeeService>(() => new EmployeeService(unitOfWork, mapper));

        private readonly Lazy<IDepartmentService> _departmentService = new Lazy<IDepartmentService>(() => new DepartmentService(unitOfWork, mapper));

        public IProductService ProductService => _productService.Value;
        public IEmployeeService EmployeeService => _employeeService.Value;
        public IDepartmentService DepartmentService => _departmentService.Value;
    }
}
