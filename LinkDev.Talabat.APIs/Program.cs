
using LinkDev.Talabat.APIs.Extensions;
using LinkDev.Talabat.Application;
using LinkDev.Talabat.Domain.Contracts;
using LinkDev.Talabat.Infrastructure.Persistence;
using LinkDev.Talabat.Infrastructure.Persistence.Data;
using LinkDev.Talabat.Infrastructure.Persistence.Data.Seeds;
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
            webApplicationBuilder.Services.AddApplicationServices();

            #endregion

            var app = webApplicationBuilder.Build();

            #region Database Initialization & Seeding

            await app.InitializeStoreContextAsync();

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
