using AutoMapper;
using LinkDev.Talabat.Application.Abstraction.Services;
using LinkDev.Talabat.Application.Abstraction.Services.Baskets;
using LinkDev.Talabat.Application.Mapping.Profiles;
using LinkDev.Talabat.Application.Services;
using LinkDev.Talabat.Application.Services.Baskets;
using LinkDev.Talabat.Domain.Contracts.Infrastructure;
using Microsoft.Extensions.Configuration;
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

            services.AddScoped(typeof(Func<IBasketService>), (serviceProvider) =>
            {
                var _mapper = serviceProvider.GetRequiredService<IMapper>();
                var _basketRepository = serviceProvider.GetRequiredService<IBasketRepository>();
                var _config = serviceProvider.GetRequiredService<IConfiguration>();

                return () => new BasketService(_basketRepository, _mapper, _config);
            });
            return services;
        }
    }
}
