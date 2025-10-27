using LinkDev.Talabat.Domain.Contracts;
using LinkDev.Talabat.Infrastructure.Persistence.Data;
using Microsoft.Extensions.Configuration;

namespace LinkDev.Talabat.Infrastructure.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("StoreContext"));
            }/*, ServiceLifetime.Scoped,ServiceLifetime.Scoped*/);

            services.AddScoped<IStoreContextInitializer, StoreContextInitializer>();
            //services.AddScoped(typeof(IStoreContextInitializer), typeof(StoreContextInitializer));

            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

            return services;
        }
    }
}
