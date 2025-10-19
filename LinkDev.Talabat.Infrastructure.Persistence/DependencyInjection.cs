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
            return services;
        }
    }
}
