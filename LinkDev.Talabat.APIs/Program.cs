
using LinkDev.Talabat.Infrastructure.Persistence;
using LinkDev.Talabat.Infrastructure.Persistence.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LinkDev.Talabat.APIs
{
    public class Program
    {
        // Entry Point
        public static async Task Main(string[] args)
        {
            var webApplicationBuilder = WebApplication.CreateBuilder(args);

            #region Configure Service
            // Add services to the container.

            webApplicationBuilder.Services.AddControllers(); // Register Required Services by ASP.NET Core Web APIs to DI Container

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            webApplicationBuilder.Services.AddEndpointsApiExplorer();
            webApplicationBuilder.Services.AddSwaggerGen();

            webApplicationBuilder.Services.AddPersistenceServices(webApplicationBuilder.Configuration);

            #endregion

            var app = webApplicationBuilder.Build();
            
            #region Apply Any Pending Migrations - Update Database
            using var scope = app.Services.CreateAsyncScope();
            var StoreContext = scope.ServiceProvider.GetRequiredService<StoreContext>();
            // Ask Runtime Env For an Object from "Store Context" Service Explicitly.

            var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();
            //var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
            try
            {// TrustedServerCertificate
                if (StoreContext.Database.GetPendingMigrations().Any())
                    await StoreContext.Database.MigrateAsync(); //Update-Database
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();

                logger.LogError(ex.Message, "An Error has been occurred during applying the migrations.");
            } 
            #endregion

            #region Configure Kestrel Middlewates
            // Configure the HTTP request pipeline.

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            //app.UseAuthorization();

            app.MapControllers();
            #endregion

            app.Run();
        }
    }
}
