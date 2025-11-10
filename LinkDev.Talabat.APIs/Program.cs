
using LinkDev.Talabat.APIs.Controllers;
using LinkDev.Talabat.APIs.Controllers.Errors;
using LinkDev.Talabat.APIs.Extensions;
using LinkDev.Talabat.Application;
using LinkDev.Talabat.Domain.Contracts;
using LinkDev.Talabat.Infrastructure.Persistence;
using LinkDev.Talabat.Infrastructure.Persistence.Data;
using LinkDev.Talabat.Infrastructure.Persistence.Data.Seeds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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

            webApplicationBuilder.Services
                .AddControllers() // Register Required Services by ASP.NET Core Web APIs to DI Container
                .AddApplicationPart(typeof(AssemblyControllersInformation).Assembly) // To make sure that Controllers from other assemblies are registered, but it already see it by default.
                .ConfigureApiBehaviorOptions((setupAction) =>
                {
                    setupAction.SuppressModelStateInvalidFilter = false;
                    // Will not execute any endpoint that has invalid model state
                    setupAction.InvalidModelStateResponseFactory = (actionContext) =>
                    {
                        var errors = actionContext.ModelState.Where(p => p.Value.Errors.Count > 0)
                                       .Select(p => new ApiValidationErrorResponse.ValidationError()
                                       {
                                           Field = p.Key,
                                           Errors = p.Value.Errors.Select(p => p.ErrorMessage)
                                       });

                        return new BadRequestObjectResult(new ApiValidationErrorResponse()
                        {
                            Errors = errors
                        });
                    };

                });


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

            // Routing Middleware should be placed before any middleware that depends on routing decisions
            app.UseRouting();

            app.UseStaticFiles();

            app.UseHttpsRedirection();


            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            #endregion

            app.Run();
        }
    }
}
