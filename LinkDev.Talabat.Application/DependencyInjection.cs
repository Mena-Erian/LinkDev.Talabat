using AutoMapper;
using LinkDev.Talabat.Application.Abstraction.Services;
using LinkDev.Talabat.Application.Profiles;
using LinkDev.Talabat.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LinkDev.Talabat.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            /// services.AddAutoMapper(mapper => mapper.AddProfile(new MappingProfile()));
            /// services.AddAutoMapper(mapper => mapper.AddProfile<MappingProfile>());
            /// services.AddAutoMapper(typeof(MappingProfile).Assembly);
            services.AddAutoMapper(typeof(MappingProfile));



            //services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<IServiceManager, ServiceManager>();
            return services;
        }
    }
}
