using AutoMapper;
using LinkDev.Talabat.Application.Abstraction.Models.Products;
using LinkDev.Talabat.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Application.Profiles
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.Brand, o => o.MapFrom(src => src.Brand!.Name))
                .ForMember(d => d.Category, o => o.MapFrom(src => src.Category!.Name))
                ;
        }
    }
}
