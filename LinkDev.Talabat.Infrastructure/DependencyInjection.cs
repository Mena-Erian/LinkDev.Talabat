using LinkDev.Talabat.Domain.Contracts.Infrastructure;
using LinkDev.Talabat.Infrastructure.RedisRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Redis Configuration
            services.AddScoped<IConnectionMultiplexer>(sp =>
            {

                var config = new ConfigurationOptions
                {
                    EndPoints = { { configuration["ConnectionStrings:Redis:Endpoint"]! } },
                    // If your Redis requires authentication, set the User and Password properties
                    User = configuration["ConnectionStrings:Redis:User"],
                    Password = configuration["ConnectionStrings:Redis:Password"]
                };

                return ConnectionMultiplexer.Connect(config);

            });

            services.AddScoped<IBasketRepository, BasketRepository>();

            return services;
        }
    }
}

//public class ConnectBasicExample
//{

//    public void run()
//    {
//        var muxer = ConnectionMultiplexer.Connect(
//            new ConfigurationOptions
//            {
//                EndPoints = { { "re**********s.com", 19461 } },
//                User = "default",
//                Password = ""
//            }
//        );
//        var db = muxer.GetDatabase();

//        db.StringSet("foo", "bar");
//        RedisValue result = db.StringGet("foo");
//        Console.WriteLine(result); // >>> bar

//    }
//}
