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

                var muxer = new ConfigurationOptions
                {
                    EndPoints = { { configuration.GetConnectionString("Redis:Endpoint") ?? "", int.Parse(configuration.GetConnectionString("Redis:Port") ?? "0") } },
                    // If your Redis requires authentication, set the User and Password properties
                    User = configuration.GetConnectionString("Redis:User"),
                    Password = configuration.GetConnectionString("Redis:Password"),
                     
                };

                var cm = ConnectionMultiplexer.Connect(muxer);

                var db = cm.GetDatabase();
                Console.WriteLine(db);

                return cm;
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


/*
 using StackExchange.Redis;

public class ConnectBasicExample
{

    public void run()
    {
        var muxer = ConnectionMultiplexer.Connect(
            new ConfigurationOptions{
                EndPoints= { {"redis-19461.crce176.me-central-1-1.ec2.cloud.redislabs.com", 19461} },
                User="default",
                Password="5Wj2eIPsTxZSHhxhFvH4WBEH37vpyo0N"
            }
        );
        var db = muxer.GetDatabase();
        
        db.StringSet("foo", "bar");
        RedisValue result = db.StringGet("foo");
        Console.WriteLine(result); // >>> bar
        
    }
}

 */