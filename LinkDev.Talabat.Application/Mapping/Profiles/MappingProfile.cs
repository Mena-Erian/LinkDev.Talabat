using AutoMapper;
using LinkDev.Talabat.Application.Abstraction.Models.Baskets;
using LinkDev.Talabat.Application.Abstraction.Models.Departments;
using LinkDev.Talabat.Application.Abstraction.Models.Employees;
using LinkDev.Talabat.Application.Abstraction.Models.Products;
using LinkDev.Talabat.Application.Mapping.Resolvers;
using LinkDev.Talabat.Domain.Entities.Basket;
using LinkDev.Talabat.Domain.Entities.Departments;
using LinkDev.Talabat.Domain.Entities.Employees;
using LinkDev.Talabat.Domain.Entities.Products;
using LinkDev.Talabat.Domain.Entities.Products.Products;

namespace LinkDev.Talabat.Application.Mapping.Profiles
{
    internal class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.Brand, o => o.MapFrom(src => src.Brand!.Name))
                .ForMember(d => d.Category, o => o.MapFrom(src => src.Category!.Name))
                //.ForMember(d => d.PictureUrl, o => o.MapFrom(src => $"{"url.."}{src.PictureUrl}"))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductPictureUrlResolver>());


            CreateMap<ProductBrand, BrandDto>();
            CreateMap<ProductCategory, CategoryDto>();

            CreateMap<Employee, EmployeeResultDto>()
                .ForMember(d => d.DepartmentName, opt => opt.MapFrom(src => src.Department!.Name))
                .ReverseMap();
            CreateMap<Department, DepartmentResultDto>()
                .ForMember(d => d.EmployeesNames, opt => opt.MapFrom(src => src.Employees != null ? src.Employees.Select(e => e.Name).ToList() : new List<string>()))
                .ReverseMap();

            CreateMap<Basket, BasketDto>().ReverseMap();

        }
    }
}
